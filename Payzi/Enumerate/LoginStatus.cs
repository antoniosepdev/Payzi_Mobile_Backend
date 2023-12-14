using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Enumerate
{
    public enum LoginStatus
    {
        Success,
        InvalidEmailOrPassword,
        UserLocked,
        UserApprovedOut,
        NotAccessAllowed
    }
}
