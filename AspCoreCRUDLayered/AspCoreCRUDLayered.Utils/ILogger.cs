using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreCRUDLayered.Utils
{
    public interface ILogger
    {
        public void LogException(Exception ex);

    }

}
