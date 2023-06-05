namespace TaskBoardApp.Data.Constants;

public static class DataConstants
{
    public static class UserConstants
    {
        internal const int UserFirstLastNameMaxLength = 15;
        internal const int UserNameMaxLength = 150;
    }

    public static class TaskConstants
    {
        internal const int MaxTaskTitle = 70;
        internal const int MinTaskTitle = 5;

        internal const int MaxTaskDescription = 1000;
        internal const int MinTaskDescription = 10;
    }

    public static class BoardConstants
    {
        internal const int MaxBoardName = 30;
        internal const int MinBoardName = 3;
    }
}
