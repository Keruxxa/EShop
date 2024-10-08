namespace EShop.Application
{
    public static class Constants
    {
        #region User

        public const string USER_EMAIL_IS_NOT_UNIQUE = "This email is already in used";
        public const string USER_PHONE_IS_NOT_UNIQUE = "This phone is already in used";
        public const string USER_IS_ALREADY_AUTHENTICATED = "User is already signed in";
        public const string USER_WRONG_EMAIL = "User with such email does not exist";
        public const string USER_WRONG_PASSWORD = "Password is wrong";

        #endregion

        public const string SERVER_SIDE_ERROR = "An error occured on the server side";

        public const string ESHOP_SERVER_COOKIES = "eshop-server-cookies";
    }
}
