namespace FreeERP.Utils
{
    public class DB
    {
        public static string GetDBConfig()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Model", "DBConfig");
        }
    }
}
