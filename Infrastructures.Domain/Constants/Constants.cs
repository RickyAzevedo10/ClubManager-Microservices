namespace Infrastructures.Domain.Constants
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

           
              
            public readonly struct FacilityNotifications
            {
                public static readonly string FACILITY_DOES_NOT_EXIST = "FACILITY_DOES_NOT_EXIST";
                public static readonly string FACILITY_ALREADY_EXISTS = "FACILITY_ALREADY_EXISTS";
                public static readonly string ERROR_FACILITY_CREATED = "ERROR_FACILITY_CREATED";
                public static readonly string ERROR_FACILITY_UPDATED = "ERROR_FACILITY_UPDATED";
                public static readonly string ERROR_FACILITY_PERMISSIONS_CREATED = "ERROR_FACILITY_PERMISSIONS_CREATED";
                public static readonly string INVALID_FACILITY_CREDENTIALS = "INVALID_FACILITY_CREDENTIALS";
                public static readonly string INVALID_REFRESH_TOKEN = "INVALID_REFRESH_TOKEN";
                public static readonly string FACILITY_CREATED = "FACILITY_CREATED";
                public static readonly string FACILITY_UPDATED = "FACILITY_UPDATED";
                public static readonly string FACILITY_DELETED = "FACILITY_DELETED";
            }

            public readonly struct FacilityReservationNotifications
            {
                public static readonly string FACILITY_RESERVATION_DOES_NOT_EXIST = "FACILITY_RESERVATION_DOES_NOT_EXIST";
                public static readonly string FACILITY_RESERVATION_ALREADY_EXISTS = "FACILITY_RESERVATION_ALREADY_EXISTS";
                public static readonly string ERROR_FACILITY_RESERVATION_CREATED = "ERROR_FACILITY_RESERVATION_CREATED";
                public static readonly string ERROR_FACILITY_RESERVATION_UPDATED = "ERROR_FACILITY_RESERVATION_UPDATED";
                public static readonly string ERROR_FACILITY_RESERVATION_PERMISSIONS_CREATED = "ERROR_FACILITY_RESERVATION_PERMISSIONS_CREATED";
                public static readonly string INVALID_FACILITY_RESERVATION_CREDENTIALS = "INVALID_FACILITY_RESERVATION_CREDENTIALS";
                public static readonly string INVALID_REFRESH_TOKEN = "INVALID_REFRESH_TOKEN";
                public static readonly string FACILITY_RESERVATION_CREATED = "FACILITY_RESERVATION_CREATED";
                public static readonly string FACILITY_RESERVATION_UPDATED = "FACILITY_RESERVATION_UPDATED";
                public static readonly string FACILITY_RESERVATION_DELETED = "FACILITY_RESERVATION_DELETED";
            }

            public readonly struct MaintenanceRequestNotifications
            {
                public static readonly string MAINTENANCE_REQUEST_DOES_NOT_EXIST = "MAINTENANCE_REQUEST_DOES_NOT_EXIST";
                public static readonly string MAINTENANCE_REQUEST_ALREADY_EXISTS = "MAINTENANCE_REQUEST_ALREADY_EXISTS";
                public static readonly string ERROR_MAINTENANCE_REQUEST_CREATED = "ERROR_MAINTENANCE_REQUEST_CREATED";
                public static readonly string ERROR_MAINTENANCE_REQUEST_UPDATED = "ERROR_MAINTENANCE_REQUEST_UPDATED";
                public static readonly string ERROR_MAINTENANCE_REQUEST_PERMISSIONS_CREATED = "ERROR_MAINTENANCE_REQUEST_PERMISSIONS_CREATED";
                public static readonly string INVALID_MAINTENANCE_REQUEST_CREDENTIALS = "INVALID_MAINTENANCE_REQUEST_CREDENTIALS";
                public static readonly string INVALID_REFRESH_TOKEN = "INVALID_REFRESH_TOKEN";
                public static readonly string MAINTENANCE_REQUEST_CREATED = "MAINTENANCE_REQUEST_CREATED";
                public static readonly string MAINTENANCE_REQUEST_UPDATED = "MAINTENANCE_REQUEST_UPDATED";
                public static readonly string MAINTENANCE_REQUEST_DELETED = "MAINTENANCE_REQUEST_DELETED";
            }

            public readonly struct MaintenanceHistoryNotifications
            {
                public static readonly string MAINTENANCE_HISTORY_DATETIME_INVALID = "MAINTENANCE_HISTORY_DATETIME_INVALID";
            }

            

        }
    }
}
