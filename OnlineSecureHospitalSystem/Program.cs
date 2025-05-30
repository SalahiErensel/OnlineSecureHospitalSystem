using Blazored.LocalStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.Models;
using OnlineSecureHospitalSystem.Services.Admin;
using OnlineSecureHospitalSystem.Services.Authentication;
using OnlineSecureHospitalSystem.Services.Authorization;
using OnlineSecureHospitalSystem.Services.Profile;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AccessControlService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IAdminService, AdminService>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=online_secure_hospital.db"));


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();