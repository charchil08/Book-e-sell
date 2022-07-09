using System;

namespace BookStore.Models
{
    public class Messages
    {
        public static string GeneralExceptionCode = "general_exception_message";
        public static string GeneralExceptionMessage = "The server encountered an error and could not complete your request.";

        public static string InvalidCredentialsCode = "invalid_credentials";
        public static string InvalidCredentialsMessage = "Invalid username or password.";
    }
}
