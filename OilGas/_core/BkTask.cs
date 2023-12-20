using OilGas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace RSW
{
    public class BkTask
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BkTask()
        {
            // 從組態檔載入相關參數，例如 SmtpHost、SmtpPort、SenderEmail 等等.
        }
        private DateTime startdt = DateTime.Now;
        private int runCount = 0;
        private bool _stopping = false;


        public void Run()
        {
            logger.Info("啟動BkTask背景");
            System.IO.File.AppendAllText(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(("~/logs")), "startlog.txt"), $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}啟動BkTask背景" + Environment.NewLine);
            var aThread = new Thread(TaskLoop);
            aThread.IsBackground = true;
            aThread.Priority = ThreadPriority.BelowNormal;  // 避免此背景工作拖慢 ASP.NET 處理 HTTP 請求.
            aThread.Start();
        }

        public void Stop()
        {
            _stopping = true;
        }



        private void TaskLoop()
        {
            // 設定每一輪工作執行完畢之後要間隔幾分鐘再執行下一輪工作.
            const int LoopIntervalInMinutes = 1000 * 60 * 15;//15分鐘1次

            logger.Info("背景TaskLoop on thread ID: " + Thread.CurrentThread.ManagedThreadId.ToString());
            while (!_stopping)
            {
                try
                {
                    Todo();
                    logger.Info("=========================================");
                }
                catch (Exception ex)
                {
                    // 發生意外時只記在 log 裡，不拋出 exception，以確保迴圈持續執行.
                    logger.Error("BkTask.TaskLoop錯誤:"+ex.ToString());
                }
                finally
                {
                    // 每一輪工作完成後的延遲.
                    System.Threading.Thread.Sleep(LoopIntervalInMinutes);
                }
            }
        }

        private void Todo()
        {
            logger.Info($"To do ......啟動時間:{startdt.ToString("yyyy/MM/dd HH:mm:ss")};次數:{(++runCount)}");
            string step = "job:";

            //是否開發環境
            bool isDev = ConfigurationManager.AppSettings["IsDev"].ToString() == "true";

            //工作1
            if (!isDev)
            {
                logger.Info("工作1");
                logger.Info(step + "Start:" + "ToCarFuel_BasicData_List:加油站/A統計報表專區/現況資料-基本資料欄位清單");
                ToCarFuel_BasicData_List();
                logger.Info(step + "End:" + "ToCarFuel_BasicData_List:加油站/A統計報表專區/現況資料-基本資料欄位清單");
            }
            //工作2
            if (!isDev)
            {
                logger.Info("工作2");
                logger.Info(step + "Start:" + "ToCarGas_BasicData_List:汽車加氣站/B統計報表專區/現況資料-基本資料欄位清單");
                ToCarGas_BasicData_List();
                logger.Info(step + "End:" + "ToCarGas_BasicData_List:汽車加氣站/B統計報表專區/現況資料-基本資料欄位清單");
            }
            //工作3
            if (!isDev)
            {
                logger.Info("工作3");
                logger.Info(step + "Start:" + "ToFishGas_BasicData_List:漁船加油站/C統計報表專區/現況資料-基本資料欄位清單");
                ToFishGas_BasicData_List();
                logger.Info(step + "End:" + "ToFishGas_BasicData_List:漁船加油站/C統計報表專區/現況資料-基本資料欄位清單");
            }
            //工作4
            if (!isDev)
            {
                logger.Info("工作4");
                logger.Info(step + "Start:" + "ToSelfFuel_BasicData:自用加儲油/D統計報表專區/基本資料清單查詢");
                ToSelfFuel_BasicData();
                logger.Info(step + "End:" + "ToSelfFuel_BasicData:自用加儲油/D統計報表專區/基本資料清單查詢");
            }
            //工作5
            if (!isDev)
            {
                logger.Info("工作5");
                logger.Info(step + "Start:" + "ToCarFuel_BasicData_Log_List:加油站/A統計報表專區/變更歷程-基本資料欄位清單");
                ToCarFuel_BasicData_Log_List();
                logger.Info(step + "End:" + "ToCarFuel_BasicData_Log_List:加油站/A統計報表專區/變更歷程-基本資料欄位清單");
            }
            //工作6
            if (!isDev)
            {
                logger.Info("工作6");
                logger.Info(step + "Start:" + "ToRpt_CarGas_BasicData_Log_List:汽車加氣站/B統計報表專區/變更歷程-基本資料欄位清單");
                ToRpt_CarGas_BasicData_Log_List();
                logger.Info(step + "End:" + "ToRpt_CarGas_BasicData_Log_List:汽車加氣站/B統計報表專區/變更歷程-基本資料欄位清單");
            }            
            //工作7
            if (!isDev)
            {
                logger.Info("工作7");
                logger.Info(step + "Start:" + "ToRpt_FishGas_BasicData_Log_List:漁船加油站/C統計報表專區/變更歷程-基本資料欄位清單");
                ToRpt_FishGas_BasicData_Log_List();
                logger.Info(step + "End:" + "ToRpt_FishGas_BasicData_Log_List:漁船加油站/C統計報表專區/變更歷程-基本資料欄位清單");
            }
            //工作8
            if (!isDev)
            {
                logger.Info("工作8");
                logger.Info(step + "Start:" + "ToRpt_PortGas_BasicData:航港自用加儲油/E統計報表專區/現況資料匯出");
                ToRpt_PortGas_BasicData();
                logger.Info(step + "End:" + "ToRpt_PortGas_BasicData:航港自用加儲油/E統計報表專區/現況資料匯出");
            }
            //工作9
            if (!isDev)
            {
                logger.Info("工作9");
                logger.Info(step + "Start:" + "ToRpt_PortGas_BasicDataLog:航港自用加儲油/E統計報表專區/變更歷程資料匯出");
                ToRpt_PortGas_BasicDataLog();
                logger.Info(step + "End:" + "ToRpt_PortGas_BasicDataLog:航港自用加儲油/E統計報表專區/變更歷程資料匯出");
            }
            //工作10
            if (!isDev)
            {
                logger.Info("工作10");
                logger.Info(step + "Start:" + "ToRpt_vw_CarFuel_GSM_Select:加油站/A統計報表專區/地下儲油槽列管狀況查詢");
                ToRpt_vw_CarFuel_GSM_Select();
                logger.Info(step + "End:" + "ToRpt_vw_CarFuel_GSM_Select:加油站/A統計報表專區/地下儲油槽列管狀況查詢");
            }
            //工作11
            if (!isDev)
            {
                logger.Info("工作11");
                logger.Info(step + "Start:" + "ToRpt_vw_CarFuel_GSM_Select:加油站/A異常報表/系統最後發文與營運狀況不符合之清單");
                ToRpt_vw_CarFuel_CaseError1();
                logger.Info(step + "End:" + "ToRpt_vw_CarFuel_GSM_Select:加油站/A異常報表/系統最後發文與營運狀況不符合之清單");
            }
        }

        private void ToCarFuel_BasicData_List()
        {
            try
            {
                OilGas.Rpt_CarFuel_BasicData_List.GetAllDatas();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToCarGas_BasicData_List()
        {
            try
            {
                OilGas.Rpt_CarGas_BasicData_List.GetAllDatas();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToFishGas_BasicData_List()
        {
            try
            {
                OilGas.Rpt_FishGas_BasicData_List.GetAllDatas();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToSelfFuel_BasicData()
        {
            try
            {
                OilGas.Rpt_SelfFuel_BasicData.GetAllDatas();
                OilGas.Rpt_SelfFuel_BasicData.GetLogAllDatas();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToCarFuel_BasicData_Log_List()
        {
            try
            {
                OilGas.Rpt_CarFuel_BasicData_Log_List.GetAllDatas();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToRpt_CarGas_BasicData_Log_List()
        {
            try
            {
                OilGas.Rpt_CarGas_BasicData_Log_List.GetAllDatas();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToRpt_FishGas_BasicData_Log_List()
        {
            try
            {
                OilGas.Rpt_FishGas_BasicData_Log_List.GetAllDatas();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToRpt_PortGas_BasicData()
        {
            try
            {
                OilGas.Rpt_PortGas_BasicData.GetAllDatas();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToRpt_PortGas_BasicDataLog()
        {
            try
            {
                OilGas.Rpt_PortGas_BasicDataLog.GetAllDatas();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToRpt_vw_CarFuel_GSM_Select()
        {
            try
            {
                OilGas.Rpt_CarFuel_GSM_Select.GetAllvsCFGS();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void ToRpt_vw_CarFuel_CaseError1()
        {
            try
            {
                OilGas.Rpt_CarFuel_CaseError1.GetAllvwCFCE1();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}