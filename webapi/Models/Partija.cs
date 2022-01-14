using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    public class Partija
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Igrac")]
        public int? BeliIgracID { get; set; }
        public Igrac Beli { get; set; }
        [ForeignKey("Igrac")]
        public int? CrniIgracID { get; set; }
        public Igrac Crni { get; set; }
        [RegularExpression(@"[01]-[01]")]
        public string Ishod { get; set; }
        [Range(1, 1000)]
        public int BrojPoteza { get; set; }
        [Range(0,100)]
        public int Runda { get; set; }
        [MaxLength(2000)]
        [Required]
        public string Notacija { get; set; }
        [ForeignKey("Turnir")]
        public int? TurnirID { get; set; }
        public Turnir Turnir { get; set; }
    }
}