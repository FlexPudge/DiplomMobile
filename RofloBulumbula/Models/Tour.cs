using System;
using System.Collections.Generic;



namespace RofloBulumbula
{
    public partial class Tour
    {
   

        public int Id { get; set; }
        public string Title { get; set; }
        public string TypeTour { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public int? Complexity { get; set; }
        public int? MinimumAge { get; set; }
        public string Price { get; set; }
        public byte[] Image { get; set; }
        public string GeneralInformation { get; set; }
        public string HotInformation { get; set; }
        public string AdditionalInformation { get; set; }

    }
}
