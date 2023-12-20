namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_FishGas_Closed
    {
        [ColumnDef(Display = "�d�ߴ���-���O", EditType = EditType.Select, SelectItems = "{'1':'�{�p��ƭק�ɶ�','2':'�ܧ���{��Ʈɶ�'}",
            DefaultValue = "1", Required = true, Filter = true, Visible = false)]
        public string CaseType { get; set; }

        [ColumnDef(Display = "�����O", Sortable = true)]
        public string CityName { get; set; }

        [ColumnDef(Display = "�����Ƨ�", Sortable = true)]
        public int CityRank { get; set; }

        [ColumnDef(Display = "�d�߮ɶ�", EditType = EditType.Date, Filter = true, 
            FilterAssign = FilterAssignType.Between, Visible = false)]
        public DateTime? Mod_date { get; set; }

        [Key]
        [ColumnDef(Display = "�ץ�s��", Sortable = true)]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "���o", Sortable = true)]
        public int cpc { get; set; }

        [ColumnDef(Display = "���o-�Ȱ���~", Sortable = true)]
        public int cpc_closed { get; set; }

        [ColumnDef(Display = "�D���o", Sortable = true)]
        public int notcpc { get; set; }

        [ColumnDef(Display = "�D���o-�Ȱ���~", Sortable = true)]
        public int notcpc_closed { get; set; }
    }

    public partial class vw_FishGas_Closed_show
    {
        [ColumnDef(Display = "�d�ߴ���-���O", EditType = EditType.Select, SelectItems = "{'1':'�{�p��ƭק�ɶ�','2':'�ܧ���{��Ʈɶ�'}",
            DefaultValue = "1", Required = true, Filter = true, Visible = false)]
        public string CaseType { get; set; }

        [Display(Name = "����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY { get; set; }
        [ColumnDef(Display = "�d�߮ɶ�", EditType = EditType.Date, Filter = true,
            FilterAssign = FilterAssignType.Between, Visible = false)]
        [NotMapped]
        public DateTime? Mod_date { get; set; }

        [ColumnDef(Display = "�����O", Sortable = true)]
        public string CityName { get; set; }

        [ColumnDef(Display = "���o(�v�}�~�����Ȱ���~)", Sortable = true)]
        public int cpc { get; set; }

        [ColumnDef(Display = "���o�Ȱ���~", Sortable = true)]
        public int cpc_closed { get; set; }

        [ColumnDef(Display = "�D���o(�v�}�~�����Ȱ���~)", Sortable = true)]
        public int notcpc { get; set; }

        [ColumnDef(Display = "�D���o�Ȱ���~", Sortable = true)]
        public int notcpc_closed { get; set; }

        [ColumnDef(Display = "�`�p", Sortable = true)]
        public int tv { get; set; }

    }
}
