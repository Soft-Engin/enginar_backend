using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Data
{
    public class BlogDefaultData
    {
        public void PopulateBlogData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blogs>().HasData(
            new Blogs
            {
                Id = 1,
                RecipeId = null,
                Header = "THE ART OF FOOD PRESENTATION: A Comprehensive Anal",
                BodyText = "If there was a list of every entity that ever existed, The art of food presentation would rank TOP5 no doubt. VOL#1",
                UserId = "61",
                CreatedAt = new DateTime(2024, 12, 31, 0, 17, 54, DateTimeKind.Utc)
            },
            new Blogs
            {
                Id = 2,
                RecipeId = null,
                Header = "COOKING WITH SEASONAL VEGETABLES: A Comprehensive ",
                BodyText = "If there was a list of every entity that ever existed, Cooking with seasonal vegetables would rank TOP5 no doubt. VOL#2",
                UserId = "88",
                CreatedAt = new DateTime(2024, 12, 31, 0, 18, 1, DateTimeKind.Utc)
            },
            new Blogs
            {
                Id = 3,
                RecipeId = null,
                Header = "THE BEST COFFEE BRANDS: A Comprehensive Analysis, ",
                BodyText = "If there was a list of every entity that ever existed, The best coffee brands would rank TOP5 no doubt. VOL#3",
                UserId = "44",
                CreatedAt = new DateTime(2024, 12, 31, 0, 18, 8, DateTimeKind.Utc)
            },
            new Blogs
            {
                Id = 4,
                RecipeId = null,
                Header = "HOMEMADE JAMS AND PRESERVES: A Comprehensive Analy",
                BodyText = "If there was a list of every entity that ever existed, Homemade jams and preserves would rank TOP5 no doubt. VOL#4",
                UserId = "72",
                CreatedAt = new DateTime(2024, 12, 31, 0, 18, 13, DateTimeKind.Utc)
            },
            new Blogs
            {
                Id = 5,
                RecipeId = null,
                Header = "FOOD PHOTOGRAPHY: A Comprehensive Analysis, VOL. 5",
                BodyText = "If there was a list of every entity that ever existed, Food photography would rank TOP5 no doubt. VOL#5",
                UserId = "39",
                CreatedAt = new DateTime(2024, 12, 31, 0, 18, 20, DateTimeKind.Utc)
            },
            new Blogs
            {
                Id = 6,
                RecipeId = null,
                Header = "THE ART OF FOOD PRESENTATION: A Comprehensive Anal",
                BodyText = "If there was a list of every entity that ever existed, The art of food presentation would rank TOP5 no doubt. VOL#6",
                UserId = "57",
                CreatedAt = new DateTime(2024, 12, 31, 0, 18, 26, DateTimeKind.Utc)
            },
            new Blogs
            {
                Id = 7,
                RecipeId = null,
                Header = "BEST BBQ TECHNIQUES: A Comprehensive Analysis, VOL",
                BodyText = "If there was a list of every entity that ever existed, Best BBQ Techniques would rank TOP5 no doubt. VOL#7",
                UserId = "1",
                CreatedAt = new DateTime(2024, 12, 31, 0, 18, 31, DateTimeKind.Utc)
            },
            new Blogs
            {
                Id = 8,
                RecipeId = null,
                Header = "EMBRACE THE COMFORT OF FOODS AROUND THE GLOBE",
                BodyText =
                "In a world of culinary delights, there's solace to be found in comfort foods—those dishes that evoke a sense of home, warmth, and nostalgia. "
                    + "But what if we ventured beyond our familiar culinary horizons and explored the comforting flavors of other cultures? "
                    + "From the hearty stews of Europe to the spicy curries of Asia, every corner of the globe holds its own culinary gems that offer a unique and comforting experience. "
                    + "Let's embark on a gastronomic journey and discover some of the world's most comforting foods:\n\n"
                    + "1. Osso Buco (Italy): Slow-braised veal shanks in a rich tomato-based sauce, perfect for a chilly evening.\n"
                    + "2. Pad Thai (Thailand): A stir-fried noodle dish with a harmonious balance of sweet, sour, and savory flavors.\n"
                    + "3. Chicken Tikka Masala (India): A creamy, aromatic curry featuring tender chicken marinated in yogurt and spices.\n"
                    + "4. Bœuf Bourguignon (France): A classic French beef stew with red wine, mushrooms, and bacon, perfect for a cozy dinner party.\n"
                    + "5. Moqueca (Brazil): A flavorful seafood stew cooked in a coconut milk broth, reflecting the vibrancy of Brazilian cuisine.\n\n"
                    + "Exploring these different dishes not only satisfies our taste buds but also connects us to diverse cultures and their culinary traditions. "
                    + "Whether it's the heartwarming comfort of a steaming bowl of stew or the spicy indulgence of a curry, these foods have the power to transport us to different corners of the world.\n\n"
                    + "So, next time you're seeking solace in food, don't be afraid to step out of your comfort zone. "
                    + "Embrace the culinary wonders that await you beyond your borders and discover the comforting embrace of foods around the globe."
                    ,
                UserId = "37",
                CreatedAt = new DateTime(2024, 12, 31, 0, 18, 37, DateTimeKind.Utc)
            },
            new Blogs
            {
                Id = 9,
                RecipeId = null,
                Header = "THE WORLD OF SPICY FOODS: A Comprehensive Analysis",
                BodyText = "If there was a list of every entity that ever existed, The World of Spicy Foods would rank TOP5 no doubt. VOL#8",
                UserId = "67",
                CreatedAt = new DateTime(2024, 12, 31, 0, 18, 43, DateTimeKind.Utc)
            },
            new Blogs
            {
                Id = 10,
                RecipeId = null,
                Header = "COFFEE BREWING METHODS: A Comprehensive Analysis, ",
                BodyText = "If there was a list of every entity that ever existed, Coffee brewing methods would rank TOP5 no doubt. VOL#9",
                UserId = "11",
                CreatedAt = new DateTime(2024, 12, 31, 0, 18, 48, DateTimeKind.Utc)
            }
            );

            modelBuilder.Entity<Blogs>().HasData(
                // 100 Opinionated Blog Posts (all with RecipeId = null)
                new Blogs { Id = 101, RecipeId = null, Header = "The Magic of a Simple Smoothie", BodyText = "I just adore the way a simple smoothie can kickstart my day. It's a total game-changer!", UserId = "1", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 102, RecipeId = null, Header = "Mastering the Art of the Stir-Fry", BodyText = "Honestly, a perfect stir-fry is where cooking joy happens. Fresh ingredients make all the difference, and I'm all about it!", UserId = "15", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 103, RecipeId = null, Header = "Lentil Soup: A Global Comfort Food", BodyText = "There's something so deeply comforting about lentil soup, no matter where it's from. It's my ultimate go-to when I need a hug in a bowl.", UserId = "22", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 104, RecipeId = null, Header = "Baking Salmon to Perfection", BodyText = "Baking salmon is an art form, and when done right, it's pure magic. I can't get enough of that flaky goodness!", UserId = "38", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 105, RecipeId = null, Header = "Caprese: A Taste of Summer", BodyText = "Caprese salad? It's like summer on a plate! The simplicity and flavors just blow me away every time.", UserId = "55", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 106, RecipeId = null, Header = "Oatmeal: Beyond the Basic Bowl", BodyText = "Oatmeal, oh how I love thee, let me count the ways. So many creative twists make this breakfast staple a blast!", UserId = "71", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 107, RecipeId = null, Header = "Spicy Black Bean Burgers: A Vegetarian Delight", BodyText = "I'm obsessed with spicy black bean burgers. They're the perfect vegetarian alternative, and the flavor is just incredible!", UserId = "84", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 108, RecipeId = null, Header = "The Ultimate Guacamole Guide", BodyText = "I can't imagine life without fresh guacamole. It's creamy, flavorful, and utterly irresistible. Seriously, I'm in love!", UserId = "92", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 109, RecipeId = null, Header = "Elevating Avocado Toast", BodyText = "Avocado toast is the hero we all deserve. It's so versatile, especially with an egg on top. Pure genius!", UserId = "2", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 110, RecipeId = null, Header = "Chicken Curry: A Symphony of Flavors", BodyText = "A truly good chicken curry is a symphony of flavors that gets me every single time. The aromatics alone make my heart sing!", UserId = "18", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 11, RecipeId = null, Header = "The Richness of Mushroom Risotto", BodyText = "Mushroom risotto? Oh, it's just decadent and creamy heaven in a bowl. I could eat it every day!", UserId = "33", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 12, RecipeId = null, Header = "Shrimp Scampi: A Quick Culinary Delight", BodyText = "Shrimp scampi is my go-to for a quick yet fancy meal. It's incredibly delicious and satisfying. What's not to like?", UserId = "47", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 13, RecipeId = null, Header = "Spicy Beef Tacos: A Fiesta in Every Bite", BodyText = "Spicy beef tacos are my absolute jam! It's a fiesta in every bite, and I'm here for it!", UserId = "59", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 14, RecipeId = null, Header = "Spinach and Feta Stuffed Chicken: A Healthy Indulgence", BodyText = "Spinach and feta stuffed chicken? It's the healthy indulgence I need. Full of flavor and so satisfying!", UserId = "76", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 15, RecipeId = null, Header = "Fluffy Pancakes: A Breakfast Staple", BodyText = "Fluffy pancakes are pure breakfast bliss, I don't know what i'd do without them!", UserId = "88", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 16, RecipeId = null, Header = "Eggplant Parmesan: An Italian Classic", BodyText = "Eggplant Parmesan is an absolute classic in my book. The layered flavors, the sauciness, it's just sublime!", UserId = "99", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 17, RecipeId = null, Header = "Caprese Pasta Salad: A Light and Refreshing Meal", BodyText = "Caprese pasta salad? It's so light and refreshing, it's pure joy. I love it, love it, love it!", UserId = "5", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 18, RecipeId = null, Header = "Tuna Salad Sandwich: A Simple and Satisfying Lunch", BodyText = "A well-made tuna salad sandwich? So satisfying for lunch, you'd be hard-pressed to find a better midday meal.", UserId = "20", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 19, RecipeId = null, Header = "Chicken and Veggie Skewers: A BBQ Favorite", BodyText = "Chicken and veggie skewers are a BBQ must-have, so easy to make and incredibly yummy. It's a win-win!", UserId = "35", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 20, RecipeId = null, Header = "Baked Sweet Potato Fries: A Healthier Indulgence", BodyText = "I'm obsessed with baked sweet potato fries. They're so much better than regular fries!", UserId = "42", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 21, RecipeId = null, Header = "The Perfect Roast Chicken Recipe", BodyText = "A roast chicken should be juicy and flavorful, and getting it just right is a pure joy. It's a must-know cooking skill in my opinion!", UserId = "51", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 22, RecipeId = null, Header = "Exploring Regional Pasta Dishes of Italy", BodyText = "Italy has so many amazing regional pasta dishes, it’s insane! I honestly think they've mastered the art of pasta.", UserId = "66", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 23, RecipeId = null, Header = "The Art of Homemade Pizza Dough", BodyText = "Making your own pizza dough from scratch is a huge undertaking, but it's so rewarding when done correctly. Crispy crusts for the win!", UserId = "78", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 24, RecipeId = null, Header = "Understanding the Basics of French Baking", BodyText = "French baking can be intimidating, but when you get it right, it's just magnificent. Honestly, they are masters of baking.", UserId = "91", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 25, RecipeId = null, Header = "Flavorful Vegetarian Chili Options", BodyText = "I'm a big fan of a good vegetarian chili. There are endless creative combinations that can blow your mind!", UserId = "4", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 26, RecipeId = null, Header = "The Secret to a Crispy Fried Chicken", BodyText = "Fried chicken should be crispy and golden, if you ask me! Anything less, and I just can't get into it.", UserId = "17", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 27, RecipeId = null, Header = "Mastering the Perfect Steak", BodyText = "I believe everyone should know how to cook a great steak, I just love a well cooked steak. It's such a satisfying meal.", UserId = "29", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 28, RecipeId = null, Header = "The Variety of Rice Dishes from Around the World", BodyText = "I find it incredible how rice can be transformed into so many amazing dishes. Rice is an absolute staple for a reason!", UserId = "48", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 29, RecipeId = null, Header = "The Magic of Slow Cooking: A Guide to Slow Cooker Recipes", BodyText = "I swear by my slow cooker. It's my secret weapon for effortless and flavorful meals. What a gem!", UserId = "60", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 30, RecipeId = null, Header = "Exploring the Diversity of Asian Noodles", BodyText = "Asian noodles are so diverse and delicious. I could easily dedicate my life to exploring the countless dishes!", UserId = "73", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 31, RecipeId = null, Header = "The Science Behind Bread Baking", BodyText = "I find bread baking to be so fascinating! The science that goes into it is just unreal, and the results are heavenly.", UserId = "87", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 32, RecipeId = null, Header = "Exploring the Benefits of Fermented Foods", BodyText = "Fermented foods are a game-changer, I tell you. The health benefits are amazing, and the taste, if you like the taste, is unique!", UserId = "96", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 33, RecipeId = null, Header = "Plant-Based Burgers: A Comprehensive Guide", BodyText = "Plant-based burgers are getting better and better, and honestly, I'm here for it! The creativity is endless.", UserId = "10", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 34, RecipeId = null, Header = "Quick and Easy Weekday Dinner Options", BodyText = "On busy weeknights, quick and easy dinners are a lifesaver. It’s all about minimal fuss, maximum flavor, and I cannot be happier!", UserId = "24", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 35, RecipeId = null, Header = "The Importance of a Balanced Breakfast", BodyText = "I believe a balanced breakfast is crucial for a productive day. It sets the tone, I swear!", UserId = "39", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 36, RecipeId = null, Header = "Understanding the World of Chocolate", BodyText = "Chocolate! I just adore chocolate! It's a gift from the gods, if you ask me.", UserId = "52", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 37, RecipeId = null, Header = "The Benefits of Cooking with Fresh Herbs", BodyText = "Fresh herbs are my secret ingredient to elevate any dish. I cannot emphasize this enough!", UserId = "68", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 38, RecipeId = null, Header = "Budget-Friendly Meal Ideas for Families", BodyText = "Eating well shouldn't break the bank! There are so many ways to eat healthy on a budget, and it's totally doable!", UserId = "80", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 39, RecipeId = null, Header = "Knife Skills Every Cook Should Master", BodyText = "Good knife skills are essential in the kitchen. A good chef with good knife skills is a happy chef. It's like magic!", UserId = "95", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 40, RecipeId = null, Header = "The Joys of Cooking at Home", BodyText = "Cooking at home is one of the most gratifying experiences, and I fully believe everyone should do it more often!", UserId = "7", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 41, RecipeId = null, Header = "Elevating Your Meals with Spices", BodyText = "Spices can transform a dish, and they're always worth experimenting with! Honestly, I cannot express how important spices are.", UserId = "19", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 42, RecipeId = null, Header = "Effective Meal Prepping Techniques", BodyText = "Meal prepping can be a huge help. It's like a lifesaver for busy days! Time-savers are a blessing!", UserId = "31", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 43, RecipeId = null, Header = "Understanding the Basics of Clean Eating", BodyText = "I'm all about clean eating, focusing on natural, whole foods. This is something I truly believe in for better health and wellness.", UserId = "45", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 44, RecipeId = null, Header = "Choosing the Right Cooking Oils", BodyText = "Choosing the right cooking oil is important. You do not want to use just anything! It's something to be more careful about.", UserId = "63", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 45, RecipeId = null, Header = "The Art of Plating and Food Presentation", BodyText = "Presentation matters. Plating a dish can elevate it in so many ways! I can't get enough of a beautiful plate.", UserId = "77", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 46, RecipeId = null, Header = "Essential Food Safety Practices for Home Cooks", BodyText = "Food safety is non-negotiable. Always be mindful of how you handle your food. Just do it right!", UserId = "89", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 47, RecipeId = null, Header = "A Guide to Gluten-Free Baking", BodyText = "Gluten-free baking is not that scary once you understand its ins and outs. Experiment and find what you love!", UserId = "98", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 48, RecipeId = null, Header = "Dairy-Free Alternatives: Taste and Health Benefits", BodyText = "Dairy-free alternatives are getting so good now, and they are definitely worth exploring! I'm a big fan, especially with allergies!", UserId = "12", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 49, RecipeId = null, Header = "Healthy Eating on a Budget", BodyText = "You don't have to be rich to eat healthy! It's absolutely achievable on a budget, and I wholeheartedly believe that.", UserId = "25", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 50, RecipeId = null, Header = "Understanding Vegetarianism and its Benefits", BodyText = "Vegetarianism? It's definitely worth considering for its many benefits, and I absolutely respect those who are in it!", UserId = "40", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 51, RecipeId = null, Header = "The Basics of Bread Making", BodyText = "Bread making is not that difficult, and making your own bread at home can be so rewarding. I always try to make my own!", UserId = "57", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 52, RecipeId = null, Header = "Incorporating Garden Herbs into Your Cooking", BodyText = "Fresh herbs from my garden are a must-have. They elevate every single dish! I swear, you should try it!", UserId = "70", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 53, RecipeId = null, Header = "Creative Uses for Leftovers", BodyText = "I hate to waste food, so using leftovers in new, creative ways is something I love. Let's be more eco-friendly!", UserId = "82", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 54, RecipeId = null, Header = "The Joys of Grilling at Home", BodyText = "I absolutely love to grill! It's so easy and a great way to get together with friends and family, what is there not to like?!", UserId = "94", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 55, RecipeId = null, Header = "How to Select the Right Cooking Equipment", BodyText = "Having the right equipment in the kitchen is a must. It's such a game-changer, and I can't recommend it enough!", UserId = "1", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 56, RecipeId = null, Header = "How Home Gardening Improves Your Nutrition", BodyText = "Home gardening is just fantastic! It's healthy for your mind and body, and I can't recommend it enough!", UserId = "13", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 57, RecipeId = null, Header = "Understanding Healthy Fats and Their Benefits", BodyText = "Healthy fats are crucial for our bodies, and I'm always trying to find new ways to incorporate them into my diet.", UserId = "28", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 58, RecipeId = null, Header = "The Science Behind Baking Ingredients", BodyText = "Baking is science, and that's what makes it so amazing. Understanding the science makes you a better cook, and I wholeheartedly agree!", UserId = "41", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 59, RecipeId = null, Header = "Understanding Common Food Allergies", BodyText = "Being aware of food allergies is so important, so we don't unknowingly hurt ourselves and others!", UserId = "58", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 60, RecipeId = null, Header = "Maintaining Healthy Eating While Traveling", BodyText = "Eating healthy while traveling can be a challenge but also a really fun one, I love exploring new options!", UserId = "69", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 61, RecipeId = null, Header = "The Benefits of Eating Protein", BodyText = "Protein is essential and there are many ways to get it. I think you can't be healthier without enough protein!", UserId = "83", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 62, RecipeId = null, Header = "A Journey Through Different Teas", BodyText = "I find tea to be so calming. I love trying new varieties and flavors. It's such a beautiful ritual!", UserId = "97", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 63, RecipeId = null, Header = "The Importance of Sharing Meals With Your Family", BodyText = "Family meals are a must in my opinion. It's such a beautiful way to bond and spend time with loved ones.", UserId = "9", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 64, RecipeId = null, Header = "Exploring Spices From Around the World", BodyText = "Spices are the key to unlocking a variety of flavors. I absolutely love trying new spices in my dishes!", UserId = "21", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 65, RecipeId = null, Header = "Easy Slow Cooker Recipes", BodyText = "Slow cookers are a busy person’s best friend. What can be better than coming home to a delicious ready-to-eat meal?!", UserId = "36", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 66, RecipeId = null, Header = "The Importance of Eating Whole Grains", BodyText = "Whole grains are incredibly important, and I think everyone should incorporate them more into their daily diet. There's so much to choose from!", UserId = "50", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 67, RecipeId = null, Header = "Creative Cake Decorating Techniques", BodyText = "Decorating cakes can be so fun! It's like art you can eat, and it's incredibly satisfying!", UserId = "64", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 68, RecipeId = null, Header = "Understanding Portion Control and Its Benefits", BodyText = "Portion control can be very helpful in maintaining a healthy diet. I'm a big advocate for mindful eating!", UserId = "79", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 69, RecipeId = null, Header = "Exploring Coffee and Its Varieties", BodyText = "Coffee is my ultimate energy booster. I love trying out new methods and different types of coffee beans, its a world of options!", UserId = "90", CreatedAt = DateTime.UtcNow },
               new Blogs { Id = 70, RecipeId = null, Header = "Homemade Sauces to Enhance Your Cooking", BodyText = "Homemade sauces can change your dish for the better. I definitely think you should be more experimental!", UserId = "3", CreatedAt = DateTime.UtcNow },
               new Blogs { Id = 71, RecipeId = null, Header = "The Benefits of Berries in Your Diet", BodyText = "Berries are a great addition to your diet, there's just so many options and they are so incredibly healthy!", UserId = "16", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 72, RecipeId = null, Header = "How to Create a Balanced and Nutritious Meal", BodyText = "A balanced meal is so important, it’s all about incorporating all the necessary elements into each meal. I'm always aiming for the perfect balance!", UserId = "30", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 73, RecipeId = null, Header = "Why You Should Eat More Fish", BodyText = "Eating fish is a must. There's just so many health benefits, and they taste amazing. What more could you want?", UserId = "43", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 74, RecipeId = null, Header = "Understanding Antioxidants and Their Impact", BodyText = "Antioxidants are important for good health. I always focus on ways to incorporate more of them into my diet!", UserId = "56", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 75, RecipeId = null, Header = "An Introduction to Vegan Cooking", BodyText = "Vegan cooking can be so much more creative and delicious than many people think, I'm all about it!", UserId = "67", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 76, RecipeId = null, Header = "Homemade Soups to Warm You Up", BodyText = "There's just something about a homemade soup that's so good for the soul. Especially in the cold months, it's the best!", UserId = "81", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 77, RecipeId = null, Header = "Why Calcium Is Important In Your Diet", BodyText = "Calcium is essential for your bones. I'm always looking for ways to incorporate more of it into my meals.", UserId = "93", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 78, RecipeId = null, Header = "How to Build a Beautiful Charcuterie Board", BodyText = "Charcuterie boards are my go-to appetizer. They are incredibly fun to create, and everyone loves them! It’s a total win!", UserId = "6", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 79, RecipeId = null, Header = "The Benefits of Nuts and Seeds", BodyText = "I always incorporate nuts and seeds into my snacks for an extra boost of nutrients, I strongly recommend you do the same!", UserId = "14", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 80, RecipeId = null, Header = "Quick and Easy Breakfast Ideas", BodyText = "Easy breakfasts are a must for busy mornings. No excuses for skipping breakfast. It's the most important meal!", UserId = "27", CreatedAt = DateTime.UtcNow },
               new Blogs { Id = 81, RecipeId = null, Header = "Reducing Waste Through Proper Food Storage", BodyText = "I am not a fan of wasting food, so proper food storage techniques are key! Let's be more mindful of our habits!", UserId = "37", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 82, RecipeId = null, Header = "Making Your Own Salad Dressings", BodyText = "I'm a big fan of homemade dressings. Why settle for store-bought when you can make your own, and so easily?!", UserId = "53", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 83, RecipeId = null, Header = "The Health Benefits of Yogurt", BodyText = "Yogurt is such a versatile and healthy snack, and I'm always looking for ways to incorporate it into my diet!", UserId = "65", CreatedAt = DateTime.UtcNow },
               new Blogs { Id = 84, RecipeId = null, Header = "Understanding the Glycemic Index", BodyText = "I always try to keep track of the glycemic index of foods, it’s a habit that helps me eat better. I strongly suggest you do too!", UserId = "74", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 85, RecipeId = null, Header = "Understanding Natural Sweeteners", BodyText = "I always aim for natural sweeteners instead of processed white sugar. It's my health focus!", UserId = "86", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 86, RecipeId = null, Header = "Exploring Plant-Based Milks", BodyText = "Plant-based milks can be a good option for those with allergies. I love exploring all the various options available nowadays!", UserId = "100", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 87, RecipeId = null, Header = "Fun Baking Recipes to Make With Your Kids", BodyText = "Baking with kids is so much fun, It is a great way to bond. Try out some kid friendly recipes today!", UserId = "8", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 88, RecipeId = null, Header = "Improving Your Overall Cooking Skills", BodyText = "You can improve your cooking skills through consistent practice and learning. It's always worth it!", UserId = "23", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 89, RecipeId = null, Header = "Using Essential Oils In Cooking", BodyText = "Using essential oils in cooking is something I’m still exploring. Be careful because a little bit goes a long way!", UserId = "32", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 90, RecipeId = null, Header = "Food Pairing and Flavor Combinations", BodyText = "Food pairing is an art! Experiment and see what combinations work for you. It's pure magic when it goes right!", UserId = "46", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 91, RecipeId = null, Header = "Making Pasta From Scratch", BodyText = "Making your own pasta from scratch is so satisfying. If you have the time I suggest you do it too!", UserId = "61", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 92, RecipeId = null, Header = "Exploring the Variety of Cheese", BodyText = "The world of cheese is vast! Each one is so different and delicious, what is there not to like?! I'm always trying out new options!", UserId = "72", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 93, RecipeId = null, Header = "How to Build a Healthy and Flavorful Salad", BodyText = "A healthy salad is so incredibly important to maintain a healthy lifestyle. Experiment with new options to make each one exciting!", UserId = "85", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 94, RecipeId = null, Header = "Understanding Different Types of Peppers", BodyText = "I love different types of peppers. They can be so spicy! I just can't get enough of the added heat!", UserId = "99", CreatedAt = DateTime.UtcNow },
                 new Blogs { Id = 95, RecipeId = null, Header = "The Importance of Eating Whole Foods", BodyText = "Whole foods are the key to health and vitality. That is my belief, and it works for me so far!", UserId = "11", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 96, RecipeId = null, Header = "Proper Hand Washing Before Cooking", BodyText = "Washing your hands before handling food is a must! It's essential to be mindful of hygiene, and you cannot be careful enough!", UserId = "26", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 97, RecipeId = null, Header = "Organizing Your Pantry for Improved Efficiency", BodyText = "I swear by an organized pantry. It makes life so much easier, and cooking a lot less stressful, that is why I highly suggest it!", UserId = "44", CreatedAt = DateTime.UtcNow },
               new Blogs { Id = 98, RecipeId = null, Header = "The Benefits of Cooking With Cast Iron", BodyText = "Cooking with cast iron can be so beneficial! The heat distribution is just perfect! If you haven't already tried it, please do!", UserId = "49", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 99, RecipeId = null, Header = "Understanding Different Types of Marinades", BodyText = "Marinades can take your meal to a whole new level, and I believe everyone should explore more options to bring out the best flavors!", UserId = "54", CreatedAt = DateTime.UtcNow },
                new Blogs { Id = 100, RecipeId = null, Header = "The Different Types of Flour for Baking", BodyText = "Knowing the different types of flour is so important in baking. It's key to getting the best results, and I am all about the best results!", UserId = "62", CreatedAt = DateTime.UtcNow }

);
        }
                }
            }
