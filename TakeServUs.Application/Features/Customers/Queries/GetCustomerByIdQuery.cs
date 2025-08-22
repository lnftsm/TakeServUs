using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.Common.Exceptions;

using TakeServUs.Application.DTOs;

namespace TakeServUs.Application.Features.Customers.Queries;

public class GetCustomerByIdQuery : IRequest<CustomerDto>
{
  public Guid Id { get; set; }
}

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetCustomerByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
  {
    var customer = await _context.Customers
        .AsNoTracking()
        .Include(c => c.User) // Eager load the related User
        .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

    if (customer == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Customer), request.Id);
    }

    // AutoMapper will handle mapping from customer and user to the DTO
    return _mapper.Map<CustomerDto>(customer);
  }
}
