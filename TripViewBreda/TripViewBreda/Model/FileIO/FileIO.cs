using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Model.FileIO
{
    interface FileIO
    {
        void read();
        bool write(Model.Information.Subjects subject);
    }
}
