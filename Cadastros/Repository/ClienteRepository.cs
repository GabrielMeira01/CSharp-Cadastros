using System.Data;
using System.Data.SqlClient;
using Cadastros.Models;
using Dapper;

namespace Cadastros.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private IConfiguration _configuration;
        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> AdicionarCliente(ClienteDTO cliente)
        {
            var parametro = new DynamicParameters();
            parametro.Add("@Nome", cliente.Nome, DbType.String);
            parametro.Add("@CEP", cliente.CEP, DbType.String);
            parametro.Add("@Logradouro", cliente.Logradouro, DbType.String);
            parametro.Add("@Complemento", cliente.Complemento, DbType.String);
            parametro.Add("@Bairro", cliente.Bairro, DbType.String);
            parametro.Add("@Cidade", cliente.Cidade, DbType.String);
            parametro.Add("@UF", cliente.UF, DbType.String);
            parametro.Add("@CodigoIBGE", cliente.CodigoIBGE, DbType.String);
            parametro.Add("@Retorno", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            var inserir = @"
                    BEGIN TRY
	                    INSERT INTO 
		                    [Cadastro].[dbo].[Cliente] 
		                    (
			                    [Nome]
		                    )
	                    VALUES 
		                    (
                                @Nome
                            )
	                    SET @Retorno = 1
                    END TRY
                    BEGIN CATCH
	                    SET @Retorno = 0
                    END CATCH

                    IF @Retorno = 1
	                    INSERT INTO 
		                    [Cadastro].[dbo].[Endereco]
		                    (
			                     [Cep]
			                    ,[Logradouro]
			                    ,[Complemento]
			                    ,[Bairro]
			                    ,[Cidade]
			                    ,[Uf]
			                    ,[CodigoIbge]
		                    )
	                    VALUES 
		                    (
                                 @CEP
                                ,@Logradouro
                                ,@Complemento 
                                ,@Bairro
                                ,@Cidade
                                ,@UF
                                ,@CodigoIBGE
                            )
                    ELSE 
	                    SET @Retorno = 0

                    SELECT @Retorno";

            bool resultado;
            using (var conn = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                conn.Open();
                var execute = await conn.QueryAsync<bool>(inserir, parametro);
                resultado = execute.FirstOrDefault();
            }

            return resultado;
        }

        public async Task<bool> EditarCliente(ClienteDTO cliente)
        {
            var parametro = new DynamicParameters();
            parametro.Add("@Codigo", cliente.Codigo, DbType.String);
            parametro.Add("@Nome", cliente.Nome, DbType.String);
            parametro.Add("@CEP", cliente.CEP, DbType.String);
            parametro.Add("@Logradouro", cliente.Logradouro, DbType.String);
            parametro.Add("@Complemento", cliente.Complemento, DbType.String);
            parametro.Add("@Bairro", cliente.Bairro, DbType.String);
            parametro.Add("@Cidade", cliente.Cidade, DbType.String);
            parametro.Add("@UF", cliente.UF, DbType.String);
            parametro.Add("@CodigoIBGE", cliente.CodigoIBGE, DbType.String);
            parametro.Add("@Retorno", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            var editar = @"
                    BEGIN TRY
	                    UPDATE
		                    [Cadastro].[dbo].[Cliente] 
	                    SET [Nome] = @Nome
	                    WHERE
		                    [Codigo] = @Codigo
	                    SET @Retorno = 1
                    END TRY
                    BEGIN CATCH
	                    SET @Retorno = 0
                    END CATCH

                    IF @Retorno = 1
	                    UPDATE
		                    [Cadastro].[dbo].[Endereco]
		                        SET  [Cep]		  = @CEP
				                    ,[Logradouro]  = @Logradouro
				                    ,[Complemento] = @Complemento 
				                    ,[Bairro]	  = @Bairro
				                    ,[Cidade]	  = @Cidade
				                    ,[Uf]		  = @UF
				                    ,[CodigoIbge]  = @CodigoIBGE
		                    WHERE
			                    [Codigo] = @Codigo
                    ELSE 
	                    SET @Retorno = 0

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

        public async Task<bool> ExcluirCliente(int id)
        {
            var parametro = new DynamicParameters();
            parametro.Add("@Id", id, DbType.String);
            parametro.Add("@Retorno", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            var excluir = @"
                    BEGIN TRY
	                    DELETE
		                    [Cadastro].[dbo].[Cliente] 
	                    WHERE
                                [Codigo] = @Id
	                    SET @Retorno = 1
                    END TRY
                    BEGIN CATCH
	                    SET @Retorno = 0
                    END CATCH

                    IF @Retorno = 1
	                    DELETE
		                    [Cadastro].[dbo].[Endereco] 
	                    WHERE
                                [Codigo] = @Id
	                    SET @Retorno = 1

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

        public async Task<List<ClienteDTO>> ObterCliente()
        {
            var consulta = @"
                    SELECT
	                     C.[Codigo]
	                    ,C.[Nome]
	                    ,E.[Cep]
	                    ,E.[Logradouro]
	                    ,E.[Complemento]
	                    ,E.[Bairro]
	                    ,E.[Cidade]
	                    ,E.[Uf]
	                    ,E.[CodigoIbge]
                    FROM 
                    [Cadastro].[dbo].[Cliente] C 
                    INNER JOIN [Cadastro].[dbo].[Endereco] E ON C.[Codigo] = E.[Codigo]";

            List<ClienteDTO> resultado;
            using (var conn = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                conn.Open();
                var execute = await conn.QueryAsync<ClienteDTO>(consulta);
                resultado = execute.ToList();
            }
            return resultado;
        }

        public async Task<ClienteDTO> ObterClientePorId(int id)
        {
            var parametro = new DynamicParameters();
            parametro.Add("@Codigo", id, DbType.String);

            var consulta = @"
                    SELECT
	                     C.[Codigo]
	                    ,C.[Nome]
	                    ,E.[Cep]
	                    ,E.[Logradouro]
	                    ,E.[Complemento]
	                    ,E.[Bairro]
	                    ,E.[Cidade]
	                    ,E.[Uf]
	                    ,E.[CodigoIbge]
                    FROM 
                        [Cadastro].[dbo].[Cliente] C 
                        INNER JOIN [Cadastro].[dbo].[Endereco] E ON C.[Codigo] = E.[Codigo]
                    WHERE
                       C.[Codigo] = @Codigo";

            ClienteDTO resultado;
            using (var conn = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                conn.Open();
                var execute = await conn.QueryAsync<ClienteDTO>(consulta, parametro);
                resultado = execute.FirstOrDefault();
            }
            return resultado;
        }
    }
}