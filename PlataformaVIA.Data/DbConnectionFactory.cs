namespace PlataformaVIA.Data
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;

    public class DbConnectionFactory : IConnectionFactory
    {
        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;
        private readonly string _name;

        private ConnectionStringSettings connectionString; //= ConfigurationManager.ConnectionStrings["ConexionPlataformaVIA"];


        public DbConnectionFactory()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["ConexionPlataformaVIA"];

            if (connectionString == null) throw new ArgumentNullException("connectionString");

            //var conStr = ConfigurationManager.ConnectionStrings[connectionName];
            //if (conStr == null)
            //    throw new ConfigurationErrorsException(string.Format("Failed to find connection string named '{0}' in app/web.config.", connectionName));

            _name = connectionString.ProviderName;
            _provider = DbProviderFactories.GetFactory(connectionString.ProviderName);
            _connectionString = connectionString.ConnectionString;

        }

        public IDbConnection Create()
        {
            var connection = _provider.CreateConnection();
            if (connection == null)
                throw new ConfigurationErrorsException(string.Format("Fallo al crear una conexión con la cadena '{0}' in app/web.config.", _name));

            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
