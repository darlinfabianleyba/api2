using portar_proyectos_api.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Atiendeme.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }
        
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
    }
}
