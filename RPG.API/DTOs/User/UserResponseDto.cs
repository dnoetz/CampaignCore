namespace RPG.API.DTOs.User;

public record UserResponseDto(int Id, string Username, string FirstName, string LastName, string Email, DateTime Birthday);