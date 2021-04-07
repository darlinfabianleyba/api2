using Atiendeme.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Data.Entities
{
    public class FinalProject
    {
        public int Id { get; set; }
        public string ImageSRC { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FinalDocumentationSRC { get; set; }
        public string ExamGrade { get; set; }
        public string State { get; set; }
        public int StudentId { get; set; }
    }
}
