using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Models {
    public class Turnir {
        [Key]
        public int TurnirID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }
        public int? DrzavaID { get; set; }
        public Drzava Drzava { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DatumOd { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DatumDo { get; set; }
        [RegularExpression(@"[\d]+(\|[\d]+)?(\+[\d]+)?")]
        public string TimeControl { get; set; }
        [Range(0, 100)]
        public int BrojRundi { get; set; }
        public int MaxIgraca { get; set; }
        [JsonIgnore]
        public List<Partija> Partije { get; set; }
        [JsonIgnore]
        public List<Ucesnik> Ucesnici { get; set; }
    }
}