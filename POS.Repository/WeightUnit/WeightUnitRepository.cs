using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;

namespace POS.Repository
{
    public class WeightUnitRepository : GenericRepository<WeightUnit, POSDbContext>, IWeightUnitRepository
    {
        public WeightUnitRepository(IUnitOfWork<POSDbContext> uow)
 : base(uow)
        {
        }
    }
}
