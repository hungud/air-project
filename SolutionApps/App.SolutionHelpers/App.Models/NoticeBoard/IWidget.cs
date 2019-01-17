
namespace App.Models.NoticeBoard
{
  public  interface IWidget
    {
        int SortOrder { get; set; }
        string ClassName { get; set; }
        string FooterText { get; set; }
        string HeaderText { get; set; }        
        ISubWidget SubWidget { get; set; }
    }
}
