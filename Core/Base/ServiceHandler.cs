using KLFGroup.Core.Interfaces;
using KLFGroup.Data;
using KLFGroup.Data.Entities;
using KLFGroup.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KLFGroup.Core.Base
{
    public class ServiceHandler : IServiceHandler
    {
        private readonly ILogger<ServiceHandler> _loggerFactory;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly KLFGroupContext _context;
        public ServiceHandler(KLFGroupContext context, ILogger<ServiceHandler> loggerFactory, ApplicationDbContext applicationDbContext)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            _applicationDbContext = applicationDbContext;
        }

        public bool ExistActivityUser(string userId)
        {
            return _context.UserActivities
                .Any(u => u.UserId == userId);
        }

        public bool ExistActivityUserByAcitivityId(string userId, int activityId)
        {
            return _context.UserActivities
                .Any(u => u.UserId == userId && u.ActivityId == activityId);
        }

        public int UseActivityUser(UserActivity userActivity)
        {
            try
            {
                //switch (userActivity.ActivityId)
                //{
                //    case 1:
                bool existUserActivity = ExistActivityUserByAcitivityId(userActivity.UserId, userActivity.ActivityId);

                if (existUserActivity)
                {
                    var result = AddUserCountOccurency(userActivity);
                    return result;
                }
                else
                {
                    userActivity.FirstOccurence = DateTime.Now;
                    userActivity.Occurence = 1;
                    _context.UserActivities.Add(userActivity);
                    var result = _context.SaveChanges();
                    return result;
                }
                //        break;
                //    case 2:
                //        break;
                //    case 3:
                //        break;
                //    case 4:
                //        break;
                //    default:
                //        break;
                //}
            }
            catch (Exception ex)
            {
                _loggerFactory.LogError(ex, "Exception in Use Activity User");
                return ex.HResult;
            }

        }

        public int AddActivity(Activity activity)
        {
            try
            {
                _context.Activities.Add(activity);
                var result = _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                _loggerFactory.LogError(ex, "Exception in Add Activity");
                return ex.HResult;
            }
        }

        public int GetUserCountOccurency(string userId, int activityId)
        {
            var q = _context.UserActivities
                .Where(u => u.UserId == userId && u.ActivityId == activityId)
                .Select(m => m.Occurence)
                .FirstOrDefault();
            return q;
        }

        public int AddUserCountOccurency(UserActivity userActivity)
        {
            int addedCount = GetUserCountOccurency(userActivity.UserId, userActivity.ActivityId) + 1;
            var q = _context.UserActivities
                .Where(u => u.UserId == userActivity.UserId && u.ActivityId == userActivity.ActivityId)
                .FirstOrDefault();
            q.Occurence = addedCount;
            q.LastOccurence = userActivity.LastOccurence;
            _context.UserActivities.Update(q);
            int res = _context.SaveChanges();
            return res;
        }

        public List<ReportUserActivityViewmodel> GetReportUserActivity()
        {
            var user = _applicationDbContext.Users.ToList();
            var userActivity = _context.UserActivities.ToList();
            var activity = _context.Activities.ToList();
            var report = (from ac in activity
                          join uac in userActivity on ac.Id equals uac.ActivityId
                          join us in user on uac.UserId equals us.Id

                          select new ReportUserActivityViewmodel
                          {
                              Username = us.UserName,
                              ActivityName = ac.Name,
                              Amount = uac.Occurence,
                              LastOccurrence = uac.LastOccurence,
                              FirstOccurrence = uac.FirstOccurence
                          }).ToList();
            return report.OrderByDescending(o => o.Username).ToList();

        }
    }


}
