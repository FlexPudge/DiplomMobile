using System;
using System.Collections.Generic;


namespace RofloBulumbula
{
    public partial class Voucher
    {
        public int Id { get; set; }
        public int? Idclients { get; set; }
        public int? Idtours { get; set; }
        public DateTime? DateSale { get; set; }

        public virtual Client IdclientsNavigation { get; set; }
        public virtual Tour IdtoursNavigation { get; set; }
    }
}
