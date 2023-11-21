using Portolo.Primary.Repository;
using Portolo.Utility.Cryptography;
using System;
using System.Configuration;

namespace Portolo.Primary.Data
{
    public class PrimaryUnitOfWork : IDisposable
    {
        private PrimaryContext dbContext;

        private ICountryRepository countryRepository;
        private ICountryTimeZoneRepository countryTimeZoneRepository;
        private IStateRepository stateRepository;
        private IOrganizationTypesRepository organizationTypesRepository;
        private ILookupCodeRepository lookupCodeRepository;
        private IPhoneTypesRepository phoneTypesRepository;

        private bool disposed;
        public string DbConnection { get; set; }
        public PrimaryUnitOfWork(string dbConncetion)
        {
            var saltKey = ConfigurationManager.AppSettings["SaltKey"];
            this.DbConnection = Decryption.Decrypt(dbConncetion, saltKey);
        }

        public ICountryRepository CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                {
                    this.dbContext = new PrimaryContext(this.DbConnection);
                    this.countryRepository = new CountryRepository(this.dbContext)
                    {
                        DbConnection = this.DbConnection
                    };
                }

                return this.countryRepository;
            }
        }

        public ICountryTimeZoneRepository CountryTimeZoneRepository
        {
            get
            {
                if (this.countryTimeZoneRepository == null)
                {
                    this.dbContext = new PrimaryContext(this.DbConnection);
                    this.countryTimeZoneRepository = new CountryTimeZoneRepository(this.dbContext)
                    {
                        DbConnection = this.DbConnection
                    };
                }

                return this.countryTimeZoneRepository;
            }
        }
        public IStateRepository StateRepository
        {
            get
            {
                if (this.stateRepository == null)
                {
                    this.dbContext = new PrimaryContext(this.DbConnection);
                    this.stateRepository = new StateRepository(this.dbContext)
                    {
                        DbConnection = this.DbConnection
                    };
                }

                return this.stateRepository;
            }
        }

        public IOrganizationTypesRepository OrganizationTypesRepository
        {
            get
            {
                if (this.organizationTypesRepository == null)
                {
                    this.dbContext = new PrimaryContext(this.DbConnection);
                    this.organizationTypesRepository = new OrganizationTypesRepository(this.dbContext)
                    {
                        DbConnection = this.DbConnection
                    };
                }

                return this.organizationTypesRepository;
            }
        }
        public ILookupCodeRepository LookupCodeRepository
        {
            get
            {
                if (this.lookupCodeRepository == null)
                {
                    this.dbContext = new PrimaryContext(this.DbConnection);
                    this.lookupCodeRepository = new LookupCodeRepository(this.dbContext)
                    {
                        DbConnection = this.DbConnection
                    };
                }

                return this.lookupCodeRepository;
            }
        }

        public IPhoneTypesRepository PhoneTypesRepository
        {
            get
            {
                if (this.phoneTypesRepository == null)
                {
                    this.dbContext = new PrimaryContext(this.DbConnection);
                    this.phoneTypesRepository = new PhoneTypesRepository(this.dbContext)
                    {
                        DbConnection = this.DbConnection
                    };
                }

                return this.phoneTypesRepository;
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
