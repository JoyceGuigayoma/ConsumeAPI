namespace UnityMembers.Models
{
    public class Mount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EnhancedDescription { get; set; }
        public string Tooltip { get; set; }
        public string Movement { get; set; }
        public int Seats { get; set; }
        public int Order { get; set; }
        public int OrderGroup { get; set; }
        public string Patch { get; set; }
        public int? ItemId { get; set; }
        public bool Tradeable { get; set; }
        public string Owned { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public object Bgm { get; set; }
        public List<Source> Sources { get; set; }
        public Achievement Achievement { get; set; }
    }
}
