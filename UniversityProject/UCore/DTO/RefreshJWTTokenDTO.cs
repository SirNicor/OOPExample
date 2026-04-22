namespace UCore;

public class RefreshJWTTokenDTO
{
        public long Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool RevokedAt { get; set; }
        public long IdAuthorizationTable { get; set; }
}