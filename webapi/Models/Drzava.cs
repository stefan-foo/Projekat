using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Models 
{
    public class Drzava 
    {
        [Key]
        public int DrzavaID { get; set; }
        [Required]
        public string Naziv { get; set; }
        [JsonIgnore]
        public List<Igrac> Predstavnici { get; set; }
        [JsonIgnore]
        public List<Turnir> TurniriLokacije { get; set; }
    }
}