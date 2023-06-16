using WebApi.Common.Extensions.Configuration;
using WorkingGood.Log;

var builder = WebApplication.CreateBuilder(args);
var logger = WgLogger.CreateInstance(builder.Configuration);
    try
    {
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddConfiguration(builder.Configuration);
    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
}