﻿using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco.PIX
{
    public class PixEstaticoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorPix { get; set; }

        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public decimal Valor { get; set; }

        public string MerchantName { get; set; }

        public string MerchantCity { get; set; }

        public string QRString { get; set; }

        public string QRCode { get; set; }
    }
}