using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Portolo.Email;
using Portolo.Framework.Utils;
using Portolo.Security.Data;
using Portolo.Security.Repository;
using Portolo.Security.Request;
using Portolo.Security.Response;
using Portolo.Utility.Configuration;

namespace Portolo.Security
{
    public partial class SecurityService : ISecurityService
    {
        private readonly IUserRepository userRepository;

        public SecurityService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public SecurityService()
        {
        }
        //private string DbConnection => ConfigurationUtility.Current.GetSection<string>("DbConnection");
        private string DbConnection => ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

        public UserDTO ValidateUsers(UserRequestDTO request)
        {
            using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
            {
                return unitOfWork.UserRepository.ValidateUsers(request);
            }
        }

        public int SaveUser(UserRequestDTO request)
        {
            using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
            {
                return unitOfWork.UserRepository.SaveUser(request);
            }
        }
        //public List<CountryResponseDTO> GetCountry(CountryRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.CountryRepository.GetCountry(request);
        //    }
        //}

        //public CountryResponseDTO GetSelectCountry(CountryRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.CountryRepository.GetSelectCountry(request);
        //    }
        //}
        //public int SaveCountry(CountryRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.CountryRepository.SaveCountry(request);
        //    }
        //}

        //public List<StateResponseDTO> GetState(StateRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.StateRepository.GetState(request);
        //    }
        //}
        //public List<CountryTimeZoneResponseDTO> GetCountryTimeZone(CountryTimeZoneRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.CountryTimeZoneRepository.GetCountryTimeZone(request);
        //    }
        //}
        //public List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.ApplicationTextRepository.GetApplicationText(request);
        //    }
        //}
        //public List<OrganizationTypesResponseDTO> GetOrganizationTypes()
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.OrganizationTypesRepository.GetOrganizationTypes();
        //    }
        //}
        //public List<PhoneTypesResponseDTO> GetPhoneTypes()
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.PhoneTypesRepository.GetPhoneTypes();
        //    }
        //}
        //public List<ApplicationSettingsResponseDTO> GetApplicationSettings(ApplicationSettingsRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.ApplicationSettingsRepository.GetApplicationSettings(request);
        //    }
        //}
        //public ApplicationSettingsResponseDTO GetSelectApplicationSettings(ApplicationSettingsRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.ApplicationSettingsRepository.GetSelectApplicationSettings(request);
        //    }
        //}
        //public List<SendEmailToResponseDTO> GetSendEmailTo()
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.SendEmailToRepository.GetSendEmailTo();
        //    }
        //}
        //public List<SuffixesResponseDTO> GetSuffixes()
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.SuffixesRepository.GetSuffixes();
        //    }
        //}

        //public List<SiteOrgTypeResponseDTO> GetSiteOrgType(SiteOrgTypeRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.SiteOrgTypeRepository.GetSiteOrgType(request);
        //    }
        //}

        //public List<LookupCodeResponseDTO> GetLookupCode(LookupCodeRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.LookupCodeRepository.GetLookupCode(request);
        //    }
        //}

        //public LookupCodeResponseDTO GetSelectLookupCode(LookupCodeRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.LookupCodeRepository.GetSelectLookupCode(request);
        //    }
        //}
        //public int SaveLookupCode(LookupCodeRequestDTO request)
        //{
        //    using (var unitOfWork = new SecurityUnitOfWork(this.DbConnection))
        //    {
        //        return unitOfWork.LookupCodeRepository.SaveLookupCode(request);
        //    }
        //}

    }
}
