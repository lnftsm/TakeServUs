using AutoMapper;
using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Materials.Commands;

public class UpdateMaterialCommand : IRequest<MaterialDto>
{
  public Guid Id { get; set; }
  public required string Code { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required Money UnitCost { get; set; }
  public decimal QuantityInStock { get; set; }
  public bool IsActive { get; set; }
}

public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, MaterialDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public UpdateMaterialCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<MaterialDto> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
  {
    var material = await _context.Materials.FindAsync(new object[] { request.Id }, cancellationToken);

    if (material == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Material), request.Id);
    }

    material.Code = request.Code;
    material.Name = request.Name;
    material.Description = request.Description;
    material.UnitCost = request.UnitCost;
    material.QuantityInStock = request.QuantityInStock;
    material.IsActive = request.IsActive;

    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<MaterialDto>(material);
  }
}
