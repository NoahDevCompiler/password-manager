namespace password_manager_API.Models
{
    public class TotpCredentialsDto
    {
        public string Secret { get; set; }
        public string IV { get; set; }
        public string code { get; set; }
    }
}
