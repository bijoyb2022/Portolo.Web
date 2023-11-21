using Portolo.Framework.Data;
using Portolo.Security.Data;
using Portolo.Security.Request;
using Portolo.Security.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Portolo.Security.Repository
{
    public class UserRepository : GenericRepository<User, SecurityContext>, IUserRepository
    {
        public UserRepository(SecurityContext context)
            : base(context)
        {
        }
        public int SaveUser(UserRequestDTO request)
        {
            var salutationKey = new SqlParameter("@SalutationKey", request.SalutationKey);
            var firstName = new SqlParameter("@FirstName", request.FirstName);
            var middleName = new SqlParameter("@MiddleName", request.MiddleName);
            var lastName = new SqlParameter("@LastName", request.LastName);
            var suffixesKey = new SqlParameter("@SuffixesKey", request.SuffixesKey);
            var userName = new SqlParameter("@UserName", request.UserName);
            var sendEmailToKey = new SqlParameter("@SendEmailToKey", request.SendEmailToKey);
            var emailTypeKey1 = new SqlParameter("@EmailTypeKey1", request.EmailTypeKey1);
            var email1 = new SqlParameter("@Email1", request.Email1);
            var sendEmailToKey2 = new SqlParameter("@SendEmailToKey2", request.SendEmailToKey2);
            var emailTypeKey2 = new SqlParameter("@EmailTypeKey2", request.EmailTypeKey2);
            var email2 = new SqlParameter("@Email2", request.Email2);
            var password = new SqlParameter("@Password", request.Password);
            var countryKey = new SqlParameter("@CountryKey", request.CountryKey);
            var address1 = new SqlParameter("@Address1", request.Address1);
            var address2 = new SqlParameter("@Address2", request.Address2);
            var city = new SqlParameter("@City", request.City);
            var stateKey = new SqlParameter("@StateKey", request.StateKey);
            var postalCode = new SqlParameter("@PostalCode", request.PostalCode);
            var countryTimeZoneKey = new SqlParameter("@CountryTimeZoneKey", request.CountryTimeZoneKey);
            var contactType1 = new SqlParameter("@ContactType1", request.ContactType1);
            var countryISDCode1 = new SqlParameter("@CountryISDCode1", request.CountryISDCode1);
            var contactNumber1 = new SqlParameter("@ContactNumber1", request.ContactNumber1);
            var contactType2 = new SqlParameter("@ContactType2", request.ContactType2);
            var countryISDCode2 = new SqlParameter("@CountryISDCode2", request.CountryISDCode2);
            var contactNumber2 = new SqlParameter("@ContactNumber2", request.ContactNumber2);
            var organizationTypeKey = new SqlParameter("@OrganizationTypeKey", request.OrganizationTypeKey);
            var siteOrgTypeKey = new SqlParameter("@SiteOrgTypeKey", request.SiteOrgTypeKey);
            var jobTitle = new SqlParameter("@JobTitle", request.JobTitle);
            var departmentName = new SqlParameter("@DepartmentName", request.DepartmentName);
            var type = new SqlParameter("@Type", request.OptType);
            var indicator = new SqlParameter("@OutputStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };


            this.dbContext.Database.SqlQuery<UserDTO>("exec [User].[upSaveUser] @SalutationKey,@FirstName,@MiddleName,@LastName,@SuffixesKey," +
                "@UserName,@SendEmailToKey,@EmailTypeKey1,@Email1,@SendEmailToKey2,@Email2,@Password,@CountryKey,@Address1,@Address2,@City," +
                "@StateKey,@PostalCode,@CountryTimeZoneKey,@ContactType1,@CountryISDCode1,@ContactNumber1,@ContactType2,@CountryISDCode2," +
                "@ContactNumber2,@OrganizationTypeKey,@SiteOrgTypeKey,@JobTitle,@DepartmentName,@Type,@OutputStatus output",
                salutationKey, firstName, middleName, lastName, 
                suffixesKey, userName, sendEmailToKey,emailTypeKey1, 
                email1, sendEmailToKey2, email2, password, countryKey, 
                address1, address2, city, stateKey, postalCode, 
                countryTimeZoneKey, contactType1, countryISDCode1, 
                contactNumber1, contactType2, countryISDCode2, contactNumber2, 
                organizationTypeKey, siteOrgTypeKey, jobTitle, departmentName,
                type, indicator);

            return Convert.ToInt32(indicator);
        }
        public UserDTO ValidateUsers(UserRequestDTO request)
        {
            var userName = new SqlParameter("@UserName", (object)request.UserName ?? DBNull.Value);
            var password = new SqlParameter("@Password", (object)request.Password ?? DBNull.Value);
            var type = new SqlParameter("@Type", (object)request.OptType ?? DBNull.Value);

            return this.dbContext.Database.SqlQuery<UserDTO>("exec [User].[upGetLogin] @UserName,@Password,@Type ",
                    userName,
                    password,
                    type)
                .First();
        }
    }
}
