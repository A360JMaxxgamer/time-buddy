namespace TimeBuddy.Service.Configurations;

public class PostgresConfiguration
{
    public string Host { get; set; } = "127.0.0.1";

    public string Database { get; set; } = "time_buddy";

    public string Username { get; set; } = "postgres";

    public string Password { get; set; } = "postgres";

    public string GetConnectionString() => $"Host={Host};Database={Database};Username={Username};Password={Password}";
    
    public static PostgresConfiguration ReadFromConfiguration(IConfiguration configuration)
    {
        var postgresSection = configuration.GetSection("Postgres");

        var result = new PostgresConfiguration();
        if (!postgresSection.Exists())
        {
            return result;
        }
        
        postgresSection.Bind(result);
        return result;
    }
}