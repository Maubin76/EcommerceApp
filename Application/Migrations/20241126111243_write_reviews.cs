using Application.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class write_reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert reviews for items
            var reviews = new[]
            {
                new Review(1, "user1", 5, "Excellent product! Highly recommend."),
                new Review(1, "user2", 4, "Good quality, but could be improved."),
                new Review(1, "user3", 3, "It works fine, but the design is not great."),

                new Review(2, "user4", 5, "Loved it! Exactly as described."),
                new Review(2, "user5", 4, "Great product, but shipping took longer than expected."),
                new Review(2, "user6", 2, "Not what I expected, the item is of poor quality."),

                new Review(3, "user7", 5, "Amazing! The best purchase I've made this year."),
                new Review(3, "user8", 3, "It’s decent, but I expected more for the price."),
                new Review(3, "user9", 4, "Good value for money, though could improve in some areas."),

                new Review(4, "user10", 5, "Fantastic item! Exceeded my expectations."),
                new Review(4, "user11", 2, "Disappointed. The product did not match the description."),
                new Review(4, "user12", 3, "It’s okay, but the build quality isn’t great."),

                new Review(5, "user13", 4, "Very good quality. Would buy again."),
                new Review(5, "user14", 5, "Just what I needed. Perfect fit!"),
                new Review(5, "user15", 3, "Not bad, but it could be a little more durable."),

                new Review(6, "user16", 5, "Top-notch item, well worth the price."),
                new Review(6, "user17", 4, "Great performance, but design could be improved."),
                new Review(6, "user18", 2, "Not satisfied. The item broke after a few uses."),
            };

            foreach (var review in reviews)
            {
                migrationBuilder.InsertData(
                    table: "Reviews",
                    columns: new[] { "id", "itemId", "userId", "rating", "comment" },
                    values: new object[] { Guid.NewGuid(), review.itemId, review.userId, review.rating, review.comment });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
