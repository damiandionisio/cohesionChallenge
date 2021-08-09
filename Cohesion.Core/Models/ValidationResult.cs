using System.Collections.Generic;

namespace Cohesion.Core
{
    public class ValidationResult
    {
        public static ValidationResult NoErrors => new ValidationResult();
        public string GeneralErrorMessage { get; set; }

        public bool HasErrors { get => Errors != null; }

        public IEnumerable<Error> Errors { get; }
    }
    public class Error
    {
        public IEnumerable<ValidationMessage> Messages { get; }
        public string PropertyName { get; }
    }

    public class ValidationMessage
    {
        public string Message { get; set; }

        public ValidationMessage(string message)
        {
            Message = message;
        }
    }
}
