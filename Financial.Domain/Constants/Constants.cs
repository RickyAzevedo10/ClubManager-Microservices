namespace Financial.Domain.Constants
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

            public readonly struct ExpenseNotifications
            {
                public static readonly string EXPENSE_DOES_NOT_EXIST = "EXPENSE_DOES_NOT_EXIST";
                public static readonly string EXPENSE_ALREADY_EXISTS = "EXPENSE_ALREADY_EXISTS";
                public static readonly string ERROR_EXPENSE_CREATED = "ERROR_EXPENSE_CREATED";
                public static readonly string ERROR_EXPENSE_UPDATED = "ERROR_EXPENSE_UPDATED";
                public static readonly string ERROR_EXPENSE_PERMISSIONS_CREATED = "ERROR_EXPENSE_PERMISSIONS_CREATED";
                public static readonly string EXPENSE_CREATED = "EXPENSE_CREATED";
                public static readonly string EXPENSE_UPDATED = "EXPENSE_UPDATED";
                public static readonly string EXPENSE_DELETED = "EXPENSE_DELETED";
            }

            public readonly struct EntityNotifications
            {
                public static readonly string ENTITY_DOES_NOT_EXIST = "ENTITY_DOES_NOT_EXIST";
                public static readonly string ENTITY_ALREADY_EXISTS = "ENTITY_ALREADY_EXISTS";
                public static readonly string ERROR_ENTITY_CREATED = "ERROR_ENTITY_CREATED";
                public static readonly string ERROR_ENTITY_UPDATED = "ERROR_ENTITY_UPDATED";
                public static readonly string ERROR_ENTITY_PERMISSIONS_CREATED = "ERROR_ENTITY_PERMISSIONS_CREATED";
                public static readonly string ENTITY_CREATED = "ENTITY_CREATED";
                public static readonly string ENTITY_UPDATED = "ENTITY_UPDATED";
                public static readonly string ENTITY_DELETED = "ENTITY_DELETED";
            }

            public readonly struct RevenueNotifications
            {
                public static readonly string REVENUE_DOES_NOT_EXIST = "REVENUE_DOES_NOT_EXIST";
                public static readonly string REVENUE_ALREADY_EXISTS = "REVENUE_ALREADY_EXISTS";
                public static readonly string ERROR_REVENUE_CREATED = "ERROR_REVENUE_CREATED";
                public static readonly string ERROR_REVENUE_UPDATED = "ERROR_REVENUE_UPDATED";
                public static readonly string ERROR_REVENUE_PERMISSIONS_CREATED = "ERROR_REVENUE_PERMISSIONS_CREATED";
                public static readonly string REVENUE_CREATED = "REVENUE_CREATED";
                public static readonly string REVENUE_UPDATED = "REVENUE_UPDATED";
                public static readonly string REVENUE_DELETED = "REVENUE_DELETED";
            }

        }
    }
}
