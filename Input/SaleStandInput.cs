namespace MySalesStandSystem.Input
{
    public class SaleStandInput
    {
        public string salesStandName { get; set; }
        public string address { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string description { get; set; }
        public byte[]? image { get; set; }
        public int UserId { get; set; }
    }
}
