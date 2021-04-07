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
    public class TeacherService : ITeacherService
    {
        private readonly AppSettings _appSettings;
        DataContext _context;
        public TeacherService(IOptions<AppSettings> appSettings, DataContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public object GetAllChapterProject(int TeacherId)
        {
            return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId)
                    join studentSection in _context.StudentSections
                    on sections.Id equals studentSection.SectionId
                    join chapterProjects in _context.ChapterProjects
                    on studentSection.StudentId equals chapterProjects.StudentId
                    join student in _context.Students
                    on chapterProjects.StudentId equals student.Id
                    join user in _context.Users
                    on student.Id equals user.StudentId
                    select new
                    {
                        chapterProjectsId = chapterProjects.Id,
                        chapterProjects.StudentId,
                        chapterProjects.Name,
                        chapterProjects.ChapterNumber,
                        chapterProjects.Grade,
                        chapterProjects.ChapterDocumentationSRC,

                        studenName = user.Name,
                        student.Enrollment
                    }).ToList();
        }

        public object GetAllFinalProjectForEvaluate(int TeacherId, string section, string projectState)
        {
            if (projectState == "all" && section != "all")
            {
                return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId && x.SectionNumber == section)
                        join studentSection in _context.StudentSections
                        on sections.Id equals studentSection.SectionId
                        join finalProject in _context.FinalProjects
                        on studentSection.StudentId equals finalProject.StudentId
                        join student in _context.Students
                        on finalProject.StudentId equals student.Id
                        join user in _context.Users
                        on student.Id equals user.StudentId
                        select new
                        {
                            finalProjectId = finalProject.Id,
                            finalProject.StudentId,
                            finalProject.ExamGrade,
                            finalProject.State,
                            finalProject.FinalDocumentationSRC,
                            finalProject.ImageSRC,
                            finalProject.Description,
                            finalProject.Name,

                            studenName = user.Name,
                            student.Enrollment
                        }).ToList();
            }
            if (section == "all" && projectState != "all")
            {
                

                return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId)
                        join studentSection in _context.StudentSections
                        on sections.Id equals studentSection.SectionId
                        join finalProject in _context.FinalProjects.Where(x => x.State == projectState)
                        on studentSection.StudentId equals finalProject.StudentId
                        join student in _context.Students
                        on finalProject.StudentId equals student.Id
                        join user in _context.Users
                        on student.Id equals user.StudentId
                        select new
                        {
                            finalProjectId = finalProject.Id,
                            finalProject.StudentId,
                            finalProject.ExamGrade,
                            finalProject.State,
                            finalProject.FinalDocumentationSRC,
                            finalProject.ImageSRC,
                            finalProject.Description,
                            finalProject.Name,

                            studenName = user.Name,
                            student.Enrollment
                        }).ToList();
            }
            if (section == "all" && projectState == "all")
            {
                return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId)
                        join studentSection in _context.StudentSections
                        on sections.Id equals studentSection.SectionId
                        join finalProject in _context.FinalProjects
                        on studentSection.StudentId equals finalProject.StudentId
                        join student in _context.Students
                        on finalProject.StudentId equals student.Id
                        join user in _context.Users
                        on student.Id equals user.StudentId
                        select new
                        {
                            finalProjectId = finalProject.Id,
                            finalProject.StudentId,
                            finalProject.ExamGrade,
                            finalProject.State,
                            finalProject.FinalDocumentationSRC,
                            finalProject.ImageSRC,
                            finalProject.Description,
                            finalProject.Name,

                            studenName = user.Name,
                            student.Enrollment
                        }).ToList();

            }

            return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId && x.SectionNumber == section)
                    join studentSection in _context.StudentSections
                    on sections.Id equals studentSection.SectionId
                    join finalProject in _context.FinalProjects.Where(x => x.State == projectState)
                    on studentSection.StudentId equals finalProject.StudentId
                    join student in _context.Students
                    on finalProject.StudentId equals student.Id
                    join user in _context.Users
                    on student.Id equals user.StudentId
                    select new
                    {
                        finalProjectId = finalProject.Id,
                        finalProject.StudentId,
                        finalProject.ExamGrade,
                        finalProject.State,
                        finalProject.FinalDocumentationSRC,
                        finalProject.ImageSRC,
                        finalProject.Description,
                        finalProject.Name,

                        studenName = user.Name,
                        student.Enrollment
                    }).ToList();
        }

        public object GetAllProjectForEvaluate(int TeacherId, string section, string projectState)
        {
            if (projectState == "all" && section != "all") { 
                return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId && x.SectionNumber == section)
                            join studentSection in _context.StudentSections 
                            on sections.Id equals studentSection.SectionId
                            join proposedProjects in _context.ProposedProjects
                            on studentSection.StudentId equals proposedProjects.StudentId
                            join student in _context.Students
                            on proposedProjects.StudentId equals student.Id
                            join user in _context.Users
                            on student.Id equals user.StudentId
                        select new {
                                proposedProjectsId = proposedProjects.Id,
                                proposedProjects.StudentId,
                                proposedProjects.Name,
                                proposedProjects.State,
                                proposedProjects.Justification,
                                proposedProjects.Description,

                                student.BelongGroup,
                                studenName = user.Name,
                                student.Enrollment
                            }).ToList();
            }
            if(section == "all" && projectState != "all")
            {
                return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId)
                        join studentSection in _context.StudentSections
                        on sections.Id equals studentSection.SectionId
                        join proposedProjects in _context.ProposedProjects.Where(x => x.State == projectState)
                        on studentSection.StudentId equals proposedProjects.StudentId
                        join student in _context.Students
                        on proposedProjects.StudentId equals student.Id
                        join user in _context.Users
                        on student.Id equals user.StudentId
                        select new
                        {
                            proposedProjectsId = proposedProjects.Id,
                            proposedProjects.StudentId,
                            proposedProjects.Name,
                            proposedProjects.State,
                            proposedProjects.Justification,
                            proposedProjects.Description,

                            student.BelongGroup,
                            studenName = user.Name,
                            student.Enrollment
                        }).ToList();
            }
            if (section == "all" && projectState == "all")
            {
                return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId)
                        join studentSection in _context.StudentSections
                        on sections.Id equals studentSection.SectionId
                        join proposedProjects in _context.ProposedProjects
                        on studentSection.StudentId equals proposedProjects.StudentId
                        join student in _context.Students
                        on proposedProjects.StudentId equals student.Id
                        join user in _context.Users
                        on student.Id equals user.StudentId
                        select new
                        {
                            proposedProjectsId = proposedProjects.Id,
                            proposedProjects.StudentId,
                            proposedProjects.Name,
                            proposedProjects.State,
                            proposedProjects.Justification,
                            proposedProjects.Description,

                            student.BelongGroup,
                            studenName = user.Name,
                            student.Enrollment
                        }).ToList();

            }
            return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId && x.SectionNumber == section)
                    join studentSection in _context.StudentSections
                    on sections.Id equals studentSection.SectionId
                    join proposedProjects in _context.ProposedProjects.Where(x => x.State == projectState)
                    on studentSection.StudentId equals proposedProjects.StudentId
                    join student in _context.Students
                    on proposedProjects.StudentId equals student.Id
                    join user in _context.Users
                    on student.Id equals user.StudentId
                    select new
                    {
                        proposedProjectsId = proposedProjects.Id,
                        proposedProjects.StudentId,
                        proposedProjects.Name,
                        proposedProjects.State,
                        proposedProjects.Justification,
                        proposedProjects.Description,

                        student.BelongGroup,
                        studenName = user.Name,
                        student.Enrollment
                    }).ToList();
        }

        public List<Section> GetAllSection(int TeacherId)
        {
            return _context.Sections.Where(x => x.TeacherId == TeacherId).ToList();
        }

        public object GetAllStudentForCredentials(int TeacherId, string estudentState, string section)
        {
            if (estudentState == "all" && section != "all")
            {
                return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId && x.SectionNumber == section)
                        join studentSection in _context.StudentSections
                        on sections.Id equals studentSection.SectionId
                        join student in _context.Students
                        on studentSection.StudentId equals student.Id
                        join user in _context.Users
                        on student.Id equals user.StudentId
                        select new
                        {
                            studentId = student.Id,
                            student.Enrollment,
                            student.HomeState,
                            student.BelongGroup,
                            student.Career,
                            student.State,
                            user.Name
                        }).ToList();
            }
            if (section == "all" && estudentState != "all")
            {
                return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId)
                        join studentSection in _context.StudentSections
                        on sections.Id equals studentSection.SectionId
                        join student in _context.Students.Where(x => x.State == estudentState)
                        on studentSection.StudentId equals student.Id
                        join user in _context.Users
                        on student.Id equals user.StudentId
                        select new
                        {
                            studentId = student.Id,
                            student.Enrollment,
                            student.HomeState,
                            student.BelongGroup,
                            student.Career,
                            student.State,
                            user.Name
                        }).ToList();
            }
            if (section == "all" && estudentState == "all")
            {
                return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId)
                        join studentSection in _context.StudentSections
                        on sections.Id equals studentSection.SectionId
                        join student in _context.Students
                        on studentSection.StudentId equals student.Id
                        join user in _context.Users
                        on student.Id equals user.StudentId
                        select new
                        {
                            studentId = student.Id,
                            student.Enrollment,
                            student.HomeState,
                            student.BelongGroup,
                            student.Career,
                            student.State,
                            user.Name
                        }).ToList();
            }

            return (from sections in _context.Sections.Where(x => x.TeacherId == TeacherId && x.SectionNumber == section)
                    join studentSection in _context.StudentSections
                    on sections.Id equals studentSection.SectionId
                    join student in _context.Students.Where(x => x.State == estudentState)
                    on studentSection.StudentId equals student.Id
                    join user in _context.Users
                    on student.Id equals user.StudentId
                    select new
                    {
                        studentId = student.Id,
                        student.Enrollment,
                        student.HomeState,
                        student.BelongGroup,
                        student.Career,
                        student.State,
                        user.Name
                    }).ToList();
        }

        public async Task updateChapterProject(ChapterProject chapterProject)
        {
            _context.Update(chapterProject);
            await _context.SaveChangesAsync();
        }

        public async Task updateFinalProject(FinalProject finalProject)
        {
            _context.Update(finalProject);
            await _context.SaveChangesAsync();
        }

        public async Task updateProjectForEvaluate(ProposedProject proposedProject)
        {
            _context.Update(proposedProject);
            await _context.SaveChangesAsync();
        }

        public async Task updateStudentForCredentials(Student student)
        {
            _context.Update(student);
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
