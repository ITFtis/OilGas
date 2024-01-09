using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    [Table("PublicFacility")]
    public class PublicFacility
    {
        [Key]
        public string Place_name { get; set; }
        public string Chinese_phonetic { get; set; }
        public string Common_phonetic { get; set; }
        public string Another_name { get; set; }
        public string County { get; set; }
        public string Town { get; set; }
        public string Village { get; set; }
        public string Place_mean { get; set; }
        public string Year_f { get; set; }
        public string Year_l { get; set; }
        public string Place_type { get; set; }
        public string Language { get; set; }
        public string Denominate { get; set; }
        public string Place_describe { get; set; }
        public string History_describe { get; set; }
        public string Place_content { get; set; }
        public string Map_ref { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
    }
}