using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.IncidentApp.Models
{
    public class Incident
    {
        public string Id { get; set; }
        public string AssignedToId { get; set; }
        public string Subject { get; set; }
        public int Description { get; set; }
        public DateTime DateOpened { get; set; }
        public bool Closed { get; set; }
        public DateTime DateClosed { get; set; }
        public string ImageLink { get; set; }
        public string AudioLink { get; set; }
    }
}
