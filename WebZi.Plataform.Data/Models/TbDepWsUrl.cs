using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepWsUrl
{
    public byte WsUrlId { get; set; }

    public string WsName { get; set; }

    public string WsUrl { get; set; }

    public string WsUsername { get; set; }

    public string WsPassword { get; set; }
}
