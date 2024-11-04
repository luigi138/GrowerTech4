using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowerTech_MVC.Models
{
    [Table("SENSORES")]
    public class Sensor : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SENSOR_ID")] 
        public override int Id { get; set; }

        [Required(ErrorMessage = "O tipo do sensor é obrigatório.")]
        [Column("TIPO")]
        [MaxLength(50, ErrorMessage = "O tipo não pode ter mais que 50 caracteres.")]
        [Display(Name = "Tipo do Sensor")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A localização é obrigatória.")]
        [Column("LOCALIZACAO")]
        [MaxLength(100, ErrorMessage = "A localização não pode ter mais que 100 caracteres.")]
        [Display(Name = "Localização")]
        public string Localizacao { get; set; } = string.Empty;

        [Column("CREATED_AT")]
        public override DateTime CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public override DateTime UpdatedAt { get; set; }

        public virtual ICollection<DadoClimatico> DadosClimaticos { get; set; }


        public Sensor()
        {
            Tipo = string.Empty;
            Localizacao = string.Empty;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            DadosClimaticos = new List<DadoClimatico>();
        }

    
        public bool IsActive()
        {
            return (DateTime.UtcNow - UpdatedAt).TotalHours <= 24;
        }

        public override string ToString()
        {
            return $"{Tipo} - {Localizacao}";
        }
    }
}