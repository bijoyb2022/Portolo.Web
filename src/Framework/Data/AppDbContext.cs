using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using Portolo.Framework.Log;
using Portolo.Utility.Configuration;

namespace Portolo.Framework.Data
{
    public class AppDbContext : DbContext
    {
        private const string DbConnectionKey = "DbConnection";
        private const string SaltKeyKey = "SaltKey";
        private const string BaseURLKey = "BaseUrl";
        private const string BaseUrlDemoKey = "BaseUrlDemo";
        private const string IsDemoKey = "IsDemo";
        private const string AttachmentFilesKey = "AttachmentFiles";

        public AppDbContext(string dbConnection, bool winAuth = false)
            : base(ConnectionString(dbConnection, winAuth))
        {
            this.InitializeDatabase();
        }

        public static string DbConnectionString => ConfigurationUtility.Current.GetSection<string>(AppDbContext.DbConnectionKey);

        public static string SaltKey => ConfigurationUtility.Current.GetSection<string>(AppDbContext.SaltKeyKey);
        public static string BaseUrl => ConfigurationUtility.Current.GetSection<string>(AppDbContext.BaseURLKey);
        public static string BaseUrlDemo => ConfigurationUtility.Current.GetSection<string>(AppDbContext.BaseUrlDemoKey);
        public static string IsDemo => ConfigurationUtility.Current.GetSection<string>(AppDbContext.IsDemoKey);
        public static string AttachmentFiles => ConfigurationUtility.Current.GetSection<string>(AppDbContext.AttachmentFilesKey);
        
        public virtual void InitializeDatabase()
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Database.CommandTimeout = 900;
            if (bool.Parse(ConfigurationManager.AppSettings["EnableSqlLog"] ?? "false"))
            {
                this.Database.Log = s => SqlLog.WriteLog(s);
            }
        }

        private static string ConnectionString(string dbConnection, bool winAuth)
        {
            var connection = dbConnection.Split(';');
            var dataSource = connection[0];
            var initialCatalog = connection[1];
            var user = string.Empty;
            var password = string.Empty;
            if (!winAuth)
            {
                user = connection[2];
                password = connection[3];
            }

            var sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = dataSource;
            sqlBuilder.InitialCatalog = initialCatalog;
            sqlBuilder.PersistSecurityInfo = true;
            sqlBuilder.IntegratedSecurity = winAuth;
            sqlBuilder.MultipleActiveResultSets = true;
            sqlBuilder.MaxPoolSize = 1000;
            sqlBuilder.MinPoolSize = 20;
            if (!winAuth)
            {
                sqlBuilder.UserID = user;
                sqlBuilder.Password = password;
            }

            return sqlBuilder.ToString();
        }
    }
}