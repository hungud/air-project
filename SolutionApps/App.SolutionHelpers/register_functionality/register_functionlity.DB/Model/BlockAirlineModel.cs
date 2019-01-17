using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace register_functionlity.DB.Model
{
    public class BlockAirlineModel
    {
        public string IATACode { get; set; }
        public bool IsNetfareMarkup { get; set; }
        public bool IsPublicMarkup { get; set; }
        public string WebsiteUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
