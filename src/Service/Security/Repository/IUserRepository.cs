using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portolo.Framework.Data;
using Portolo.Security.Data;
using Portolo.Security.Request;
using Portolo.Security.Response;

namespace Portolo.Security.Repository
{
    public interface IUserRepository : IGenericRepository<User, SecurityContext>
    {
        UserDTO ValidateUsers(UserRequestDTO request);
        int SaveUser(UserRequestDTO request);
    }
}
