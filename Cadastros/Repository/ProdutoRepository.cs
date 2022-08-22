using System.Data;
using System.Data.SqlClient;
using Cadastros.Models;
using Dapper;

namespace Cadastros.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private IConfiguration _configuration;
        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> AdicionarProduto(ProdutoDTO Produto)
        {
            var parametro = new DynamicParameters();
            parametro.Add("@Descricao", Produto.Descricao, DbType.String);
            parametro.Add("@PesoLiquido", Produto.PesoLiquido, DbType.String);
            parametro.Add("@PrecoUnitario", Produto.PrecoUnitario, DbType.String);
            parametro.Add("@Retorno", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            var inserir = @"
                    BEGIN TRY
                        INSERT INTO
                        [Cadastro].[dbo].[Produto]
                        (
                             [Descricao]
                            ,[PesoLiquido]
                            ,[PrecoUnitario]
                        )
                        VALUES
                        (
                             @Descricao
                            ,@PesoLiquido
                            ,@PrecoUnitario
                        )
	                    SET @Retorno = 1
                    END TRY
                    BEGIN CATCH
                        SET @Retorno = 0
                    END CATCH";

            bool resultado;
            using (var conn = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                conn.Open();
                var execute = await conn.QueryAsync<bool>(inserir, parametro);
                resultado = execute.FirstOrDefault();
            }

            return resultado;
        }

        public async Task<bool> EditarProduto(ProdutoDTO Produto)
        {
            var parametro = new DynamicParameters();
            parametro.Add("@Codigo", Produto.Codigo, DbType.String);
            parametro.Add("@Descricao", Produto.Descricao, DbType.String);
            parametro.Add("@PesoLiquido", Produto.PesoLiquido, DbType.String);
            parametro.Add("@PrecoUnitario", Produto.PrecoUnitario, DbType.String);
            parametro.Add("@Retorno", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            var editar = @"
                    BEGIN TRY
                        UPDATE
                        [Cadastro].[dbo].[Produto]
		                    SET  [Descricao]     = @Descricao
			                    ,[PesoLiquido]   = @PesoLiquido
			                    ,[PrecoUnitario] = @PrecoUnitario
	                    WHERE
		                    [Codigo] = @Codigo
  
	                    SET @Retorno = 1
                    END TRY
                    BEGIN CATCH
                        SET @Retorno = 0
                    END CATCH

                    SELECT @Retorno";

            bool resultado;
            using (var conn = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                conn.Open();
                var execute = await conn.QueryAsync<bool>(editar, parametro);
                resultado = execute.FirstOrDefault();
            }
            return resultado;
        }

        public async Task<bool> ExcluirProduto(int id)
        {
            var parametro = new DynamicParameters();
            parametro.Add("@Id", id, DbType.String);
            parametro.Add("@Retorno", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            var excluir = @"
                BEGIN TRY
	                    DELETE
		                    [Cadastro].[dbo].[Produto] 
	                    WHERE
                            [Codigo] = @Id
	                    SET @Retorno = 1
                    END TRY
                    BEGIN CATCH
	                    SET @Retorno = 0
                    END CATCH
                    
                    SELECT @Retorno";

            bool resultado;
            using (var conn = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                conn.Open();
                var execute = await conn.QueryAsync<bool>(excluir, parametro);
                resultado = execute.FirstOrDefault();
            }

            return resultado; ;
        }

        public async Task<List<ProdutoDTO>> ObterProduto()
        {
            var consulta = @"
                    SELECT
                         [Codigo]
                        ,[Descricao]    
                        ,[PesoLiquido]   
                        ,[PrecoUnitario]
                    FROM
                        [Cadastro].[dbo].[Produto]";

            List<ProdutoDTO> resultado;
            using (var conn = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                conn.Open();
                var execute = await conn.QueryAsync<ProdutoDTO>(consulta);
                resultado = execute.ToList();
            }
            return resultado;
        }

        public async Task<ProdutoDTO> ObterProdutoPorId(int id)
        {
            var parametro = new DynamicParameters();
            parametro.Add("@Codigo", id, DbType.String);

            var consulta = @"
                    SELECT
                         [Codigo]
                        ,[Descricao]    
                        ,[PesoLiquido]   
                        ,[PrecoUnitario]
                    FROM
                        [Cadastro].[dbo].[Produto]
                    WHERE
                        [Codigo] = @Codigo";

            ProdutoDTO resultado;
            using (var conn = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                conn.Open();
                var execute = await conn.QueryAsync<ProdutoDTO>(consulta, parametro);
                resultado = execute.FirstOrDefault();
            }
            return resultado;
        }
    }
}