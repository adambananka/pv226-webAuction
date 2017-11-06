using WebAuction.BusinessLayer.DataTransferObjects.Common;

namespace WebAuction.BusinessLayer.DataTransferObjects
{
    public class UserDto : DtoBase
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
