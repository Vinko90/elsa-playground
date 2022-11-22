using Elsa;
using Elsa.Activities.UserTask.Extensions;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.Sqlite;
using Elsa.WorkflowTesting.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    
    // Add services to the container
    services.AddRazorPages();
    
    // Elsa Server settings.
    var elsaSection = builder.Configuration.GetSection("Elsa");

    services.AddElsa(options => options
        .UseEntityFrameworkPersistence(ef => ef.UseSqlite())
        .AddConsoleActivities()
        .AddHttpActivities(elsaSection.GetSection("Server").Bind)
        .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
        .AddQuartzTemporalActivities()
        .AddJavaScriptActivities()
        .AddUserTaskActivities()
        .AddActivitiesFrom<Program>()
        .AddFeatures(new[] { typeof(Program) }, builder.Configuration)
        .WithContainerName(elsaSection.GetSection("Server:ContainerName").Get<string>())
    );
    
    services.AddElsaSwagger();
    services.AddElsaApiEndpoints();
    services.AddWorkflowTestingServices();

    // Allow arbitrary client browser apps to access the API.
    // In a production environment, make sure to allow only origins you trust.
    services.AddCors(cors => 
        cors.AddDefaultPolicy(policy => 
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .WithExposedHeaders("Content-Disposition")
            )
        );
}


var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => 
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Elsa"));
    }
    
    app.UseCors();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.UseHttpActivities();
    app.UseEndpoints(endpoints =>
    {
        // Maps a SignalR hub for the designer to connect to when testing workflows.
        endpoints.MapWorkflowTestHub();
        // Elsa Server uses ASP.NET Core Controllers.
        endpoints.MapControllers();

        endpoints.MapFallbackToPage("/_Host");
    });
}

app.Run();