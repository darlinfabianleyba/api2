using Atiendeme.Data.Entities;
using portar_proyectos_api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace portar_proyectos_api.Data.Interfaces
{
    public interface IincognitoService
    {
        User Authentication(string Mail, string Password);
        Student GetStudentById (int Id);
        Task Register(UserDto userDto);
        Task<List<ProposedProject>> GetAllProposedProject();
        List<ProposedProject> GetAllProposedProjectByCareer(string Career);
        Task<List<FinalProject>> GetAllFinalProject();
        List<FinalProject> GetAllFinalProjectByCareer(string Career);
    }
}
