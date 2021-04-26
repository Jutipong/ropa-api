namespace WebApi.Models
{
    public class AppSittingModel
    {
        public string SystemID { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public JwtModel Jwt { get; set; }
        public ConnectionStringModel ConnectionStrings { get; set; }
    }
    public class ConnectionStringModel
    {
        public string TCRBDB { get; set; }
        public string UserManagement { get; set; }
    }


    public class JwtModel
    {
        public string Secret { get; set; }

        public int ExpiryTimeInDay { get; set; }

    }
}
