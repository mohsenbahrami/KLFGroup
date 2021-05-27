using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLFGroup.ViewModel
{
    public class ReportUserActivityViewmodel
    {
        public string Username { get; set; }
        public string ActivityName { get; set; }
        public int Amount { get; set; }
        public DateTime FirstOccurrence { get; set; }
        public DateTime LastOccurrence { get; set; }

    }
}
