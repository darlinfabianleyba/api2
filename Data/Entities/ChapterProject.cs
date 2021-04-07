using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Data.Entities
{
    public class ChapterProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChapterNumber { get; set; }
        public string Grade { get; set; }
        public string ChapterDocumentationSRC { get; set; }
        public int StudentId { get; set; }
    }
}
