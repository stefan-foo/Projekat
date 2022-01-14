using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models 
{
    public class Drzava 
    {
        [Key]
        public int DrzavaID { get; set; }
        [Required]
        public string Naziv { get; set; }
        public List<Igrac> Predstavnici { get; set; }
        public List<Turnir> TurniriLokacije { get; set; }
    }
}