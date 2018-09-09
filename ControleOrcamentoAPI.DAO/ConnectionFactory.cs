using System;
using System.Data;
using System.Data.SqlClient;

namespace ControleOrcamentoAPI.DAO
{
    internal class ConnectionFactory : IDisposable
    {
        private static IDbConnection _cnn;

        private bool _manterConexao = false;

        public ConnectionFactory(bool manterConexao = false)
        {
            _manterConexao = manterConexao;
        }

        static ConnectionFactory()
        {
            if (_cnn == null)
                _cnn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["stringConnection"].ToString());//TODO: REVER DEPRECADO
        }

        internal IDbConnection GetConnection()
        {
            return _cnn;
        }

        #region ObterParametro
        internal IDataParameter ObterParametro(string nome, object value)
        {
            return ObterParametro(nome, value, SqlDbType.Text);
        }

        internal IDataParameter ObterParametro(string nome, object value, SqlDbType type = SqlDbType.Text, int tamanho = 99999, ParameterDirection direcao = ParameterDirection.Input, bool nulavel = true)
        {
            return new SqlParameter(nome, value);
        }
        #endregion

        #region ExecutaComando
        internal int ExecutaComando(string sql)
        {
            return ExecutaComando(sql, null);
        }

        internal int ExecutaComando(string sql, IDataParameter[] parameters)
        {
            return ExecutaComando(sql, parameters);
        }

        internal int ExecutaComando(string sql, IDataParameter[] parameters, IDbTransaction transaction)
        {
            var cmd = new SqlCommand(sql, (SqlConnection)_cnn, (SqlTransaction)transaction);
            _cnn.Open();
            var qtd = cmd.ExecuteNonQuery();
            if (!_manterConexao) _cnn.Close();
            return qtd;
        }
        #endregion

        #region ObterDados
        internal DataTable ObterDados(string sql)
        {
            return ObterDados(sql, null);
        }

        internal DataTable ObterDados(string sql, IDataParameter[] parameters)
        {
            return ObterDados(sql, parameters, null);
        }

        internal DataTable ObterDados(string sql, IDataParameter[] parameters, IDbTransaction transaction)
        {
            var cmd = new SqlCommand(sql, (SqlConnection)_cnn, (SqlTransaction)transaction);
            if ((parameters != null) && (parameters.Length > 0))
                cmd.Parameters.AddRange(parameters);
            _cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            if (!_manterConexao) _cnn.Close();
            if (ds == null)
                return null;
            if ((ds == null) || (ds.Tables == null) || (ds.Tables.Count < 1))
                return null;
            return ds.Tables[0];
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (_cnn != null)
            {
                if (_cnn.State != ConnectionState.Closed)
                    _cnn.Close();
            }
        }
        #endregion
    }
}
