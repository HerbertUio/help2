namespace Help.Desk.Domain.Dtos.UserDtos;

public class ResponseLoginDto
{
    public UserDto User { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
}