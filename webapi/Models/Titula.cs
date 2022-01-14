using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Models 
{
    public class Titula {
        [Key]
        public int ID { get; set; }
        [MaxLength(10)]
        [Required]
        public string Title { get; set; }
        [JsonIgnore]
        public List<Igrac> Igraci { get; set; }
    }
}