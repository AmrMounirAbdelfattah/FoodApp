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
        UserIsNotFound,
        WrongOtp,
        UnableToSendEmail,
        //201-300 Recipe
        RecipeIsNotFound = 201,
        InvalidUserID,
        //301-400 RecipeImage
        RecipeImageIsNotFound = 301,
        //401- 500 Rating
        RecipeHasBeenRated = 401,
        EmptyCategoryName = 1000,
        FailedUpdateUser
    }
}
