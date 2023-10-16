namespace MyGym.Service.DTOs
{
    public class RefreshToken
    {
        public string RToken { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Expires { get; set; }
    }
}
