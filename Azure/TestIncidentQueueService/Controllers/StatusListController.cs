using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using TestIncidentQueueService.Models;
using TestIncidentQueueService.Utils;

namespace TestIncidentQueueService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    [AuthorizeRole(Enums.AdRoles.Manager)]
    [RoutePrefix("api/StatusList")]
    public class StatusListController : ApiController
    {
        // GET api/WorkerList
        public async Task<IList<UserStatus>> Get()
        {
            const int daysBack = -30;
            var workerListController = new WorkerListController();
            var workers = await workerListController.Get();
            var incidentController = new IncidentController();
            var service = new Microsoft.WindowsAzure.Mobile.Service.ApiServices(Configuration);
            incidentController.Services = service;
            incidentController.InitializeInt(ControllerContext);

            var currentDate = DateTime.UtcNow;
            var startDate = currentDate.AddDays(daysBack);
            
            var returnValue = new List<UserStatus>();
            foreach (var worker in workers)
            {
                var userStatus = new UserStatus {User = worker};
                userStatus.TotalCompleteIncidentsPast30Days =
                    incidentController.GetAllIncidents().Count(i => i.AssignedToId== worker.UserId && i.DateClosed >= startDate);
                userStatus.TotalOpenIncidents = incidentController.GetAllIncidents().Count(i => i.AssignedToId == worker.UserId && !i.Closed);
                var openIncidents = incidentController.GetAllIncidents().Where(i => i.AssignedToId == worker.UserId && !i.Closed);//.Average(i => ((currentDate - i.DateOpened).Minutes));
                double totalMinutes = 0;
                if (openIncidents.Any())
                {
                    foreach (var incident in openIncidents)
                    {
                        totalMinutes += (DateTime.UtcNow - incident.DateOpened).TotalMinutes;
                    }
                    userStatus.AvgWaitTimeOfOpenIncidents = Convert.ToInt32(totalMinutes/openIncidents.Count());
                }
                returnValue.Add(userStatus);                    
            }
            return returnValue;
        }
    }
}