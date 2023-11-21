using Portolo.Security.Repository;
using Portolo.Utility.Cryptography;
using System;
using System.Configuration;

namespace Portolo.Security.Data
{
    public class SecurityUnitOfWork : IDisposable
    {
        private SecurityContext dbContext;

        private IUserRepository userRepository;
        //private ICountryRepository countryRepository;
        //private IStateRepository stateRepository;
        //private ICountryTimeZoneRepository countryTimeZoneRepository;
        //private IApplicationTextRepository applicationTextRepository;
        //private IOrganizationTypesRepository organizationTypesRepository;
        //private IPhoneTypesRepository phoneTypesRepository;
        //private IApplicationSettingsRepository applicationSettingsRepository;
        //private ISendEmailToRepository sendEmailToRepository;
        //private ISuffixesRepository suffixesRepository;
        //private ISiteOrgTypeRepository siteOrgTypeRepository;
        //private ILookupCodeRepository lookupCodeRepository;

        private bool disposed;
        public string DbConnection { get; set; }
        public SecurityUnitOfWork(string dbConncetion)
        {
            var saltKey = ConfigurationManager.AppSettings["SaltKey"];
            this.DbConnection = Decryption.Decrypt(dbConncetion, saltKey);
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.dbContext = new SecurityContext(this.DbConnection);
                    this.userRepository = new UserRepository(this.dbContext)
                    {
                        DbConnection = this.DbConnection
                    };
                }

                return this.userRepository;
            }
        }

        //public ILookupCodeRepository LookupCodeRepository
        //{
        //    get
        //    {
        //        if (this.lookupCodeRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.userRepository = new UserRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.lookupCodeRepository;
        //    }
        //}

        //public ICountryRepository CountryRepository
        //{
        //    get
        //    {
        //        if (this.countryRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.userRepository = new UserRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.countryRepository;
        //    }
        //}
        //public IStateRepository StateRepository
        //{
        //    get
        //    {
        //        if (this.stateRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.userRepository = new UserRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.stateRepository;
        //    }
        //}

        //public ICountryTimeZoneRepository CountryTimeZoneRepository
        //{
        //    get
        //    {
        //        if (this.countryTimeZoneRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.countryTimeZoneRepository = new CountryTimeZoneRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.countryTimeZoneRepository;
        //    }
        //}
        //public IApplicationTextRepository ApplicationTextRepository
        //{
        //    get
        //    {
        //        if (this.applicationTextRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.applicationTextRepository = new ApplicationTextRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.applicationTextRepository;
        //    }
        //}
        //public IOrganizationTypesRepository OrganizationTypesRepository
        //{
        //    get
        //    {
        //        if (this.organizationTypesRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.organizationTypesRepository = new OrganizationTypesRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.organizationTypesRepository;
        //    }
        //}

        //public IPhoneTypesRepository PhoneTypesRepository
        //{
        //    get
        //    {
        //        if (this.phoneTypesRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.phoneTypesRepository = new PhoneTypesRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.phoneTypesRepository;
        //    }
        //}
        //public IApplicationSettingsRepository ApplicationSettingsRepository
        //{
        //    get
        //    {
        //        if (this.applicationSettingsRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.applicationSettingsRepository = new ApplicationSettingsRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.applicationSettingsRepository;
        //    }
        //}
        //public ISendEmailToRepository SendEmailToRepository
        //{
        //    get
        //    {
        //        if (this.sendEmailToRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.sendEmailToRepository = new SendEmailToRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.sendEmailToRepository;
        //    }
        //}
        //public ISuffixesRepository SuffixesRepository
        //{
        //    get
        //    {
        //        if (this.suffixesRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.suffixesRepository = new SuffixesRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.suffixesRepository;
        //    }
        //}

        //public ISiteOrgTypeRepository SiteOrgTypeRepository
        //{
        //    get
        //    {
        //        if (this.siteOrgTypeRepository == null)
        //        {
        //            this.dbContext = new SecurityContext(this.DbConnection);
        //            this.siteOrgTypeRepository = new SiteOrgTypeRepository(this.dbContext)
        //            {
        //                DbConnection = this.DbConnection
        //            };
        //        }

        //        return this.siteOrgTypeRepository;
        //    }
        //}       

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
