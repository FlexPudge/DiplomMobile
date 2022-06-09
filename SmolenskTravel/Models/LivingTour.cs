using System;
using System.Collections.Generic;


namespace SmolenskTravel
{
    public partial class LivingTour
    {
        public LivingTour()
        {
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string Title { get; set; }


        public virtual ICollection<Tour> Tours { get; set; }
    }
}
