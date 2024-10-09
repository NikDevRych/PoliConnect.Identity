using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(builder.Configuration["Swagger:Version"], new OpenApiInfo
    {
        Version = builder.Configuration["Swagger:Version"],
        Title = builder.Configuration["Swagger:Title"],
        Description = builder.Configuration["Swagger:Description"],
        Contact = new OpenApiContact
        {
            Name = builder.Configuration["Swagger:Contact:Name"],
            Email = builder.Configuration["Swagger:Contact:Email"],
            Url = new Uri(builder.Configuration["Swagger:Contact:Url"]!)
        },
        License = new OpenApiLicense
        {
            Name = builder.Configuration["Swagger:License:Name"],
            Url = new Uri(builder.Configuration["Swagger:License:Url"]!)
        }
    });
});

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/{builder.Configuration["Swagger:Version"]}/swagger.json", 
            $"{builder.Configuration["Swagger:Title"]} {builder.Configuration["Swagger:Version"]}");
    });
}

app.Run();
