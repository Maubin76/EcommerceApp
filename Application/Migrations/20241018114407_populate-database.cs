using Application.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class populatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var item1 = new Item
            {
                id = 1,
                imageUrl = "/items/The_raft.png",
                name = "Tempest of Survival",
                description = "In this reimagining of \"The Raft of the Medusa,\" a group " +
                    "of survivors struggles on a raft amidst a raging storm. The figures, captured in desperate " +
                    "poses, are framed by crashing waves and dark clouds, with rays of light cutting through " +
                    "to highlight their emotional turmoil. The natural palette of stormy blues and muted flesh " +
                    "tones emphasizes the intensity of the human struggle against nature.",
                price = 1800
            };

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "name", "description", "price", "imageUrl" },
                values: new object[] { item1.id, item1.name, item1.description, item1.price, item1.imageUrl }
            );

            var item2 = new Item
            {
                id = 2,
                imageUrl = "/items/The_scream.png",
                name = "Whispers in the Wind",
                description = "This reimagining of \"The Scream\" evokes the same raw emotion " +
                    "of anguish but within a more natural, serene landscape. The central figure " +
                    "is set against flowing hills and soft skies, where bold brushstrokes in earthy " +
                    "tones blend human distress with the beauty of nature, creating a powerful contrast.",
                price = 1100
            };

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "name", "description", "price", "imageUrl" },
                values: new object[] { item2.id, item2.name, item2.description, item2.price, item2.imageUrl }
            );

            var item3 = new Item
            {
                id = 3,
                imageUrl = "/items/Starry_night.png",
                name = "Starry Horizons",
                description = "This AI-generated painting draws inspiration from Van Gogh's iconic " +
                "style, with swirling brushstrokes and vivid colors. The dreamlike landscape features " +
                "rolling hills under a dynamic starry sky, where bold stars and twisting clouds create " +
                "a sense of vibrant energy, reminiscent of the post-impressionist master’s emotional intensity.",
                price = 500
            };

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "name", "description", "price", "imageUrl" },
                values: new object[] { item3.id, item3.name, item3.description, item3.price, item3.imageUrl }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
