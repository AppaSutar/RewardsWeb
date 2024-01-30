using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardsWeb.Models.Interface
{
    public interface IPointsRepo 
    {
        List<PointsResponse> Get(Guid subOrganisationId);
    }
}
