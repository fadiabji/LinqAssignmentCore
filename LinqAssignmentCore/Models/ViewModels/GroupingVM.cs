using LinqAssignmentCore.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqAssignmentCore.Models.ViewModels
{
    public class GroupingVM
    {
        public string Manufacturer { get; set; }
        public List<Car> Cars { get; set; }
    }
}