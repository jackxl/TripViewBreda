using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Navigation
{
    public class NavigationManager
    {
        private Route activeRoute;
        private int currentStep;
        public NavigationManager()
        {

        }
        public Route GetActiveRoute()
        {
            return activeRoute;
        }
        public void SetActiveRoute(Route activeRoute)
        {
            this.activeRoute = activeRoute;
        }
    }
}
