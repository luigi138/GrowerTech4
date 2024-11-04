using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowerTech_MVC.Models
{
    [Table("DADOS_CLIMATICOS")]
    public class DadoClimatico : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DADO_CLIMATICO_ID")]
        public override int Id { get; set; }

        [Required(ErrorMessage = "O sensor é obrigatório.")]
        [Column("SENSOR_ID")]
        [Display(Name = "Sensor")]
        public int SensorId { get; set; }

        [ForeignKey("SensorId")]
        [Display(Name = "Sensor")]
        public virtual Sensor Sensor { get; set; } = null!;

        [Required(ErrorMessage = "A data é obrigatória.")]
        [Column("DATA")]
        [Display(Name = "Data da Medição")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "A temperatura é obrigatória.")]
        [Column("TEMPERATURA")]
        [Display(Name = "Temperatura")]
        [Range(-50, 100, ErrorMessage = "A temperatura deve estar entre -50°C e 100°C")]
        public double Temperatura { get; set; }

        [Required(ErrorMessage = "A umidade é obrigatória.")]
        [Column("UMIDADE")]
        [Display(Name = "Umidade")]
        [Range(0, 100, ErrorMessage = "A umidade deve estar entre 0% e 100%")]
        public double Umidade { get; set; }

        [Column("CREATED_AT")]
        public override DateTime CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public override DateTime UpdatedAt { get; set; }

        public DadoClimatico()
        {
            Data = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsTemperaturaAlta()
        {
            return Temperatura > 35;
        }

        public bool IsUmidadeAlta()
        {
            return Umidade > 80;
        }

        public string GetStatusTemperatura()
        {
            if (Temperatura < 0) return "Muito Frio";
            if (Temperatura < 15) return "Frio";
            if (Temperatura < 25) return "Agradável";
            if (Temperatura < 35) return "Quente";
            return "Muito Quente";
        }

        public string GetStatusUmidade()
        {
            if (Umidade < 30) return "Baixa";
            if (Umidade < 60) return "Normal";
            return "Alta";
        }

        [NotMapped]
        public string TemperaturaFormatada => $"{Temperatura:F1}°C";

        [NotMapped]
        public string UmidadeFormatada => $"{Umidade:F1}%";

        [NotMapped]
        public string DataFormatada => Data.ToString("dd/MM/yyyy HH:mm:ss");

        [NotMapped]
        public bool IsDadoRecente => (DateTime.UtcNow - Data).TotalHours <= 24;
    }
}