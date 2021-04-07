using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Dtos
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string TeacherCode { get; set; }
        public List<string> Sections { get; set; }
        public string Password { get; set; }
    }
}
