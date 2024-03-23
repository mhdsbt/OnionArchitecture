using Domain.Common;
using Domain.Enum.AutoCallRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AutoCallRequest:BaseEntity
    {
        public string TrackId { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public AutoCallRequestStatus Status { get; set; }
        public AutoCallMessageType MessageType { get; set; }
        public Guid GroupId { get; set; }
        public long UserId { get; set; }
    }
}
