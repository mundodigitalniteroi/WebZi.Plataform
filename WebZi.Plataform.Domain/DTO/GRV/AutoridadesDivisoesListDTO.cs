using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class AutoridadesDivisoesListDTO
    {

        public MensagemDTO Mensagem { get; set; } = new();

        public List<AutoridadesDivisoesDTO> Listagem { get; set; }
    }
}
