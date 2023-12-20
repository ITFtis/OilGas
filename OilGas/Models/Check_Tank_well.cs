namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Tank_well
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "¬d®Ö½s¸¹", Order = 1)]
        public string CheckNo { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "¥[ªo¯¸½s¸¹", Order = 1)]
        public string CaseNo { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }






        [StringLength(10)]
        [Display(Name = "Àx¼Ñ½s¸¹", Order = 1)]
        public string TankNo { get; set; }

        [StringLength(10)]
        [ColumnDef(EditType = EditType.Select, SelectItems = "{\"1\":\"92\",\"2\":\"95\",\"3\":\"98\",\"4\":\"®ãªo\",\"5\":\"°sºë¨Tªo\",\"6\":\"¥ÒºØº®²î¥Îªo\",\"7\":\"¤AºØº®²î¥Îªo\"}")]
        [Display(Name = "ªo«~", Order = 1)]
        public string Oil { get; set; }

        [StringLength(10)]
        [ColumnDef(EditType = EditType.Select, SelectItems = "{\"1\":\"¤JªoºÝ\",\"2\":\"¥XªoºÝ\",\"3\":\"¤H¤u¶qE11\",\"4\":\"¥X/¤JªoºÝ\",\"5\":\"¨ä¥L\"}")]
        [Display(Name = "³±¤«¦ì¸m", Order = 1)]
        public string Well_Place { get; set; }

        [StringLength(50)]
        [ColumnDef(EditType = EditType.Select, SelectItems = "{\"1\":\"´úÃz¾¹\",\"2\":\"PID/FID\"}")]
        [Display(Name = "ÀË´ú»ö¾¹", Order = 1)]
        public string Testing_instruments { get; set; }

        [Display(Name = "ÀË´ú­È(%LEL)", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public decimal? Detection { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "ÀË´ú­ÈPID(PPMV)", Order = 1)]
        public decimal? PID { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "ÀË´ú­ÈFID(PPMV)", Order = 1)]
        public decimal? FID { get; set; }




        [Display(Name = "ÀË´ú­È", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string CITY
        {
            get
            {

                if (Testing_instruments == "1")
                {
                    return Detection+"%LEL";
                }
                else
                {
                    return PID+"PID(PPMN)/"+ FID+"FID(PPMV)";
                }

               
            }
            set
            {
            }
        }






        [Column(TypeName = "ntext")]
        [Display(Name = "³Æµù", Order = 1)]
        public string Notes { get; set; }



        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string SlotNo { get; set; }


    }
}
