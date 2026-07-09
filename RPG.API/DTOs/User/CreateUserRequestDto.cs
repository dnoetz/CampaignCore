namespace RPG.API.DTOs.User;

public record CreateUserRequestDto(string Username, string FirstName, string LastName, string Password, string Email, DateTime Birthday);