using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeSearch.BLL;
using EmployeeSearch.DB;
using EmployeeSearch.Interface;
using EmployeeSearch.Models;

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
            dashboard.SummaryByGender = summaryByGender.ToDictionary(obj => obj.Key, obj => obj.Count()).ToList();
            dashboard.SummaryByYear = summaryByYear.ToDictionary(obj => obj.Key, obj => obj.Count()).ToList();
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

        public ActionResult ContactParticalView(string keyword)
        {
            ViewBag.Message = "Your contact page.";
            IEmployeeBLL employeeBll = new EmployeeBLL();
            List<Employee> employees=new List<Employee>();
            if (string.IsNullOrEmpty(keyword))
            {
                employees = employeeBll.GetAllEmployees();
            }
            else
            {
                employees = employeeBll.GetAllEmployees()
                    .FindAll(obj =>
                        obj.Eid.HasValue && obj.Eid.Value.ToString().Contains(keyword)
                        || obj.Ename.Contains(keyword)
                        || obj.Name.Contains(keyword)
                        || obj.Badge.Contains(keyword)
                    );
            }

            return PartialView("_Contact",employees);
        }
    }
}