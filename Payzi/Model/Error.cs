using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Error
{
    public string ErrorCode { get; set; } = null!;

    public string ErrorMessage { get; set; } = null!;

    public int? ErrorCodeOnApp { get; set; }

    public string? ErrorMessageOnApp { get; set; }
}
