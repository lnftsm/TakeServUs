using TakeServUs.Application;
using TakeServUs.Persistence;

var builder = WebApplication.CreateBuilder(args);


// Add services from other layers
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
  builder.Configuration.AddUserSecrets<Program>();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
