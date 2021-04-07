using Atiendeme.Data.Entities;
using Microsoft.EntityFrameworkCore;
using portar_proyectos_api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atiendeme.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<FinalProject> FinalProjects  { get; set; }
        public DbSet<ProposedProject> ProposedProjects { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentSection> StudentSections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ChapterProject> ChapterProjects { get; set; }
        
    }
}
