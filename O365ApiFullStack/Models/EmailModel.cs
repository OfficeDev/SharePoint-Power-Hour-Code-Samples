using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace O365ApiFullStack.Models
{
    public class EmailModel
    {
        public String Subject { get; set; }
        public String Body { get; set; }

        public string Recipient { get; set; }
    }
}