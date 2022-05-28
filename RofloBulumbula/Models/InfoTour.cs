using System;
using System.Collections.Generic;


namespace RofloBulumbula
{
    public partial class InfoTour
    {

        public int Id { get; set; }
        public string GeneralInformation { get; set; }
        public string HotInformation { get; set; }
        public string AdditionalInformation { get; set; }
        
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
