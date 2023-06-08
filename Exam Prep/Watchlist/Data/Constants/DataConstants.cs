using System.Security.Policy;

namespace Watchlist.Data.Constants;

public static class DataConstants
{
    public static class GenreConstants 
    {
        public const int GenreNameMaxLength = 50;
        public const int GenreNameMinLength = 5;
    }

    public static class MovieConstants
    {
        public const int MovieTitleMaxLength = 50;
        public const int MovieTitleMinLength = 10;
        public const int MovieDirectorMaxLength = 50;
        public const int MovieDirectorMinLength = 5;
        public const string MovieRatingLowerBorder = "0.0";
        public const string MovieRatingUpperBorder = "10.0";
    }

    public static class UserConstants 
    {
        public const int UserUserNameMaxLength = 20;
        public const int UserUserNameMinLength = 5;
        public const int UserEmailMaxLength = 60;
        public const int UserEmailMinLength = 10;
        public const int UserPasswordMaxLength = 20;
        public const int UserPasswordMinLength = 5;
    }
}
