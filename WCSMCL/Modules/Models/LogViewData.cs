using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCSMCL.ViewModels;

namespace WCSMCL.Modules.Models
{
    public class LogViewData : ViewDataBase<string>
    {
        public LogViewData(string data) : base(data)
        {

        }
    }
}
