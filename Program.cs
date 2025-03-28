using Voltix.SettingMicroservice.Protos;
using Voltix.Shared.Extensions;
using Voltix.UserMicroservice.Contexts;
using Voltix.UserMicroservice.GrpcServices;
using Voltix.UserMicroservice.Services;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<ApplicationContext>("voltix-user-microservice-database");

builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddGrpcClient<SettingProto.SettingProtoClient>(options => {
    options.Address = new Uri("https://voltix-setting-microservice");
});

var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGrpcService<UserGrpcService>();

app.UpdateDatabase<ApplicationContext>();

app.Run();