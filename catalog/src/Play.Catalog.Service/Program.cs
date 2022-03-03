using Play.Catalog.Service.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = true; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCatalogServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
await app.RunAsync();