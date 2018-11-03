namespace Musiction.API.Entities
{
    public class AccountInfo
    {
        public int test_credits_remaining { get; set; }
        public int credits_remaining { get; set; }
        public Plan plan { get; set; }
    }

    public class Plan
    {
        public string name { get; set; }
        public int price_per_month { get; set; }
        public int conversions_per_month { get; set; }
        public int maximum_file_size { get; set; }
    }
}
