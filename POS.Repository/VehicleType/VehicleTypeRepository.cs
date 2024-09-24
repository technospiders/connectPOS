using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;

namespace POS.Repository
{
    public class VehicleTypeRepository : GenericRepository<VehicleType, POSDbContext>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(IUnitOfWork<POSDbContext> uow)
 : base(uow)
        {
        }
    }
}
