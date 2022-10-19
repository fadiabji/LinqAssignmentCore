using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqAssignmentCore.Models.Db;

namespace LinqAssignmentCore.Models.ViewModels
{
    public class GroupJoinSelectManyVM
    {
        public string Country { get; set; }
        public List<Car> CarBrands { get; set; }
    }
}