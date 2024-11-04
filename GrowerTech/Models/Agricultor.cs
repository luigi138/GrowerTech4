using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowerTech_MVC.Models
{
    [Table("AGRICULTORES")]
    public class Agricultor : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AGRICULTOR_ID")]
        public override int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Column("NOME")]
        [MaxLength(100, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A escala é obrigatória.")]
        [Column("ESCALA")]
        [MaxLength(50, ErrorMessage = "A escala não pode ter mais que 50 caracteres.")]
        [Display(Name = "Escala")]
        public string Escala { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [Column("ENDERECO")]
        [MaxLength(200, ErrorMessage = "O endereço não pode ter mais que 200 caracteres.")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; } = string.Empty;

        [Column("CREATED_AT")]
        public override DateTime CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public override DateTime UpdatedAt { get; set; }

        public Agricultor()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
