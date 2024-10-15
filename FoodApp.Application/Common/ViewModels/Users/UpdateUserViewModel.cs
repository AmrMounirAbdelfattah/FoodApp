namespace FoodApp.Application.Common.ViewModels.Users
{
    public record UpdateUserViewModel(string UserName, string Password, string ConfirmPassword,
       string Email, string Phone, string Country);
}
