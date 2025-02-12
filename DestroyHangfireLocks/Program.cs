using Npgsql;

namespace DestroyHangfireLocks;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: ./DestroyHangfireLocks <postgres_connection_string>");
            return;
        }

        var connString = args[0];
        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();
        await using var command = new NpgsqlCommand("TRUNCATE TABLE hangfire.lock;", conn);
        await command.ExecuteNonQueryAsync();
    }
}