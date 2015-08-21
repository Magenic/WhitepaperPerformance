using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.IncidentApp.Models
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Manager { get; set; }

        public override bool Equals(object obj)
        {
            var compareUserProfile = obj as UserProfile;
            if (compareUserProfile == null)
            {
                return false;
            }
            return UserId == compareUserProfile.UserId;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
