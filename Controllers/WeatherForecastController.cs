using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace testFirestore.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        await CreateUser();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    public async Task CreateUser()
    {
        try
        {
            var adminAuth = FirebaseApp.GetInstance("sendmeglobal");
            if (adminAuth == null)
            {
                adminAuth = FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile("/home/kenny/Desktop/devResources/testFirestore/Google-Creds/send-me-global-prod-4e6ceec97007.json"),
                }, "sendmeglobal");
            }

            _logger.LogInformation($"Creating new user kent2ckymaduka@gmail.com.....");

            var auth = FirebaseAuth.GetAuth(adminAuth);

            UserRecordArgs args = new()
            {
                Email = "kent2ckdymaduka@gmail.com",
                EmailVerified = false,
                Password = "user.Password",
                DisplayName = "Kennis Maduka",
                Disabled = false,
            };

            // var userRecord = await auth.CreateUserAsync(args).ConfigureAwait(false);
            // get the new users Uid 
            // Console.WriteLine($"{userRecord.Uid}");

            var database = FirestoreDb.Create("send-me-global-prod");
            var docRef = database.Collection("guests").Document("uYXsbzuaosobr78r4rje");
            await docRef.DeleteAsync();
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.ToString());
            throw new Exception(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.ToString());
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.ToString());
            throw new Exception();
        }
    }

    public FirebaseApp GetOrCreateFirebaseAdminAuthInstance()
    {
        FirebaseApp adminAuth = FirebaseApp.GetInstance("sendmeglobal");
        if (adminAuth == null)
        {
            adminAuth = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("/home/kenny/Desktop/devResources/testFirestore/Google-Creds/send-me-global-prod-4e6ceec97007.json"),
            }, "sendmeglobal");
        }
        return adminAuth;
    }
}
