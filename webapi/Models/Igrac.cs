using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Models {
    public class Igrac {
        [Key]
        public int IgracID { get; set; }
        public Titula Titula { get; set; }
        [Required]
        [MaxLength(30)]
        public string Ime { get; set; }
        [MaxLength(30)]
        [Required]
        public string Prezime { get; set; }
        public Drzava Drzava { get; set; }
        [Range(100, 3500)]
        public int BlitzRating { get; set; }
        [Range(100, 3500)]
        public int ClassicalRating { get; set; }
        [Range(100, 3500)]
        public int RapidRating { get; set; }
        [JsonIgnore]
        public List<Ucesnik> Ucesnik { get; set; }
        [JsonIgnore]
        public List<Partija> PBeli { get; set; }
        [JsonIgnore]
        public List<Partija> PCrni { get; set; }
    }
}