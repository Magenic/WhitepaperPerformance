using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using TestIncidentQueueService.DataObjects;
using TestIncidentQueueService.Models;

namespace TestIncidentQueueService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    [EnableQuery(MaxTop = 1000)] 
    public class IncidentDetailController : TableController<IncidentDetail>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<IncidentDetail>(context, Request, Services);
        }

        // GET tables/IncidentDetail
        public IQueryable<IncidentDetail> GetAllIncidentDetails()
        {
            return Query();
        }

        // GET tables/IncidentDetail/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<IncidentDetail> GetIncidentDetail(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/IncidentDetail/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<IncidentDetail> PatchIncidentDetail(string id, Delta<IncidentDetail> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/IncidentDetail
        public async Task<IHttpActionResult> PostIncidentDetail(IncidentDetail item)
        {
            item.DateEntered = DateTime.UtcNow;
            IncidentDetail current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/IncidentDetail/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteIncidentDetail(string id)
        {
            return DeleteAsync(id);
        }
    }
}