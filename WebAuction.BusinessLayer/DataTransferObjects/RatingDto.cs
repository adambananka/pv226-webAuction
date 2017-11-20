using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class RatingDto : DtoBase
    {
        public UserDto Seller { get; set; }

        public DateTime Time { get; set; }

        public int Stars { get; set; }

        public string Feedback { get; set; }

        protected bool Equals(RatingDto other)
        {
            if (!Id.Equals(Guid.Empty))
            {
                return Id == other.Id;
            }
            return Time == other.Time &&
                   Equals(Seller, other.Seller);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == GetType() &&
                   Equals((RatingDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ Time.GetHashCode();
                hashCode = (hashCode * 397) ^ (Seller?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{Seller.Name} has {Stars} stars";
        }
    }
}
