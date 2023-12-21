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
        [Display(Name = "��~�D��", Order = 1)]
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

        [Display(Name = "�d�ֵ��G", Order = 2)]
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
                        //2005�H�e�n�����A���O��ƫܤ֡A�bSQL����B�̴N�n
                        return "Check_" + CaseType + "?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
                    //break;

                    // case "CarFuel_BasicData":
                    default:
                        //2009�H�e�n���i��
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

        [Display(Name = "�Ƭd���G", Order = 2)]
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

        [Display(Name = "���ɵ��G", Order = 2)]
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

        [Display(Name = "�O�_���ɵ��G", Order = 2)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [NotMapped]
        public bool Counseling
        {
            get
            {

                var Check_Item = db.Check_Counseling.Where(X => X.CaseNo == CaseNo);
                //����ݯण��qCACHE
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




        [Display(Name = "�ʥ���", Order = 2)]
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
                            //����ݯण��qCACHE
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
                            //����ݯण��qCACHE
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
                        //����ݯण��qCACHE
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
                        //����ݯण��qCACHE
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