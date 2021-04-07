using Atiendeme.Data;
using Microsoft.Extensions.Options;
using portar_proyectos_api.Data.Entities;
using portar_proyectos_api.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace portar_proyectos_api.Service.Services
{
    public class StudentService : IStudentService
    {

        private readonly AppSettings _appSettings;
        DataContext _context;
        public StudentService(IOptions<AppSettings> appSettings, DataContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task CreateChapterProject(ChapterProject chapterProject)
        {
            _context.Add(chapterProject);
            await _context.SaveChangesAsync();
        }

        public async Task CreateProposedProject(ProposedProject proposedProject)
        {
            _context.Add(proposedProject);
            await _context.SaveChangesAsync();
        }

        public List<ChapterProject> GetAllChapterProject(int StudentId)
        {
            return _context.ChapterProjects.Where(x => x.StudentId == StudentId).ToList();
        }

        public object GetAllProposedProject(int UserId, string GroupId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == UserId);
            var section = _context.Students.Where(x => x.Id == user.StudentId).Select(x => x.StudentSection).ToList();
            var resurt = (from student in _context.Students.Where(x => x.BelongGroup == GroupId)
                       join studentSections in _context.StudentSections.Where(x => x.SectionId == section[0].SectionId)
                       on student.Id equals studentSections.StudentId
                       join proposedProjects in _context.ProposedProjects
                       on student.Id equals proposedProjects.StudentId
                       select new { 
                           proposedProjects.Id,
                           proposedProjects.Name,
                           proposedProjects.Description,
                           proposedProjects.Justification,
                           proposedProjects.State,
                           proposedProjects.StudentId,

                           studentName = student.User.Name
                       }).ToList();    
            return resurt;
    }

        public FinalProject GetFinalProyect(int StudentId)
        {
            return _context.FinalProjects.FirstOrDefault(x => x.StudentId == StudentId);
        }

        public async Task<ProposedProject> GetProposedProject(int StudentId)
        {
            return await _context.ProposedProjects.FindAsync(StudentId);
        }

        public async Task CreateFinalProyect(FinalProject finalProyect)
        {
            _context.Add(finalProyect);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProposedProject(ProposedProject proposedProject)
        {
            _context.Update(proposedProject);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserForFinalProject(int Id, string HomeState)
        {
            var user = _context.Students.FirstOrDefault(x => x.Id == Id);
            user.HomeState = HomeState;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
