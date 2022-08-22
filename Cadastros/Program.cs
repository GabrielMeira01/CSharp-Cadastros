using Cadastros.Repository;
using Cadastros.Service;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

//Repository
builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>();

//Service
builder.Services.AddSingleton<IClienteService, ClienteService>();
builder.Services.AddSingleton<IProdutoService, ProdutoService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
