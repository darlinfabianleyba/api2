using Atiendeme.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Enrollment { get; set; }
        public string HomeState { get; set; }
        public string BelongGroup { get; set; }
        public string Career { get; set; }
        public string State { get; set; }
        public List<ProposedProject> ProposedProjects { get; set; }
        public FinalProject FinalProject { get; set; }
        public List<ChapterProject> ChapterProjects { get; set; }
        public StudentSection StudentSection { get; set; }
        public User User { get; set; }
    }
}
