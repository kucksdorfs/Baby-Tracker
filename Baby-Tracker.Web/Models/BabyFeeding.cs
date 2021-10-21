namespace Baby_Tracker.Web.Models
{
    using System;

    public enum FoodType
    {
        Solid = 0,
        BreastMilk = 1,
        Formula = 2
    }
    public enum AmountType
    {
        Milliliter = 1,
        ImperialOunces = 2,
        USOunces = 3
    }

    public class BabyFeeding : BabyEventable
    {
        [PrimaryFieldAttribute]
        public Guid ID { get; set; }
        public Baby Baby { get; set; }
        public FoodType FoodType { get; set; }
        public int? Amount { get; set; }
        public AmountType? AmountType { get; set; }

    }
}
