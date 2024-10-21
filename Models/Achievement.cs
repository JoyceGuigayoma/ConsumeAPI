namespace UnityMembers.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public int Order { get; set; }
        public string Patch { get; set; }
        public string Icon { get; set; }
        public Category Category { get; set; }
        public Type Type { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
