using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Database;

namespace WebZi.Plataform.Data.Services.Sistema
{
    public class ExclusaoHierarquicaService
    {
        private readonly AppDbContext _context;

        public ExclusaoHierarquicaService(AppDbContext context)
        {
            _context = context;
        }

        public void Iniciar(string NomeTabelaMae, string NomeColunaTabelaMae, int TabelaMaeId)
        {
            List<StoreProcedureForeingKeyModel> TabelasFilhas = ListarForeingKeys(NomeTabelaMae);

            if (TabelasFilhas?.Count > 0)
            {
                string colunaFilha;

                List<int> FilhasIds;

                foreach (StoreProcedureForeingKeyModel filha in TabelasFilhas)
                {
                    // Nome da Coluna na Tabela Filha
                    colunaFilha = ListarPrimaryKeys(filha.FKTABLE_NAME).FirstOrDefault().COLUMN_NAME;

                    // Seleciona o ID da Tabela Filha de acordo com o ID da Tabela Mãe, nulo caso não existir registro filha
                    FilhasIds = SelecionarId(filha.FKTABLE_NAME, filha.FKCOLUMN_NAME, TabelaMaeId);

                    if (FilhasIds == null)
                    {
                        continue;
                    }

                    foreach (int FilhaId in FilhasIds)
                    {
                        Iniciar(filha.FKTABLE_NAME, colunaFilha, FilhaId);

                        Excluir(filha.FKTABLE_NAME, colunaFilha, FilhaId);
                    }
                }
            }

            Excluir(NomeTabelaMae, NomeColunaTabelaMae, TabelaMaeId);
        }

        private List<StoreProcedurePrimaryKeyModel> ListarPrimaryKeys(string tabelaMae)
        {
            SqlParameter Parameter = new()
            {
                ParameterName = "pktable_name",
                SqlDbType = SqlDbType.VarChar,
                Value = tabelaMae,
            };

            return _context.Set<StoreProcedurePrimaryKeyModel>()
                .FromSqlRaw("EXEC [dbo].[sp_pkeys] @pktable_name", Parameter)
                .ToList();
        }

        private List<StoreProcedureForeingKeyModel> ListarForeingKeys(string tabelaMae)
        {
            SqlParameter Parameter = new()
            {
                ParameterName = "pktable_name",
                SqlDbType = SqlDbType.VarChar,
                Value = tabelaMae,
            };

            return _context.Set<StoreProcedureForeingKeyModel>()
                .FromSqlRaw("EXEC [dbo].[sp_fkeys] @pktable_name", Parameter)
                .ToList();
        }

        private List<int> SelecionarId(string tabela, string coluna, int id)
        {
            StringBuilder SQL = new();

            List<int> list = new();

            string pk = ListarPrimaryKeys(tabela).FirstOrDefault().COLUMN_NAME;

            SQL.Append("SELECT ").Append(pk).AppendLine(" AS Value");

            SQL.Append("  FROM ").AppendLine(tabela);

            SQL.Append(" WHERE ").Append(coluna).Append(" = ").Append(id);

            List<GenericIntModel> Ids = _context.GenericInt
                .FromSqlRaw(SQL.ToString())
                .ToList();

            if (Ids?.Count > 0)
            {
                foreach (GenericIntModel item in Ids)
                {
                    list.Add(item.Value);
                }

                return list;
            }

            return null;
        }

        private void Excluir(string tabela, string coluna, int id)
        {
            StringBuilder SQL = new();

            SQL.Append("DELETE FROM ").AppendLine(tabela);

            SQL.Append(" WHERE ").Append(coluna).Append(" = ").Append(id);

            _context.Database.ExecuteSqlRaw(SQL.ToString());
        }
    }
}