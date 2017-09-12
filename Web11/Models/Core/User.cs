using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public enum Role
    {
        Admin,
        Moderator,
        Regular
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime RegistrationTime { get; set; }

    }
}