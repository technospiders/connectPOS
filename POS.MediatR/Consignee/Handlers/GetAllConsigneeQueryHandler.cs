using POS.MediatR.CommandAndQuery;
using POS.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Handlers
{
    public class GetAllConsigneeQueryHandler : IRequestHandler<GetAllConsigneeQuery, ConsigneeList>
    {
        private readonly IConsigneeRepository _consigneeRepository;
        public GetAllConsigneeQueryHandler(IConsigneeRepository consigneeRepository)
        {
            _consigneeRepository = consigneeRepository;
        }
        public async Task<ConsigneeList> Handle(GetAllConsigneeQuery request, CancellationToken cancellationToken)
        {
            return await _consigneeRepository.GetConsignees(request.ConsigneeResource);
        }
    }
}
