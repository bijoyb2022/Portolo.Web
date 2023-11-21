using Portolo.Primary.Data;
using Portolo.Primary.Repository;
using Portolo.Primary.Request;
using Portolo.Primary.Response;
using System.Collections.Generic;
using System.Configuration;

namespace Portolo.Primary
{
    public partial class PrimaryService : IPrimaryService
    {
        private readonly ICountryRepository countryRepository;

        public PrimaryService(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        public PrimaryService()
        {
        }

        //private string DbConnection => ConfigurationUtility.Current.GetSection<string>("DbConnection");
        private string DbConnection => ConfigurationManager.ConnectionStrings["DbConnection"].ToString();

        public List<CountryResponseDTO> GetCountry(CountryRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.CountryRepository.GetCountry(request);
            }
        }

        public CountryResponseDTO GetSelectCountry(CountryRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.CountryRepository.GetSelectCountry(request);
            }
        }
        public int SaveCountry(CountryRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.CountryRepository.SaveCountry(request);
            }
        }

        public List<CountryTimeZoneResponseDTO> GetCountryTimeZone(CountryTimeZoneRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.CountryTimeZoneRepository.GetCountryTimeZone(request);
            }
        }

        public List<StateResponseDTO> GetState(StateRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.StateRepository.GetState(request);
            }
        }

        public List<OrganizationTypesResponseDTO> GetOrganizationTypes(OrganizationTypesRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.OrganizationTypesRepository.GetOrganizationTypes(request);
            }
        }

        public List<LookupCodeResponseDTO> GetLookupCode(LookupCodeRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.LookupCodeRepository.GetLookupCode(request);
            }
        }

        public LookupCodeResponseDTO GetSelectLookupCode(LookupCodeRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.LookupCodeRepository.GetSelectLookupCode(request);
            }
        }
        public int SaveLookupCode(LookupCodeRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.LookupCodeRepository.SaveLookupCode(request);
            }
        }

        public List<PhoneTypesResponseDTO> GetPhoneTypes(PhoneTypesRequestDTO request)
        {
            using (var unitOfWork = new PrimaryUnitOfWork(this.DbConnection))
            {
                return unitOfWork.PhoneTypesRepository.GetPhoneTypes(request);
            }
        }

    }
}