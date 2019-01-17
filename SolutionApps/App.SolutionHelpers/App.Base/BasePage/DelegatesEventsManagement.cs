namespace App.Base
{
    namespace DelegatesEventsManagement
    {
        using System;
        using System.Web.UI.WebControls;

        #region Control EventsDelegate



        public delegate void TreeNodeHandler(object sender, NodeEventArgs e);
        public class NodeEventArgs : EventArgs
        {
            private TreeNode tnode;
            public TreeNode TNode
            {
                get
                {
                    return tnode;
                }
                set
                {
                    tnode = value;
                }
            }
            public NodeEventArgs(TreeNode TSNode)
            {
                this.TNode = TSNode;
            }

        }





        public delegate void LinkButtonHandler(object sender, LinkButtonEventArgs e);
        public class LinkButtonEventArgs : EventArgs
        {
            private LinkButton lnkbtn;
            public LinkButton LnkBtn
            {
                get
                {
                    return lnkbtn;
                }
                set
                {
                    lnkbtn = value;
                }
            }
            public LinkButtonEventArgs(LinkButton LnkClick)
            {
                this.LnkBtn = LnkClick;
            }
        }




        public delegate void ImageButtonHandler(object sender, ImageButtonEventArgs e);
        public class ImageButtonEventArgs : EventArgs
        {
            private ImageButton imgbtn;
            public ImageButton IMGBtn
            {
                get
                {
                    return imgbtn;
                }
                set
                {
                    imgbtn = value;
                }
            }
            public ImageButtonEventArgs(ImageButton ImgClick)
            {
                this.IMGBtn = ImgClick;
            }
        }



        public delegate void ButtonHandler(object sender, ButtonEventArgs e);
        public class ButtonEventArgs : EventArgs
        {
            private Button btn;
            public Button LnkBtn
            {
                get
                {
                    return btn;
                }
                set
                {
                    btn = value;
                }
            }
            public ButtonEventArgs(Button BTNClick)
            {
                this.LnkBtn = BTNClick;
            }
        }




        #endregion Control EventsDelegate


        #region Other EventsDelegate

        public delegate void projectChangedHandler(object sender, projectChangedEventArgs e);
        public delegate void blockChangedHandler(object sender, blockChangedEventArgs e);
        public delegate void LocationSelectedHandler(object sender, LocationSelectedEventArgs e);
        public delegate void FinYearSelectedHandler(object sender, FinYearSelectedEventArgs e);

        public class projectChangedEventArgs : EventArgs
        {
            public decimal _projectID = 0;
            public decimal projectID
            {
                get { return _projectID; }
            }

            public projectChangedEventArgs(decimal projectID)
            {

                this._projectID = projectID;
            }

        }

        public class blockChangedEventArgs : EventArgs
        {
            private decimal _blockID = 0;

            public decimal blockID
            {
                get { return _blockID; }
            }

            public blockChangedEventArgs(decimal blockID)
            {
                this._blockID = blockID;
            }
        }

        public class LocationSelectedEventArgs : EventArgs
        {
            private string _companyName = string.Empty;
            private string _locationName = string.Empty;

            public string CompanyName
            {
                get { return _companyName; }
            }

            public string LocationName
            {
                get { return _locationName; }
            }

            public LocationSelectedEventArgs(string CompanyName, string LocationName)
            {
                this._locationName = LocationName;
                this._companyName = CompanyName;
            }
        }

        public class FinYearSelectedEventArgs : EventArgs
        {
            private string _finYearDesc = string.Empty;

            public string FinYearDesc
            {
                get { return _finYearDesc; }
            }

            public FinYearSelectedEventArgs(string FinYearDesc)
            {
                this._finYearDesc = FinYearDesc;
            }
        }
        #endregion Other EventsDelegate


    }
}