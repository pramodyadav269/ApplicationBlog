using System.ComponentModel.DataAnnotations;

namespace ApplicationBlog.Model
{
    public class FriendRequest
    {
        [Key]
        public long RequestId { get; set; }
        public long? RequestedBy { get; set; }
        public long? RequestedTo { get; set; }
        public bool IsRequestAccepted { get; set; }
        public bool IsRequestRejected { get; set; }
        public bool IsUnfriend { get; set; }        
        public DateTime? RequestedOn { get; set; }
        public DateTime? AcceptedOn { get; set; }
        public DateTime? RejectedOn { get; set; }
        public DateTime? UnfriendOn { get; set; }
        public bool IsActive { get; set; }
    }
}
