using Atiendeme.Data;
using Atiendeme.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using portar_proyectos_api.Data.Entities;
using portar_proyectos_api.Data.Interfaces;
using portar_proyectos_api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace portar_proyectos_api.Service.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppSettings _appSettings;
        DataContext _context;
        public AdminService(IOptions<AppSettings> appSettings, DataContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            var user = _context.Users.FirstOrDefault(x => x.TeacherId == teacher.Id);
            _context.Remove(user);
            _context.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task EditTeacher(Teacher teacher)
        {
            _context.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public object GetAllTeacher()
        {

            var innerJoin = (from teacher in _context.Teachers // outer sequence
                            join user in _context.Users //inner sequence 
                            on teacher.Id equals user.TeacherId // key selector 
                            select new 
                            {
                                Id = teacher.Id,
                                Mail = user.Mail,
                                Name = user.Name
                            }).ToList();

            return innerJoin;
        }

        public async Task RegisterTeacher(TeacherDto teacher)
        {

            if (string.IsNullOrWhiteSpace(teacher.Password))
                throw new AppException("Password is required");

            if (await _context.Users.AnyAsync(x => x.Mail == teacher.Mail))
                throw new AppException("Email \"" + teacher.Mail + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(teacher.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Mail = teacher.Mail,
                Name = teacher.Name,
                Role = Role.Teacher,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var teacherCreate = new Teacher
            {
                TeacherCode = teacher.TeacherCode,
                User = user
            };
            
            _context.Add(teacherCreate);
            await _context.SaveChangesAsync();

            var teacherGet =_context.Users.FirstOrDefault(x => x.Mail == teacher.Mail);
            var sections = new List<Section>();
            teacher.Sections.ForEach(x => 
            {
                sections.Add(new Section { TeacherId = (int)teacherGet.TeacherId, SectionNumber = x });
            });
            await _context.AddRangeAsync(sections);
            await _context.SaveChangesAsync();
            
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

    }
}
