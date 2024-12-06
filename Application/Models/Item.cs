using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class Item
    {
        public int id;
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string category{get;set;}
        public string imageUrl { get; set; }
        public Item(){}

        public Item(int _id,string _name, string _description, decimal _price, string _imageUrl,string _category)
        {
            id=_id;
            name = _name;
            description = _description;
            price = _price;
            imageUrl = _imageUrl;
            category=_category;
        }
        public override string ToString()
        {
            return $"ID: {id}, Name: {name}, Description: {description}, Price: {price:C}, Image URL: {imageUrl}";
        }
    }

}
