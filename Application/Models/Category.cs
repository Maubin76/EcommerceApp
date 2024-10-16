using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class Category
    {
        public int id{get;set;} [Required]
        private string Title { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

        public Category(int _id, string _Title, List<Item> _Items)
        {
            id = _id;
            Title = _Title;
            Items = _Items;
        }
    }
}
