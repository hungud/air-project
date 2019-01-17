
namespace App.Models.NoticeBoard
{
    public class NoticeBoard : IWidget
    {
        public int SortOrder { get; set; }
        public string ClassName { get; set; }    
        public string FooterText { get; set; }    
        public string HeaderText { get; set; }    
        public ISubWidget SubWidget { get; set; }

    }
}