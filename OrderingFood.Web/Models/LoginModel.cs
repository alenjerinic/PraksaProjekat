using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingFood.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel:AdministratorModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}