using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mockdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ac761b68-ec60-43b1-a2b9-29d5b77f209f", "36ef3207-4642-4fea-b882-991f4a72f3cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8ef4a0d2-86e8-4cbf-ab26-c34be7a95ae5", "a508a619-5b75-4a45-bcab-d07c3c985080" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4482b19c-ae99-49b6-95e9-0c58b43a0549", "594dec23-7a92-4000-acf9-4156a44afab2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ad837030-4eb6-4581-a8b2-87ffdfefb602", "adac071e-fa54-4446-8c2b-826d0f159d1f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "33174906-4bc7-4311-9a99-fead4aaee6fd", "1e443c0a-d3bf-43e1-977f-42835cafeaff" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "BodyText", "CreatedAt", "Header", "Image", "RecipeId", "UserId" },
                values: new object[] { 71, "Peynir Ezmesinin Tatlı Versiyonu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peynir Ezmesinin Tatlı Versiyonu", null, 2, "1" });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CityId", "Name", "PostCode" },
                values: new object[] { 3, 2, "Rat Street", 42069 });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, "Patates", 1 },
                    { 2, "Ejder Meyvesi", 2 },
                    { 5, "Domates", 1 },
                    { 6, "Soğan", 1 },
                    { 7, "Sarımsak", 1 },
                    { 8, "Tuz", 7 },
                    { 9, "Biber", 7 },
                    { 10, "Limon Suyu", 2 },
                    { 11, "Marul", 1 },
                    { 12, "Peynir", 4 },
                    { 13, "Bulgur", 5 },
                    { 14, "Balık", 6 },
                    { 15, "Yumurta", 6 },
                    { 16, "Maydanoz", 8 },
                    { 17, "Ceviz", 9 },
                    { 18, "Nane", 8 },
                    { 19, "Elma", 2 },
                    { 20, "Bal", 2 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "BodyText", "CreatedAt", "Header", "Image", "PreparationTime", "ServingSize", "UserId" },
                values: new object[,]
                {
                    { 3, "Zeytinyağlı Enginar Kulesi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeytinyağlı Enginar Kulesi", null, 90, 2, "2" },
                    { 4, "Enginar & Zeytinli Püre hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Zeytinli Püre", null, 330, 9, "3" },
                    { 5, "Bahar Enginar Salatası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bahar Enginar Salatası", null, 360, 4, "1" },
                    { 6, "Enginar Çorbası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Çorbası", null, 255, 4, "4" },
                    { 7, "Enginar Soslu Makarna hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Soslu Makarna", null, 285, 7, "4" },
                    { 8, "Enginar & Avokado Salatası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Avokado Salatası", null, 165, 10, "5" },
                    { 9, "Zeytinyağlı Enginar Ruloları hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeytinyağlı Enginar Ruloları", null, 270, 6, "4" },
                    { 10, "Enginarlı Yoğurtlu Meze hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Yoğurtlu Meze", null, 300, 10, "4" },
                    { 11, "Enginar Dolgulu Kabak Çiçeği hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Dolgulu Kabak Çiçeği", null, 15, 3, "1" },
                    { 12, "Enginarlı Humus hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Humus", null, 210, 8, "2" },
                    { 13, "Kremalı Enginar Çorbası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kremalı Enginar Çorbası", null, 255, 1, "5" },
                    { 14, "Enginar Pane hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Pane", null, 195, 7, "5" },
                    { 15, "Enginar Frittata hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Frittata", null, 360, 6, "3" },
                    { 16, "Enginar & Fesleğenli Pesto hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Fesleğenli Pesto", null, 15, 5, "1" },
                    { 17, "Enginarlı Pizza hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Pizza", null, 210, 3, "3" },
                    { 18, "Enginar Kebabı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Kebabı", null, 30, 9, "4" },
                    { 19, "Baharatlı Enginar Atıştırmalığı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baharatlı Enginar Atıştırmalığı", null, 180, 5, "3" },
                    { 20, "Enginar & Kuşkonmaz Garnitürü hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Kuşkonmaz Garnitürü", null, 255, 2, "2" },
                    { 21, "Enginarlı Patates Püresi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Patates Püresi", null, 75, 9, "2" },
                    { 22, "Limonlu Enginar Tatlısı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Limonlu Enginar Tatlısı", null, 315, 3, "3" },
                    { 23, "Enginar ve Tulum Peynirli Salata hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar ve Tulum Peynirli Salata", null, 270, 10, "2" },
                    { 24, "Fırında Parmesanlı Enginar hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fırında Parmesanlı Enginar", null, 90, 2, "3" },
                    { 25, "Zeytin Ezmesi ile Enginar Kanepesi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeytin Ezmesi ile Enginar Kanepesi", null, 30, 5, "2" },
                    { 26, "Enginarlı Couscous Salatası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Couscous Salatası", null, 60, 9, "3" },
                    { 27, "Kinoa ve Enginar Pilavı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kinoa ve Enginar Pilavı", null, 345, 8, "4" },
                    { 28, "Enginarlı Karides Sote hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Karides Sote", null, 225, 1, "3" },
                    { 29, "Enginar & Nar Ekşili Sos hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Nar Ekşili Sos", null, 180, 6, "4" },
                    { 30, "Fırında Baharatlı Enginar Yaprakları hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fırında Baharatlı Enginar Yaprakları", null, 30, 9, "4" },
                    { 31, "Enginarlı Sebze Güveci hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Sebze Güveci", null, 30, 8, "3" },
                    { 32, "Enginar Bruschetta hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Bruschetta", null, 345, 7, "1" },
                    { 33, "Zeytinyağlı Enginar Şakşuka hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeytinyağlı Enginar Şakşuka", null, 210, 9, "3" },
                    { 34, "Enginarlı Mercimek Salatası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Mercimek Salatası", null, 225, 3, "2" },
                    { 35, "Enginar Tatlısı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Tatlısı", null, 75, 3, "1" },
                    { 36, "Enginar ve Somon Carpaccio hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar ve Somon Carpaccio", null, 150, 9, "3" },
                    { 37, "Enginar Tartar hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Tartar", null, 255, 8, "2" },
                    { 38, "Enginar Püresi ile Dana Eti hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Püresi ile Dana Eti", null, 330, 7, "3" },
                    { 39, "Enginar & Ispanaklı Pide hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Ispanaklı Pide", null, 330, 1, "3" },
                    { 40, "Enginarlı Kabak Çorbası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Kabak Çorbası", null, 330, 6, "2" },
                    { 41, "Enginar Çiçeği Tatlısı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Çiçeği Tatlısı", null, 180, 6, "3" },
                    { 42, "Enginarlı Zeytinyağlı Sarma hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Zeytinyağlı Sarma", null, 300, 10, "4" },
                    { 43, "Fırında Enginarlı Kabak hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fırında Enginarlı Kabak", null, 105, 7, "1" },
                    { 44, "Enginar ve Labneli Tart hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar ve Labneli Tart", null, 60, 6, "1" },
                    { 45, "Enginarlı Bezelye Garnitürü hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Bezelye Garnitürü", null, 270, 3, "1" },
                    { 46, "Enginar Dolgulu Tavuk hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Dolgulu Tavuk", null, 120, 8, "3" },
                    { 47, "Enginarlı Makarna Salatası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Makarna Salatası", null, 150, 1, "3" },
                    { 48, "Fırında Enginarlı Yumurta hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fırında Enginarlı Yumurta", null, 315, 8, "1" },
                    { 49, "Enginar Graten hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Graten", null, 75, 6, "3" },
                    { 50, "Enginar ve Hardal Sosu hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar ve Hardal Sosu", null, 75, 6, "3" },
                    { 51, "Enginarlı Deniz Mahsulleri Tabağı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Deniz Mahsulleri Tabağı", null, 210, 1, "5" },
                    { 52, "Enginar Çıtırları hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Çıtırları", null, 270, 7, "2" },
                    { 53, "Enginar Kalbi Mezesı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Kalbi Mezesı", null, 135, 3, "3" },
                    { 54, "Zeytinyağlı Enginar Kulesi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeytinyağlı Enginar Kulesi", null, 300, 9, "1" },
                    { 55, "Enginar & Zeytinli Püre hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Zeytinli Püre", null, 120, 5, "5" },
                    { 56, "Bahar Enginar Salatası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bahar Enginar Salatası", null, 150, 10, "2" },
                    { 57, "Enginar Çorbası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Çorbası", null, 270, 6, "3" },
                    { 58, "Akdeniz Enginar Tabağı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Akdeniz Enginar Tabağı", null, 30, 10, "2" },
                    { 59, "Limonlu Enginar Sosu hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Limonlu Enginar Sosu", null, 240, 5, "5" },
                    { 60, "Fırında Enginar Cipsi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fırında Enginar Cipsi", null, 315, 7, "5" },
                    { 61, "Enginar & Peynir Ezmesi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Peynir Ezmesi", null, 45, 10, "5" },
                    { 62, "Enginarlı Bulgur Pilavı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Bulgur Pilavı", null, 285, 7, "4" },
                    { 63, "Zeytinyağlı Enginar Dolması hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeytinyağlı Enginar Dolması", null, 360, 3, "1" },
                    { 64, "Közlenmiş Enginar Kreması hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Közlenmiş Enginar Kreması", null, 210, 3, "4" },
                    { 65, "Enginar Turşusu hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Turşusu", null, 180, 4, "3" },
                    { 66, "Enginarlı Yeşil Salata hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Yeşil Salata", null, 330, 4, "1" },
                    { 67, "Enginarlı Omlet hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Omlet", null, 315, 10, "3" },
                    { 68, "Enginar Köftesi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Köftesi", null, 90, 1, "5" },
                    { 69, "Enginar Smoothie hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Smoothie", null, 75, 5, "4" },
                    { 70, "Enginar Tart hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Tart", null, 15, 1, "3" },
                    { 71, "Enginar Çayı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Çayı", null, 255, 7, "5" },
                    { 72, "Enginar Suflesi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Suflesi", null, 330, 3, "2" },
                    { 73, "Enginar Patatesli Güveç hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Patatesli Güveç", null, 240, 1, "4" },
                    { 74, "Enginar ve Tereyağlı Sos hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar ve Tereyağlı Sos", null, 180, 4, "5" },
                    { 75, "Enginarlı Dondurma hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Dondurma", null, 345, 4, "5" },
                    { 76, "Enginarlı Beyaz Peynir Ezmesi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Beyaz Peynir Ezmesi", null, 360, 1, "3" },
                    { 77, "Enginar Kroket hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Kroket", null, 90, 5, "5" },
                    { 78, "Enginar & Fırında Kuşkonmaz hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Fırında Kuşkonmaz", null, 255, 7, "5" },
                    { 79, "Enginar Tatlı Topları hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Tatlı Topları", null, 360, 4, "2" },
                    { 80, "Enginar Fırında Peynirli hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Fırında Peynirli", null, 75, 7, "1" },
                    { 81, "Enginar Çikolata Fonu hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Çikolata Fonu", null, 360, 7, "2" },
                    { 82, "Enginar ve Yoğurtlu Kabak hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar ve Yoğurtlu Kabak", null, 60, 4, "3" },
                    { 83, "Enginar Çorbası Parmesanlı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Çorbası Parmesanlı", null, 15, 10, "4" },
                    { 84, "Enginar Çorbası Tavuklu hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Çorbası Tavuklu", null, 195, 5, "3" },
                    { 85, "Enginar Kalbi Pane hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Kalbi Pane", null, 225, 2, "5" },
                    { 86, "Zeytinli Enginar Tart hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeytinli Enginar Tart", null, 330, 7, "5" },
                    { 87, "Enginar ve Baharatlı Sebzeler hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar ve Baharatlı Sebzeler", null, 195, 1, "2" },
                    { 88, "Enginar Şiş hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Şiş", null, 15, 7, "1" },
                    { 89, "Enginar Zeytinyağlı Pilaki hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Zeytinyağlı Pilaki", null, 30, 4, "5" },
                    { 90, "Enginarlı Mantı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Mantı", null, 30, 10, "2" },
                    { 91, "Enginar Tavuklu Salata hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Tavuklu Salata", null, 360, 1, "2" },
                    { 92, "Enginarlı Lahana Salatası hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Lahana Salatası", null, 120, 2, "5" },
                    { 93, "Enginar ve Fesleğenli Frittata hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar ve Fesleğenli Frittata", null, 15, 1, "4" },
                    { 94, "Enginar Tava hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Tava", null, 75, 1, "5" },
                    { 95, "Enginar Çubukları hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Çubukları", null, 105, 3, "4" },
                    { 96, "Enginar & Maydanozlu Tereyağı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Maydanozlu Tereyağı", null, 315, 10, "4" },
                    { 97, "Fırında Enginar Sufle hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fırında Enginar Sufle", null, 90, 3, "1" },
                    { 98, "Enginar & Limonlu Patates hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar & Limonlu Patates", null, 225, 1, "4" },
                    { 99, "Enginar Böreği hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Böreği", null, 135, 10, "2" },
                    { 100, "Enginarlı Havuçlu Salata hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Havuçlu Salata", null, 195, 4, "3" },
                    { 101, "Enginar Tatlı Soslu hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Tatlı Soslu", null, 330, 9, "5" },
                    { 102, "Enginar Ezmesi Pesto hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Ezmesi Pesto", null, 225, 9, "3" },
                    { 103, "Enginarlı Balık Güveç hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginarlı Balık Güveç", null, 30, 6, "4" },
                    { 104, "Enginar ve Naneli Dip hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar ve Naneli Dip", null, 255, 7, "3" },
                    { 105, "Enginar Kalp Tabağı hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enginar Kalp Tabağı", null, 75, 3, "2" },
                    { 106, "Oğuzhan'ın Enginar Suflesi hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oğuzhan'ın Enginar Suflesi", null, 285, 6, "4" }
                });

            migrationBuilder.InsertData(
                table: "Recipes_Ingredients",
                columns: new[] { "Id", "IngredientId", "Quantity", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 3, 3, 2.0, 2, "adet" },
                    { 4, 4, 3.0, 2, "yemek kaşığı" }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "BodyText", "CreatedAt", "Header", "Image", "RecipeId", "UserId" },
                values: new object[,]
                {
                    { 2, "Makarna ve Enginar Aşkı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Makarna ve Enginar Aşkı", null, 43, "4" },
                    { 3, "Avokado ile Hafiflik blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avokado ile Hafiflik", null, 41, "2" },
                    { 4, "Rulo Şıklığı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rulo Şıklığı", null, 44, "3" },
                    { 5, "Yoğurtlu Lezzet Patlaması blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yoğurtlu Lezzet Patlaması", null, 27, "3" },
                    { 6, "Kabak Çiçeğinde Enginar blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kabak Çiçeğinde Enginar", null, 86, "1" },
                    { 7, "Humusun Sırrı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Humusun Sırrı", null, 13, "3" },
                    { 8, "Kremanın Yumuşaklığı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kremanın Yumuşaklığı", null, 63, "5" },
                    { 9, "Kızarmış Keyif blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kızarmış Keyif", null, 66, "5" },
                    { 10, "Frittata ile Güne Başla blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frittata ile Güne Başla", null, 19, "2" },
                    { 11, "Fesleğenli Taze Notalar blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fesleğenli Taze Notalar", null, 93, "5" },
                    { 12, "Pizzada Akdeniz Esintisi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pizzada Akdeniz Esintisi", null, 23, "2" },
                    { 13, "Kebabın Enginar Hali blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kebabın Enginar Hali", null, 75, "2" },
                    { 14, "Baharatlı Atıştırmalıklar blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baharatlı Atıştırmalıklar", null, 74, "5" },
                    { 15, "Kuşkonmazın Tazeliği blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kuşkonmazın Tazeliği", null, 8, "5" },
                    { 16, "Patates ve Enginar Uyumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patates ve Enginar Uyumu", null, 34, "1" },
                    { 17, "Limonlu Tatlı Dokunuş blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Limonlu Tatlı Dokunuş", null, 63, "1" },
                    { 18, "Tulum Peynirinin Şıklığı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tulum Peynirinin Şıklığı", null, 85, "4" },
                    { 19, "Parmesan İle Fırın Keyfi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Parmesan İle Fırın Keyfi", null, 25, "3" },
                    { 20, "Zeytinli Kanepenin Lezzeti blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeytinli Kanepenin Lezzeti", null, 48, "1" },
                    { 21, "Couscous Şöleni blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Couscous Şöleni", null, 90, "3" },
                    { 22, "Kinoanın Gücü blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kinoanın Gücü", null, 100, "2" },
                    { 23, "Deniz Mahsulleri ve Enginar blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deniz Mahsulleri ve Enginar", null, 10, "3" },
                    { 24, "Nar Ekşisinin Aroması blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nar Ekşisinin Aroması", null, 4, "1" },
                    { 25, "Yaprakların Fırında Dansı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yaprakların Fırında Dansı", null, 75, "3" },
                    { 26, "Sebze Güvecin Kraliçesi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sebze Güvecin Kraliçesi", null, 12, "4" },
                    { 27, "Bruschetta ile Hafiflik blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bruschetta ile Hafiflik", null, 77, "3" },
                    { 28, "Şakşukanın Yeni Hali blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Şakşukanın Yeni Hali", null, 10, "2" },
                    { 29, "Mercimek ve Enginar Uyumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mercimek ve Enginar Uyumu", null, 86, "4" },
                    { 30, "Tatlıda Enginar Denemesi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tatlıda Enginar Denemesi", null, 44, "3" },
                    { 31, "Somonun Zenginliği blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Somonun Zenginliği", null, 42, "1" },
                    { 32, "Tartarın Zerafeti blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tartarın Zerafeti", null, 84, "4" },
                    { 33, "Dana Etinin Püresi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dana Etinin Püresi", null, 31, "3" },
                    { 34, "Pideye Yeni Bir Soluk blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pideye Yeni Bir Soluk", null, 26, "4" },
                    { 35, "Kabak ve Enginar Çorbası blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kabak ve Enginar Çorbası", null, 7, "3" },
                    { 36, "Tatlı Çiçekler blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tatlı Çiçekler", null, 13, "3" },
                    { 37, "Zeytinyağlı Dolmanın Kraliçesi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeytinyağlı Dolmanın Kraliçesi", null, 101, "2" },
                    { 38, "Kabak Fırında Yeniden blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kabak Fırında Yeniden", null, 58, "4" },
                    { 39, "Labne İle Uyum blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Labne İle Uyum", null, 51, "5" },
                    { 40, "Bezelye ile Taze Bir Dokunuş blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bezelye ile Taze Bir Dokunuş", null, 25, "2" },
                    { 41, "Tavuğun Dolgulu Şöleni blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tavuğun Dolgulu Şöleni", null, 90, "1" },
                    { 42, "Makarna Salatasında Fark blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Makarna Salatasında Fark", null, 3, "3" },
                    { 43, "Yumurta ve Fırında Uyumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yumurta ve Fırında Uyumu", null, 92, "2" },
                    { 44, "Gratenin Altın Çağı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gratenin Altın Çağı", null, 32, "4" },
                    { 45, "Hardallı Sos Şıklığı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hardallı Sos Şıklığı", null, 45, "3" },
                    { 46, "Denizden Sofraya blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Denizden Sofraya", null, 63, "2" },
                    { 47, "Çıtırların Cazibesi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Çıtırların Cazibesi", null, 73, "3" },
                    { 48, "Kalplerde Bir Lezzet blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kalplerde Bir Lezzet", null, 27, "5" },
                    { 49, "Kulenin Zirvesi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kulenin Zirvesi", null, 21, "4" },
                    { 50, "Pürede Gizli Tatlar blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pürede Gizli Tatlar", null, 102, "1" },
                    { 51, "Baharın Getirdiği Tazelik blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baharın Getirdiği Tazelik", null, 36, "1" },
                    { 52, "Kaseye Dolu Çorba blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kaseye Dolu Çorba", null, 27, "1" },
                    { 53, "Akdeniz Mutfağının İncisi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Akdeniz Mutfağının İncisi", null, 68, "2" },
                    { 54, "Sosun Asidik Tadına Yolculuk blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sosun Asidik Tadına Yolculuk", null, 95, "2" },
                    { 55, "Fırından Gelen Cips Keyfi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fırından Gelen Cips Keyfi", null, 55, "4" },
                    { 56, "Peynir Ezmesi ve Lezzet blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peynir Ezmesi ve Lezzet", null, 84, "3" },
                    { 57, "Bulgurun Dansı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bulgurun Dansı", null, 100, "1" },
                    { 58, "Dolmada Şıklık blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dolmada Şıklık", null, 94, "4" },
                    { 59, "Kremanın Közle Uyumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kremanın Közle Uyumu", null, 62, "1" },
                    { 60, "Turşuda Keskin Tatlar blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Turşuda Keskin Tatlar", null, 104, "4" },
                    { 61, "Yeşilin Ferahlığı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeşilin Ferahlığı", null, 9, "3" },
                    { 62, "Güne Omletle Başlayın blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Güne Omletle Başlayın", null, 5, "3" },
                    { 63, "Köfte ve Enginarın Uyumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Köfte ve Enginarın Uyumu", null, 61, "3" },
                    { 64, "Smoothie ile Sağlık blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smoothie ile Sağlık", null, 22, "4" },
                    { 65, "Tartta Tatlı Kaçamak blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tartta Tatlı Kaçamak", null, 81, "2" },
                    { 66, "Çayda Farklı Bir Deneyim blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Çayda Farklı Bir Deneyim", null, 19, "4" },
                    { 67, "Suflede Zirve Tat blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suflede Zirve Tat", null, 37, "4" },
                    { 68, "Güveçte Lezzet Patlaması blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Güveçte Lezzet Patlaması", null, 42, "4" },
                    { 69, "Tereyağında Mucize blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tereyağında Mucize", null, 18, "5" },
                    { 70, "Dondurmanın Enginarlı Hali blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dondurmanın Enginarlı Hali", null, 94, "5" },
                    { 72, "Kroket ile Atıştırmalık Keyfi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kroket ile Atıştırmalık Keyfi", null, 15, "1" },
                    { 73, "Fırında Kuşkonmaz Uyumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fırında Kuşkonmaz Uyumu", null, 81, "3" },
                    { 74, "Tatlı Topların Zirvesi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tatlı Topların Zirvesi", null, 73, "2" },
                    { 75, "Peynirli Fırında Harmoni blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peynirli Fırında Harmoni", null, 53, "3" },
                    { 76, "Çikolatada Enginar Şaşkınlığı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Çikolatada Enginar Şaşkınlığı", null, 7, "1" },
                    { 77, "Yoğurt ve Kabak Uyumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yoğurt ve Kabak Uyumu", null, 30, "3" },
                    { 78, "Çorbanın Parmesan Dokunuşu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Çorbanın Parmesan Dokunuşu", null, 66, "4" },
                    { 79, "Tavuklu Çorbanın Sırrı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tavuklu Çorbanın Sırrı", null, 57, "3" },
                    { 80, "Pane ile Altın Renkler blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pane ile Altın Renkler", null, 94, "5" },
                    { 81, "Tartta Zeytin Aşkı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tartta Zeytin Aşkı", null, 45, "1" },
                    { 82, "Sebzelerin Baharatlı Dansı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sebzelerin Baharatlı Dansı", null, 76, "1" },
                    { 83, "Şiş Lezzet blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Şiş Lezzet", null, 106, "1" },
                    { 84, "Pilakide Hafiflik blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pilakide Hafiflik", null, 40, "3" },
                    { 85, "Mantının Enginar Yorumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mantının Enginar Yorumu", null, 83, "2" },
                    { 86, "Salatada Tavuğun Efsanesi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salatada Tavuğun Efsanesi", null, 104, "2" },
                    { 87, "Lahananın Tazeliği blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lahananın Tazeliği", null, 71, "3" },
                    { 88, "Frittata Şıklığı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frittata Şıklığı", null, 8, "2" },
                    { 89, "Tavada Lezzet Patlaması blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tavada Lezzet Patlaması", null, 94, "4" },
                    { 90, "Çubuklarda Hafiflik blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Çubuklarda Hafiflik", null, 48, "5" },
                    { 91, "Maydanozla Aromalı Tereyağı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maydanozla Aromalı Tereyağı", null, 4, "1" },
                    { 92, "Fırında Sufle Denemesi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fırında Sufle Denemesi", null, 28, "3" },
                    { 93, "Limonun Enginarla Uyumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Limonun Enginarla Uyumu", null, 74, "1" },
                    { 94, "Börek ve Enginar Şıklığı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Börek ve Enginar Şıklığı", null, 27, "5" },
                    { 95, "Havuçlu Salatada Farklılık blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Havuçlu Salatada Farklılık", null, 58, "5" },
                    { 96, "Tatlı Sosun Enginar Yorumu blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tatlı Sosun Enginar Yorumu", null, 56, "4" },
                    { 97, "Ezmede Pesto Esintisi blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ezmede Pesto Esintisi", null, 19, "3" },
                    { 98, "Balık Güveçte Tat blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Balık Güveçte Tat", null, 101, "3" },
                    { 99, "Naneli Dip Ferahlığı blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Naneli Dip Ferahlığı", null, 78, "4" },
                    { 100, "Kalpten Gelen Lezzet blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kalpten Gelen Lezzet", null, 87, "1" },
                    { 101, "Oğuzhan'ınkini yemeden ben en iyisini yedim deme! blog yazısı, enginarın farklı yönlerini keşfedin.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oğuzhan'ınkini yemeden ben en iyisini yedim deme!", null, 32, "4" }
                });

            migrationBuilder.InsertData(
                table: "Recipes_Ingredients",
                columns: new[] { "Id", "IngredientId", "Quantity", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 5, 20, 2.0, 3, "tatlı kaşığı" },
                    { 6, 9, 2.0, 3, "yemek kaşığı" },
                    { 7, 15, 1.0, 3, "çimdik" },
                    { 8, 1, 3.0, 3, "tane" },
                    { 9, 18, 3.0, 4, "kap" },
                    { 10, 13, 2.0, 4, "kilo" },
                    { 11, 9, 3.0, 4, "adet" },
                    { 12, 12, 3.0, 4, "çimdik" },
                    { 13, 20, 3.0, 4, "kilo" },
                    { 14, 7, 3.0, 4, "tatlı kaşığı" },
                    { 15, 15, 3.0, 4, "adet" },
                    { 16, 6, 4.0, 5, "avuç" },
                    { 17, 13, 3.0, 5, "çimdik" },
                    { 18, 7, 3.0, 5, "kilo" },
                    { 19, 20, 1.0, 6, "kap" },
                    { 20, 14, 1.0, 6, "çimdik" },
                    { 21, 12, 4.0, 6, "tatlı kaşığı" },
                    { 22, 16, 4.0, 6, "tane" },
                    { 23, 1, 1.0, 6, "kap" },
                    { 24, 6, 3.0, 6, "çimdik" },
                    { 25, 8, 4.0, 6, "avuç" },
                    { 26, 20, 3.0, 7, "tatlı kaşığı" },
                    { 27, 8, 2.0, 7, "avuç" },
                    { 28, 12, 1.0, 7, "yemek kaşığı" },
                    { 29, 11, 2.0, 7, "bardak" },
                    { 30, 10, 1.0, 8, "yemek kaşığı" },
                    { 31, 16, 2.0, 8, "kap" },
                    { 32, 19, 4.0, 8, "kilo" },
                    { 33, 7, 1.0, 8, "tatlı kaşığı" },
                    { 34, 4, 2.0, 8, "yemek kaşığı" },
                    { 35, 9, 1.0, 8, "bardak" },
                    { 36, 12, 1.0, 8, "kap" },
                    { 37, 8, 1.0, 8, "adet" },
                    { 38, 9, 3.0, 9, "bardak" },
                    { 39, 20, 3.0, 9, "bardak" },
                    { 40, 15, 4.0, 9, "çimdik" },
                    { 41, 8, 3.0, 9, "adet" },
                    { 42, 13, 3.0, 9, "çimdik" },
                    { 43, 2, 2.0, 9, "avuç" },
                    { 44, 7, 3.0, 10, "avuç" },
                    { 45, 18, 2.0, 10, "çimdik" },
                    { 46, 16, 3.0, 10, "kilo" },
                    { 47, 17, 1.0, 10, "tane" },
                    { 48, 2, 2.0, 10, "tane" },
                    { 49, 20, 1.0, 10, "yemek kaşığı" },
                    { 50, 5, 2.0, 10, "çimdik" },
                    { 51, 11, 4.0, 10, "çimdik" },
                    { 52, 4, 4.0, 11, "tatlı kaşığı" },
                    { 53, 1, 3.0, 11, "tane" },
                    { 54, 18, 2.0, 11, "adet" },
                    { 55, 11, 4.0, 11, "kilo" },
                    { 56, 17, 1.0, 11, "bardak" },
                    { 57, 12, 2.0, 11, "yemek kaşığı" },
                    { 58, 3, 4.0, 11, "avuç" },
                    { 59, 8, 4.0, 11, "avuç" },
                    { 60, 5, 1.0, 11, "kap" },
                    { 61, 6, 3.0, 12, "adet" },
                    { 62, 13, 1.0, 12, "tane" },
                    { 63, 8, 1.0, 12, "çimdik" },
                    { 64, 14, 2.0, 12, "adet" },
                    { 65, 4, 3.0, 12, "kilo" },
                    { 66, 15, 4.0, 12, "tane" },
                    { 67, 1, 2.0, 13, "çimdik" },
                    { 68, 16, 1.0, 13, "adet" },
                    { 69, 7, 3.0, 13, "tatlı kaşığı" },
                    { 70, 9, 2.0, 14, "yemek kaşığı" },
                    { 71, 8, 1.0, 14, "avuç" },
                    { 72, 11, 1.0, 14, "avuç" },
                    { 73, 4, 4.0, 14, "çimdik" },
                    { 74, 14, 1.0, 14, "adet" },
                    { 75, 7, 2.0, 14, "kap" },
                    { 76, 2, 1.0, 14, "adet" },
                    { 77, 13, 2.0, 14, "tatlı kaşığı" },
                    { 78, 8, 2.0, 15, "yemek kaşığı" },
                    { 79, 5, 4.0, 15, "tatlı kaşığı" },
                    { 80, 7, 1.0, 15, "tane" },
                    { 81, 1, 1.0, 15, "tane" },
                    { 82, 20, 2.0, 16, "çimdik" },
                    { 83, 16, 4.0, 16, "tane" },
                    { 84, 13, 4.0, 16, "tatlı kaşığı" },
                    { 85, 13, 4.0, 17, "avuç" },
                    { 86, 7, 3.0, 17, "yemek kaşığı" },
                    { 87, 6, 2.0, 17, "kap" },
                    { 88, 8, 2.0, 17, "kilo" },
                    { 89, 16, 1.0, 18, "kap" },
                    { 90, 19, 4.0, 18, "çimdik" },
                    { 91, 17, 3.0, 18, "adet" },
                    { 92, 2, 3.0, 18, "tatlı kaşığı" },
                    { 93, 15, 3.0, 18, "çimdik" },
                    { 94, 9, 1.0, 18, "adet" },
                    { 95, 6, 4.0, 18, "kap" },
                    { 96, 12, 3.0, 18, "bardak" },
                    { 97, 13, 4.0, 19, "kap" },
                    { 98, 20, 3.0, 19, "kap" },
                    { 99, 7, 4.0, 19, "kap" },
                    { 100, 4, 2.0, 19, "tane" },
                    { 101, 3, 3.0, 20, "yemek kaşığı" },
                    { 102, 14, 3.0, 20, "kap" },
                    { 103, 4, 1.0, 20, "adet" },
                    { 104, 16, 1.0, 20, "tane" },
                    { 105, 1, 2.0, 20, "avuç" },
                    { 106, 12, 4.0, 20, "adet" },
                    { 107, 15, 4.0, 20, "tane" },
                    { 108, 8, 1.0, 20, "çimdik" },
                    { 109, 2, 1.0, 21, "bardak" },
                    { 110, 8, 2.0, 21, "bardak" },
                    { 111, 10, 4.0, 21, "çimdik" },
                    { 112, 17, 2.0, 22, "tatlı kaşığı" },
                    { 113, 7, 4.0, 22, "avuç" },
                    { 114, 8, 4.0, 22, "bardak" },
                    { 115, 15, 1.0, 22, "bardak" },
                    { 116, 13, 4.0, 22, "kap" },
                    { 117, 18, 3.0, 23, "bardak" },
                    { 118, 6, 1.0, 23, "yemek kaşığı" },
                    { 119, 9, 1.0, 23, "yemek kaşığı" },
                    { 120, 11, 3.0, 23, "tane" },
                    { 121, 7, 3.0, 23, "bardak" },
                    { 122, 20, 4.0, 24, "kilo" },
                    { 123, 9, 3.0, 24, "bardak" },
                    { 124, 7, 3.0, 24, "adet" },
                    { 125, 17, 4.0, 24, "çimdik" },
                    { 126, 9, 2.0, 25, "avuç" },
                    { 127, 4, 3.0, 25, "çimdik" },
                    { 128, 6, 4.0, 25, "avuç" },
                    { 129, 8, 1.0, 25, "çimdik" },
                    { 130, 7, 1.0, 25, "avuç" },
                    { 131, 1, 1.0, 25, "adet" },
                    { 132, 11, 4.0, 25, "tatlı kaşığı" },
                    { 133, 14, 1.0, 25, "avuç" },
                    { 134, 16, 3.0, 25, "çimdik" },
                    { 135, 7, 4.0, 26, "adet" },
                    { 136, 20, 4.0, 26, "çimdik" },
                    { 137, 11, 4.0, 26, "tatlı kaşığı" },
                    { 138, 9, 3.0, 26, "çimdik" },
                    { 139, 8, 4.0, 27, "kap" },
                    { 140, 5, 3.0, 27, "adet" },
                    { 141, 14, 2.0, 27, "bardak" },
                    { 142, 1, 1.0, 28, "kilo" },
                    { 143, 5, 4.0, 28, "kilo" },
                    { 144, 13, 1.0, 28, "tane" },
                    { 145, 4, 4.0, 28, "yemek kaşığı" },
                    { 146, 2, 2.0, 28, "tane" },
                    { 147, 14, 2.0, 28, "kilo" },
                    { 148, 20, 1.0, 28, "avuç" },
                    { 149, 12, 1.0, 28, "tatlı kaşığı" },
                    { 150, 13, 1.0, 29, "tane" },
                    { 151, 7, 3.0, 29, "tane" },
                    { 152, 19, 1.0, 29, "kilo" },
                    { 153, 18, 3.0, 29, "kilo" },
                    { 154, 8, 2.0, 29, "bardak" },
                    { 155, 10, 4.0, 29, "çimdik" },
                    { 156, 11, 2.0, 30, "bardak" },
                    { 157, 17, 1.0, 30, "çimdik" },
                    { 158, 12, 2.0, 30, "bardak" },
                    { 159, 2, 4.0, 30, "adet" },
                    { 160, 9, 4.0, 30, "kap" },
                    { 161, 4, 2.0, 30, "tatlı kaşığı" },
                    { 162, 1, 3.0, 30, "adet" },
                    { 163, 18, 3.0, 30, "kilo" },
                    { 164, 16, 4.0, 31, "avuç" },
                    { 165, 19, 4.0, 31, "bardak" },
                    { 166, 1, 2.0, 31, "kap" },
                    { 167, 18, 1.0, 31, "kap" },
                    { 168, 12, 1.0, 31, "yemek kaşığı" },
                    { 169, 9, 4.0, 31, "kap" },
                    { 170, 15, 3.0, 32, "adet" },
                    { 171, 17, 1.0, 32, "kap" },
                    { 172, 14, 2.0, 32, "tane" },
                    { 173, 19, 2.0, 32, "avuç" },
                    { 174, 9, 1.0, 32, "kilo" },
                    { 175, 3, 1.0, 32, "çimdik" },
                    { 176, 18, 4.0, 32, "adet" },
                    { 177, 16, 3.0, 33, "kilo" },
                    { 178, 20, 4.0, 33, "kap" },
                    { 179, 17, 3.0, 33, "tatlı kaşığı" },
                    { 180, 15, 4.0, 33, "tane" },
                    { 181, 12, 2.0, 33, "bardak" },
                    { 182, 4, 2.0, 34, "tatlı kaşığı" },
                    { 183, 9, 1.0, 34, "tane" },
                    { 184, 5, 1.0, 34, "kap" },
                    { 185, 14, 4.0, 34, "avuç" },
                    { 186, 11, 1.0, 34, "kilo" },
                    { 187, 20, 1.0, 34, "çimdik" },
                    { 188, 10, 1.0, 34, "kilo" },
                    { 189, 1, 4.0, 35, "bardak" },
                    { 190, 4, 2.0, 35, "tatlı kaşığı" },
                    { 191, 14, 2.0, 35, "çimdik" },
                    { 192, 16, 2.0, 35, "bardak" },
                    { 193, 6, 3.0, 36, "tane" },
                    { 194, 9, 1.0, 36, "kilo" },
                    { 195, 3, 2.0, 36, "tatlı kaşığı" },
                    { 196, 1, 3.0, 36, "tane" },
                    { 197, 16, 2.0, 36, "tatlı kaşığı" },
                    { 198, 11, 1.0, 36, "adet" },
                    { 199, 4, 4.0, 36, "çimdik" },
                    { 200, 15, 3.0, 36, "yemek kaşığı" },
                    { 201, 7, 2.0, 37, "tatlı kaşığı" },
                    { 202, 8, 1.0, 37, "adet" },
                    { 203, 19, 2.0, 37, "çimdik" },
                    { 204, 4, 3.0, 37, "avuç" },
                    { 205, 20, 4.0, 37, "yemek kaşığı" },
                    { 206, 2, 2.0, 38, "çimdik" },
                    { 207, 20, 1.0, 38, "bardak" },
                    { 208, 7, 1.0, 38, "yemek kaşığı" },
                    { 209, 17, 3.0, 38, "tane" },
                    { 210, 14, 3.0, 38, "tatlı kaşığı" },
                    { 211, 9, 1.0, 38, "tane" },
                    { 212, 8, 2.0, 38, "kilo" },
                    { 213, 1, 1.0, 38, "bardak" },
                    { 214, 10, 3.0, 38, "adet" },
                    { 215, 2, 4.0, 39, "tane" },
                    { 216, 5, 1.0, 39, "çimdik" },
                    { 217, 3, 2.0, 39, "yemek kaşığı" },
                    { 218, 10, 2.0, 39, "tane" },
                    { 219, 17, 3.0, 39, "bardak" },
                    { 220, 16, 4.0, 39, "tane" },
                    { 221, 15, 1.0, 39, "yemek kaşığı" },
                    { 222, 4, 1.0, 39, "avuç" },
                    { 223, 9, 4.0, 40, "adet" },
                    { 224, 3, 2.0, 40, "adet" },
                    { 225, 8, 3.0, 40, "tatlı kaşığı" },
                    { 226, 1, 2.0, 40, "kilo" },
                    { 227, 5, 4.0, 40, "kap" },
                    { 228, 19, 2.0, 40, "kap" },
                    { 229, 4, 2.0, 40, "tatlı kaşığı" },
                    { 230, 10, 1.0, 40, "bardak" },
                    { 231, 2, 3.0, 41, "çimdik" },
                    { 232, 4, 4.0, 41, "yemek kaşığı" },
                    { 233, 9, 3.0, 41, "kilo" },
                    { 234, 16, 1.0, 41, "adet" },
                    { 235, 11, 3.0, 41, "bardak" },
                    { 236, 20, 2.0, 41, "kap" },
                    { 237, 5, 2.0, 41, "avuç" },
                    { 238, 14, 2.0, 41, "çimdik" },
                    { 239, 9, 4.0, 42, "çimdik" },
                    { 240, 3, 3.0, 42, "tatlı kaşığı" },
                    { 241, 8, 1.0, 42, "yemek kaşığı" },
                    { 242, 13, 4.0, 42, "tane" },
                    { 243, 17, 2.0, 42, "adet" },
                    { 244, 18, 4.0, 42, "kilo" },
                    { 245, 2, 2.0, 42, "tatlı kaşığı" },
                    { 246, 19, 3.0, 42, "yemek kaşığı" },
                    { 247, 10, 4.0, 43, "kilo" },
                    { 248, 9, 2.0, 43, "yemek kaşığı" },
                    { 249, 15, 1.0, 43, "kilo" },
                    { 250, 20, 3.0, 43, "tane" },
                    { 251, 3, 1.0, 44, "adet" },
                    { 252, 20, 4.0, 44, "yemek kaşığı" },
                    { 253, 19, 1.0, 44, "çimdik" },
                    { 254, 10, 4.0, 45, "yemek kaşığı" },
                    { 255, 8, 2.0, 45, "yemek kaşığı" },
                    { 256, 12, 2.0, 45, "tatlı kaşığı" },
                    { 257, 4, 3.0, 45, "bardak" },
                    { 258, 2, 3.0, 46, "kap" },
                    { 259, 17, 1.0, 46, "tatlı kaşığı" },
                    { 260, 8, 1.0, 46, "çimdik" },
                    { 261, 7, 1.0, 47, "kap" },
                    { 262, 20, 2.0, 47, "yemek kaşığı" },
                    { 263, 5, 4.0, 47, "bardak" },
                    { 264, 1, 1.0, 47, "yemek kaşığı" },
                    { 265, 13, 4.0, 47, "yemek kaşığı" },
                    { 266, 18, 4.0, 47, "çimdik" },
                    { 267, 15, 4.0, 47, "tatlı kaşığı" },
                    { 268, 17, 3.0, 47, "kilo" },
                    { 269, 15, 3.0, 48, "adet" },
                    { 270, 6, 4.0, 48, "yemek kaşığı" },
                    { 271, 13, 4.0, 48, "adet" },
                    { 272, 10, 1.0, 48, "kap" },
                    { 273, 9, 1.0, 48, "tatlı kaşığı" },
                    { 274, 5, 2.0, 48, "bardak" },
                    { 275, 20, 2.0, 49, "kilo" },
                    { 276, 12, 2.0, 49, "çimdik" },
                    { 277, 4, 3.0, 49, "yemek kaşığı" },
                    { 278, 13, 2.0, 49, "tatlı kaşığı" },
                    { 279, 14, 3.0, 49, "yemek kaşığı" },
                    { 280, 8, 4.0, 49, "tatlı kaşığı" },
                    { 281, 3, 4.0, 49, "avuç" },
                    { 282, 10, 3.0, 49, "adet" },
                    { 283, 9, 3.0, 49, "tane" },
                    { 284, 16, 1.0, 50, "tatlı kaşığı" },
                    { 285, 6, 3.0, 50, "avuç" },
                    { 286, 17, 2.0, 50, "avuç" },
                    { 287, 11, 1.0, 50, "kilo" },
                    { 288, 18, 3.0, 50, "tatlı kaşığı" },
                    { 289, 20, 2.0, 50, "kilo" },
                    { 290, 13, 4.0, 50, "adet" },
                    { 291, 2, 1.0, 51, "tatlı kaşığı" },
                    { 292, 10, 2.0, 51, "adet" },
                    { 293, 8, 2.0, 51, "adet" },
                    { 294, 6, 4.0, 52, "bardak" },
                    { 295, 9, 4.0, 52, "kap" },
                    { 296, 13, 4.0, 52, "adet" },
                    { 297, 20, 1.0, 52, "kap" },
                    { 298, 10, 2.0, 53, "bardak" },
                    { 299, 18, 1.0, 53, "adet" },
                    { 300, 14, 1.0, 53, "adet" },
                    { 301, 19, 3.0, 53, "çimdik" },
                    { 302, 7, 1.0, 53, "tane" },
                    { 303, 13, 2.0, 53, "kilo" },
                    { 304, 17, 3.0, 53, "bardak" },
                    { 305, 9, 2.0, 53, "tatlı kaşığı" },
                    { 306, 11, 2.0, 54, "kap" },
                    { 307, 13, 4.0, 54, "bardak" },
                    { 308, 20, 3.0, 54, "adet" },
                    { 309, 14, 4.0, 54, "kap" },
                    { 310, 17, 2.0, 54, "tatlı kaşığı" },
                    { 311, 5, 2.0, 54, "çimdik" },
                    { 312, 12, 2.0, 54, "kilo" },
                    { 313, 6, 2.0, 54, "tatlı kaşığı" },
                    { 314, 20, 4.0, 55, "avuç" },
                    { 315, 11, 2.0, 55, "yemek kaşığı" },
                    { 316, 2, 4.0, 55, "adet" },
                    { 317, 19, 2.0, 55, "tatlı kaşığı" },
                    { 318, 9, 4.0, 55, "tatlı kaşığı" },
                    { 319, 13, 3.0, 55, "tane" },
                    { 320, 18, 2.0, 55, "kilo" },
                    { 321, 8, 1.0, 55, "çimdik" },
                    { 322, 17, 2.0, 56, "çimdik" },
                    { 323, 20, 2.0, 56, "çimdik" },
                    { 324, 6, 3.0, 56, "bardak" },
                    { 325, 1, 4.0, 56, "adet" },
                    { 326, 13, 2.0, 56, "avuç" },
                    { 327, 14, 2.0, 56, "tane" },
                    { 328, 3, 4.0, 56, "kilo" },
                    { 329, 7, 1.0, 56, "tane" },
                    { 330, 18, 2.0, 56, "kilo" },
                    { 331, 16, 2.0, 57, "çimdik" },
                    { 332, 2, 3.0, 57, "çimdik" },
                    { 333, 15, 2.0, 57, "avuç" },
                    { 334, 20, 2.0, 58, "avuç" },
                    { 335, 16, 2.0, 58, "tatlı kaşığı" },
                    { 336, 12, 3.0, 58, "avuç" },
                    { 337, 2, 1.0, 58, "tane" },
                    { 338, 2, 2.0, 59, "tatlı kaşığı" },
                    { 339, 8, 2.0, 59, "avuç" },
                    { 340, 3, 1.0, 59, "tatlı kaşığı" },
                    { 341, 6, 4.0, 59, "yemek kaşığı" },
                    { 342, 15, 4.0, 59, "tane" },
                    { 343, 10, 4.0, 60, "kap" },
                    { 344, 19, 4.0, 60, "adet" },
                    { 345, 18, 4.0, 60, "adet" },
                    { 346, 9, 3.0, 61, "kilo" },
                    { 347, 10, 2.0, 61, "kap" },
                    { 348, 18, 2.0, 61, "kap" },
                    { 349, 5, 1.0, 61, "yemek kaşığı" },
                    { 350, 4, 3.0, 61, "adet" },
                    { 351, 7, 2.0, 62, "kap" },
                    { 352, 15, 2.0, 62, "tatlı kaşığı" },
                    { 353, 6, 3.0, 62, "tatlı kaşığı" },
                    { 354, 1, 2.0, 62, "çimdik" },
                    { 355, 17, 4.0, 62, "avuç" },
                    { 356, 20, 1.0, 62, "bardak" },
                    { 357, 3, 2.0, 62, "yemek kaşığı" },
                    { 358, 1, 1.0, 63, "kilo" },
                    { 359, 2, 1.0, 63, "tane" },
                    { 360, 3, 2.0, 63, "bardak" },
                    { 361, 15, 3.0, 63, "adet" },
                    { 362, 17, 4.0, 64, "adet" },
                    { 363, 18, 1.0, 64, "tane" },
                    { 364, 11, 3.0, 64, "yemek kaşığı" },
                    { 365, 12, 1.0, 64, "çimdik" },
                    { 366, 2, 2.0, 64, "avuç" },
                    { 367, 1, 4.0, 64, "kap" },
                    { 368, 9, 1.0, 64, "yemek kaşığı" },
                    { 369, 18, 2.0, 65, "kap" },
                    { 370, 6, 4.0, 65, "kilo" },
                    { 371, 7, 4.0, 65, "tatlı kaşığı" },
                    { 372, 2, 4.0, 65, "tane" },
                    { 373, 5, 2.0, 65, "adet" },
                    { 374, 17, 4.0, 65, "yemek kaşığı" },
                    { 375, 3, 4.0, 66, "kap" },
                    { 376, 17, 2.0, 66, "kilo" },
                    { 377, 14, 3.0, 66, "kap" },
                    { 378, 4, 3.0, 66, "bardak" },
                    { 379, 20, 2.0, 66, "kilo" },
                    { 380, 9, 1.0, 66, "çimdik" },
                    { 381, 16, 2.0, 66, "kilo" },
                    { 382, 13, 3.0, 66, "bardak" },
                    { 383, 12, 3.0, 67, "adet" },
                    { 384, 2, 4.0, 67, "bardak" },
                    { 385, 16, 3.0, 67, "kap" },
                    { 386, 7, 2.0, 67, "çimdik" },
                    { 387, 3, 3.0, 67, "avuç" },
                    { 388, 14, 3.0, 67, "tatlı kaşığı" },
                    { 389, 12, 3.0, 68, "tane" },
                    { 390, 16, 2.0, 68, "bardak" },
                    { 391, 10, 1.0, 68, "adet" },
                    { 392, 6, 2.0, 68, "tane" },
                    { 393, 5, 4.0, 69, "avuç" },
                    { 394, 20, 2.0, 69, "yemek kaşığı" },
                    { 395, 11, 4.0, 69, "kilo" },
                    { 396, 14, 1.0, 69, "kap" },
                    { 397, 18, 3.0, 69, "tatlı kaşığı" },
                    { 398, 9, 2.0, 69, "yemek kaşığı" },
                    { 399, 12, 2.0, 69, "avuç" },
                    { 400, 10, 3.0, 69, "adet" },
                    { 401, 18, 4.0, 70, "yemek kaşığı" },
                    { 402, 15, 2.0, 70, "tane" },
                    { 403, 7, 4.0, 70, "yemek kaşığı" },
                    { 404, 13, 3.0, 70, "kilo" },
                    { 405, 12, 4.0, 70, "bardak" },
                    { 406, 11, 2.0, 71, "kap" },
                    { 407, 5, 3.0, 71, "tatlı kaşığı" },
                    { 408, 13, 4.0, 71, "kilo" },
                    { 409, 12, 1.0, 71, "avuç" },
                    { 410, 7, 3.0, 71, "yemek kaşığı" },
                    { 411, 20, 1.0, 71, "avuç" },
                    { 412, 2, 1.0, 72, "tatlı kaşığı" },
                    { 413, 18, 2.0, 72, "tane" },
                    { 414, 13, 4.0, 72, "bardak" },
                    { 415, 16, 3.0, 73, "tane" },
                    { 416, 17, 1.0, 73, "adet" },
                    { 417, 14, 4.0, 73, "tatlı kaşığı" },
                    { 418, 14, 4.0, 74, "avuç" },
                    { 419, 12, 1.0, 74, "tane" },
                    { 420, 10, 2.0, 74, "bardak" },
                    { 421, 13, 4.0, 75, "tatlı kaşığı" },
                    { 422, 9, 2.0, 75, "avuç" },
                    { 423, 2, 1.0, 75, "avuç" },
                    { 424, 14, 2.0, 75, "tatlı kaşığı" },
                    { 425, 17, 3.0, 75, "kilo" },
                    { 426, 1, 1.0, 75, "yemek kaşığı" },
                    { 427, 8, 3.0, 76, "tane" },
                    { 428, 16, 2.0, 76, "çimdik" },
                    { 429, 2, 3.0, 76, "kap" },
                    { 430, 14, 2.0, 76, "çimdik" },
                    { 431, 20, 1.0, 76, "avuç" },
                    { 432, 12, 3.0, 76, "bardak" },
                    { 433, 3, 3.0, 76, "kilo" },
                    { 434, 1, 3.0, 76, "tane" },
                    { 435, 16, 2.0, 77, "çimdik" },
                    { 436, 1, 3.0, 77, "yemek kaşığı" },
                    { 437, 3, 4.0, 77, "tane" },
                    { 438, 17, 3.0, 77, "yemek kaşığı" },
                    { 439, 8, 1.0, 78, "kap" },
                    { 440, 17, 1.0, 78, "bardak" },
                    { 441, 11, 3.0, 78, "tane" },
                    { 442, 16, 1.0, 78, "kilo" },
                    { 443, 10, 4.0, 78, "bardak" },
                    { 444, 3, 1.0, 78, "kap" },
                    { 445, 14, 4.0, 78, "kap" },
                    { 446, 5, 4.0, 78, "kap" },
                    { 447, 6, 1.0, 78, "yemek kaşığı" },
                    { 448, 14, 1.0, 79, "çimdik" },
                    { 449, 20, 3.0, 79, "bardak" },
                    { 450, 11, 1.0, 79, "tatlı kaşığı" },
                    { 451, 16, 4.0, 79, "tatlı kaşığı" },
                    { 452, 4, 3.0, 79, "bardak" },
                    { 453, 13, 1.0, 79, "tatlı kaşığı" },
                    { 454, 19, 4.0, 79, "tatlı kaşığı" },
                    { 455, 9, 3.0, 79, "tane" },
                    { 456, 11, 3.0, 80, "çimdik" },
                    { 457, 8, 3.0, 80, "kap" },
                    { 458, 7, 1.0, 80, "avuç" },
                    { 459, 17, 1.0, 80, "tane" },
                    { 460, 5, 2.0, 80, "yemek kaşığı" },
                    { 461, 18, 3.0, 80, "yemek kaşığı" },
                    { 462, 10, 4.0, 80, "tatlı kaşığı" },
                    { 463, 3, 2.0, 80, "kap" },
                    { 464, 20, 1.0, 81, "tatlı kaşığı" },
                    { 465, 1, 4.0, 81, "adet" },
                    { 466, 18, 1.0, 81, "adet" },
                    { 467, 11, 3.0, 81, "bardak" },
                    { 468, 8, 4.0, 81, "tatlı kaşığı" },
                    { 469, 16, 2.0, 81, "bardak" },
                    { 470, 9, 4.0, 82, "kilo" },
                    { 471, 12, 2.0, 82, "yemek kaşığı" },
                    { 472, 15, 2.0, 82, "çimdik" },
                    { 473, 7, 2.0, 82, "tane" },
                    { 474, 17, 1.0, 82, "çimdik" },
                    { 475, 5, 1.0, 83, "tatlı kaşığı" },
                    { 476, 20, 2.0, 83, "avuç" },
                    { 477, 12, 2.0, 83, "adet" },
                    { 478, 15, 4.0, 84, "avuç" },
                    { 479, 7, 1.0, 84, "yemek kaşığı" },
                    { 480, 6, 4.0, 84, "avuç" },
                    { 481, 17, 1.0, 84, "kilo" },
                    { 482, 14, 1.0, 85, "adet" },
                    { 483, 9, 3.0, 85, "kilo" },
                    { 484, 7, 3.0, 85, "kap" },
                    { 485, 10, 3.0, 85, "avuç" },
                    { 486, 12, 4.0, 85, "kap" },
                    { 487, 13, 3.0, 86, "kap" },
                    { 488, 3, 3.0, 86, "tane" },
                    { 489, 6, 1.0, 86, "kap" },
                    { 490, 19, 1.0, 86, "tane" },
                    { 491, 17, 1.0, 86, "kilo" },
                    { 492, 15, 3.0, 86, "yemek kaşığı" },
                    { 493, 1, 4.0, 86, "tane" },
                    { 494, 20, 4.0, 86, "avuç" },
                    { 495, 16, 4.0, 87, "bardak" },
                    { 496, 1, 1.0, 87, "yemek kaşığı" },
                    { 497, 4, 2.0, 87, "avuç" },
                    { 498, 2, 2.0, 88, "kilo" },
                    { 499, 18, 1.0, 88, "bardak" },
                    { 500, 19, 4.0, 88, "adet" },
                    { 501, 17, 1.0, 88, "tatlı kaşığı" },
                    { 502, 7, 2.0, 88, "avuç" },
                    { 503, 4, 3.0, 88, "kap" },
                    { 504, 13, 3.0, 88, "bardak" },
                    { 505, 20, 3.0, 88, "çimdik" },
                    { 506, 15, 4.0, 88, "bardak" },
                    { 507, 5, 3.0, 89, "çimdik" },
                    { 508, 1, 1.0, 89, "çimdik" },
                    { 509, 2, 4.0, 89, "yemek kaşığı" },
                    { 510, 13, 2.0, 89, "tatlı kaşığı" },
                    { 511, 8, 4.0, 90, "kap" },
                    { 512, 20, 4.0, 90, "çimdik" },
                    { 513, 13, 2.0, 90, "avuç" },
                    { 514, 10, 4.0, 90, "tane" },
                    { 515, 6, 3.0, 90, "tatlı kaşığı" },
                    { 516, 12, 2.0, 91, "avuç" },
                    { 517, 17, 1.0, 91, "bardak" },
                    { 518, 19, 1.0, 91, "yemek kaşığı" },
                    { 519, 5, 3.0, 91, "avuç" },
                    { 520, 8, 2.0, 91, "çimdik" },
                    { 521, 4, 3.0, 91, "tatlı kaşığı" },
                    { 522, 5, 3.0, 92, "çimdik" },
                    { 523, 17, 1.0, 92, "çimdik" },
                    { 524, 11, 1.0, 92, "tatlı kaşığı" },
                    { 525, 16, 2.0, 92, "kap" },
                    { 526, 15, 2.0, 93, "tatlı kaşığı" },
                    { 527, 18, 3.0, 93, "tatlı kaşığı" },
                    { 528, 19, 2.0, 93, "kilo" },
                    { 529, 13, 3.0, 93, "kilo" },
                    { 530, 10, 3.0, 93, "adet" },
                    { 531, 1, 1.0, 93, "tane" },
                    { 532, 16, 1.0, 93, "kilo" },
                    { 533, 14, 1.0, 93, "yemek kaşığı" },
                    { 534, 18, 3.0, 94, "adet" },
                    { 535, 2, 4.0, 94, "çimdik" },
                    { 536, 15, 3.0, 94, "avuç" },
                    { 537, 11, 4.0, 94, "yemek kaşığı" },
                    { 538, 1, 2.0, 94, "yemek kaşığı" },
                    { 539, 5, 2.0, 94, "avuç" },
                    { 540, 20, 3.0, 94, "adet" },
                    { 541, 17, 2.0, 94, "çimdik" },
                    { 542, 12, 3.0, 95, "tane" },
                    { 543, 17, 1.0, 95, "avuç" },
                    { 544, 13, 3.0, 95, "tane" },
                    { 545, 10, 4.0, 95, "kap" },
                    { 546, 9, 1.0, 95, "avuç" },
                    { 547, 8, 4.0, 95, "avuç" },
                    { 548, 18, 1.0, 95, "kap" },
                    { 549, 1, 4.0, 95, "kilo" },
                    { 550, 7, 3.0, 96, "bardak" },
                    { 551, 3, 2.0, 96, "tatlı kaşığı" },
                    { 552, 5, 4.0, 96, "çimdik" },
                    { 553, 4, 1.0, 96, "tatlı kaşığı" },
                    { 554, 10, 2.0, 96, "kap" },
                    { 555, 16, 2.0, 97, "adet" },
                    { 556, 17, 4.0, 97, "tane" },
                    { 557, 7, 2.0, 97, "yemek kaşığı" },
                    { 558, 5, 2.0, 98, "bardak" },
                    { 559, 4, 4.0, 98, "bardak" },
                    { 560, 20, 4.0, 98, "yemek kaşığı" },
                    { 561, 2, 4.0, 98, "avuç" },
                    { 562, 19, 3.0, 98, "yemek kaşığı" },
                    { 563, 16, 1.0, 98, "bardak" },
                    { 564, 10, 3.0, 98, "çimdik" },
                    { 565, 18, 2.0, 99, "bardak" },
                    { 566, 16, 2.0, 99, "bardak" },
                    { 567, 6, 1.0, 99, "tane" },
                    { 568, 15, 2.0, 99, "çimdik" },
                    { 569, 12, 2.0, 99, "adet" },
                    { 570, 2, 4.0, 99, "tane" },
                    { 571, 10, 3.0, 99, "tatlı kaşığı" },
                    { 572, 5, 3.0, 100, "kap" },
                    { 573, 3, 1.0, 100, "çimdik" },
                    { 574, 1, 4.0, 100, "kilo" },
                    { 575, 13, 3.0, 100, "tane" },
                    { 576, 18, 2.0, 100, "avuç" },
                    { 577, 9, 2.0, 100, "kilo" },
                    { 578, 3, 2.0, 101, "tane" },
                    { 579, 12, 4.0, 101, "avuç" },
                    { 580, 16, 4.0, 101, "bardak" },
                    { 581, 15, 3.0, 101, "bardak" },
                    { 582, 14, 2.0, 101, "kap" },
                    { 583, 18, 3.0, 101, "adet" },
                    { 584, 19, 4.0, 101, "adet" },
                    { 585, 4, 2.0, 102, "kap" },
                    { 586, 6, 4.0, 102, "kap" },
                    { 587, 3, 2.0, 102, "kilo" },
                    { 588, 14, 4.0, 102, "çimdik" },
                    { 589, 1, 1.0, 102, "bardak" },
                    { 590, 16, 3.0, 102, "adet" },
                    { 591, 10, 3.0, 102, "kilo" },
                    { 592, 5, 4.0, 103, "adet" },
                    { 593, 7, 2.0, 103, "tane" },
                    { 594, 3, 1.0, 103, "kilo" },
                    { 595, 18, 1.0, 103, "yemek kaşığı" },
                    { 596, 12, 4.0, 104, "bardak" },
                    { 597, 13, 4.0, 104, "tatlı kaşığı" },
                    { 598, 14, 1.0, 104, "tane" },
                    { 599, 20, 3.0, 104, "adet" },
                    { 600, 18, 3.0, 105, "kap" },
                    { 601, 6, 3.0, 105, "çimdik" },
                    { 602, 8, 1.0, 105, "çimdik" },
                    { 603, 2, 3.0, 105, "adet" },
                    { 604, 17, 4.0, 106, "bardak" },
                    { 605, 7, 3.0, 106, "kilo" },
                    { 606, 5, 4.0, 106, "kilo" },
                    { 607, 3, 1.0, 106, "kap" },
                    { 608, 18, 1.0, 106, "bardak" },
                    { 609, 8, 1.0, 106, "tatlı kaşığı" },
                    { 610, 4, 2.0, 106, "kap" },
                    { 611, 20, 4.0, 106, "kap" },
                    { 612, 6, 3.0, 106, "tane" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 533);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 534);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 535);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 536);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 537);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 538);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 539);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 540);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 541);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 542);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 543);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 544);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 545);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 546);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 547);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 548);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 549);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 550);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 551);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 552);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 553);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 554);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 555);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 556);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 558);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 559);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 560);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 561);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 563);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 564);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 565);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 566);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 567);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 568);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 569);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 570);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 571);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 572);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 573);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 574);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 575);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 576);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 577);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 578);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 579);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 580);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 581);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 582);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 583);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 584);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 585);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 586);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 587);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 588);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 589);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 590);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 591);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 592);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 593);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 594);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 595);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 596);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 597);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 598);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 599);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 605);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 606);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 607);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 608);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 609);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 610);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 611);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 612);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cc101e71-31c3-4617-a937-bf803453fd84", "945da122-702a-43d0-a1ba-48027dc89a87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "07125bb8-98cf-469f-8aa3-b3614eb5cba1", "abfbf528-ef93-4a68-ba33-84b060cbc7d1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e313c33f-1067-410c-9859-b8faa3c4b417", "b99c1df9-7914-47a1-894d-7204a48c3252" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "691537e8-7251-4e13-8a62-6c9f53d61e22", "adec1588-b5d1-4fe6-b527-fe29834a2c4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "27c45632-9a27-4fc5-9993-3ad7cc3fb664", "4db157a4-2fb6-4b16-b9d7-dd87e311fa2d" });
        }
    }
}
