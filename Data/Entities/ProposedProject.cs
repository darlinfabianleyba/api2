using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portar_proyectos_api.Data.Entities
{
    public class ProposedProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string State { get; set; }
        public int StudentId { get; set; }
    }
}
