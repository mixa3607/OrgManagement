using System.ComponentModel.DataAnnotations;

namespace WebApiSharedParts.Attributes
{
    public class NotEqualAttribute : CompareAttribute
    {
        public NotEqualAttribute(string otherProperty) : base(otherProperty) { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var res = base.IsValid(value, validationContext);
            return res == ValidationResult.Success ? new ValidationResult("Properties cant be equal") : ValidationResult.Success;
        }
    }
}