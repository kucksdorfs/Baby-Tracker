namespace Baby_Tracker.Web.Models
{
    using System;

    public class Baby
    {
        [PrimaryFieldAttribute]
        public Guid ID { get; set; }

        public String Name { get; set; }

        public DateTime DOB { get; set; }
    }
}
