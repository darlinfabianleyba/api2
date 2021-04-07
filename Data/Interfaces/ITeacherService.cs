using portar_proyectos_api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Data.Interfaces
{
    public interface ITeacherService
    {
        object GetAllStudentForCredentials(int TeacherId, string estudentState, string section);
        Task updateStudentForCredentials(Student student);
        object GetAllProjectForEvaluate(int TeacherId, string projectState, string section);
        Task updateProjectForEvaluate(ProposedProject proposedProject);
        object GetAllFinalProjectForEvaluate(int TeacherId, string projectState, string section);
        Task updateFinalProject(FinalProject finalProject);
        object GetAllChapterProject(int TeacherId);
        Task updateChapterProject(ChapterProject chapterProject); 
        List<Section> GetAllSection(int TeacherId);
        Task UpdateUserForFinalProject(int Id, string HomeState);
    }
}
