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
            List<StoreProcedureForeingKeyModel> TabelasFilhas = ListForeingKeys(NomeTabelaMae);

            if (TabelasFilhas?.Count > 0)
            {
                string colunaFilha;

                List<int> FilhasIds;

                foreach (StoreProcedureForeingKeyModel filha in TabelasFilhas)
                {
                    // Nome da Coluna na Tabela Filha
                    colunaFilha = ListPrimaryKeys(filha.FKTABLE_NAME)
                        .FirstOrDefault().COLUMN_NAME;

                    // Seleciona o ID da Tabela Filha de acordo com o ID da Tabela Mãe, nulo caso não existir registro filha
                    FilhasIds = ListIds(filha.FKTABLE_NAME, filha.FKCOLUMN_NAME, TabelaMaeId);

                    if (FilhasIds == null)
                    {
                        continue;
                    }

                    foreach (int FilhaId in FilhasIds)
                    {
                        Iniciar(filha.FKTABLE_NAME, colunaFilha, FilhaId);

                        DeleteFilha(filha.FKTABLE_NAME, colunaFilha, FilhaId);
                    }
                }
            }

            DeleteFilha(NomeTabelaMae, NomeColunaTabelaMae, TabelaMaeId);
        }

        private List<StoreProcedurePrimaryKeyModel> ListPrimaryKeys(string tabelaMae)
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

        private List<StoreProcedureForeingKeyModel> ListForeingKeys(string tabelaMae)
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

        private List<int> ListIds(string tableName, string columnName, int id)
        {
            string pk = ListPrimaryKeys(tableName)
                .FirstOrDefault()
                .COLUMN_NAME;

            StringBuilder SQL = new();

            SQL.Append("SELECT ").Append(pk).AppendLine(" AS Value");

            SQL.Append("  FROM ").AppendLine(tableName);

            SQL.Append(" WHERE ").Append(columnName).Append(" = ").Append(id);

            List<GenericIntModel> list = _context.GenericInt
                .FromSqlRaw(SQL.ToString())
                .ToList();

            List<int> Ids = new();

            if (list?.Count > 0)
            {
                foreach (GenericIntModel item in list)
                {
                    Ids.Add(item.Value);
                }

                return Ids;
            }

            return null;
        }

        private void DeleteFilha(string tableName, string columnName, int id)
        {
            StringBuilder SQL = new();

            SQL.Append("DELETE FROM ").AppendLine(tableName);

            SQL.Append(" WHERE ").Append(columnName).Append(" = ").Append(id);

            _context.Database.ExecuteSqlRaw(SQL.ToString());
        }
    }
}