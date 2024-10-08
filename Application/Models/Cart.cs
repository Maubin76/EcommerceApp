using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class Cart
    {
        

        public Guid id{get;set;} 
        public string userId { get; set; }
        public List<Item> items { get; set; } = new List<Item>();
        
        public Cart(Guid _id, string _userId, List<Item> _items)
        {
            id = _id;
            userId = _userId;
            items = _items ?? new List<Item>();;
        }

        
        public decimal GetPrice(){
            decimal finalPrice=0;
            foreach (Item i in this.items){
                finalPrice+=i.price;
            }
            return finalPrice;
        }

        public void AddItem(Item item){
            this.items.Add(item);
        }
        public void DeleteItem(Item item){
            int itemId=item.id;
            var itemToRemove = this.items.FirstOrDefault(i => i.id == item.id);
            if (itemToRemove != null)
            {
                this.items.Remove(itemToRemove);
            }
        }
    }
}
