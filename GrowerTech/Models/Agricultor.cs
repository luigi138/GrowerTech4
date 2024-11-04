using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GrowerTech_MVC.Models
{
    [Table("AGRICULTORES")]
    public class Agricultor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgricultorId { get; set; }

        [Column("NOME")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Column("ESCALA")]
        public string Escala { get; set; }

        [Column("ENDERECO")]
        [MaxLength(200)]
        public string Endereco { get; set; }
    }
}
