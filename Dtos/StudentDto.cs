using portar_proyectos_api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Career { get; set; }
        public List<ProposedProject> ProposedProjects { get; set; }
    }
}
