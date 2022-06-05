using System;
using System.Collections.Generic;



namespace RofloBulumbula
{
    public partial class Idpassport
    {
        public int Id { get; set; }
        public int? Idclient { get; set; }
        public int? Idpassport1 { get; set; }
        public int? IdzagranPassport { get; set; }

        public virtual Client IdclientNavigation { get; set; }
        public virtual Passport Idpassport1Navigation { get; set; }
        public virtual ZagranPassport IdzagranPassportNavigation { get; set; }
    }
}
