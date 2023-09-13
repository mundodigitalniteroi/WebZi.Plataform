using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Local;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;

namespace WebZi.Plataform.Data.Services.Atendimento
{
    public class AtendimentoService
    {
        private readonly AppDbContext _context;

        public AtendimentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AtendimentoModel> GetById(int id)
        {
            AtendimentoModel atendimento = await _context.Atendimentos
                .FirstOrDefaultAsync(w => w.AtendimentoId.Equals(id));

            return atendimento;
        }

        public async Task<AtendimentoModel> GetByProcesso(string numeroFormulario, int clienteId, int depositoId)
        {
            AtendimentoModel atendimento = await _context.Atendimentos
                .Include(i => i.Grv)
                .FirstOrDefaultAsync(w => w.Grv.NumeroFormularioGrv.Equals(numeroFormulario) && w.Grv.ClienteId.Equals(clienteId) && w.Grv.DepositoId.Equals(depositoId));

            return atendimento;
        }

        public async Task<string> ChecarGrvParaCadastro(AtendimentoModel Atendimento)
        {
            StringBuilder erros = new();

            #region Consultas
            if (Atendimento.GrvId <= 0)
            {
                return "Primeiro é necessário informar o ID do GRV";
            }

            GrvModel grv = await _context.Grvs
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .FirstOrDefaultAsync(w => w.GrvId.Equals(Atendimento.GrvId));

            if (grv == null)
            {
                return "ID do GRV inexistente";
            }

            if (grv.StatusOperacao.StatusOperacaoId != 'V' && grv.StatusOperacao.StatusOperacaoId != '1')
            {
                return $"Status do GRV não está apto para o cadastro do Atendimento: {grv.StatusOperacao.Descricao}";
            }

            AtendimentoModel atendimentoConsulta = await GetById(Atendimento.GrvId);

            if (atendimentoConsulta == null)
            {
                return "Este GRV já possui um Atendimento cadastrado";
            }
            #endregion Consultas

            if (Atendimento.UsuarioCadastroId <= 0)
            {
                erros.AppendLine("Primeiro é necessário informar o Usuário");
            }

            #region Responsável
            if (Atendimento.QualificacaoResponsavelId <= 0)
            {
                erros.AppendLine("Primeiro é necessário informar a Qualificação do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNome))
            {
                erros.AppendLine("Primeiro é necessário informar o Nome do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDocumento))
            {
                erros.AppendLine("Primeiro é necessário informar o CPF do Responsável");
            }
            else if (!DocumentHelper.IsCPF(Atendimento.ResponsavelDocumento))
            {
                erros.AppendLine("CPF do Responsável é inválido");
            }

            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelCnh))
            {
                if (!DocumentHelper.IsCNH(Atendimento.ResponsavelCnh))
                {
                    erros.AppendLine("CNH do Responsável é inválido");
                }
            }
            #endregion Responsável

            #region Endereço
            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelCep))
            {
                erros.AppendLine("Primeiro é necessário informar o CEP do Responsável");
            }
            else if (!LocalHelper.IsCEP(Atendimento.ResponsavelCep))
            {
                erros.AppendLine("CEP do Responsável inválido");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelEndereco))
            {
                erros.AppendLine("Primeiro é necessário informar o Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelNumero))
            {
                erros.AppendLine("Primeiro é necessário informar o Número do Logradouro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelBairro))
            {
                erros.AppendLine("Primeiro é necessário informar o Bairro do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelMunicipio))
            {
                erros.AppendLine("Primeiro é necessário informar Município do Responsável");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelUf))
            {
                erros.AppendLine("Primeiro é necessário informar a Unidade Federativa do Responsável");
            }
            #endregion Endereço

            #region DDD + Telefone/Celular do Responsável
            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelTelefone))
            {
                if ((!ContactHelper.IsTelephone(Atendimento.ResponsavelTelefone) || !ContactHelper.IsCellphone(Atendimento.ResponsavelTelefone)))
                {
                    erros.AppendLine("Telefone/Celular inválido");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.ResponsavelDdd))
                {
                    erros.AppendLine("Ao informar o Número do Telefone/Celular do Responsável também é preciso informar o DDD");
                }
                else if (!ContactHelper.IsCellphone(Atendimento.ResponsavelDdd))
                {
                    erros.AppendLine("DDD inválido");
                }
            }
            #endregion DDD + Telefone/Celular do Responsável

            #region Proprietário
            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioNome))
            {
                erros.AppendLine("Primeiro é necessário informar o Nome do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId <= 0)
            {
                erros.AppendLine("Primeiro é necessário informar o Tipo do Documento do Proprietário");
            }

            if (string.IsNullOrWhiteSpace(Atendimento.ProprietarioDocumento))
            {
                erros.AppendLine("Primeiro é necessário informar o Documento do Proprietário");
            }

            if (Atendimento.ProprietarioTipoDocumentoId <= 0)
            {
                erros.AppendLine("Primeiro é necessário informar o Tipo do Documento do Proprietário");
            }
            else
            {
                TipoDocumentoIdentificacaoModel TipoDocumentoIdentificacao = await _context.TiposDocumentosIdentificacao
                    .FirstOrDefaultAsync(w => w.TipoDocumentoIdentificacaoId.Equals(Atendimento.ProprietarioTipoDocumentoId));

                if (TipoDocumentoIdentificacao == null)
                {
                    erros.AppendLine("Tipo do Documento do Proprietário inexistente");
                }
                else if (TipoDocumentoIdentificacao.Codigo.Equals("CPF") && !DocumentHelper.IsCPF(Atendimento.ProprietarioDocumento))
                {
                    erros.AppendLine("O CPF do Proprietário é inválido");
                }
                else if (TipoDocumentoIdentificacao.Codigo.Equals("CNPJ") && !DocumentHelper.IsCNPJ(Atendimento.ProprietarioDocumento))
                {
                    erros.AppendLine("O CNPJ do Proprietário é inválido");
                }
            }
            #endregion Proprietário

            #region Nota Fiscal
            if (grv.Cliente.FlagEmissaoNotaFiscalSap == 'S')
            {
                #region Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNome))
                {
                    erros.AppendLine("Primeiro é necessário informar o Nome do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDocumento))
                {
                    erros.AppendLine("Primeiro é necessário informar o CPF ou CNPJ do Receptor da Nota Fiscal");
                }
                else if (!DocumentHelper.IsCPF(Atendimento.NotaFiscalDocumento) || !DocumentHelper.IsCNPJ(Atendimento.NotaFiscalDocumento))
                {
                    erros.AppendLine("CPF ou CNPJ do Receptor da Nota Fiscal inválido");
                }
                #endregion Receptor da Nota Fiscal

                #region Endereço do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalCep))
                {
                    erros.AppendLine("Primeiro é necessário informar o CEP do Receptor da Nota Fiscal");
                }
                else if (!LocalHelper.IsCEP(Atendimento.NotaFiscalCep))
                {
                    erros.AppendLine("CEP do Receptor da Nota Fiscal inválido");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEndereco))
                {
                    erros.AppendLine("Primeiro é necessário informar o Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalNumero))
                {
                    erros.AppendLine("Primeiro é necessário informar o Número do Endereço do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalBairro))
                {
                    erros.AppendLine("Primeiro é necessário informar o Bairro do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalMunicipio))
                {
                    erros.AppendLine("Primeiro é necessário informar o Município do Receptor da Nota Fiscal");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalUf))
                {
                    erros.AppendLine("Primeiro é necessário informar a UF do Receptor da Nota Fiscal");
                }
                #endregion Endereço do Receptor da Nota Fiscal

                #region Contatos do Receptor da Nota Fiscal
                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalTelefone))
                {
                    erros.AppendLine("Primeiro é necessário informar o Número do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsTelephone(Atendimento.NotaFiscalTelefone) && !ContactHelper.IsCellphone(Atendimento.NotaFiscalTelefone))
                {
                    erros.AppendLine("Número do Telefone/Celular do Receptor da Nota Fiscal inválido");
                }

                if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDdd))
                {
                    erros.AppendLine("Primeiro é necessário informar o DDD do Telefone/Celular do Receptor da Nota Fiscal");
                }
                else if (!ContactHelper.IsDDD(Atendimento.NotaFiscalDdd))
                {
                    erros.AppendLine("DDD do Número do Telefone/Celular do Receptor da Nota Fiscal inválido");
                }

                if (!string.IsNullOrWhiteSpace(Atendimento.NotaFiscalEmail) && !EmailHelper.IsEmail(Atendimento.NotaFiscalEmail))
                {
                    erros.AppendLine("E-mail do Receptor da Nota Fiscal é inválido");
                }
                #endregion Contatos do Receptor da Nota Fiscal


                #region Inscrição Municipal do Tomador do Serviço
                if (!string.IsNullOrWhiteSpace(Atendimento.NotaFiscalDocumento) && DocumentHelper.IsCNPJ(Atendimento.NotaFiscalDocumento))
                {
                    if (string.IsNullOrWhiteSpace(Atendimento.NotaFiscalInscricaoMunicipal))
                    {
                        erros.AppendLine("Primeiro é necessário informar a Inscrição Municipal do Tomador do Serviço do Receptor da Nota Fiscal");
                    }

                    // Informar a Inscrição Municipal do Tomador do Serviço do Receptor da Nota Fiscal só é obrigatorio
                    // caso o Cliente esteja cadastrado na regra do Faturamento "ATENDINSCRICMUNIC".

                    FaturamentoRegraModel faturamentoRegra = await _context.FaturamentoRegras
                        .Include(i => i.FaturamentoRegraTipo)
                        .FirstOrDefaultAsync(w => w.ClienteId.Equals(grv.ClienteId) &&
                                                  w.FaturamentoRegraTipo.Codigo.Equals("ATENDINSCRICMUNIC"));

                    if (faturamentoRegra != null)
                    {
                        erros.AppendLine("Primeiro é necessário informar a Inscrição Municipal do Tomador do Serviço do Receptor da Nota Fiscal");

                        erros.AppendLine("Ao informar o CNPJ do Receptor da Nota Fiscal é preciso informar a Inscrição Municipal do Tomador do Serviço");
                    }
                }
                #endregion Inscrição Municipal do Tomador do Serviço
            }
            #endregion Nota Fiscal

            if (!string.IsNullOrWhiteSpace(erros.ToString()))
            {
                return erros.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}