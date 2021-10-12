using System;
using System.Collections.Generic;
using System.Text;

namespace EFCExample.DataModels
{
    public class Email
    {
        public int id { get; set; }
        public string email { get; set; }

        public virtual Pessoa pessoa{ get; set; }
    }
}
