using System.ComponentModel.DataAnnotations;

namespace GrowerTech_MVC.Models
{
    public class User
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        public string Address { get; set; }
        public string Scale { get; set; }
    }
}
