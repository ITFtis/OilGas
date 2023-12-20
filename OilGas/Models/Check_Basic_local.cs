namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("Check_Basic_local")]
    public partial class Check_Basic_local
    {
        [Key]
        [Display(Name = "�[�o���s��", Order = 1)]
        public string CaseNo { get; set; }

        [Display(Name = "�[�o���W��", Order = 1)]
        [StringLength(70)]
        public string Gas_Name { get; set; }


        [Display(Name = "�ˬd�H��", Order = 1)]
        public string inspectors { get; set; }


        [Display(Name = "���˥[�o�����ˤH��", Order = 1)]
        public string Inspection { get; set; }

        [Display(Name = "���˥[�o�����ˤH��¾��", Order = 1)]
        public string Inspection_title { get; set; }


        [Display(Name = "����", Order = 1)]
        [ColumnDef(Filter = true, Visible = false, VisibleEdit = false, EditType = EditType.Select,
       SelectItemsClassNamespace = UsercitySelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY
        {
            get
            {
                if (CaseNo != null && CaseNo.Length > 6)
                {
                    return CaseNo.Substring(4, 2);
                }
                else
                {
                    return CaseNo;
                }
            }
            set
            {
            }
        }


        [Display(Name = "�ˬd���", Order = 1)]
        public DateTime? CheckDate { get; set; }

        [Display(Name = "�U��EXCEL", Order = 1)]
        [ColumnDef( Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string EXCEL
        {
            get
            {
                return CaseNo;
            }
            set
            {
            }
        }
       


        [StringLength(100)]
        [Display(Name = "�H�U�лx", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"��~�D��\",\"1\":\"�[�o�����W\",\"2\":\"��~�ɶ�\",\"3\":\"�Ѫo�t�Ӽлx�ΦW��\"}")]
        public string A01_Options { get; set; }

        [StringLength(1)]
        [Display(Name = "�O�_�����B�ҼХ�", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string A01 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string A01_Notes { get; set; }




        [StringLength(100)]
        [Display(Name = "�U�C���ҬO�_�]�mĵ�ټлx�μнu�G(�ФĿ��ˬd���G���ŦX����)", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"�x�o�Ѱϳ]�m�u�Y�T�Ϥ��v�лx�]�����զr�ХܪO�s�@�^�C\",\"1\":\"�[�o�ϳ]�m�u�����[�o�v�B�u�Y�T�Ϥ��v�лx�]�����զr�ХܪO�s�@�^�C\",\"2\":\"�[�o���X�J�f�]��V�лx�A�é�a�����զ�b�Y�нu�C\",\"3\":\"���o�ϩ�a���H���u�ХܡC\",\"4\":\"�B�׳]�m���׼лx�C\"}")]
        public string A02_Options { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string A02_Notes { get; set; }

        [StringLength(100)]
        [Display(Name = "�O�_�ХܲŦX�U�C�W�w����o�����Ϊo�~����G(�ФĿ��ˬd���G���ŦX����)", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"�Хܻ��欰���U�o�~���s�����C\",\"1\":\"�Хܦ�m���Z�{���a�ɤJ�f�B5���ؽd�򤺡C\",\"2\":\"�Хܤ覡���T�w���A�Хܪ��U�t���a��1���إH�W�A�B���o���B�����B���A�û��H�]���ө��C\",\"3\":\"�Хܻ��椧�Ʀr�W��A�C�r��17�����H�W�A�e10�����H�W�C\"}")]
        public string A03_Options { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string A03_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o���g��\�i���ӵn�O�ƶ��O�_�P��֭�ƶ��۲šC", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string B01 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B01_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o����a�X�B�J�f�B�ۧU�[�o�Ϥέ��n�������t�m�O�_�P��֭�۲šC", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string B02 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B02_Notes { get; set; }








        [StringLength(1)]
        [Display(Name = "��֭�[�o���j��1", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_gun1 { get; set; }


        [Display(Name = "��֭�[�o���j��1�x��", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B03_gun1_value { get; set; }

        [StringLength(1)]
        [Display(Name = "��֭�[�o���j��2", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_gun2 { get; set; }


        [Display(Name = "��֭�[�o���j��2�x��", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B03_gun2_value { get; set; }

        [StringLength(1)]
        [Display(Name = "��֭�[�o���j��3", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_gun3 { get; set; }


        [Display(Name = "��֭�[�o���j��3�x��", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B03_gun3_value { get; set; }

        [StringLength(1)]
        [Display(Name = "��֭�[�o���j��4", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_gun4 { get; set; }


        [Display(Name = "��֭�[�o���j��4�x��", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B03_gun4_value { get; set; }


        [StringLength(1)]
        [Display(Name = "�[�o���O�_�P��֭�۲šG", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string B03 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_Notes { get; set; }












        [StringLength(1)]
        [Display(Name = "��֭�a�U�x�o�Ѥ���1", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B04_tank1 { get; set; }


        [Display(Name = "��֭�a�U�x�o�Ѥ���1�y��", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B04_tank1_value { get; set; }

        [StringLength(1)]
        [Display(Name = "��֭�a�U�x�o�Ѥ���2", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B04_tank2 { get; set; }


        [Display(Name = "��֭�a�U�x�o�Ѥ���2�y��", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B04_tank2_value { get; set; }

        [StringLength(1)]
        [Display(Name = "��֭�a�U�x�o�Ѥ���3", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B04_tank3 { get; set; }

        [Display(Name = "��֭�a�U�x�o�Ѥ���3�y��", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B04_tank3_value { get; set; }


        [StringLength(1)]
        [Display(Name = "�a�U�x�o�ѬO�_�P��֭�۲šG", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string B04 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B04_Notes { get; set; }






        [StringLength(100)]
        [Display(Name = "�H�U�]�m���ݳ]�I�O�_�g�ӽЮ֭�C", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"�T����²���O�i�]�I\",\"1\":\"�~���]�I\",\"2\":\"²���Ʀ��˴��A�ȳ]�I\",\"3\":\"�P��T�����Ϋ~�]�I\",\"4\":\"�۰ʳc���\",\"5\":\"�h�C��ưȾ�\",\"6\":\"�����Ʒ~���c�e�U���O�A�ȳ]�I\"}")]
        public string B05_Options { get; set; }

        [StringLength(1)]
        [Display(Name = "�ˬd���G", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string B05 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B05_Notes { get; set; }





        [StringLength(100)]
        [Display(Name = "�H�U�]�m���綵�جO�_�g�ӽЮ֭�C", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"�K�Q�ө�\",\"1\":\"�c��A���~\",\"2\":\"������\",\"3\":\"���βG�ƥ۪o��\",\"4\":\"�N��T���w������\",\"5\":\"�T�����P�ۦ樮�R��ί���\",\"6\":\"�g�P���q�m��\",\"7\":\"�s�i�A��\",\"8\":\"���ѳ��Ҩѳ]�m���ľ��c��~���ҥ~�۰ʤƪA�ȳ]��\",\"9\":\"�����L�H�e�U�N�����~�A��\",\"10\":\"�q�ƶȨѦ����~�������Ҥ��~��~��\",\"11\":\"������~���Ϋγ��]�m��ʹq�ܰ�a�x\",\"12\":\"�γ��ѥL�H�]�m�˸m�e�q���Τ��ʢ`�çQ�ΤӶ��ध�ۥεo�q�]��\",\"13\":\"��L�g�����D�޾����֭㤧���綵��\"}")]
        public string B06_Options { get; set; }

        [StringLength(1)]
        [Display(Name = "�ˬd���G", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string B06 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B06_Notes { get; set; }


        [StringLength(1)]
        [Display(Name = "�O�_�[�J�[�o���ӷ~�P�~���|�C", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string C01 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string C01_Notes { get; set; }


        [StringLength(1)]
        [Display(Name = "�O�_��O���@�N�~�d���O�I�ηN�~�ìV�d���I�C", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string D01 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string D01_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "�O�I���O�I���B�O�_�F�̤p�O�I���B�C", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string D02 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string D02_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "�O�_�̥[�o����B�]�Ʀۦ�w���ˬd��ۦ��I�[�o���]�I�C��B�C��ΨC�b�~�w���ˬd�A�ûs�@�ˬd�����C", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string E01 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string E01_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "�e���w���ˬd�����O�_�O�s1�~�H�W�C", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string E02 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string E02_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "�e���w���ˬd���ظg�ˬd�O�_���ʥ�", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"�_\",\"1\":\"�O\"}")]
        public string E03 { get; set; }


        [Display(Name = "�ﵽ����C", Order = 1)]
        [ColumnDef(Visible = false)]
        public DateTime? E_date { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o���ʪo���ҩ����O�_�O�s1�~�H�W�C", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"���ŦX\",\"1\":\"�ŦX\"}")]
        public string F01 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string F01_Notes { get; set; }



        [StringLength(100)]
        [Display(Name = "�{���]�d�A�O�_���o�{�U�C����", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"��c�⤧�T�o�B��o�U���Χ@���C\",\"1\":\"�H�y�q���[�o���H�~������A�c��T�o�B��o�C\",\"2\":\"��֭��a�d��~�i��T�o�B��o����I�欰����~�C\",\"3\":\"�]�m���g�֭㤧�X�B�J�f�A�Ѩ����q��Υ[�o�ϥΡC\",\"4\":\"��[�o�������Ԩ��D�H�~���a�ϡA�i��[�o�欰�C\",\"5\":\"��`��o�ܤ��e�n�`�M�W�L4,000���ɪo������������Ψ����˸����o�ѡ]��^�C\",\"6\":\"��`�T�o�ܪo������������Ψ����˸����e�n�`�M�F200���ɥH�W���o�ѡ]��^�C\",\"7\":\"��L�綠�@�w�����v�T�������欰�C\"}")]
        public string G01 { get; set; }

        [StringLength(1)]
        [Display(Name = "�{���]�d�A�O�_���S��Ϊo�ݨD�̥H��]�ѡ^�[�o���c��欰�]��_�̡A�U�C2�ڧK��^", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"�_\",\"1\":\"�O\"}")]
        public string G02 { get; set; }

        [StringLength(1)]
        [Display(Name = "�T�o�ƶq�b10���ɥH�W�ή�o�ƶq�b500���ɥH�W�̡A�~�̬O�_�̤����D�޾����W�w���n�O��n�O�A�é���O����a���e���W�߶K�����D�޾����W�w��ĵ�y����", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"�_\",\"1\":\"�O\"}")]
        public string G02_1 { get; set; }

        [StringLength(1)]
        [Display(Name = "���O����a���e���O�_�������e���B�Ƚ�e���ζ콦�U�C", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"�_\",\"1\":\"�O\"}")]
        public string G02_2 { get; set; }

        [StringLength(500)]
        [Display(Name = "�Ƶ�", Order = 1)]
        [ColumnDef(Visible = false)]
        public string H_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "���˥[�o�����ˤH���O�_�����z�N��", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"�_\",\"1\":\"�O\"}")]
        public string I01 { get; set; }

        [StringLength(500)]
        [Display(Name = "���z�N���p�U�G", Order = 1)]
        [ColumnDef(Visible = false)]
        public string I01_Notes { get; set; }
    }
}
