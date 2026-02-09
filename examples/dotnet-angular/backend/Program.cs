using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.MapGet("/ops/health", () => Results.Ok(new { status = "ok" }))
  .AllowAnonymous();

app.MapHealthChecks("/ops/health/live", new HealthCheckOptions { Predicate = _ => false })
  .AllowAnonymous();
app.MapHealthChecks("/ops/health/ready", new HealthCheckOptions { Predicate = _ => true })
  .AllowAnonymous();

app.MapGet("/ops/info", () =>
{
  // Traceability evidence injected by pipeline + K8s Downward API (when deployed).
  var info = new
  {
    service = "kritis-example-api",
    gitSha = Environment.GetEnvironmentVariable("GIT_SHA"),
    buildId = Environment.GetEnvironmentVariable("BUILD_ID"),
    requirementId = Environment.GetEnvironmentVariable("REQUIREMENT_ID"),
    pod = new
    {
      name = Environment.GetEnvironmentVariable("POD_NAME"),
      uid = Environment.GetEnvironmentVariable("POD_UID"),
      node = Environment.GetEnvironmentVariable("NODE_NAME"),
      @namespace = Environment.GetEnvironmentVariable("POD_NAMESPACE")
    }
  };
  return Results.Ok(info);
}).AllowAnonymous();

app.Run();

