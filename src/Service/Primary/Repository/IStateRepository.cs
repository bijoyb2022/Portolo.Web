using Portolo.Framework.Data;
using Portolo.Primary.Data;
using Portolo.Primary.Request;
using Portolo.Primary.Response;
using System.Collections.Generic;

namespace Portolo.Primary.Repository
{
    public interface IStateRepository : IGenericRepository<States, PrimaryContext>
    {
        List<StateResponseDTO> GetState(StateRequestDTO request);
    }
}
