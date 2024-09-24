using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;

namespace POS.Repository
{
    public class ReasonForExportRepository : GenericRepository<ReasonForExport, POSDbContext>, IReasonForExportRepository
    {
        public ReasonForExportRepository(IUnitOfWork<POSDbContext> uow)
 : base(uow)
        {
        }
    }
}
