using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataCredencial
{
    public byte AlterDataCredencialId { get; set; }

    public string ApiHostUrl { get; set; }

    public string ApiOauthUrl { get; set; }

    public string ApiUrl { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}
