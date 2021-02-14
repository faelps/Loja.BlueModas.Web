using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueModas.Web.ViewModel
{
    public class RegistroViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "O nome deve conter pelo menos {0} caracteres")]
        public string Nome { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "digite um email válido")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage ="digite um telefone valido")]
        public string Telefone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "a senha teve conter entre 5 e 50 caracteres", MinimumLength = 5)]
        public string Password { get; set; }
        [Required(ErrorMessage = "o campo deve ser preenchido")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
