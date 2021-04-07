using portar_proyectos_api.Data.Entities;
using portar_proyectos_api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Data.Interfaces
{
    public interface IAdminService
    {
        object GetAllTeacher();
        Task DeleteTeacher(int id);
        Task EditTeacher(Teacher teacher);
        Task RegisterTeacher(TeacherDto teacher);
    }
}
