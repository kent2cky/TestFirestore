using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using SignalRChat.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chatHub");

app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("/home/kenny/Desktop/devResources/testFirestore/Google-Creds/send-me-global-prod-4e6ceec97007.json"),
});

app.Run();
