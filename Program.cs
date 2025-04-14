using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTasks01.Components;
using MyTasks01.Helper;
using MyTasks01.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<MyTasksDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Transient);

builder.Services.AddTransient<BaseService<TaskService>>();
builder.Services.AddTransient<BaseService<SubTaskService>>();
//builder.Services.AddScoped<BaseService<SprintService>>();
builder.Services.AddRazorComponents();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<NotifierBackgroundService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddTransient<TaskService>();
builder.Services.AddTransient<SubTaskService>();
builder.Services.AddTransient<ListService>();
//builder.Services.AddScoped<SprintService>();
builder.Services.AddRadzenComponents();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
