using System;
using System.Configuration;
using Portolo.Services.Common.Data;
using Portolo.Utility.Cryptography;

namespace Portolo.Common.Repository
{
    public class CommonUnitOfWork : IDisposable
    {
        private CommonContext dbContext;

        private ILanguageRepository languageRepository;
        private IResourceRepository resourceRepository;

        public CommonUnitOfWork(string dbConnection)
        {
            var saltKey = ConfigurationManager.AppSettings["SaltKey"];
            this.DbConnection = Decryption.Decrypt(dbConnection, saltKey);
        }

        public string DbConnection { get; set; }

        public IResourceRepository ResourceRepository
        {
            get
            {
                if (this.resourceRepository == null)
                {
                    this.dbContext = new CommonContext(this.DbConnection, false);
                    this.resourceRepository = new ResourceRepository(this.dbContext);
                }

                return this.resourceRepository;
            }
        }

        public ILanguageRepository LanguageRepository
        {
            get
            {
                if (this.languageRepository == null)
                {
                    this.dbContext = new CommonContext(this.DbConnection, false);
                    this.languageRepository = new LanguageRepository(this.dbContext);
                }

                return this.languageRepository;
            }
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }
    }
}