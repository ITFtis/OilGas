namespace OilGas.Models
{
    using OilGas.Models.BASE;
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Item_SelfDown_Action : Check_Item_SelfDown_BASE
    {
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Frequency { get; set; }
    }
}
