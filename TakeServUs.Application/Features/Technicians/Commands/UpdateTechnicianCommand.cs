using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;

namespace TakeServUs.Application.Features.Technicians.Commands;


public class UpdateTechnicianCommand : IRequest<TechnicianDto>
{
  public Guid Id { get; set; } // Technician Id
  public string? EmployeeCode { get; set; }
  public string? Specialization { get; set; }
  public bool IsAvailable { get; set; }
}

public class UpdateTechnicianCommandHandler : IRequestHandler<UpdateTechnicianCommand, TechnicianDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public UpdateTechnicianCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<TechnicianDto> Handle(UpdateTechnicianCommand request, CancellationToken cancellationToken)
  {
    var technician = await _context.Technicians
        .Include(t => t.User)
        .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

    if (technician == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Technician), request.Id);
    }

    technician.EmployeeCode = request.EmployeeCode;
    technician.Specialization = request.Specialization;
    technician.IsAvailable = request.IsAvailable;

    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<TechnicianDto>(technician);
  }
}
