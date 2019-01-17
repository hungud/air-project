using System.Collections.Generic;
using System.Linq;
namespace App.Models.NoticeBoard
{
    public class NoticeBoardModals
    {
        //
        // GET: /Widget/
        public List<IWidget> NoticeBoardWidgetContainer()
        {
            return GetWidgetData();
        }
        public List<IWidget> AllAreasWidgetContainer(List<string> applicationallAreasnames)
        {
            return GetAllAreasWidget(applicationallAreasnames);
        }

        public List<IWidget> GetWidgetData()
        {
            var noticeboardWidget = new List<IWidget>
            {
                new NoticeBoard()
                {
                    SortOrder = 1, ClassName = "widgethigh", HeaderText = "Notice Board", FooterText = "" ,
                    SubWidget = new SubWidget { Topic = "Office Time", Description = "Office time will be change next month",Link="http://localhost:49334/GIS/DashBoard/index.html",Url="http://localhost:49334/GIS/DashBoard/index.html",UrlName="GIS DashBoard" },
                },
                new NoticeBoard()
                {
                    SortOrder = 4, ClassName = "widgetmedium", HeaderText = "Notice Board", FooterText = "" ,
                    SubWidget = new SubWidget { Topic = "Salary", Description = "Salary is dusburst plese check your account",Link="http://localhost:49334/GIS/DashBoard/index.html",Url="http://localhost:49334/GIS/DashBoard/index.html",UrlName="GIS DashBoard" },
                },
                new NoticeBoard()
                {
                    SortOrder = 8, ClassName = "widgetlow", HeaderText = "Notice Board", FooterText = "" ,
                    SubWidget = new SubWidget { Topic = "About Lunch", Description = "We need the feed back from you about the luch",Link="http://localhost:49334/GIS/DashBoard/index.html",Url="http://localhost:49334/GIS/DashBoard/index.html",UrlName="GIS DashBoard" },
                },              
                new NoticeBoard()
                {
                     SortOrder = 2, ClassName = "widgethigh", HeaderText = "Notice Board", FooterText = "" ,
                     SubWidget = new SubWidget { Topic = "Emergency Meeting", Description = "All  the managers please come to our meeting room by 11.30",Link="http://localhost:49334/GIS/DashBoard/index.html",Url="http://localhost:49334/GIS/DashBoard/index.html",UrlName="GIS DashBoard" },
                },
                 new NoticeBoard()
                {
                    SortOrder = 5, ClassName = "widgetmedium", HeaderText = "Notice Board", FooterText = "" ,
                    SubWidget = new SubWidget { Topic = "Office Meetting", Description = "Todays meeting is cancel" ,Link="http://localhost:49334/GIS/DashBoard/index.html",Url="http://localhost:49334/GIS/DashBoard/index.html",UrlName="GIS DashBoard" },
                },
                new NoticeBoard()
                {
                     SortOrder = 7, ClassName = "widgetlow", HeaderText = "Notice Board", FooterText = "" ,
                     SubWidget = new SubWidget { Topic = "HoliDay Notice", Description = "We shifted our holiday leave for 1 day",Link="http://localhost:49334/GIS/DashBoard/index.html",Url="http://localhost:49334/GIS/DashBoard/index.html",UrlName="GIS DashBoard" },
                },              
                 
                new NoticeBoard()
                {
                     SortOrder = 3, ClassName = "widgethigh", HeaderText = "Notice Board", FooterText = "" ,
                     SubWidget = new SubWidget { Topic = "Vacancy", Description = "We need a sound asp.net developer with C#",Link="http://localhost:49334/GIS/DashBoard/index.html",Url="http://localhost:49334/GIS/DashBoard/index.html",UrlName="GIS DashBoard" },
                },
                new NoticeBoard()
                {
                    SortOrder = 6, ClassName = "widgetmedium", HeaderText = "Notice Board", FooterText = "" ,
                    SubWidget = new SubWidget { Topic = "HR Notice", Description = "Visiting cards proof send to you mail please check" ,Link="http://localhost:49334/GIS/DashBoard/index.html",Url="http://localhost:49334/GIS/DashBoard/index.html",UrlName="GIS DashBoard" },
                },
            };
            return noticeboardWidget.OrderBy(p => p.SortOrder).ToList();
        }

        public List<IWidget> GetAllAreasWidget(List<string> applicationallAreasnames)
        {
            //@Html.ActionLink(MyAreas, "Index", new { area = MyAreas, controller = MyAreas }, new { @class = "bz_common_actions" })
            List<IWidget> noticeboardWidget = new List<IWidget>();
            List<string> ApplicationAllAreasNames = applicationallAreasnames;
            if (ApplicationAllAreasNames != null)
            {
                foreach (var MyAreas in ApplicationAllAreasNames)
                {
                    noticeboardWidget.Add(new NoticeBoard(){
                         SortOrder = 1,
                         ClassName = "widgethigh",
                         HeaderText = MyAreas,
                         FooterText = MyAreas,
                         SubWidget = new SubWidget { Topic = MyAreas, Description = MyAreas+" Area Data", Link =MyAreas, Url = MyAreas, UrlName = MyAreas }
                    });
                }
            }
            return noticeboardWidget.OrderBy(p => p.SortOrder).ToList();
        }
    }

    public class ApplicationModals
    {
        public ApplicationModals()
        {

        }
        public List<IWidget> NoticeBoardWidgetContainer()
        {
            return new NoticeBoardModals().NoticeBoardWidgetContainer();
        }
        public List<IWidget> AllAreasWidgetContainer(List<string> applicationallAreasnames)
        {
            return new NoticeBoardModals().AllAreasWidgetContainer(applicationallAreasnames);
        }
    }
}
