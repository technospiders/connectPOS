using POS.Data.Dto;
using POS.Helper;
using MediatR;
using System;

namespace POS.MediatR.CommandAndQuery
{
    public class RemovedConsigneeImageCommand: IRequest<ServiceResponse<ConsigneeDto>>
    {
        public Guid Id { get; set; }
    }
}
