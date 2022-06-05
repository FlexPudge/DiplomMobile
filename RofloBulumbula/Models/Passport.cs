using System;
using System.Collections.Generic;


namespace RofloBulumbula
{
    public partial class Passport
    {
        public Passport()
        {
            Idpassports = new HashSet<Idpassport>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateBirthday { get; set; }
        public string Gender { get; set; }
        public string Number { get; set; }
        public DateTime? DateOfIssue { get; set; }


        public virtual ICollection<Idpassport> Idpassports { get; set; }
    }
}
