using Atiendeme.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Data.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string TeacherCode { get; set; }
        public List<Section> Sections { get; set; }
        public User User { get; set; }

    }
}
