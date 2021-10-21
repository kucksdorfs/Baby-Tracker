namespace Baby_Tracker.Web.Models
{
    using System;

    public interface BabyEventable
    {
        [PrimaryFieldAttribute]
        public Guid ID { get; set; }
        public Baby Baby { get; set; }
    }
}
