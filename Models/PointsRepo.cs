using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RewardsWeb.Models.Interface;

namespace RewardsWeb.Models
{
    public class PointsRepo : IPointsRepo
    {
        private readonly DbContext _context;
        public DatabaseContext Context => _context as DatabaseContext;

        public ILog Logger { get; }

        public PointsRepo(DbContext dbContext, ILog logger)
        {
            _context = dbContext;
            Logger = logger;
        }
        public List<PointsResponse> Get(Guid subOrganisationId)
        {
            var result = Context.Customers.SingleOrDefault();
            var points = new List<PointsResponse>();
            if (result.Any())
            {
                foreach (var item in result)
                {
                    var pointsResponse = new PointsResponse { Id = item.Id, CustomerName = item.Name, Month= item.Month, Amount= item.Amount};
                   
                    points.Add(pointsResponse);
                }
            }

            return points;
        }
    }
}
