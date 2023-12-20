using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NPOI.XWPF.UserModel.XWPFTableCell;

namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "Sign", Name = "課程報名", MenuPath = "資訊查詢/I線上報名", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.Update| FuncEnum.Delete, AllowAnonymous = false)]
    public class SignController : APaginationModelController<Sign>
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: FileDownload
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Sign> GetModelEntity()
        {
            return new ModelEntity<Sign>(new OilGasModelContextExt());
        }


        protected override void UpdateDBObject(IModelEntity<Sign> dbEntity, IEnumerable<Sign> objs)
        {


            base.UpdateDBObject(dbEntity, objs);
        }
        protected override void AddDBObject(IModelEntity<Sign> dbEntity, IEnumerable<Sign> objs)
        {
            objs.First().SignId = Guid.NewGuid();


            base.AddDBObject(dbEntity, objs);
        }

        public ActionResult ExportSignUserExcel(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.課程報名);
            string fileTitle = "課程報名學員名單";

            // List<string> titles = new List<string>() { "查核輔導專區_儲槽總數差異勾稽報表，查詢條件:" };

            var Lesson = GetModelEntity().GetAll().ToList();

            var LessonID = KeyValue.GetFilterParaValue(paras, "LessonID");
            var iquery = from a in Lesson
                         join b in db.Lesson on a.LessonID equals b.LessonID into c
                         from d in c.DefaultIfEmpty()
                         where a.LessonID.ToString() == LessonID || LessonID == null || LessonID ==""
                         select new { Sign = a, Lesson = d };




            //產出Dynamic資料 (給Excel)
            List<dynamic> list = new List<dynamic>();
            var output = iquery.ToList();
            foreach (var data in output)
            {
                dynamic f = new ExpandoObject();
                f.課程 = data.Lesson.ClassName;
                f.姓名 = data.Sign.Name;
                f.身分證 = data.Sign.IdentityId;
                f.性別 = data.Sign.gender;
                f.電話 = data.Sign.Tel;
                f.手機 = data.Sign.Mobile;
                f.Email = data.Sign.Email;
                f.地址 = data.Sign.address;
                f.生日 = data.Sign.birth;
                f.職業 = data.Sign.Occupation;
                f.興趣 = data.Sign.Hobbies;


                f.SheetName = fileTitle;//sheep.名稱;
                list.Add(f);
            }

            //查無符合資料表數
            if (list.Count == 0)
            {
                return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
            }

            //產出excel
            string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF1(fileTitle, null, list, folder, "N");
            string path = folder + fileName;
            url = OilGas.Cm.PhysicalToUrl(path);

            if (url == "")
            {
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExportSignUserword(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.簽到單);
    

            //抓資料
            var Lesson = GetModelEntity().GetAll().ToList();
            var LessonID = KeyValue.GetFilterParaValue(paras, "LessonID");
            var iquery = from a in Lesson
                         join b in db.Lesson on a.LessonID equals b.LessonID into c
                         from d in c.DefaultIfEmpty()
                         where a.LessonID.ToString() == LessonID 
                         select new { Sign = a, Lesson = d };

            var list = iquery.ToList();
            //查無符合資料表數
            if (list.Count == 0 || LessonID == null || LessonID == "")
            {
                return Json(new { result = false, errorMessage = "請選擇課程" }, JsonRequestBehavior.AllowGet);
            }

          
            //創WORD
            XWPFDocument doc = new XWPFDocument();

            //第一行字"課程+簽到單"
            XWPFParagraph paragraph = doc.CreateParagraph();
            paragraph.Alignment = ParagraphAlignment.CENTER;
            XWPFRun run1 = paragraph.CreateRun(); 
            run1.FontSize = 21;
            run1.SetText(list[0].Lesson.ClassName + "簽到單");    
            run1.IsBold = true;



            //創表格
            int rowCount = list.Count+1; // 行(根據搜尋結果改變)
            int columnCount = 2; // 列
            XWPFTable table = doc.CreateTable(rowCount, columnCount);
            table.Width = 5000;

            //填表格
            for (int row = 0; row < rowCount; row++)
            {
               
                if (row == 0)
                {
                    
                    // 指定格子
                    XWPFTableCell cell = table.GetRow(row).GetCell(0);
                    // 填字
                    var Paragraphs = cell.Paragraphs[0];
                    Paragraphs.Alignment = ParagraphAlignment.CENTER;
                    XWPFRun cellRun = cell.Paragraphs[0].CreateRun();  
                    cellRun.FontSize = 18;
                    cellRun.IsBold = true;
                    cellRun.SetText("學員");




                    // 指定格子
                    XWPFTableCell cell2 = table.GetRow(row).GetCell(1);
                    // 填字
                    var Paragraphs2 = cell2.Paragraphs[0];
                    Paragraphs2.Alignment = ParagraphAlignment.CENTER;          
                    XWPFRun cellRun2 = cell2.Paragraphs[0].CreateRun();
                    cellRun2.FontSize = 18;
                    cellRun2.IsBold = true;
                    cellRun2.SetText("簽名");// 填字

                }
                else
                {
                    table.GetRow(row).Height = 1000;

                    // 指定格子
                    XWPFTableCell cell = table.GetRow(row).GetCell(0);
                    //垂直置中
                    cell.SetVerticalAlignment(XWPFVertAlign.CENTER);

                    // 填字
                    var Paragraphs = cell.Paragraphs[0];
                    Paragraphs.Alignment = ParagraphAlignment.CENTER;
                    XWPFRun cellRun = cell.Paragraphs[0].CreateRun();
                    cellRun.FontSize = 12;
                    cellRun.SetText(list[row - 1].Sign.Name); // 填字

                }

            }


            string path = folder + "簽到單.docx";
            url = OilGas.Cm.PhysicalToUrl(path);

            if (url == "")
            {
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //匯出
                //如果沒資料夾則新建資料夾
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                doc.Write(fs);
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);
            }

         
        }

    }
}