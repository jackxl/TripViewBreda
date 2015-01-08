using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Model.FileIO
{
    public interface FileIO
    {
        Task read();
        bool write(Model.Information.Subjects subject);
    }
}
