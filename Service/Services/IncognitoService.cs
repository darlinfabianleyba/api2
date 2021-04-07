using Atiendeme.Data;
using Atiendeme.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using portar_proyectos_api.Data.Entities;
using portar_proyectos_api.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;

namespace portar_proyectos_api.Service.Services
{
    public class IncognitoService : IincognitoService
    {

        private readonly AppSettings _appSettings;
        DataContext _context;
        public IncognitoService(IOptions<AppSettings> appSettings, DataContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public User Authentication(string Mail, string Password)
        {
            if (string.IsNullOrEmpty(Mail) || string.IsNullOrEmpty(Password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Mail == Mail);

            

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
                return null;

            if(user.StudentId != null)
            {
                var student = _context.Students.FirstOrDefault(x => x.Id == user.StudentId);
                if(student.State == "evaluate")
                    throw new AppException("Estás pendiente de aprobación");
                if (student.State == "denied")
                    throw new AppException("Tu credencial de acceso ha sido negada");
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;//.WithoutPassword();
        }

        public async Task<List<FinalProject>> GetAllFinalProject()
        {
            return await _context.FinalProjects.Where(x => x.State == "approved").ToListAsync();
        }

        public List<FinalProject> GetAllFinalProjectByCareer(string Career)
        {
            return (from student in _context.Students.Where(x => x.Career == Career)
                    join finalProjects in _context.FinalProjects.Where(x => x.State == "approved")
                    on student.Id equals finalProjects.StudentId
                    select finalProjects).ToList();
        }

        public async Task<List<ProposedProject>> GetAllProposedProject()
        {
            return await _context.ProposedProjects.Where(x => x.State == "potential").ToListAsync();
        }

        public List<ProposedProject> GetAllProposedProjectByCareer(string Career)
        {
            return (from student in _context.Students.Where(x => x.Career == Career)
                    join proposedProjects in _context.ProposedProjects.Where(x => x.State == "potential")
                    on student.Id equals proposedProjects.StudentId
                    select proposedProjects).ToList();
        }

        public async Task Register(UserDto userDto)
        {
             if (string.IsNullOrWhiteSpace(userDto.Password))
                 throw new AppException("Password is required");

             if (await _context.Users.AnyAsync(x => x.Mail == userDto.Mail))
                 throw new AppException("Email \"" + userDto.Mail + "\" is already taken");

            var teacher = _context.Users.FirstOrDefault(x => x.Mail == userDto.MailITSCTeacher);
            if (teacher == null)
                throw new AppException("El email \"" + userDto.MailITSCTeacher + "\" de el profesor no es valido");

            var section = _context.Sections.FirstOrDefault(c => c.TeacherId == teacher.TeacherId && c.SectionNumber == userDto.MatterCode);
            if (section == null)
                throw new AppException("El codigo \"" + userDto.MatterCode + "\" de secion no es valido");

            byte[] passwordHash, passwordSalt;
             CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

             var users = new User
             {
                 Mail = userDto.Mail,
                 Name = userDto.Name,
                 PasswordHash = passwordHash,
                 PasswordSalt = passwordSalt,
                 Role = Role.Student,
             };

             var student = new Student
             {
                 Enrollment = userDto.Enrollment,
                 BelongGroup = userDto.SubjectCode,
                 Career = userDto.Career,
                 User = users,
                 State = "evaluate"
             };

            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
           
            var studentGetId = _context.Users.FirstOrDefault(x => x.Mail == userDto.Mail).StudentId;
            var studentSection = new StudentSection { SectionId = section.Id, StudentId = (int)studentGetId};
            _context.Add(studentSection);
            _context.SaveChanges();
            
        }

        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Student GetStudentById(int Id)
        {
            return _context.Students.FirstOrDefault(x => x.Id == Id);
        }
    }
}
