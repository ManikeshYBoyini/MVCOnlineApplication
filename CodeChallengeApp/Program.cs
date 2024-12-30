using CodeChallengeApp.Interface;
using CodeChallengeApp.Middlewares;
using CodeChallengeApp.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddTransient<IProductResource, ProductResource>();
builder.Services.AddScoped<ICategoryResource, CategoryResource>();
builder.Services.AddScoped<ISubCategoryResource, SubCategoryResource>();
builder.Services.AddScoped<IProductFiles, ProductFiles>();
builder.Services.AddScoped<CommonExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseGlobalExceptionMiddleware();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
