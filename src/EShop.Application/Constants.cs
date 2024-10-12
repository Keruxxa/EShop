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

        #region Basket

        public const string PRODUCT_DOES_NOT_EXIST_IN_BASKET = "Product does not exist in the basket";

        #endregion

        #region Order

        public const string ORDER_ITEM_DOES_NOT_EXIST = "Some products in the order does not exist";

        #endregion

        #region Basket



        #endregion

        #region Favorite

        public const string PRODUCT_DOES_NOT_EXIST_IN_FAVORITE = "Product does not exist in favorite";
        public const string PRODUCT_ALLREADY_EXIST_IN_FAVORITE = "Product allready exist in favorite";

        #endregion

        #region Server

        public const string SERVER_SIDE_ERROR = "An error occured on the server side";

        public const string ESHOP_SERVER_COOKIES = "eshop-server-cookies";

        #endregion
    }
}
