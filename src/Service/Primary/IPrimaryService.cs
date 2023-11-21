using System.Collections.Generic;
using Portolo.Primary.Request;
using Portolo.Primary.Response;

namespace Portolo.Primary
{
    public interface IPrimaryService
    {
        List<CountryResponseDTO> GetCountry(CountryRequestDTO request);
        CountryResponseDTO GetSelectCountry(CountryRequestDTO request);
        int SaveCountry(CountryRequestDTO request);
        List<CountryTimeZoneResponseDTO> GetCountryTimeZone(CountryTimeZoneRequestDTO request);
        List<StateResponseDTO> GetState(StateRequestDTO request);
        List<OrganizationTypesResponseDTO> GetOrganizationTypes(OrganizationTypesRequestDTO request);
        List<LookupCodeResponseDTO> GetLookupCode(LookupCodeRequestDTO request);
        LookupCodeResponseDTO GetSelectLookupCode(LookupCodeRequestDTO request);
        int SaveLookupCode(LookupCodeRequestDTO request);
        List<PhoneTypesResponseDTO> GetPhoneTypes(PhoneTypesRequestDTO request);
    }
}