namespace ApplicationBlog.Model
{
    public class GetProfileDetails
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public char Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string ProfilePic { get; set; }
        public string BackgroundPic { get; set; }
        public string ProfileStatus { get; set; }
        public DateTime? RegisteredOn { get; set; }
        public string Countryname { get; set; }
    }
}
