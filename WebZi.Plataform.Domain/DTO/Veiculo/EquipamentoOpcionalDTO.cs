﻿namespace WebZi.Plataform.Domain.DTO.Veiculo
{
    public class EquipamentoOpcionalDTO
    {
        public decimal IdentificadorEquipamentoOpcional { get; set; }

        public int? OrdemVistoria { get; set; } = 0;

        public string Descricao { get; set; }

        public string ItemObrigatorio { get; set; }

        public string Status { get; set; }
    }
}