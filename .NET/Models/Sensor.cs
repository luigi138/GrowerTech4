using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GrowerTech_MVC.Models
{
    [Table("SENSORES")]
    public class Sensor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SensorId { get; set; }

        [Column("TIPO")]
        [MaxLength(50)]
        public string Tipo { get; set; }

        [Column("LOCALIZACAO")]
        [MaxLength(100)]
        public string Localizacao { get; set; }
    }
}