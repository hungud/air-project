
namespace App.Models.NoticeBoard
{
    public interface ISubWidget
    {
        string Topic { get; set; }
        string Description { get; set; }
        string Url { get; set; }
        string Link { get; set; }
        string UrlName { get; set; }
    }
}
