namespace FoodApp.Application.Common.DTOs
{
    public record SendEmailDto(
        string To,
        string Subject,
        string Body,
        string? CC = null);
}
