using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqAssignmentCore.Models.ViewModels
{
    public class AggregatingVM
    {
        public string Name { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
    }
}