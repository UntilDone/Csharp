using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebPractice.Data;
var builder = WebApplication.CreateBuilder(args);

// Kestrel 포트 설정
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(8080); // 8080 포트 수신
});

// 📌 Razor Pages 등록
builder.Services.AddRazorPages();
// 📌 SQL Server 연결 설정 추가
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=WebPracticeDb;Trusted_Connection=True;TrustServerCertificate=True;"));

var app = builder.Build();

// 📌 정적 파일 사용 설정 (wwwroot 폴더 읽게)
app.UseStaticFiles();

// 📌 Razor Pages 사용 설정
app.MapRazorPages();

app.Run();
