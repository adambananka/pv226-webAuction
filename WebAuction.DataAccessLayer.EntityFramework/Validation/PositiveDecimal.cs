using System.ComponentModel.DataAnnotations;

namespace WebAuction.DataAccessLayer.EntityFramework.Validation
{
    public class PositiveDecimal : ValidationAttribute
    {
        private decimal Min { get; } = 0;
        private decimal Max { get; } = decimal.MaxValue;

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            var number = (decimal) value;
            return number >= Min && number <= Max;
        }
    }
}