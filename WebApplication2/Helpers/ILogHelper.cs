using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Helpers
{
    public interface ILogForHelper
    {
        void Warn(string message);
        void Error(string message, Exception ex);
    }
}
