using AutoMapper;
using FluentValidation;
using MediatR;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Jobs.Commands;

public class CreateJobCommand : IRequest<JobDto>
{
  public Guid CustomerId { get; set; }
  public Guid CompanyId { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public JobPriority Priority { get; set; } = JobPriority.Normal;
  public required Address ServiceAddress { get; set; }
  public DateTime? ScheduledStartTime { get; set; }
  public DateTime? ScheduledEndTime { get; set; }
  public int? EstimatedDurationMinutes { get; set; }
}

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, JobDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public CreateJobCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<JobDto> Handle(CreateJobCommand request, CancellationToken cancellationToken)
  {
    // TODO: Check if CustomerId and CompanyId are valid.

    var job = new Job
    {
      CompanyId = request.CompanyId,
      CustomerId = request.CustomerId,
      Title = request.Title,
      Description = request.Description,
      Priority = request.Priority,
      ServiceAddress = request.ServiceAddress,
      ScheduledStartTime = request.ScheduledStartTime,
      ScheduledEndTime = request.ScheduledEndTime,
      EstimatedDurationMinutes = request.EstimatedDurationMinutes,
      JobNumber = $"JOB-{DateTime.UtcNow.Ticks}", // This should be a more robust number generation service.
      CurrentStatus = JobStatusType.Created
    };

    _context.Jobs.Add(job);
    await _context.SaveChangesAsync(cancellationToken);

    // In a real app, you might want to create a Notification here.

    return _mapper.Map<JobDto>(job);
  }
}

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
  public CreateJobCommandValidator()
  {
    RuleFor(v => v.Title).NotEmpty().MaximumLength(200);
    RuleFor(v => v.CustomerId).NotEmpty();
    RuleFor(v => v.CompanyId).NotEmpty();
    RuleFor(v => v.ServiceAddress).NotNull();
  }
}
