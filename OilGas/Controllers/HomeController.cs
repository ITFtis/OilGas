using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers
{
    public class HomeController : Dou.Controllers.UserBaseControll<User, Role>
    {
        public ActionResult Index()
        {
            
            //取得最新消息
            Dou.Models.DB.IModelEntity<news> news = new Dou.Models.DB.ModelEntity<news>(_dbContext);
            ViewBag.news = news.GetAll()
                            .OrderByDescending(x => x.news_date)
                            .Take(4)
                            .ToList();

            //取得懶人包
            Dou.Models.DB.IModelEntity<Lazybag> lazyBag = new Dou.Models.DB.ModelEntity<Lazybag>(_dbContext);
            ViewBag.lazyBag = lazyBag.GetAll().ToList();

            //圖片驗證碼
            ViewBag.CaptchaImg = GenCaptcha();

            ViewBag.test = Session["VerificationCode"];
            return View();
        }

		

		internal static System.Data.Entity.DbContext _dbContext = new OilGasModelContextExt();
        protected override Dou.Models.DB.IModelEntity<User> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<User>(_dbContext);
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult RefreshCaptcha()
        {
            var src = GenCaptcha();

            return Json(src, JsonRequestBehavior.AllowGet);
        }

		private string GenCaptcha()
		{
            string imageSrc;
			//產生隨機數
            Random random = new Random();
            string verificationCode = generatevCode();

            //存入session
            Session["VerificationCode"] = verificationCode;

			DouHelper.Misc.AddCache(verificationCode, "VerificationCode");

			//create bitmap obj
			using (Bitmap bitmap = GenerateNoisyImg(90,30,499))
            {
				
				using (Graphics g = Graphics.FromImage(bitmap))
                {

					using (Font font = new Font("Arial", 22,FontStyle.Bold))
                    {
                        using (Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0,0,bitmap.Width,bitmap.Height),Color.Blue,Color.DarkRed,1.2F,true))
                        {
                            g.DrawString(verificationCode, font, b, new PointF(2, 2));
                        }
                    }
                }

				//轉換成byte
				using (MemoryStream ms = new MemoryStream())
				{
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    byte[] imageByte = ms.ToArray();

					string base64Str = Convert.ToBase64String(imageByte);
                    imageSrc = $"data:image/jpeg;base64,{base64Str}";
				}
			}

            return imageSrc;
            
		}

		private string generatevCode()
		{
            List<char> elements = new List<char>();
            string code;
            Random r = new Random();
            char c;
            for(var i = 0; i < 4; i++)
            {
                if(i < 2)
                {
                    c = (char)r.Next(65, 91);
                }
                else
                {
                    c = (char)r.Next(48,57);
                }
                elements.Add(c);
            }

            //隨機排列
            for(var k = 0; k < elements.Count()-1; k++)
            {
                
				var targetIndex = r.Next(k + 1, elements.Count());

				char x;
				x = elements[k];
				elements[k] = elements[targetIndex];
                elements[targetIndex] = x;
			
            }
            code = string.Join("",elements);
            return code;
		}

		private Bitmap GenerateNoisyImg(int v1, int v2, int v3)
		{
            Bitmap bitmap = new Bitmap(v1,v2);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
				Random rand = new Random();

				//設定雜訊背景
				g.Clear(Color.White);
                for(var i = 0; i <24; i++)
                {
                    var x1 = rand.Next(v1);
					var x2 = rand.Next(v1);
					var y1 = rand.Next(v2);
					var y2 = rand.Next(v2);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
				}
				
				
				for (var i = 0; i < v3; i++)
				{
					int x = rand.Next(v1);
					int y = rand.Next(v2);

					bitmap.SetPixel(x, y,Color.FromArgb(rand.Next()) );
				}
			}
            return bitmap;
                
		}
	}
}