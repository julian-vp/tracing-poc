using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = Assembly.GetEntryAssembly();

builder.Services.AddOpenTelemetryTracing(b =>
{
    b
       .AddOtlpExporter(opt =>
       {
           opt.Endpoint = new Uri(Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT") ?? "");
           opt.Protocol = OtlpExportProtocol.Grpc;
       })
       .AddSource(assembly.GetName().Name)
       .SetResourceBuilder(
                           ResourceBuilder.CreateDefault()
                                          .AddService(serviceName: assembly.GetName().Name, serviceVersion: assembly.GetName().Version?.ToString()))
       .AddHttpClientInstrumentation()
       .AddAspNetCoreInstrumentation();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMetricServer();
app.UseHttpMetrics();

app.MapControllers();

app.Run();
