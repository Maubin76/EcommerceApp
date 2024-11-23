namespace Application.Models{
    public class Review{
        public Guid id{get;set;}
        public int itemId{get;set;}
        public string userId{get;set;}
        public int rating{get;set;}
        public string comment{get;set;}

        public Review(int _itemId,string _userId, int _rating, string _comment ){
            itemId=_itemId;
            userId=_userId;
            rating=_rating;
            comment=_comment;
        }

        public Review(){}
    }
}