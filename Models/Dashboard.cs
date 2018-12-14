using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeSearch.Models
{
    public class Dashboard
    {
        public List<KeyValuePair<string, int>> SummaryByGender
        {
            get;
            set;
        }

        public List<KeyValuePair<string, int>> SummaryByYear { get; set; }

        public List<KeyValuePair<string, int>> SummaryByDept { get; set; }

        public List<KeyValuePair<string, int>>SummaryByJoinDate { get; set; }

    }
}