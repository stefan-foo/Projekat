using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Models {
    public class Ucesnik {
        [Key]
        public int ID { get; set; }
        [DefaultValue(0)]
        public int Mesto { get; set; }
        [DefaultValue(0)]
        public int Bodovi { get; set; }
        public int TurnirID { get; set; }
        public Turnir Turnir { get; set; }
        public int IgracID { get; set; }
        public Igrac Igrac { get; set; }
    }
}