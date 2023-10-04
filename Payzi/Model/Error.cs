using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Error
{
    public int ErrorCode { get; set; }

    public string ErrorMessage { get; set; } = null!;

    public int? ErrorCodeOnApp { get; set; }

    public string? ErrorMessageOnApp { get; set; }
}
