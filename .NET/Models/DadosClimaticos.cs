using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GrowerTech_MVC.Models
{
    [Table("DADOS_CLIMATICOS")]
    public class DadoClimatico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DadoClimaticoId { get; set; }

        [Column("SENSOR_ID")]
        public int SensorId { get; set; }

        [ForeignKey("SensorId")]
        public Sensor Sensor { get; set; }

        [Column("DATA")]
        public DateTime Data { get; set; }

        [Column("TEMPERATURA")]
        public double Temperatura { get; set; }

        [Column("UMIDADE")]
        public double Umidade { get; set; }
    }
}