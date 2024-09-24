using POS.Common.GenericRepository;
using POS.Data;
using POS.Data.Resources;
using System.Threading.Tasks;

namespace POS.Repository
{
    public interface ISalesOrderRepository : IGenericRepository<SalesOrder>
    {
        Task<SalesOrderList> GetAllSalesOrders(SalesOrderResource salesOrderResource);
    }
}
