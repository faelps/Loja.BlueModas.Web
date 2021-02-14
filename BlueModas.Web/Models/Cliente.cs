using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.Models
{
    public class Cliente : IdentityUser
    {
        [StringLength(100)]
        [Display(Name = "Nome Completo")]
        public string FullName { get; set; }
        [StringLength(100)]
        public string Senha { get; set; }

    }
}
