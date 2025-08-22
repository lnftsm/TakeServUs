using AutoMapper;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Common.Mappings;

// Configures AutoMapper mappings.
public class MappingProfile : Profile
{
  public MappingProfile()
  {
    // Maps from the Job entity to the JobDto
    CreateMap<Job, JobDto>();
    // Add other mappings here...
  }
}
