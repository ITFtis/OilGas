namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Item_97
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }

        [StringLength(10)]
        [Display(Name = "�d�ֽs��", Order = 1)]
        public string CheckNo { get; set; }

        [Display(Name = "�d�֮ɶ�", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? CheckDate { get; set; }


        [StringLength(50)]
        [Display(Name = "�[�o���s��", Order = 1)]
        public string CaseNo { get; set; }


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

        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string I00 { get; set; }

        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J00 { get; set; }

        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string K00 { get; set; }

        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string L00 { get; set; }









        [StringLength(1)]
        [Display(Name = "��~�ǵL�x�s���褧���o�ʤp�]�˪o�~", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string A01 { get; set; }

        [StringLength(1)]
        [Display(Name = "���ΡB�o���q���a���L�o�{", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string A02 { get; set; }

        [StringLength(1)]
        [Display(Name = "�Ƥ����L�B�o/�o�{", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string A03 { get; set; }

        [StringLength(1)]
        [Display(Name = "�o�~�x�ëǵL��L�D�o�~���i�U��", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string A04 { get; set; }

        [StringLength(1)]
        [Display(Name = "�o�~�x�ëǨ������L�ܧΡB�l�a", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string A05 { get; set; }

        [StringLength(1)]
        [Display(Name = "�a�W(����)���n�L�Y���t���B�|�}", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string A06 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string A07 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string A08 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string A09 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string A10 { get; set; }


        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 1)]
        public string A_Notes { get; set; }












        [StringLength(1)]
        [Display(Name = "�t�q�L�ާ@���O�e�L��m�����B�}���c����", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B01 { get; set; }

        [StringLength(1)]
        [Display(Name = "�t�q�L�}�����u�ݤl��Ĳ�}�n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B02 { get; set; }

        [StringLength(1)]
        [Display(Name = "�t�q�L�U�������t�q���ƭȲŦX�W�w", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B03 { get; set; }

        [StringLength(1)]
        [Display(Name = "�L�D���z�ʹq��]�Ʃξ���m��Ĥ@�س��ҩβĤG�س��ҽd�򤺨ϥ�", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B04 { get; set; }

        [StringLength(1)]
        [Display(Name = "�q��]�ƴ��Y�B���y�L�l�a�A�u���L�P���r�S", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B05 { get; set; }

        [StringLength(1)]
        [Display(Name = "�q��]�ƺ|�q�_�����նs�@�Υ��`", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B06 { get; set; }

        [StringLength(1)]
        [Display(Name = "�q��]�Ʊ��a�˸m�L�渨", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B07 { get; set; }

        [StringLength(1)]
        [Display(Name = "�׹p�]�Ʊ��a�˸m�L�渨", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B08 { get; set; }

        [StringLength(1)]
        [Display(Name = "���ө��O�\�ॿ�`", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B09 { get; set; }

        [StringLength(1)]
        [Display(Name = "�o�~�x�ëǨ��z�}���Ψ��z�O�㧹�n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string B10 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 1)]
        public string B_Notes { get; set; }













        [StringLength(1)]
        [Display(Name = "���o�f�\�W��", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C01 { get; set; }

        [StringLength(1)]
        [Display(Name = "���o�f�\�ι԰駹�n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C02 { get; set; }

        [StringLength(1)]
        [Display(Name = "���o�f�o�~���O���ѥ��T���n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C03 { get; set; }

        [StringLength(1)]
        [Display(Name = "���o�f���o�L���L�����Ψ����f����", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C04 { get; set; }

        [StringLength(1)]
        [Display(Name = "���o�f�R�q���a�εL���l�B��ɵL�_��", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C05 { get; set; }

        [StringLength(1)]
        [Display(Name = "�x�o�ѳ����\�O���n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C06 { get; set; }

        [StringLength(1)]
        [Display(Name = "�x�o�ѳ������L�n���οn�o", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C07 { get; set; }

        [StringLength(1)]
        [Display(Name = "���������ξ��O����", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C08 { get; set; }

        [StringLength(1)]
        [Display(Name = "�����\�O�o�~���O���ѥ��T���n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C09 { get; set; }

        [StringLength(1)]
        [Display(Name = "��ʶq�o�f�\�԰駹�n�B�\��(�λ\����W��)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C10 { get; set; }

        [StringLength(1)]
        [Display(Name = "�������o�ުk���α��Y�L���|", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C11 { get; set; }

        [StringLength(1)]
        [Display(Name = "�������o��@�ץ��O25%LEL", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C12 { get; set; }

        [StringLength(1)]
        [Display(Name = "�I�o�����F�����u�����L�n���B�n�o", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C13 { get; set; }

        [StringLength(1)]
        [Display(Name = "�Ʈ�(�q��)�ި��B�\����", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string C14 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 1)]
        public string C_Notes { get; set; }








        [StringLength(1)]
        [Display(Name = "�[�o�����yí�T", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D01 { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o�����y�|�P�L�|�o��H", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D02 { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o�j�ξ�ֺ޵L���|", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D03 { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o�j���[���۰ʸ����˸m�\��}�n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D04 { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o��ֺަQ�ަ^���˸m�\��}�n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D05 { get; set; }

        [StringLength(1)]
        [Display(Name = "�B��L����", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D06 { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o�����L�n���B�L�n�o�εL�o��A�]�Ʊ��Y�L���|", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D07 { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o�����a�˸m�L�渨", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D08 { get; set; }

        [StringLength(1)]
        [Display(Name = "�ǰʥֱa�P��׾A��(�ۧl��)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D09 { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o���ιq�𱵽u�հt�q�ޱ��Y�B���z�n�ޱ��Y�L�P��A�w�d�ޤf�K��", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D10 { get; set; }

        [StringLength(1)]
        [Display(Name = "�I�o���[�o�����B�_�֦�m�A��B�T�w", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string D11 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 1)]
        public string D_Notes { get; set; }










        [StringLength(1)]
        [Display(Name = "�Ĥ@���q�o��^���f�ֳt���Y�\�W��", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string E01 { get; set; }

        [StringLength(1)]
        [Display(Name = "�Ĥ@���q�o��^���f�ֳt���Y�@�\�԰�ξާ@�}�n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string E02 { get; set; }

        [StringLength(1)]
        [Display(Name = "�o��^���j�ΦP�b�κ޵L���|", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string E03 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 1)]
        public string E_Notes { get; set; }







        [StringLength(1)]
        [Display(Name = "��m�w��", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string F01 { get; set; }

        [StringLength(1)]
        [Display(Name = "�����]�ƫe�L��m����", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string F02 { get; set; }

        [StringLength(1)]
        [Display(Name = "���������ľ����O���Ĵ���", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string F03 { get; set; }

        [StringLength(1)]
        [Display(Name = "����������L�l�ˡB�׻k", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string F04 { get; set; }

        [StringLength(1)]
        [Display(Name = "�Q�L�f�L��������", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string F05 { get; set; }

        [StringLength(1)]
        [Display(Name = "�Q�L����F��(�[����������)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string F06 { get; set; }

        [StringLength(1)]
        [Display(Name = "��ֺ޵L�l�ˡA���Y�ۺ�B�L�׻k/����", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string F07 { get; set; }

        [StringLength(1)]
        [Display(Name = "�ʱ����n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string F08 { get; set; }

        [StringLength(1)]
        [Display(Name = "�W�������������O���ܥ��`(���w�b����d��)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string F09 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 1)]
        public string F_Notes { get; set; }













        [StringLength(1)]
        [Display(Name = "�u�����[�o�v�Ρu�Y�T�Ϥ��v�лx���n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string G01 { get; set; }

        [StringLength(1)]
        [Display(Name = "�x�o�Ѥ����o�Ϧa�����u�ХܲM��", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string G02 { get; set; }

        [StringLength(1)]
        [Display(Name = "�����X�J�f�лx�μнu���n", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string G03 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "��װ��׼лx���n", Order = 1)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string G04 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "�O�W(�ج[)�ΰ�y���n�B�L�׷l", Order = 1)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string G05 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "���u�\�O���n", Order = 1)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string G06 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 3)]
        public string G_Notes { get; set; }









        [StringLength(1)]
        [Display(Name = "���o�f�R�q���a�q���ŦX�W�w�]50�[�H�U�^", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string H01 { get; set; }

        [StringLength(10)]
        [Display(Name = "���o�f�R�q���a�q���ŦX�W�w�]50�[�H�U�^���", Order = 3)]
        public string H01_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "�[�o�����a�q���ŦX�W�w�]25�[�H�U�^", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string H02 { get; set; }


        [StringLength(10)]
        [Display(Name = "�[�o�����a�q���ŦX�W�w�]25�[�H�U�^���", Order = 3)]
        public string H02_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "�t�q�L���a�q���ŦX�W�w�]50�[�H�U�^", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string H03 { get; set; }

        [StringLength(10)]
        [Display(Name = "�t�q�L���a�q���ŦX�W�w�]50�[�H�U�^���", Order = 3)]
        public string H03_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "�׹p�w�R�q���a�q���ŦX�W�w�]10�[�H�U�^", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string H04 { get; set; }


        [StringLength(10)]
        [Display(Name = "�׹p�w�R�q���a�q���ŦX�W�w�]10�[�H�U�^���", Order = 3)]
        public string H04_Value { get; set; }

        //[StringLength(1)]
        //[Display(Name = "�Ů����Y�����a�q���ŦX�W�w�]50�[�H�U�^", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string H05 { get; set; }

        //[StringLength(10)]
        //[Display(Name = "�Ů����Y�����a�q���ŦX�W�w�]50�[�H�U�^���", Order = 3)]
        //public string H05_Value { get; set; }


        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 3)]
        public string H_Notes { get; set; }













        [StringLength(1)]
        [Display(Name = "�B��L����", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string I01 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "���Y�L�|��", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string I02 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "���O�p���O���ܥ��`", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string I03 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "���b�c�����o�o��A��", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string I04 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "���������a�˸m�L�渨", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string I05 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "����κ޸����Y�L�P��", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string I06 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "�ֱa�P��׾A��B�L�t��", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string I07 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "���O����ñҰʫ�����֧@�ʥ��`", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string I08 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "�w���֤��@�Υ��`", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string I09 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "���@�����n", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string I10 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 3)]
        public string I_Notes { get; set; }









        [StringLength(1)]
        [Display(Name = "�B��L����", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string J01 { get; set; }

        [StringLength(1)]
        [Display(Name = "�|�q�_�������ե��`", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string J02 { get; set; }

        [StringLength(1)]
        [Display(Name = "��氱��˸m���ե��`", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string J03 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J04 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J05 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J06 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J07 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J08 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J09 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J10 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J11 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 3)]
        public string J_Notes { get; set; }









        [StringLength(1)]
        [Display(Name = "�N�o���B��ƪo�ιq�~�G�쥿�`", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string K01 { get; set; }

        [StringLength(1)]
        [Display(Name = "�U�o�q�R��", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string K02 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string K03 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string K04 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 3)]
        public string K_Notes { get; set; }






        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string M02 { get; set; }



        [StringLength(1)]
        [Display(Name = "�C��L�d�x�o�Ѫo�~�s�q�A�ç@������", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string L01 { get; set; }

        [StringLength(1)]
        [Display(Name = "�C��L�d�x�o�Ѧs�q���˴��|�ުo��@�רì���", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string L02 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "�C�~���̮����k�W�w�ѻ⦳�ҷӤ��M�~�H����������ˬd�A���˪��ˬd���i�A�[�o���ۦ�w���ˬd�t�d�H���ˬd���������P���i�O�_�O�s���n�C", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string L03 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 3)]
        public string L_Notes { get; set; }




        //[StringLength(1)]
        //[Display(Name = "�u�[�o���]�m�޲z�W�h�v��29��:�u�g��[�o���~�Ȫ̡A���̥[�o����B�]�ƥئ�w���ˬd��ۦ��I�[�o���]�I�C��B�C��ΨC�b�~�ˬd�����A���P��ڬ۲šA�ХB���O�s�@�~�H�W�C�v", Order = 1)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        //public string M01 { get; set; }

        //[Column(TypeName = "ntext")]
        //[Display(Name = "�Ƶ�", Order = 1)]
        //public string M_Notes { get; set; }



        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string F10 { get; set; }





        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }









        [Display(Name = "��~����-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Count { get; set; }

        [Display(Name = "��~����-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Conform { get; set; }

        [Display(Name = "��~����-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Doesmeet { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Unable { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? I_Unable { get; set; }


        [Display(Name = "�q��]��-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Count { get; set; }

        [Display(Name = "�q��]��-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Conform { get; set; }

        [Display(Name = "�q��]��-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Doesmeet { get; set; }

        [Display(Name = "�x�o�]��-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Count { get; set; }

        [Display(Name = "�x�o�]��-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Conform { get; set; }

        [Display(Name = "�x�o�]��-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Doesmeet { get; set; }

        [Display(Name = "�x�o�]��-�L�k�ˬd", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Unable { get; set; }

        [Display(Name = "�[�o�]��(�[�o��)-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Count { get; set; }

        [Display(Name = "�[�o�]��(�[�o��)-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Conform { get; set; }

        [Display(Name = "�[�o�]��(�[�o��)-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Doesmeet { get; set; }

        [Display(Name = "�o��^���]��-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Count { get; set; }

        [Display(Name = "�o��^���]��-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Conform { get; set; }

        [Display(Name = "�o��^���]��-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Doesmeet { get; set; }

        [Display(Name = "�����]��(������)-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Count { get; set; }

        [Display(Name = "�����]��(������)-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Conform { get; set; }

        [Display(Name = "�����]��(������)-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Doesmeet { get; set; }


















        [Display(Name = "�Хܳ]��-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Count { get; set; }

        [Display(Name = "�Хܳ]��-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Conform { get; set; }

        [Display(Name = "�Хܳ]��-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Doesmeet { get; set; }

        [Display(Name = "�R�q(�q��)���a�˸m-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Count { get; set; }

        [Display(Name = "�R�q(�q��)���a�˸m-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Conform { get; set; }

        [Display(Name = "�R�q(�q��)���a�˸m-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Doesmeet { get; set; }

        [Display(Name = "�R�q(�q��)���a�˸m-�L�k�ˬd", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Unable { get; set; }

        [Display(Name = "�Ů����Y��-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? I_Count { get; set; }

        [Display(Name = "�Ů����Y��-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? I_Conform { get; set; }

        [Display(Name = "�Ů����Y��-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? I_Doesmeet { get; set; }

        [Display(Name = "�~����-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? J_Count { get; set; }

        [Display(Name = "�~����-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? J_Conform { get; set; }

        [Display(Name = "�~����-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? J_Doesmeet { get; set; }

        [Display(Name = "�o�q��-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? K_Count { get; set; }

        [Display(Name = "�o�q��-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? K_Conform { get; set; }

        [Display(Name = "�o�q��-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? K_Doesmeet { get; set; }

        [Display(Name = "�S�O�W�w-���]�m", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? L_Count { get; set; }

        [Display(Name = "�S�O�W�w-�ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? L_Conform { get; set; }

        [Display(Name = "�S�O�W�w-���ŦX", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? L_Doesmeet { get; set; }















        //[Display(Name = "�d�ֽs��", Order = 1)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public int? M_Count { get; set; }

        //[Display(Name = "�d�ֽs��", Order = 1)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public int? M_Conform { get; set; }

        //[Display(Name = "�d�ֽs��", Order = 1)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public int? M_Doesmeet { get; set; }

        [Display(Name = "����-���]�m", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllCount { get; set; }

        [Display(Name = "����-�ŦX", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllConform { get; set; }

        [Display(Name = "����-���ŦX", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllDoesmeet { get; set; }

        [Display(Name = "����-�L�k�ˬd", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllUnable { get; set; }



























        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Unable { get; set; }




        //[StringLength(10)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public string D08_Value { get; set; }

        //[StringLength(10)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public string I05_Value { get; set; }













        [Display(Name = "��װ��׼лx���n", Order = 2)]        
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public int? G04 { get; set; }


        [Display(Name = "�O�W(�ج[)�ΰ�y���n�B�L�׷l", Order = 2)]        
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public int? G05 { get; set; }


        [Display(Name = "���u�\�O���n", Order = 2)]        
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public int? G06 { get; set; }


        [StringLength(1)]
        [Display(Name = "�u�[�o���]�m�޲z�W�h�v��29��:�u�g��[�o���~�Ȫ̡A���̥[�o����B�]�ƥئ�w���ˬd��ۦ��I�[�o���]�I�C��B�C��ΨC�b�~�ˬd�����A���P��ڬ۲šA�ХB���O�s�@�~�H�W�C�v", Order = 4)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string M01 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 4)]
        public string M_Notes { get; set; }













        [Display(Name = "�d�ֽs��", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Count { get; set; }

        [Display(Name = "�d�ֽs��", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Conform { get; set; }

        [Display(Name = "�d�ֽs��", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Doesmeet { get; set; }



    }
}
