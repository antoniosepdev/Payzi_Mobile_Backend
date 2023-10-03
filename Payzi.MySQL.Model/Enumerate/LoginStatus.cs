﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.MySQL.Model.Enumerate
{
    public enum LoginStatus
    {
        Success,
        InvalidRunOrPassword,
        UserLocked,
        UserApprovedOut,
        NotAccessAllowed
    }
}
