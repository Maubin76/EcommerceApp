using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class Article
    {
        private int id;
        private string name { get; set; }
        private string description { get; set; }
        private float price { get; set; }
        public string imageUrl { get; set; }

        public Article(string _name, string _description, float _price, string _imageUrl)
        {
            name = _name;
            description = _description;
            price = _price;
            imageUrl = _imageUrl;
        }
    }
}
