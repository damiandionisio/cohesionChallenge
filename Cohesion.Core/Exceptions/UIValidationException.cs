using System;

namespace Cohesion.Core
{
    public class UIValidationException : Exception
    {
        public ValidationResult ValidationResult { get; set; }

        public UIValidationException(ValidationResult validationResult)
        {
            this.ValidationResult = validationResult;
        }
    }
}
