using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Customers.Commands;

public class UpdateCustomerCommand : IRequest<CustomerDto>
{
  public Guid Id { get; set; } // Customer Id
  public required Address Address { get; set; }
  public string? CompanyName { get; set; }
  public string? Notes { get; set; }
  public bool IsVip { get; set; }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public UpdateCustomerCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
  {
    var customer = await _context.Customers
        .Include(c => c.User) // Include user to map full details
        .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

    if (customer == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Customer), request.Id);
    }

    customer.Address = request.Address;
    customer.CompanyName = request.CompanyName;
    customer.Notes = request.Notes;
    customer.IsVip = request.IsVip;

    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<CustomerDto>(customer);
  }
}
