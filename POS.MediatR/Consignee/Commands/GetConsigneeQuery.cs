using POS.Data.Dto;
using POS.Helper;
using MediatR;
using System;

namespace POS.MediatR.CommandAndQuery
{
    public class GetConsigneeQuery : IRequest<ServiceResponse<ConsigneeDto>>
    {
        public Guid Id { get; set; }
    }
}
