using Portolo.Pages.Data;
using Portolo.Pages.Repository;
using Portolo.Utility.Cryptography;
using System;
using System.Configuration;

namespace Portolo.Pages.Data
{
    public class PagesUnitOfWork : IDisposable
    {
        private PagesContext dbContext;

        private IApplicationTextRepository applicationTextRepository;

        private bool disposed;
        public string DbConnection { get; set; }
        public PagesUnitOfWork(string dbConncetion)
        {
            var saltKey = ConfigurationManager.AppSettings["SaltKey"];
            this.DbConnection = Decryption.Decrypt(dbConncetion, saltKey);
        }

        public IApplicationTextRepository ApplicationTextRepository
        {
            get
            {
                if (this.applicationTextRepository == null)
                {
                    this.dbContext = new PagesContext(this.DbConnection);
                    this.applicationTextRepository = new ApplicationTextRepository(this.dbContext)
                    {
                        DbConnection = this.DbConnection
                    };
                }

                return this.applicationTextRepository;
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dbContext?.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
