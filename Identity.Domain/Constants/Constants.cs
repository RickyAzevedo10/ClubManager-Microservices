namespace Identity.Domain.Constants
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

            public readonly struct InstitutionNotifications
            {
                public static readonly string INSTITUTION_NOT_FOUND = "INSTITUTION_NOT_FOUND";
                public static readonly string INSTITUTION_ALREADY_EXITS = "INSTITUTION_ALREADY_EXITS";
                public static readonly string ERROR_INSTITUTION_CREATED = "ERROR_INSTITUTION_CREATED";
                public static readonly string INSTITUTION_CREATED = "INSTITUTION_CREATED";
                public static readonly string INSTITUTION_UPDATED = "INSTITUTION_UPDATED";
                public static readonly string INSTITUTION_DELETED = "INSTITUTION_DELETED";
            }

            public readonly struct UserNotifications
            {
                public static readonly string USER_DONT_EXITS = "USER_DONT_EXITS";
                public static readonly string USER_ALREADY_EXITS = "USER_ALREADY_EXITS";
                public static readonly string ERROR_USER_CREATED = "ERROR_USER_CREATED";
                public static readonly string ERROR_USER_UPDATED = "ERROR_USER_UPDATED";
                public static readonly string ERROR_USER_PERMISSIONS_CREATED = "ERROR_USER_PERMISSIONS_CREATED";
                public static readonly string INVALID_USER_CREDENTIALS = "INVALID_USER_CREDENTIALS";
                public static readonly string INVALID_REFRESH_TOKEN = "INVALID_REFRESH_TOKEN";
                public static readonly string USER_CREATED = "USER_CREATED";
                public static readonly string USER_UPDATED = "USER_UPDATED";
                public static readonly string USER_DELETED = "USER_DELETED";
            }

           
        }
    }
}
