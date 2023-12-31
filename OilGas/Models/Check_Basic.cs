namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Collections;
    using System.Linq;



    public partial class Check_Basic : Check_Basic_Base
    {
        [Display(Name = "營業主體", Order = 1)]
        [ColumnDef(Filter = false, VisibleEdit = false, Visible = false, EditType = EditType.Select,
            SelectItemsClassNamespace = CarVehicleGas_BusinessOrganizationSelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(70)]
        public string Business_theme_FullName 
        {
            get 
            {
                var v = CarVehicleGas_BusinessOrganization.GetAllDatas().Where(a => a.Value == this.Business_theme).FirstOrDefault();
                return v == null ? "" : v.Name;
            } 
        }

        [Display(Name = "查核結果", Order = 2)]
        [ColumnDef(VisibleEdit = false)]
        [NotMapped]
        public string URL
        {
            get
            {
                DateTime time;
                switch (CaseType)
                {
                  
                    case "SelfFuel_Basic":

                        if (Tank_Well == "0")
                        {
                            return "Check_UP_" + CaseType + "?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
                        }
                        else
                        {
                            return "Check_DOWN_" + CaseType + "?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
                        }

                        //break;
                    case "FishGas_BasicData":
                        //2005以前要換表，但是資料很少，在SQL那邊處裡就好
                        return "Check_" + CaseType + "?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
                    //break;

                    // case "CarFuel_BasicData":
                    default:
                        //2009以前要換張表
                        time = new DateTime(2009, 9, 1, 00, 00, 00);
                        if (CheckDate < time)
                        {
                            return "Check97_" + CaseType + "?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
                        }
                        else
                        {
                            return "Check_" + CaseType + "?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
                        }
                        //break;
                }
               


            
            }
            set
            {
            }
        }

        [Display(Name = "複查結果", Order = 2)]
        [ColumnDef(Visible = false,VisibleEdit = false)]
        [NotMapped]
        public string URLAction
        {
            get
            {
                if (AllDoesmeet != "0")
                {
                    return "Audit_Guidance_Check_List_Action?CheckNo=" + CheckNo;
                }
                else
                {
                    return null;
                }
            }
            set
            {
            }
        }

        [Display(Name = "輔導結果", Order = 2)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [NotMapped]
        public string URLCounseling
        {
            get
            {
                if (Counseling)
                {
                    return "Audit_Guidance_Check_Counseling_List?CaseNo=" + CaseNo;
                }
                else
                {
                    return null;
                }
            }
            set
            {
            }
        }




        OilGasModelContextExt db = new OilGasModelContextExt();

        [Display(Name = "是否輔導結果", Order = 2)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [NotMapped]
        public bool Counseling
        {
            get
            {

                var Check_Item = db.Check_Counseling.Where(X => X.CaseNo == CaseNo);
                //之後看能不能從CACHE
                if (Check_Item.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
            }
        }




        [Display(Name = "缺失數", Order = 2)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [NotMapped]
        public string AllDoesmeet
        {
            get
            {


                switch (CaseType)
                {

                    case "SelfFuel_Basic":

                        if (Tank_Well == "0")
                        {
                            var Check_Item_SelfUP = db.Check_Item_SelfUP.Where(X => X.CheckNo == CheckNo);
                            //之後看能不能從CACHE
                            if (Check_Item_SelfUP.Count() > 0)
                            {
                                return Check_Item_SelfUP.FirstOrDefault().AllDoesmeet.ToString();
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else
                        {
                            var Check_Item_SelfDown = db.Check_Item_SelfDown.Where(X => X.CheckNo == CheckNo);
                            //之後看能不能從CACHE
                            if (Check_Item_SelfDown.Count() > 0)
                            {
                                return Check_Item_SelfDown.FirstOrDefault().AllDoesmeet.ToString();
                            }
                            else
                            {
                                return "0";
                            }
                        }

                    //break;
                    case "FishGas_BasicData":
                        var Check_Item_Fish = db.Check_Item_Fish.Where(X => X.CheckNo == CheckNo);
                        //之後看能不能從CACHE
                        if (Check_Item_Fish.Count() > 0)
                        {
                            return Check_Item_Fish.FirstOrDefault().AllDoesmeet.ToString();
                        }
                        else
                        {
                            return "0";
                        }
                    //break;

                    // case "CarFuel_BasicData":
                    default:
                        var Check_Item = db.Check_Item.Where(X => X.CheckNo == CheckNo);
                        //之後看能不能從CACHE
                        if (Check_Item.Count() > 0)
                        {
                            return Check_Item.FirstOrDefault().AllDoesmeet.ToString();
                        }
                        else
                        {
                            return "0";
                        }
                        //break;
                }


            }
            set
            {
            }
        }
    }
}
