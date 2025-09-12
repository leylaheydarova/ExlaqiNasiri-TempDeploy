using ExlaqiNasiri.Application.Enums;
using System.Net;

namespace ExlaqiNasiri.Application.ResultPattern
{
    public class Error
    {
        public string Title { get; init; }
        public ErrorType Type { get; init; }
        public string Message { get; init; }
        public HttpStatusCode StatusCode { get; init; }

        public IEnumerable<KeyValuePair<string, string[]>> ValidationErrorMessages { get; init; }

        public static Error None = new(ErrorType.None, HttpStatusCode.OK, string.Empty);

        private Error(ErrorType type, HttpStatusCode statusCode, string message, IEnumerable<KeyValuePair<string, string[]>>? validationErrors = null)
        {
            Title = type.ToString();
            Type = type;
            StatusCode = statusCode;
            Message = message;
            ValidationErrorMessages = validationErrors ?? [];
        }

        public static Error GeneralCaseError(string message, ErrorType type, HttpStatusCode code)
        {
            return new(type, code, message); //burada lazim olan xususi mesajlar gonderilir.
        }

        public static Error AlreadyExistError(string message)
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, $"Sorry, {message} is already exists!");
        }

        public static Error ConfirmPasswordError()
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, "The new password and confirmation password do not match.");
        }
        public static Error EmailConfirmError()
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, "Sorry, your accout still was not confirmed. For log in to website, please confirm your email firstly.");
        }
        public static Error FieldRequireError(string message)
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, $"Sorry, {message} must be filled!");
        }

        public static Error InvalidDateError(string message)
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, $"{message} is out of range!");
        }

        public static Error InvalidInputError(string message)
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, $"Sorry, {message} is not correct");
        }

        public static Error GuidFormatError(string message)
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, $"Sorry, {message} format is not correct. It must be in {Guid.Empty}");
        }

        public static Error PermissionError()
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, "You are not permitted to act!");
        }

        public static Error PasswordConfirmError()
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, "Passwords do not match!");
        }

        public static Error PastTimeError(DateTime? inputTime)
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, $"The provided time {inputTime} has already passed.");
        }

        public static Error SameError(string message)
        {
            return new(ErrorType.BadRequest, HttpStatusCode.BadRequest, $"The new {message} cannot be the same as the current {message}.");
        }

        public static Error FailError(string message)
        {
            return new(ErrorType.FailError, HttpStatusCode.InternalServerError, $"{message} failed");
        }

        public static Error OperationFailError(string message)
        {
            return new(ErrorType.FailError, HttpStatusCode.InternalServerError, $"Sorry, the operation '{message}' failed!");
        }

        public static Error RegisterFailError(string message)
        {
            return new(ErrorType.FailError, HttpStatusCode.InternalServerError, "Register failed!");
        }

        public static Error SaveFailError(string message)
        {
            return new(ErrorType.FailError, HttpStatusCode.InternalServerError, $"Sorry, the save {message} failed!");
        }

        public static Error NotFoundError(string message)
        {
            return new(ErrorType.NotFound, HttpStatusCode.NotFound, $"Sorry, {message} is not found!");
        }

        public static Error UnauthorizedError()
        {
            return new(ErrorType.Unauthorized, HttpStatusCode.Unauthorized, "You're unauthorized, need to log in.");
        }

        public static Error UnexpectedError()
        {
            return new(ErrorType.Unexpected, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
        }

        public static Error UnexpectedError(string message)
        {
            return new(ErrorType.Unexpected, HttpStatusCode.InternalServerError, message);
        }

        public static Error UserTypeError(string message)
        {
            return new(ErrorType.NotFound, HttpStatusCode.NotFound, $"Invalid user type: {message}.");
        }
    }
}
