using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeSearch.BLL;
using EmployeeSearch.DB;
using EmployeeSearch.Interface;
using EmployeeSearch.Models;
using Newtonsoft.Json;
using System.IO;
using IDictionaryBLL = EmployeeSearch.Interface.IDictionaryBLL;

namespace EmployeeSearch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSummaryData()
        {
            IEmployeeBLL employeeBll = new EmployeeBLL();
            List<Employee> employees = employeeBll.GetAllEmployees();



            var summaryByGender = employees.GroupBy(obj => obj.Gender);
            var summaryByYear = employees.OrderBy(obj => obj.BirthYear).GroupBy(obj => obj.BirthYear);
            var summaryByDept = employees.GroupBy(obj => obj.DepAbbr);
            var summaryByJoinDate = employees.OrderBy(obj => obj.JoinDate).GroupBy(obj => obj.JoinDate);
            Dashboard dashboard = new Dashboard();

            dashboard.SummaryByDept = summaryByDept.ToDictionary(obj => obj.Key, obj => obj.Count()).OrderBy(obj => obj.Key).ToList();
            dashboard.SummaryByGender = summaryByGender.ToDictionary(obj => obj.Key.ToString(), obj => obj.Count()).ToList();
            dashboard.SummaryByYear = summaryByYear.ToDictionary(obj => obj.Key.ToString(), obj => obj.Count()).ToList();
            dashboard.SummaryByJoinDate = summaryByJoinDate.ToDictionary(obj => obj.Key.HasValue ? obj.Key.Value.ToString("yyyy-MM-dd") : "", obj => obj.Count()).ToList();
            return Json(dashboard);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult MorningReading()
        {
            return View();
        }

        public ActionResult VPS()
        {
            return View();
        }

        public ActionResult Files()
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            List<FileInfo> fileInfos = new List<FileInfo>();
            var files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                fileInfos.Add(new FileInfo(file));
            }
            return View(fileInfos);
        }

        public FileResult GetFile(string path)
        {
            var fileIdBtye = Convert.FromBase64String(path);
            FileInfo filePath = new FileInfo(System.Text.Encoding.UTF8.GetString(fileIdBtye));
            return File(System.IO.File.ReadAllBytes(filePath.FullName), "application/" + filePath.Extension.Trim('.'), filePath.Name);
        }


        public JsonResult GetVPSData()
        {
            WebClient webClient = new WebClient();
            string vpsUseData = webClient.DownloadString("https://api.64clouds.com/v1/getRawUsageStats?veid=977067&api_key=private_K5iURVOKzZ0glb1IDparCUAW");
            VPS vps = JsonConvert.DeserializeObject<VPS>(vpsUseData);
            return Json(vps);
        }

        public ActionResult ContactParticalView(string keyword)
        {
            ViewBag.Message = "Your contact page.";
            IEmployeeBLL employeeBll = new EmployeeBLL();
            List<Employee> employees = new List<Employee>();
            if (string.IsNullOrEmpty(keyword))
            {
                employees = employeeBll.GetAllEmployees();
            }
            else
            {
                employees = employeeBll.GetAllEmployees()
                    //.FindAll(obj =>
                    //    obj.Eid.HasValue && obj.Eid.Value.ToString().Contains(keyword)
                    .FindAll(obj => obj.Eid.ToString().Contains(keyword)
                        || obj.Ename.Contains(keyword)
                        || obj.Name.Contains(keyword)
                        || obj.Badge.Contains(keyword)
                    );
            }

            return PartialView("_Contact", employees);
        }

        public ActionResult MorningReadingParticalView()
        {
            IDictionaryBLL iDictionaryBll=new DictionaryBLL();
            MorningReading morningReading = iDictionaryBll.GetMorningReader();
            return PartialView("_MorningReading", morningReading);
        }

        public JsonResult GetContentJsonResult(string contentUrl)
        {
            IDictionaryBLL iDictionaryBll = new DictionaryBLL();
            string morningReadingContentJsonString = iDictionaryBll.GetMorningReaderContentJsonString(contentUrl);
            return Json(morningReadingContentJsonString);
        }
        public JsonResult GetAllEmployeePhotoResult()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content\\Employee_Images");
            IEmployeeBLL employeeBll = new EmployeeBLL();
            List<Dictionary<string,string>>allEmployeePhotoInfo= employeeBll.GetAllEmployeePhotoPath(path);
            return Json(allEmployeePhotoInfo);
        }


    }
}