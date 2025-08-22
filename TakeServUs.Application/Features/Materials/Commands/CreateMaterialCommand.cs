using AutoMapper;
using FluentValidation;
using MediatR;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Materials.Commands;

public class CreateMaterialCommand : IRequest<MaterialDto>
{
  public Guid CompanyId { get; set; }
  public required string Code { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required string Unit { get; set; }
  public required Money UnitCost { get; set; }
  public decimal QuantityInStock { get; set; }
}

public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, MaterialDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public CreateMaterialCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<MaterialDto> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
  {
    var material = new Domain.Entities.Material
    {
      CompanyId = request.CompanyId,
      Code = request.Code,
      Name = request.Name,
      Description = request.Description,
      Unit = request.Unit,
      UnitCost = request.UnitCost,
      QuantityInStock = request.QuantityInStock,
      IsActive = true
    };

    _context.Materials.Add(material);
    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<MaterialDto>(material);
  }
}

public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
{
  public CreateMaterialCommandValidator()
  {
    RuleFor(x => x.CompanyId).NotEmpty();
    RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
    RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
    RuleFor(x => x.Unit).NotEmpty();
    RuleFor(x => x.UnitCost).NotNull();
  }
}
