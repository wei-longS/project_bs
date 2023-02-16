using Microsoft.Extensions.Configuration;
using SqlSugar;
using WebAPI.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//发送邮件
builder.Services.AddEmailSender(options =>
{
    options.Host = builder.Configuration["Email:Host"];
    options.Secret = builder.Configuration["Email:Secret"];
    options.FromAddr = builder.Configuration["Email:FromAddr"];
});

builder.Register();


//builder.Services.AddTransient<ISqlSugarClient>(p =>
//{
//    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
//    {
//        DbType = DbType.SqlServer,
//        ConnectionString = builder.Configuration.GetConnectionString("BaseDbSqlServer"),
//        IsAutoCloseConnection=true
//    });
//    return db;
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region 鉴权授权
app.UseAuthentication();
app.UseAuthorization();
#endregion

//使用跨域策略
app.UseCors("CorsPolicy");




app.MapControllers();

app.Run();
