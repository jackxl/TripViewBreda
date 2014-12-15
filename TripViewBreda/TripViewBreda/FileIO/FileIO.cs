using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.FileIO
{
    public abstract class FileIO
    {
        public abstract void readFile(string path);

        public abstract bool write(string path);

    }
}
