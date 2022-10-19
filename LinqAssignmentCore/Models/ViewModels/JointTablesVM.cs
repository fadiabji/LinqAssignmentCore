using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqAssignmentCore.Models.ViewModels
{
    public class JointTablesVM
    {
        public string Manufacturer { get; set; }
        public string HeadQuarters { get; set; }
        public string ModelName { get; set; }
        public int Combined { get; set; }
    }
}