using System.Data;
using System.Data.SqlClient;

namespace EnferDev.Infra.Infra
{
    public class MSSQLDB : IDB
    {
        private SqlConnection _conexao;
        private string _configuracao;

        public MSSQLDB(IDbConfiguration configuration)
        {
            _configuracao = configuration.connectionString;
        }
        public void Dispose()
        {
            _conexao.Close();
            _conexao.Dispose();
        }

        public IDbConnection GetCon()
        {
            if (_conexao == null)
            {
                return _conexao = new SqlConnection(_configuracao);
            }
            else
            {
                if (_conexao.State != ConnectionState.Open)
                    _conexao.ConnectionString = _configuracao;
            }
            return _conexao;
        }
    }
}
