namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using OilGas._applyClass;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class CarVehicleGas_LicenseNo
    {
        [Display(Name ="序")]
        [ColumnDef(VisibleEdit = false)]
        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        [ColumnDef(Visible = false,VisibleEdit = false)]
        public string City { get; set; }

        [StringLength(3)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string CityCode { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "年度")]
        [ColumnDef(EditType = EditType.Select, SelectItemsClassNamespace = LienceNoYearSelectItem.AssemblyQualifiedName)]
        public string Year { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string LicenseNo { get; set; }

        [StringLength(50)]
        [Display(Name = "發文字號")]
        public string DispatchNo { get; set; }

        [StringLength(3)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Act { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? CreateTime { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible =false, VisibleEdit = false)]
        public string Creator { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? ModifyTime { get; set; }

        [StringLength(5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Modifier { get; set; }
    }

    public class CarVehicleGas_LicenseNoSelectItems : Dou.Misc.Attr.SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.CarVehicleGas_LicenseNoSelectItems, OilGas";

        protected static IEnumerable<CarVehicleGas_LicenseNo> _carVehicleGas_LicenseNos;
        internal static IEnumerable<CarVehicleGas_LicenseNo> BasicUsers
        {
            get
            {
                if (_carVehicleGas_LicenseNos == null)
                {
                    using (var db = new OilGasModelContextExt())
                    {
                        _carVehicleGas_LicenseNos = db.CarVehicleGas_LicenseNo.ToArray();
                    }
                }
                return _carVehicleGas_LicenseNos;
            }
        }


        public static void Reset()
        {
            _carVehicleGas_LicenseNos = null;
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return BasicUsers.Select(s => new KeyValuePair<string, object>(s.DispatchNo, s.DispatchNo));
        }
    }
}
