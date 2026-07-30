using System;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.GRV
{
    public class GgvFotoModel
    {
        public int IdFoto { get; set; }

        public int IdGrv { get; set; }

        public int IdUsuarioCadastro { get; set; }
        public byte[] Foto { get; set; }

        public string TipoFoto { get; set; }

        public DateTime? DataCadastro { get; set; }
        public string TipoCadastro { get; set; }
        
    }
}
