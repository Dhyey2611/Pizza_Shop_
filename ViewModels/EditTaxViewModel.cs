namespace Pizza_Shop_.ViewModels
{
    public class EditTaxViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public bool Is_enabled { get; set; }
        public bool Is_default { get; set; }
    }
}