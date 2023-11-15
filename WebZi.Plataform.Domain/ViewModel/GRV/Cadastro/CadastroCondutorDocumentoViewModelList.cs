﻿using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CadastroCondutorDocumentoViewModelList
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorGrv { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        public List<CadastroCondutorDocumentoViewModel> ListagemDocumentoCondutor { get; set; }
    }
}