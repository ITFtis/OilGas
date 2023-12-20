namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Law_Math
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int LawMath_Index { get; set; }

        [StringLength(20)]
        [Display(Name = "�ɮצW��", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        public string LawMath_LawData_FileName { get; set; }

        [StringLength(100)]
        [Display(Name = "�k�W", Order = 1)]
        [ColumnDef(EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
             SelectGearingWith = "LawMath_LawItemNo_,Parent,true",
          SelectSourceModelNamespace = "OilGas.Models.Law_Item, OilGas",
          SelectSourceModelValueField = "Law_name",
          SelectSourceModelDisplayField = "Law_name")]
        public string LawMath_LawItem { get; set; }



        [StringLength(20)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string LawMath_LawItemNo { get; set; }






        //���F�U�Կ�檺�������
        [Display(Name = "����", Order = 1)]
        [NotMapped]
        [ColumnDef(Visible = true, Sortable = true, EditType = EditType.Select,
             SelectItemsClassNamespace = LawMath_LawItemNoSelectItemsClassImp.AssemblyQualifiedName)]
        public string LawMath_LawItemNo_
        {
            get
            {
                return LawMath_LawItemNo + "," + LawMath_LawItem;

            }
            set
            {
                LawMath_LawItemNo = value.Split(',')[0];
            }
        }
    }
    public class LawMath_LawItemNoSelectItemsClassImp : Dou.Misc.Attr.SelectItemsClass
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        public const string AssemblyQualifiedName = "OilGas.Models.LawMath_LawItemNoSelectItemsClassImp,OilGas";


        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
       
            var Law_Item = db.Law_Item.ToList();

            List<KeyValuePair<string, object>> selectlist = new List<KeyValuePair<string, object>>();


            //�ھ�Law_num�n���h��"��"
            foreach (var LawMath_LawItemNo in Law_Item)
            {

                for (int i = 1; i < LawMath_LawItemNo.Law_num; i++)
                {
                    var selectitem = new KeyValuePair<string, object>(i.ToString() +","+ LawMath_LawItemNo.Law_name, "{\"v\":\"" + "��" + i + "��" + "\",\"Parent\":\"" + LawMath_LawItemNo.Law_name + "\"}");
                    selectlist.Add(selectitem);
                }
            }


            return selectlist;
        }




     
    }

}
