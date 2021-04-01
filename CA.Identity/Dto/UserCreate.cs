
using CA.Identity.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CA.ERP.WebApp.Dto.User
{
    public class UserCreate
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public List<UserBranch> Branches { get; set; }
    }
}
