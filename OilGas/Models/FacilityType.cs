using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    [Table("FacilityType")]
    public class FacilityType
    {
        [Key]
        [StringLength(5)] 
        public string Value { get; set; }
        [StringLength(5)] 
        public string Name { get; set; }
        
        public int Rank { get; set; }
    }
}