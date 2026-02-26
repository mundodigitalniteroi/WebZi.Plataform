using WebZi.Plataform.Domain.Models.Documento;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.GRV.DRFA
{
    public class DRFAModel
    {
        public int GrvDrfaId { get; set; }                

        public int GrvId { get; set; }                    

        public byte TipoRegistroId { get; set; }   

        public short OrgaoEmissorId { get; set; }           

        public byte AutoridadeDivisaoId { get; set; }     

        public int UsuarioCadastroId { get; set; }        

        public int? UsuarioAlteracaoId { get; set; }      

        public string? AutoridadeDivisaoComplemento { get; set; } 

        public string? NumeroRegistroRouboFurto { get; set; }     

        public string? RegistroRouboFurtoMatriculaAgente { get; set; } 

        public string? RegistroRouboFurtoNomeAgente { get; set; }      

        public string? LocalRemocaoEnderecoCompleto { get; set; }      

        public string? LocalRemocaoReferencia { get; set; }            

        public string? LocalRemocaoLatitude { get; set; }              

        public string? LocalRemocaoLongitude { get; set; }             

        public string? EstadoGeralVeiculo { get; set; }                

        public DateTime DataCadastro { get; set; }                    

        public DateTime? DataAlteracao { get; set; }                  

        public char FlagRegistroRecuperacao { get; set; }    

        public char FlagRegistroAgendado { get; set; }


        public GrvModel Grv { get; set; }
        public TipoRegistroModel TipoRegistro { get; set; }
        public OrgaoEmissorModel OrgaoEmissor { get; set; }
        public AutoridadeDivisaoModel AutoridadeDivisao { get; set; }
        public UsuarioModel UsuarioCadastro { get; set; }
        public UsuarioModel UsuarioAlteracao { get; set; }
    }
}
