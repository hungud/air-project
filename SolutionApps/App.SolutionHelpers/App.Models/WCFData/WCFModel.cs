using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace App.Model.WCFData
{
    [DataContract]
    public class WCFModel
    {
        [DataMember]
        public string O_NAME_EN { get; set; }
        [DataMember]
        public string O_NAME_AR { get; set; }
        [DataMember]
        public string A_NAME { get; set; }
        [DataMember]
        public int A_MOBILE { get; set; }
        [DataMember]
        public int PIN { get; set; }
        [DataMember]
        public string AREA { get; set; }
        [DataMember]
        public int ZONE_NO { get; set; }
        [DataMember]
        public int HOUSE_NO { get; set; }
        [DataMember]
        public int O_QID { get; set; }
        [DataMember]
        public int OWNER_TYPE { get; set; }
        [DataMember]
        public string CRMS_SR_NO { get; set; }

    }
    [DataContract]
    public class Book
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string BookName { get; set; }


    }
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
