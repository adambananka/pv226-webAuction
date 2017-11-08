using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class UserCompleteDto : DtoBase
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsAdmin { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSal { get; set; }
    }
}
