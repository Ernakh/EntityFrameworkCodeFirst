using System;
using System.Collections.Generic;
using System.Text;

namespace EFCExample.DataModels
{
    public class Pessoa
    {
        public int id { get; set; }
        public string nome { get; set; }

        //public List<Email> pessoaEmails { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
    }
}
