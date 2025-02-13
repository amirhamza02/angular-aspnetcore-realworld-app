﻿using System.ComponentModel.DataAnnotations;

namespace Flone.Data.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Roles = new List<UserRole>();
        }
        public int Id { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(250)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

       // [Column(TypeName = "varchar")]
        [MaxLength(50), Required]
        public string LastName { get; set; }

        public List<UserRole> Roles { get; set;}
      
    }
}



  
     