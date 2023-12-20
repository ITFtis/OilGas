namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using static OilGas.Controllers.basicController;

    public partial class PortGas_BasicData
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public long ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        [ColumnDef(Filter = true)]
        [Display(Name = "�ץ�s��", Order = 1)]
        public string CaseNo { get; set; }




        [StringLength(50)]
        [Display(Name = "�]�ƨϥΥD��", Order = 7)]
        public string ApparatusOwner { get; set; }


        [StringLength(25)]
        [Display(Name = "�]�I�a�I����", Order = 3)]
        [ColumnDef(Filter = true,EditType = EditType.Select, SelectGearingWith = "Location,Parent,true", SelectItems = "{\"��ů�\":\"��ů�\",\"�Ӵ�\":\"�Ӵ�\",\"�u�~�M�δ�\":\"�u�~�M�δ�\"}")]
        public string LocationType { get; set; }


        [StringLength(50)]
        [Display(Name = "�]�I�a�I", Order = 4)]
        [ColumnDef(Filter = true,EditType = EditType.Select,
          SelectItemsClassNamespace = LocationSelectItemsClassImp.AssemblyQualifiedName)]
        public string Location { get; set; }


        [StringLength(200)]
        [ColumnDef(Filter = true)]
        [Display(Name = "�]�I�W��", Order = 5)]
        public string Gas_Name { get; set; }


        [StringLength(200)]
        [Display(Name = "�]�I�Ҧb�a", Order = 6)]
        public string Gas_Location { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? BusinessType1 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? BusinessType2 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? BusinessType3 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? BusinessType4 { get; set; }

        [ColumnDef(Visible = false,EditType =EditType.Date)]
        [StringLength(20)]
        [Display(Name = "�֭�ϥΤ��", Order = 18)]
        public string Report_date { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Date, Filter = true, FilterAssign = FilterAssignType.Between)]
        [StringLength(20)]
        [Display(Name = "������", Order = 2)]
        public string Recipient_date { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Date)]
        [StringLength(20)]
        [Display(Name = "�����ϥΤ��", Order = 19)]
        public string StopReport_date { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        [Display(Name = "�֭�]�m�帹", Order = 20)]//�֭�]�m�帹(�r)
        public string LicenseNo1 { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(50)]
        [Display(Name = "�r ��", Order = 21)]//�֭�]�m�帹(��)
        public string LicenseNo2 { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        [Display(Name = "�ϥΪ��p(���6)", Order = 16)]
        public string LicenseNo3 { get; set; }


        [StringLength(255)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string LicenseNote1 { get; set; }


        [StringLength(255)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string LicenseNote2 { get; set; }




        [StringLength(10)]
        [Display(Name = "�ϥΪ��p", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string UsageState { get; set; }

       
        [StringLength(10)]
        [Display(Name = "�ϥΪ��p(���1)", Order = 12)]
        [ColumnDef(Visible = false,EditType = EditType.Select, SelectGearingWith = "UsageState2,Parent,true",
          SelectItemsClassNamespace = UsageState1ItemsClassImp.AssemblyQualifiedName)]
        public string UsageState1 { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Select, SelectGearingWith = "UsageState3,Parent,true",
                SelectItemsClassNamespace = UsageState2ItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [Display(Name = "�ϥΪ��p(���2)", Order = 13)]
        public string UsageState2 { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Select, SelectGearingWith = "UsageState4,Parent,true",
           SelectItemsClassNamespace = UsageState3ItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [Display(Name = "�ϥΪ��p(���3)", Order = 14)]
        public string UsageState3 { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Select, SelectGearingWith = "UsageState5,Parent,true",
                SelectItemsClassNamespace = UsageState4ItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [Display(Name = "�ϥΪ��p(���4)", Order = 15)]
        public string UsageState4 { get; set; }

        [ColumnDef(Visible = false, EditType = EditType.Select,
            SelectItemsClassNamespace = UsageState5ItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [Display(Name = "�ϥΪ��p(���5)", Order = 16)]
        public string UsageState5 { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string LicenseNote3 { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(25)]
        [Display(Name = "�t�d�H�m�W", Order = 22)]
        public string Boss { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        [Display(Name = "�t�d�H�����Ҧr��", Order = 23)]
        public string Boss_ID { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(20)]
        [Display(Name = "�t�d�H�q��", Order = 24)]
        public string Boss_Tel { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(50)]
        [Display(Name = "�q�l�l��H�c", Order = 27)]
        public string Boss_Email { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(5)]
        [Display(Name = "�t�d�H�l���ϸ�", Order = 25)]
        public string Boss_AreaNo { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(100)]
        [Display(Name = "�t�d�H�a�}", Order = 26)]
        public string Boss_Address { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(100)]
        [Display(Name = "�ӽг��", Order = 8)]
        public string Apply_Name { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(20)]
        [Display(Name = "���q��", Order = 9)]
        public string Apply_Tel { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(5)]
        [Display(Name = "���l���ϸ�", Order = 10)]
        public string Apply_AreaNo { get; set; }

        [ColumnDef(Visible = false, Filter = true)]
        [StringLength(100)]
        [Display(Name = "���a�}", Order = 11)]
        public string Apply_Address { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(500)]
        [Display(Name = "�]�I���p�G�򥻳]�I", Order = 28)]
        public string BasicFacilities { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(500)]
        [Display(Name = "�]�I���p�G��L�]�I", Order = 29)]
        public string OtherFacilities { get; set; }


        [StringLength(500)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string AllFacilities { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(200)]
        [Display(Name = "�]�I���p�G�Ѫo��H", Order = 30)]
        public string SupplyTarget { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string File_name { get; set; }

        [ColumnDef(Visible = false)]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "��ƫإ߮ɶ�", Order = 31)]
        public DateTime? Create_date { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(25)]
        [Display(Name = "��ƫإߪ�", Order = 32)]
        public string Create_name { get; set; }

        [ColumnDef(Visible = false)]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "��ƭק�ɶ�", Order = 33)]
        public DateTime? Mod_date { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(25)]
        [Display(Name = "�̫�ק��", Order = 34)]
        public string Mod_name { get; set; }

        [StringLength(200)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Note1 { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(200)]
        [Display(Name = "�Ƶ�", Order = 35)]
        public string Note2 { get; set; }

        [StringLength(1000)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Note3 { get; set; }

        [StringLength(52)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string MemberID { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string UsageStateSub { get; set; }

        [StringLength(80)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string otherFacility { get; set; }

        [Column(TypeName = "smalldatetime")]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? ChangeReport_date { get; set; }

    }



    public class LocationSelectItemsClassImp : Dou.Misc.Attr.SelectItemsClass
    {

        public const string AssemblyQualifiedName = "OilGas.Models.LocationSelectItemsClassImp,OilGas";


        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {

            List<SelectITEM> Locations = new List<SelectITEM>
            {
    new SelectITEM { Value = "�O�_��ھ���", Name = "�O�_��ھ���", Parent = "��ů�" },
    new SelectITEM { Value = "����ھ���", Name = "����ھ���", Parent = "��ů�" },
    new SelectITEM { Value = "�O������", Name = "�O������", Parent = "��ů�" },
    new SelectITEM { Value = "�Ÿq����", Name = "�Ÿq����", Parent = "��ů�" },
    new SelectITEM { Value = "�O�n����", Name = "�O�n����", Parent = "��ů�" },
    new SelectITEM { Value = "������ھ���", Name = "������ھ���", Parent = "��ů�" },
    new SelectITEM { Value = "��K����", Name = "��K����", Parent = "��ů�" },
    new SelectITEM { Value = "�Ὤ����", Name = "�Ὤ����", Parent = "��ů�" },
    new SelectITEM { Value = "�O�F����", Name = "�O�F����", Parent = "��ů�" },
    new SelectITEM { Value = "��q����", Name = "��q����", Parent = "��ů�" },
    new SelectITEM { Value = "��������", Name = "��������", Parent = "��ů�" },
    new SelectITEM { Value = "�����_��X��", Name = "�����_��X��", Parent = "��ů�" },
    new SelectITEM { Value = "�����n�����", Name = "�����n�����", Parent = "��ů�" },
    new SelectITEM { Value = "��������", Name = "��������", Parent = "��ů�" },
    new SelectITEM { Value = "��������", Name = "��������", Parent = "��ů�" },
    new SelectITEM { Value = "��w����", Name = "��w����", Parent = "��ů�" },
    new SelectITEM { Value = "�C������", Name = "�C������", Parent = "��ů�" },


    new SelectITEM { Value = "�򶩴�", Name = "�򶩴�", Parent = "�Ӵ�" },
    new SelectITEM { Value = "�O�_��", Name = "�O�_��", Parent = "�Ӵ�" },
    new SelectITEM { Value = "�O����", Name = "�O����", Parent = "�Ӵ�" },
    new SelectITEM { Value = "���U��", Name = "���U��", Parent = "�Ӵ�" },
    new SelectITEM { Value = "�w����", Name = "�w����", Parent = "�Ӵ�" },
    new SelectITEM { Value = "���F��", Name = "���F��", Parent = "�Ӵ�" },
    new SelectITEM { Value = "�æw��", Name = "�æw��", Parent = "�Ӵ�" },
    new SelectITEM { Value = "������", Name = "������", Parent = "�Ӵ�" },
    new SelectITEM { Value = "�`�D��", Name = "�`�D��", Parent = "�Ӵ�" },
    new SelectITEM { Value = "Ĭ�D��", Name = "Ĭ�D��", Parent = "�Ӵ�" },
    new SelectITEM { Value = "�Ὤ��", Name = "�Ὤ��", Parent = "�Ӵ�" },
    new SelectITEM { Value = "����", Name = "����", Parent = "�Ӵ�" },
    new SelectITEM { Value = "������", Name = "������", Parent = "�Ӵ�" },


    new SelectITEM { Value = "�[���", Name = "�[���", Parent = "�u�~�M�δ�" },
    new SelectITEM { Value = "���d��", Name = "���d��", Parent = "�u�~�M�δ�" },
    new SelectITEM { Value = "�M����", Name = "�M����", Parent = "�u�~�M�δ�" }
            };


            return Locations.Select(s => new KeyValuePair<string, object>(s.Value, "{\"v\":\"" + s.Name + "\",\"Parent\":\"" + s.Parent + "\"}"));
        }

        public class SelectITEM
        {
            public string Value { get; set; }
            public string Name { get; set; }
            public string Parent { get; set; }
        }


    }


    public class UsageState1ItemsClassImp : Dou.Misc.Attr.SelectItemsClass
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        public const string AssemblyQualifiedName = "OilGas.Models.UsageState1ItemsClassImp,OilGas";


        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
           

            var UsageStatedata= db.PortGas_UsageState.Where(x=>x.UsageState_Type_ParentID == "00").ToArray();


            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }

      public string getselect(string UsageState_Type_ParentType)
        {
            var value = "";
            switch (UsageState_Type_ParentType)
            {

                case "�D�n���A":
                    value = "";
                    break;

                case "�ӽФ�":
                    value = "01";
                    break;

                case "�P�N�]�m":
                    value = "11";
                    break;

                case "�ӽШϥ�":
                    value = "21";
                    break;
                case "�P�N�ϥ�":
                    value = "31";
                    break;

            }

            return value;
      }


    }
    public class UsageState2ItemsClassImp : UsageState1ItemsClassImp
    {      
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState2ItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStatedata = db.PortGas_UsageState.Where(x => x.UsageState_Type_ParentID == "01").ToArray();
            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }
    }

    public class UsageState3ItemsClassImp : UsageState1ItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState3ItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStatedata = db.PortGas_UsageState.Where(x => x.UsageState_Type_ParentID == "02").ToArray();
            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }
    }
    public class UsageState4ItemsClassImp : UsageState1ItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState4ItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStatedata = db.PortGas_UsageState.Where(x => x.UsageState_Type_ParentID == "03").ToArray();
            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }
    }
    public class UsageState5ItemsClassImp : UsageState1ItemsClassImp
    {
        public new const string AssemblyQualifiedName = "OilGas.Models.UsageState5ItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var UsageStatedata = db.PortGas_UsageState.Where(x => x.UsageState_Type_ParentID == "04").ToArray();
            return UsageStatedata.Select(s => new KeyValuePair<string, object>(s.UsageState_TypeID, "{\"v\":\"" + s.UsageState_TypeName + "\",\"Parent\":\"" + getselect(s.UsageState_Type_ParentType) + "\"}"));
        }
    }
}
