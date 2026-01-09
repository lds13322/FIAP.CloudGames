namespace WebApi.Dtos
{
    public class UserRegisterDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } = "User";
    }

    public class UserLoginDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
