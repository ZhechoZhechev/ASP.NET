namespace Library.Data.Constants
{
    public static class DataConstants
    {
        public static class BookConstants
        {
            public const int BookTitleMinLength = 10;
            public const int BookTitleMaxLength = 50;

            public const int BookAuthorMinLength = 5;
            public const int BookAuthorMaxLength = 50;

            public const int BookDescriptiobMinLength = 5;
            public const int BookDescriptiobMaxLength = 5000;

            public const string BookRatingLowerBorder = "0.00";
            public const string BookRatingUpperBorder = "10.00";
        }

        public static class CategoryConstants 
        {
            public const int CategoryNameMinLength = 5;
            public const int CategoryNameMaxLength = 50;
        }
    }
}
