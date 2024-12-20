﻿using Application.Models;
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
                price = 1800,
                category="Category1"
            };

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "name", "description", "price", "imageUrl","category" },
                values: new object[] { item1.id, item1.name, item1.description, item1.price, item1.imageUrl,item1.category }
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
                price = 1100,
                category="Category2"
            };

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "name", "description", "price", "imageUrl","category" },
                values: new object[] { item2.id, item2.name, item2.description, item2.price, item2.imageUrl,item2.category }
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
                price = 500,
                category="Category3"
            };

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "name", "description", "price", "imageUrl","category" },
                values: new object[] { item3.id, item3.name, item3.description, item3.price, item3.imageUrl,item3.category }
            );

            var item4 = new Item
            {
                id = 4,
                imageUrl = "/items/Joconde.png",
                name = "The Timeless Smile",
                description = "This digital reinterpretation of the Mona Lisa stays true to the elegance " +
                "and mystery of Leonardo da Vinci's masterpiece. The serene, enigmatic expression, soft lighting, " +
                "and the gentle flow of the natural background evoke the Renaissance charm. The earthy tones and " +
                "balanced composition honor the spirit of the original, creating a fresh yet faithful homage to this iconic work.",
                price = 3000,
                category="Category1"
            };

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "name", "description", "price", "imageUrl","category" },
                values: new object[] { item4.id, item4.name, item4.description, item4.price, item4.imageUrl,item4.category }
            );

            var item5 = new Item
            {
                id = 5,
                imageUrl = "/items/Moutains_river.png",
                name = "Majesty of the Sierra Nevada",
                description = "This digital remake of Albert Bierstadt's \"Among the Sierra Nevada Mountains, " +
                "California\" captures the awe-inspiring beauty of the American wilderness. " +
                "Towering mountains bathed in warm sunlight reflect on the calm lake below, while " +
                "wildlife roams freely. The vibrant, natural colors and serene atmosphere echo the grandeur " +
                "and peace of the original landscape, celebrating nature's untouched splendor.",
                price = 1200,
                category="Category1"
            };

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "name", "description", "price", "imageUrl","category" },
                values: new object[] { item5.id, item5.name, item5.description, item5.price, item5.imageUrl,item5.category }
            );

            var item6 = new Item
            {
                id = 6,
                imageUrl = "/items/Time.png",
                name = "The Persistence of Dreams",
                description = "This surreal AI-generated painting draws inspiration from Salvador Dalí’s style, " +
                "featuring melting clocks draped over distorted objects in a barren landscape. The fluid shapes" +
                " and elongated shadows evoke a dreamlike atmosphere, warping the sense of time and reality. Soft " +
                "yet striking colors enhance the otherworldly, surreal mood of the piece.",
                price = 600,
                category="Category1"
            };

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "name", "description", "price", "imageUrl","category" },
                values: new object[] { item6.id, item6.name, item6.description, item6.price, item6.imageUrl,item6.category }
            );


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
