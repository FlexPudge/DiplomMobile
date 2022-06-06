using System;
using System.Collections.Generic;


namespace RofloBulumbula
{
    public partial class ZagranPassport
    {
        public ZagranPassport()
        {
            Idpassports = new HashSet<Idpassport>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public DateTime? DateBirthday { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string Number { get; set; }
        public string Srok { get; set; }
        public virtual ICollection<Idpassport> Idpassports { get; set; }
    }
}
