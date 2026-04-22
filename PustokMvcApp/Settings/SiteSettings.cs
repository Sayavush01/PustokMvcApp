namespace MVC_WEB_APP.Settings
{
    public class SiteSettings
    {
        public string ContactAddress { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string WorkingHours { get; set; }
        public SocialMediaSettings SocialMedia { get; set; }
    }

    public class SocialMediaSettings
    {
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string YoutubeUrl { get; set; }
    }
}
