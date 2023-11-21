using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portolo.Email;
using Portolo.Security.Data;
using Portolo.Security.Request;
using Portolo.Security.Response;

namespace Portolo.Security
{
	public interface ISecurityService
	{
		UserDTO ValidateUsers(UserRequestDTO request);
        int SaveUser(UserRequestDTO request);

        //List<CountryResponseDTO> GetCountry(CountryRequestDTO request);
        //CountryResponseDTO GetSelectCountry(CountryRequestDTO request);
        //int SaveCountry(CountryRequestDTO request);

        //List<CountryTimeZoneResponseDTO> GetCountryTimeZone(CountryTimeZoneRequestDTO request);
        //List<ApplicationTextResponseDTO> GetApplicationText(ApplicationTextRequestDTO request);
        //List<OrganizationTypesResponseDTO> GetOrganizationTypes();
        //List<PhoneTypesResponseDTO> GetPhoneTypes();
        //List<ApplicationSettingsResponseDTO> GetApplicationSettings(ApplicationSettingsRequestDTO request);
        //ApplicationSettingsResponseDTO GetSelectApplicationSettings(ApplicationSettingsRequestDTO request);
        //List<SendEmailToResponseDTO> GetSendEmailTo();
        //List<StateResponseDTO> GetState(StateRequestDTO request);
        //List<SuffixesResponseDTO> GetSuffixes();
        //List<SiteOrgTypeResponseDTO> GetSiteOrgType(SiteOrgTypeRequestDTO request);

        //List<LookupCodeResponseDTO> GetLookupCode(LookupCodeRequestDTO request);
        //LookupCodeResponseDTO GetSelectLookupCode(LookupCodeRequestDTO request);
        //int SaveLookupCode(LookupCodeRequestDTO request);
    }
}
