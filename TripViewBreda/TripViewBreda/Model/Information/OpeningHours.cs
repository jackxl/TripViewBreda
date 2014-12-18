using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Model.Information
{
    public class OpeningHours
    {
        private List<OpenComponent> openingHours;

        public OpeningHours()
        {
            openingHours = new List<OpenComponent>();
        }

        public List<OpenComponent> GetOpeningHours()
        {
            return openingHours;
        }

        public void AddOpenComponent(OpenComponent component)
        {
            openingHours.Add(component);
        }

        public void RemoveOpenComponent(OpenComponent component)
        {
            openingHours.Remove(component);
        }
    }
}
