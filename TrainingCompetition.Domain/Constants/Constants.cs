namespace TrainingCompetition.Domain.Constants
{
    public class Constants
    {
        public readonly struct NotificationKeys
        {
            public static readonly string DATABASE_COMMIT_ERROR = "DATABASE_COMMIT_ERROR";
            public static readonly string CANT_CONSULT = "CANT_CONSULT";
            public static readonly string CANT_DELETE = "CANT_DELETE";
            public static readonly string CANT_EDIT = "CANT_EDIT";
            public static readonly string CANT_CREATE = "CANT_CREATE";

           

            public readonly struct MatchNotifications
            {
                public static readonly string MATCH_DOES_NOT_EXIST = "MATCH_DOES_NOT_EXIST";
                public static readonly string MATCH_ALREADY_EXISTS = "MATCH_ALREADY_EXISTS";
                public static readonly string ERROR_MATCH_CREATED = "ERROR_MATCH_CREATED";
                public static readonly string ERROR_MATCH_UPDATED = "ERROR_MATCH_UPDATED";
                public static readonly string ERROR_MATCH_PERMISSIONS_CREATED = "ERROR_MATCH_PERMISSIONS_CREATED";
                public static readonly string INVALID_MATCH_CREDENTIALS = "INVALID_MATCH_CREDENTIALS";
                public static readonly string INVALID_REFRESH_TOKEN = "INVALID_REFRESH_TOKEN";
                public static readonly string MATCH_CREATED = "MATCH_CREATED";
                public static readonly string MATCH_UPDATED = "MATCH_UPDATED";
                public static readonly string MATCH_DELETED = "MATCH_DELETED";
            }
            public readonly struct MatchStatisticNotifications
            {
                public static readonly string MATCH_STATISTIC_DOES_NOT_EXIST = "MATCH_STATISTIC_DOES_NOT_EXIST";
                public static readonly string MATCH_STATISTIC_ALREADY_EXISTS = "MATCH_STATISTIC_ALREADY_EXISTS";
                public static readonly string ERROR_MATCH_STATISTIC_CREATED = "ERROR_MATCH_STATISTIC_CREATED";
                public static readonly string ERROR_MATCH_STATISTIC_UPDATED = "ERROR_MATCH_STATISTIC_UPDATED";
                public static readonly string ERROR_MATCH_STATISTIC_PERMISSIONS_CREATED = "ERROR_MATCH_STATISTIC_PERMISSIONS_CREATED";
                public static readonly string MATCH_STATISTIC_CREATED = "MATCH_STATISTIC_CREATED";
                public static readonly string MATCH_STATISTIC_UPDATED = "MATCH_STATISTIC_UPDATED";
                public static readonly string MATCH_STATISTIC_DELETED = "MATCH_STATISTIC_DELETED";
            }

            public readonly struct TrainingSessionNotifications
            {
                public static readonly string TRAINING_SESSION_DOES_NOT_EXIST = "TRAINING_SESSION_DOES_NOT_EXIST";
                public static readonly string TRAINING_SESSION_ALREADY_EXISTS = "TRAINING_SESSION_ALREADY_EXISTS";
                public static readonly string ERROR_TRAINING_SESSION_CREATED = "ERROR_TRAINING_SESSION_CREATED";
                public static readonly string ERROR_TRAINING_SESSION_UPDATED = "ERROR_TRAINING_SESSION_UPDATED";
                public static readonly string ERROR_TRAINING_SESSION_PERMISSIONS_CREATED = "ERROR_TRAINING_SESSION_PERMISSIONS_CREATED";
                public static readonly string TRAINING_SESSION_CREATED = "TRAINING_SESSION_CREATED";
                public static readonly string TRAINING_SESSION_UPDATED = "TRAINING_SESSION_UPDATED";
                public static readonly string TRAINING_SESSION_DELETED = "TRAINING_SESSION_DELETED";
            }

            public readonly struct TrainingAttendanceNotifications
            {
                public static readonly string TRAINING_ATTENDANCE_DOES_NOT_EXIST = "TRAINING_ATTENDANCE_DOES_NOT_EXIST";
                public static readonly string TRAINING_ATTENDANCE_ALREADY_EXISTS = "TRAINING_ATTENDANCE_ALREADY_EXISTS";
                public static readonly string ERROR_TRAINING_ATTENDANCE_CREATED = "ERROR_TRAINING_ATTENDANCE_CREATED";
                public static readonly string ERROR_TRAINING_ATTENDANCE_UPDATED = "ERROR_TRAINING_ATTENDANCE_UPDATED";
                public static readonly string ERROR_TRAINING_ATTENDANCE_PERMISSIONS_CREATED = "ERROR_TRAINING_ATTENDANCE_PERMISSIONS_CREATED";
                public static readonly string TRAINING_ATTENDANCE_CREATED = "TRAINING_ATTENDANCE_CREATED";
                public static readonly string TRAINING_ATTENDANCE_UPDATED = "TRAINING_ATTENDANCE_UPDATED";
                public static readonly string TRAINING_ATTENDANCE_DELETED = "TRAINING_ATTENDANCE_DELETED";
            }

            
        }
    }
}
