var builder = WebApplication.CreateBuilder(args);
// Register Api Layer
builder.Services.AddApiLayer();
// Register Application Layer
builder.Services.AddApplicationLayer(builder.Configuration);
// register cors
builder.Services.AddCors();
// compression for HTTPS requests
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>(); // Use Gzip compression
});
// build app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseAuthorization();
// custom middlewares
app.UseMiddleware<PerformanceMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
//------------------
//cors
app.UseCors(cors => cors.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
// compression for HTTPS requests
app.UseResponseCompression();
app.MapControllers();
app.ApplyMigrations();
await app.RunAsync();
