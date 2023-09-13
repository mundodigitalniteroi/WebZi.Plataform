using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLiberese
{
    public int IdLiberese { get; set; }

    public int IdGrv { get; set; }

    public int IdUsuario { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime DataDocumento { get; set; }

    public string NomeDiretorPresidente { get; set; }

    public string NomeFiscalTransporte { get; set; }

    public string NomeProprietario { get; set; }

    public string ProcuradorCompradorSocio { get; set; }

    public string Condutor { get; set; }

    public string RegistroCnh { get; set; }

    public string Categoria { get; set; }

    public string AgenteTransito { get; set; }

    public string Observacao { get; set; }

    public string ProprietarioDocumento { get; set; }

    public string ProprietarioRg { get; set; }
}
