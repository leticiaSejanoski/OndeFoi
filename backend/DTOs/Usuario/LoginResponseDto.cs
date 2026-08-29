namespace OndeFoi.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public UsuarioResponseDto Usuario { get; set; } = null!;
    }
}