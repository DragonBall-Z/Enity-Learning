namespace DemoApi.Models
{
    public class Photo
    {
        public Guid Id { get; set; }

        public string? Titile { get; set; }
        public string? PixelRatio { get; set; }
        public string? PhotUrl { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
