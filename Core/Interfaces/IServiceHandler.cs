using KLFGroup.Data.Entities;
using KLFGroup.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLFGroup.Core.Interfaces
{
    public interface IServiceHandler
    {
        int AddActivity(Activity activity);
        int UseActivityUser(UserActivity userActivity);
        List<ReportUserActivityViewmodel> GetReportUserActivity();
    }
}
