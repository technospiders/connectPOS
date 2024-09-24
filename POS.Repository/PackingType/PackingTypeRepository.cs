using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;

namespace POS.Repository
{
    public class PackingTypeRepository : GenericRepository<PackingType, POSDbContext>, IPackingTypeRepository
    {
        public PackingTypeRepository(IUnitOfWork<POSDbContext> uow)
        : base(uow)
        {
        }
    }
}
