using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbSapRequest
{
    public string AuthType { get; set; }

    public string AuthUser { get; set; }

    public string AuthPassword { get; set; }

    public string LogonUser { get; set; }

    public string RemoteUser { get; set; }

    public string LocalAddr { get; set; }

    public string QueryString { get; set; }

    public string RemoteAddr { get; set; }

    public string RemoteHost { get; set; }

    public string RemotePort { get; set; }

    public string RequestMethod { get; set; }

    public string ServerName { get; set; }

    public string ServerPort { get; set; }

    public string ServerSoftware { get; set; }

    public string Url { get; set; }

    public string HttpHost { get; set; }

    public string HttpReferer { get; set; }

    public string PathInfo { get; set; }

    public string AllHttp { get; set; }
}
