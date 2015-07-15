using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using TestIncidentQueueService.DataObjects;
using TestIncidentQueueService.Models;

namespace TestIncidentQueueService.Controllers
{
    public class IncidentController : TableController<Incident>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Incident>(context, Request, Services);
        }

        // GET tables/Incident
        public IQueryable<Incident> GetAllIncidents()
        {
            return Query();
        }

        // GET tables/Incident/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Incident> GetIncident(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Incident/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Incident> PatchIncident(string id, Delta<Incident> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Incident
        public async Task<IHttpActionResult> PostIncident(Incident item)
        {
            Incident current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new {id = current.Id}, current);
        }

        // DELETE tables/Incident/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteIncident(string id)
        {
            return DeleteAsync(id);
        }
    }
}