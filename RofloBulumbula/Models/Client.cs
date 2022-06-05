using System;
using System.Collections.Generic;


namespace RofloBulumbula
{
    public partial class Client
    {
        public Client()
        {
            Favorites = new HashSet<Favorite>();
            Idpassports = new HashSet<Idpassport>();
            Vouchers = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Fio { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string BankCard { get; set; }
        public int? Idpassport { get; set; }

       
        public virtual ICollection<Favorite> Favorites { get; set; }
       
        public virtual ICollection<Idpassport> Idpassports { get; set; }
     
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
