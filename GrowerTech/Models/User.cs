using System.ComponentModel.DataAnnotations;

namespace GrowerTech_MVC.Models
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        [StringLength(100, ErrorMessage = "O email não pode ter mais que 100 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "O endereço não pode ter mais que 200 caracteres.")]
        public string Address { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "A escala não pode ter mais que 50 caracteres.")]
        public string Scale { get; set; } = string.Empty;

        public User()
        {
            Email = string.Empty;
            Name = string.Empty;
            Address = string.Empty;
            Scale = string.Empty;
        }
    }
}