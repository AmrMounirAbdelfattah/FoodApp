namespace FoodApp.Domain.Enums
{
    public enum ErrorCode
    {
        NoError,

        UnKnown = 1,
        //101-200 User
        PasswordsDontMatch = 101,
        UserNameAlreadyExist,
        EmailAlreadyExist,
        WrongPasswordOrEmail,
        EmailIsNotFound,
        UserNameIsNotFound,
        WrongOtp,
        UnableToSendEmail,
        //201-300 Recipe
        RecipeIsNotFound = 201,
            InvalidUserID,
        //301-400 RecipeImage
        RecipeImageIsNotFound = 301,
    }
}
