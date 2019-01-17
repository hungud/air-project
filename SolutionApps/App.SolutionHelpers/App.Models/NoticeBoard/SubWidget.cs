
namespace App.Models.NoticeBoard
{
    public class SubWidget : ISubWidget
    {
        public string Topic { get; set; }        
        public string Description { get; set; }
        public string Link { get; set; }
        public string Url { get; set; }
        public string UrlName { get; set; }
    }
}