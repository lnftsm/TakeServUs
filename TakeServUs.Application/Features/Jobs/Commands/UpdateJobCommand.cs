using AutoMapper;
using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Jobs.Commands;

public class UpdateJobCommand : IRequest<JobDto>
{
  public Guid Id { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public JobPriority Priority { get; set; }
  public required Address ServiceAddress { get; set; }
  public DateTime? ScheduledStartTime { get; set; }
  public DateTime? ScheduledEndTime { get; set; }
}

public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, JobDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public UpdateJobCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<JobDto> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
  {
    var job = await _context.Jobs.FindAsync(new object[] { request.Id }, cancellationToken);

    if (job == null)
    {
      throw new NotFoundException(nameof(Job), request.Id);
    }

    job.Title = request.Title;
    job.Description = request.Description;
    job.Priority = request.Priority;
    job.ServiceAddress = request.ServiceAddress;
    job.ScheduledStartTime = request.ScheduledStartTime;
    job.ScheduledEndTime = request.ScheduledEndTime;

    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<JobDto>(job);
  }
}
