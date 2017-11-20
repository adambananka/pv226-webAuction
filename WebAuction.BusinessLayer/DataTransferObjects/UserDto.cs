using System;
using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class UserDto : DtoBase
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        protected bool Equals(UserDto other)
        {
            if (!Id.Equals(Guid.Empty))
            {
                return Id == other.Id;
            }
            return Email == other.Email;
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
                   Equals((UserDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ Email.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{Name}, email:{Email}";
        }
    }
}
