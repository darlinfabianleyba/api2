using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Data.Entities
{
    public class StudentSection
    {
        public int Id { get; set; }
        public int SectionId {get; set;}
        public int StudentId { get; set; }
    }
}
