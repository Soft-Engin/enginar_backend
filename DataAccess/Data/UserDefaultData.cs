using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class UserDefaultData
    {

        public void PopulateRolesData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "User", Description = "Default user role" },
                new Roles { Id = 2, Name = "Admin", Description = "Admin role" }
            );

        }

        public void PopulateUsersData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = "1",
                    FirstName = "Abagail",
                    LastName = "Hess",
                    UserName = "ButterflyGirl",
                    RoleId = 1,
                    Bio = "Abagail, a lover of all things with wings, especially butterflies."
                },
                new Users
                {
                    Id = "2",
                    FirstName = "Aiden",
                    LastName = "Smith",
                    UserName = "aiden.smith123",
                    RoleId = 1,
                    Bio = "Aiden, the 123rd Smith to enter our world, or so it seems."
                },
                new Users
                {
                    Id = "3",
                    FirstName = "Ada",
                    LastName = "Medina",
                    UserName = "unicorn98",
                    RoleId = 1,
                    Bio = "Ada, a dreamer and believer in all things magical, including unicorns."
                },
                new Users
                {
                    Id = "4",
                    FirstName = "Maria",
                    LastName = "Conner",
                    UserName = "mconner",
                    RoleId = 1,
                    Bio = "Maria, just a simple mconner, nothing else."
                },
                new Users
                {
                    Id = "5",
                    FirstName = "Amelia",
                    LastName = "Kim",
                    UserName = "ameliakim1987",
                    RoleId = 1,
                    Bio = "Amelia, born in the glorious year of 1987 and still shining."
                },
                new Users
                {
                    Id = "6",
                    FirstName = "Adah",
                    LastName = "Langley",
                    UserName = "Aarushi01",
                    RoleId = 1,
                    Bio = "Adah, a lover of unique names, just like her own."
                },
                new Users
                {
                    Id = "7",
                    FirstName = "Braydon",
                    LastName = "Moore",
                    UserName = "nutritious_strawberries",
                    RoleId = 1,
                    Bio = "Braydon, who believes that strawberries are not only tasty but also nutritious!"
                },
                new Users
                {
                    Id = "8",
                    FirstName = "Priscilla",
                    LastName = "Erickson",
                    UserName = "mookie13",
                    RoleId = 1,
                    Bio = "Priscilla, who's been known as mookie13 for reasons only she truly knows."
                },
                new Users
                {
                    Id = "9",
                    FirstName = "Elizabeth",
                    LastName = "Pham",
                    UserName = "Agreeable_Owl_31",
                    RoleId = 1,
                    Bio = "Elizabeth, an agreeable soul, who often sees wisdom, just like an owl."
                },
                new Users
                {
                    Id = "10",
                    FirstName = "Elijah",
                    LastName = "Harris",
                    UserName = "Crazy_Eagle46",
                    RoleId = 1,
                    Bio = "Elijah, who believes he's soaring high, just like a crazy eagle!"
                },
                new Users
                {
                    Id = "11",
                    FirstName = "Michael",
                    LastName = "Jones",
                    UserName = "Mj123",
                    RoleId = 1,
                    Bio = "Michael, who goes by Mj123, nothing more nothing less."
                },
                new Users
                {
                    Id = "12",
                    FirstName = "Amelia",
                    LastName = "Johnson",
                    UserName = "AmeliaJ",
                    RoleId = 1,
                    Bio = "Amelia, simply known as AmeliaJ, a name that holds her spirit."
                },
                new Users
                {
                    Id = "13",
                    FirstName = "Liam",
                    LastName = "Williams",
                    UserName = "Liam123",
                    RoleId = 1,
                    Bio = "Liam, a simple name that means a lot."
                },
                new Users
                {
                    Id = "14",
                    FirstName = "Alexia",
                    LastName = "Hernandez",
                    UserName = "SteepJay",
                    RoleId = 1,
                    Bio = "Alexia, the SteepJay of the world!"
                },
                new Users
                {
                    Id = "15",
                    FirstName = "Oliver",
                    LastName = "Whitehead",
                    UserName = "EarthDawn",
                    RoleId = 1,
                    Bio = "Oliver, always seeking the wonders of Earth Dawn."
                },
                new Users
                {
                    Id = "16",
                    FirstName = "Amanda",
                    LastName = "Johnson",
                    UserName = "amandajohnson123",
                    RoleId = 1,
                    Bio = "Amanda, who is just another Johnson in the vast world."
                },
                new Users
                {
                    Id = "17",
                    FirstName = "Hayden",
                    LastName = "Russo",
                    UserName = "HappyButterfly76",
                    RoleId = 1,
                    Bio = "Hayden, always spreading happiness, just like a happy butterfly."
                },
                new Users
                {
                    Id = "18",
                    FirstName = "Eleanor",
                    LastName = "Bolton",
                    UserName = "ebolton",
                    RoleId = 1,
                    Bio = "Eleanor, known to many as ebolton, a name she loves."
                },
                new Users
                {
                    Id = "19",
                    FirstName = "Edward",
                    LastName = "Owens",
                    UserName = "eowens53",
                    RoleId = 1,
                    Bio = "Edward, still living the spirit of 53."
                },
                new Users
                {
                    Id = "20",
                    FirstName = "Anna",
                    LastName = "Harris",
                    UserName = "anna_harris1996",
                    RoleId = 1,
                    Bio = "Anna, a soul that was born in the wonderful year of 1996 and never stopped shining."
                },
                new Users
                {
                    Id = "21",
                    FirstName = "Omar",
                    LastName = "Dejesus",
                    UserName = "CuddlyMuffin",
                    RoleId = 1,
                    Bio = "Omar, also known as CuddlyMuffin, for reasons unknown to many."
                },
                new Users
                {
                    Id = "22",
                    FirstName = "Sarah",
                    LastName = "Johnson",
                    UserName = "sarahj23",
                    RoleId = 1,
                    Bio = "Sarah, who is happy with the user name sarahj23."
                },
                new Users
                {
                    Id = "23",
                    FirstName = "Cody",
                    LastName = "Bailey",
                    UserName = "c.bailey69",
                    RoleId = 1,
                    Bio = "Cody, who may have made a very interesting choice with the user name c.bailey69."
                },
                new Users
                {
                    Id = "24",
                    FirstName = "Jack",
                    LastName = "Black",
                    UserName = "jblack",
                    RoleId = 1,
                    Bio = "Jack, who’s just a simple jblack, not much more."
                },
                new Users
                {
                    Id = "25",
                    FirstName = "Metin",
                    LastName = "Hikaye",
                    UserName = "sallyrooney39",
                    RoleId = 1,
                    Bio = "Metin, who, for reasons unknown, goes by sallyrooney39, a mystery of the digital age."
                },
                new Users
                {
                    Id = "26",
                    FirstName = "Tuncay",
                    LastName = "Taşkıran",
                    UserName = "Futbol",
                    RoleId = 1,
                    Bio = "Tuncay, a passionate lover of all things futbol."
                },
                new Users
                {
                    Id = "27",
                    FirstName = "Adnan",
                    LastName = "Kumandan",
                    UserName = "lordoglu",
                    RoleId = 1,
                    Bio = "Adnan, a lord in his own right, or so he calls himself."
                },
                new Users
                {
                    Id = "28",
                    FirstName = "Mustafa",
                    LastName = "Sandalye",
                    UserName = "mustii2024",
                    RoleId = 1,
                    Bio = "Mustafa, also known as mustii2024, as if the year had a meaning."
                },
                new Users
                {
                    Id = "29",
                    FirstName = "Adil",
                    LastName = "Salamura",
                    UserName = "tursuanime321",
                    RoleId = 1,
                    Bio = "Adil, a mysterious creature with a love for tursu and anime."
                },
                new Users
                {
                    Id = "30",
                    FirstName = "Tamer",
                    LastName = "Koltuk",
                    UserName = "dockhrr",
                    RoleId = 1,
                    Bio = "Tamer, a person who's user name is just a bunch of letters with no real meaning."
                },
                new Users
                {
                    Id = "31",
                    FirstName = "Berker",
                    LastName = "Gardırop",
                    UserName = "benkerr",
                    RoleId = 1,
                    Bio = "Berker, also known as benkerr, as if the two were one in the same."
                },
                new Users
                {
                    Id = "32",
                    FirstName = "Gökçe",
                    LastName = "Masa",
                    UserName = "sekai321",
                    RoleId = 1,
                    Bio = "Gökçe, who loves to travel the digital world, just like sekai."
                },
                new Users
                {
                    Id = "33",
                    FirstName = "Oğuzhan",
                    LastName = "Avize",
                    UserName = "gakimarp",
                    RoleId = 1,
                    Bio = "Oğuzhan, a simple man with a very complicated username."
                },
                new Users
                {
                    Id = "34",
                    FirstName = "Zeynep",
                    LastName = "Cam",
                    UserName = "zeyneo",
                    RoleId = 1,
                    Bio = "Zeynep, a sweet soul known to her friends as zeyneo."
                },
                new Users
                {
                    Id = "35",
                    FirstName = "Nicholas",
                    LastName = "Garza",
                    UserName = "Rattling_Cymbal301",
                    RoleId = 1,
                    Bio = "Nicholas, always bringing the rhythm, just like a rattling cymbal."
                },
                new Users
                {
                    Id = "36",
                    FirstName = "Aurora",
                    LastName = "McLaughlin",
                    UserName = "goldensunrise23",
                    RoleId = 1,
                    Bio = "Aurora, whose energy is as bright as the golden sunrise."
                },
                new Users
                {
                    Id = "37",
                    FirstName = "Antler",
                    LastName = "Hawaii",
                    UserName = "Salamura",
                    RoleId = 1,
                    Bio = "Antler, the one and only in Hawaii, with a love for Salamura."
                },
                new Users
                {
                    Id = "38",
                    FirstName = "Amelia",
                    LastName = "Roberts",
                    UserName = "ProudBird456",
                    RoleId = 1,
                    Bio = "Amelia, a proud soul, and lover of birds."
                },
                new Users
                {
                    Id = "39",
                    FirstName = "Ethan",
                    LastName = "Murray",
                    UserName = "TemptingLunch321",
                    RoleId = 1,
                    Bio = "Ethan, who is always thinking about his next tempting lunch."
                },
                new Users
                {
                    Id = "40",
                    FirstName = "Grant",
                    LastName = "Koffer",
                    UserName = "Joltik",
                    RoleId = 1,
                    Bio = "Grant, a person who's always ready to jolt into action."
                },
                new Users
                {
                    Id = "41",
                    FirstName = "Leila",
                    LastName = "Patel",
                    UserName = "LPatel123",
                    RoleId = 1,
                    Bio = "Leila, also known as LPatel123, a unique person for all of her unique ideas."
                },
                new Users
                {
                    Id = "42",
                    FirstName = "Oliver",
                    LastName = "Larson",
                    UserName = "ocelot_tiger34",
                    RoleId = 1,
                    Bio = "Oliver, a complex individual with the spirit of an ocelot and a tiger."
                },
                new Users
                {
                    Id = "43",
                    FirstName = "Kayden",
                    LastName = "Hinton",
                    UserName = "goldenrose77",
                    RoleId = 1,
                    Bio = "Kayden, who blooms in beauty and grace, just like a golden rose."
                },
                new Users
                {
                    Id = "44",
                    FirstName = "Catherine",
                    LastName = "Schiller",
                    UserName = "unknown_cat22",
                    RoleId = 1,
                    Bio = "Catherine, who is a mystery, just like her username suggests."
                },
                new Users
                {
                    Id = "45",
                    FirstName = "Samuel",
                    LastName = "Coleman",
                    UserName = "spoonedDew25",
                    RoleId = 1,
                    Bio = "Samuel, who has a very strange username, and no explanation for it."
                },
                new Users
                {
                    Id = "46",
                    FirstName = "İrfan",
                    LastName = "Hakan",
                    UserName = "hakanto",
                    RoleId = 1,
                    Bio = "İrfan, who uses hakanto for many things."
                },
                new Users
                {
                    Id = "47",
                    FirstName = "Ali",
                    LastName = "Yaman",
                    UserName = "zedmain123",
                    RoleId = 1,
                    Bio = "Ali, a person with a love for the character Zed, and 123."
                },
                new Users
                {
                    Id = "48",
                    FirstName = "Bruce",
                    LastName = "Ramirez",
                    UserName = "b3729301",
                    RoleId = 1,
                    Bio = "Bruce, whose username seems to be nothing more than a random collection of numbers and letters."
                },
                new Users
                {
                    Id = "49",
                    FirstName = "Sophia",
                    LastName = "Larson",
                    UserName = "sleepykangaroo93",
                    RoleId = 1,
                    Bio = "Sophia, who is just as sleepy and loves kangaroos."
                },
                new Users
                {
                    Id = "50",
                    FirstName = "Dylan",
                    LastName = "Fisher",
                    UserName = "LovelyLove52",
                    RoleId = 1,
                    Bio = "Dylan, who loves with all his heart, and especially the number 52."
                },
                new Users
                {
                    Id = "51",
                    FirstName = "Gabrielle",
                    LastName = "Rau",
                    UserName = "gabi.rau66",
                    RoleId = 1,
                    Bio = "Gabrielle, who likes to keep things simple, just like gabi.rau66"
                },
                new Users
                {
                    Id = "52",
                    FirstName = "Mia",
                    LastName = "Hansen",
                    UserName = "HappyPanda845",
                    RoleId = 1,
                    Bio = "Mia, a fun loving individual with a love for all things panda."
                },
                new Users
                {
                    Id = "53",
                    FirstName = "Brandon",
                    LastName = "McLaughlin",
                    UserName = "RelievedPizza981",
                    RoleId = 1,
                    Bio = "Brandon, a person who is always relieved to eat pizza. The number 981 is still a mystery."
                },
                new Users
                {
                    Id = "54",
                    FirstName = "Frank",
                    LastName = "Morris",
                    UserName = "fmorris01",
                    RoleId = 1,
                    Bio = "Frank, who uses fmorris01 for reasons only he truly knows."
                },
                new Users
                {
                    Id = "55",
                    FirstName = "Wyatt",
                    LastName = "Lyons",
                    UserName = "wyattlyons07",
                    RoleId = 1,
                    Bio = "Wyatt, who was born in the year 07."
                },
                new Users
                {
                    Id = "56",
                    FirstName = "Rhonda",
                    LastName = "Jackson",
                    UserName = "glowing_guitar_930",
                    RoleId = 1,
                    Bio = "Rhonda, who’s known for her glowing personality and talent with guitars."
                },
                new Users
                {
                    Id = "57",
                    FirstName = "Emma",
                    LastName = "Garcia",
                    UserName = "emma_garcia234",
                    RoleId = 1,
                    Bio = "Emma, who is just happy to have the user name emma_garcia234."
                },
                new Users
                {
                    Id = "58",
                    FirstName = "Isabella",
                    LastName = "King",
                    UserName = "goldenpig645",
                    RoleId = 1,
                    Bio = "Isabella, a name that suits her kind nature, a golden soul."
                },
                new Users
                {
                    Id = "59",
                    FirstName = "Celeste",
                    LastName = "Koch",
                    UserName = "esteekoch678",
                    RoleId = 1,
                    Bio = "Celeste, an estee koch to all of her friends."
                },
                new Users
                {
                    Id = "60",
                    FirstName = "Mary",
                    LastName = "Olsen",
                    UserName = "Heaven328",
                    RoleId = 1,
                    Bio = "Mary, who is as close to heaven as you can get."
                },
                new Users
                {
                    Id = "61",
                    FirstName = "Savannah",
                    LastName = "Walker",
                    UserName = "SavvyWalker1973",
                    RoleId = 1,
                    Bio = "Savannah, born in the year of 1973, who is very savvy in everything she does."
                },
                new Users
                {
                    Id = "62",
                    FirstName = "Rex",
                    LastName = "Christiansen",
                    UserName = "RChr0321",
                    RoleId = 1,
                    Bio = "Rex, the one and only RChr0321 in our universe."
                },
                new Users
                {
                    Id = "63",
                    FirstName = "Mohammad",
                    LastName = "Lynch",
                    UserName = "mohdlynch614",
                    RoleId = 1,
                    Bio = "Mohammad, who was born on June 14th."
                },
                new Users
                {
                    Id = "64",
                    FirstName = "Jack",
                    LastName = "Williams",
                    UserName = "jackwil123",
                    RoleId = 1,
                    Bio = "Jack, who uses the user name jackwil123."
                },
                new Users
                {
                    Id = "65",
                    FirstName = "Aiden",
                    LastName = "Smith",
                    UserName = "as9876",
                    RoleId = 1,
                    Bio = "Aiden, who has the interesting username of as9876."
                },
                new Users
                {
                    Id = "66",
                    FirstName = "Emily",
                    LastName = "Flores",
                    UserName = "LilTiger7523",
                    RoleId = 1,
                    Bio = "Emily, who has a fiery spirit, just like a lil' tiger!"
                },
                new Users
                {
                    Id = "67",
                    FirstName = "Joseph",
                    LastName = "O'Hara",
                    UserName = "bold111",
                    RoleId = 1,
                    Bio = "Joseph, a bold soul with triple ones."
                },
                new Users
                {
                    Id = "68",
                    FirstName = "Sandrah",
                    LastName = "Rios",
                    UserName = "sandrahrios743",
                    RoleId = 1,
                    Bio = "Sandrah, who likes her username."
                },
                new Users
                {
                    Id = "69",
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "jdoe123",
                    RoleId = 1,
                    Bio = "John, who goes by jdoe123, just a simple soul!"
                },
                new Users
                {
                    Id = "70",
                    FirstName = "Ethan",
                    LastName = "Tran",
                    UserName = "ethtran",
                    RoleId = 1,
                    Bio = "Ethan, who is known as ethtran by many of his friends."
                },
                new Users
                {
                    Id = "71",
                    FirstName = "Catherine",
                    LastName = "Schiller",
                    UserName = "unknown_cat22",
                    RoleId = 1,
                    Bio = "Catherine, a mystery wrapped in an enigma and covered in fur."
                },
                new Users
                {
                    Id = "72",
                    FirstName = "Ava",
                    LastName = "Simmons",
                    UserName = "ava.simmons96",
                    RoleId = 1,
                    Bio = "Ava, a 1996 kid, known by ava.simmons96."
                },
                new Users
                {
                    Id = "73",
                    FirstName = "Ahmet",
                    LastName = "Yılmaz",
                    UserName = "ahmetyilmaz",
                    RoleId = 1,
                    Bio = "Ahmet, always known as ahmetyilmaz to his friends."
                },
                new Users
                {
                    Id = "74",
                    FirstName = "Ayşe",
                    LastName = "Öztürk",
                    UserName = "ayseoz",
                    RoleId = 1,
                    Bio = "Ayşe, fondly called ayseoz by many."
                },
                new Users
                {
                    Id = "75",
                    FirstName = "Mehmet",
                    LastName = "Kaya",
                    UserName = "mkaya123",
                    RoleId = 1,
                    Bio = "Mehmet, a simple man with a user name that includes 123."
                },
                new Users
                {
                    Id = "76",
                    FirstName = "Fatma",
                    LastName = "Demir",
                    UserName = "fatmademir",
                    RoleId = 1,
                    Bio = "Fatma, with a user name that is as simple as they get."
                },
                new Users
                {
                    Id = "77",
                    FirstName = "Mustafa",
                    LastName = "Çelik",
                    UserName = "mcelik34",
                    RoleId = 1,
                    Bio = "Mustafa, always going by mcelik34."
                },
                new Users
                {
                    Id = "78",
                    FirstName = "Zeynep",
                    LastName = "Şahin",
                    UserName = "zeynepsahin",
                    RoleId = 1,
                    Bio = "Zeynep, who likes to keep things simple."
                },
                new Users
                {
                    Id = "79",
                    FirstName = "İbrahim",
                    LastName = "Arslan",
                    UserName = "ibrahimars",
                    RoleId = 1,
                    Bio = "İbrahim, is often called ibrahimars."
                },
                new Users
                {
                    Id = "80",
                    FirstName = "Emine",
                    LastName = "Yıldız",
                    UserName = "emineyildiz",
                    RoleId = 1,
                    Bio = "Emine, who was born a star."
                },
                new Users
                {
                    Id = "81",
                    FirstName = "Osman",
                    LastName = "Aydın",
                    UserName = "osmanaydin",
                    RoleId = 1,
                    Bio = "Osman, also known as osmanaydin."
                },
                new Users
                {
                    Id = "82",
                    FirstName = "Elif",
                    LastName = "Erdoğan",
                    UserName = "eliferd",
                    RoleId = 1,
                    Bio = "Elif, the one and only eliferd, and she wouldn’t have it any other way."
                },
                new Users
                {
                    Id = "83",
                    FirstName = "Hüseyin",
                    LastName = "Özdemir",
                    UserName = "hozdemir",
                    RoleId = 1,
                    Bio = "Hüseyin, a person known to others as hozdemir."
                },
                new Users
                {
                    Id = "84",
                    FirstName = "Hatice",
                    LastName = "Korkmaz",
                    UserName = "hatice_k",
                    RoleId = 1,
                    Bio = "Hatice, who prefers her user name hatice_k, a secret she has."
                },
                new Users
                {
                    Id = "85",
                    FirstName = "Murat",
                    LastName = "Çetin",
                    UserName = "muratcetin",
                    RoleId = 1,
                    Bio = "Murat, goes by muratcetin in all of his social medias."
                },
                new Users
                {
                    Id = "86",
                    FirstName = "Esra",
                    LastName = "Koç",
                    UserName = "esrakoc",
                    RoleId = 1,
                    Bio = "Esra, who keeps it simple with the name esrakoc."
                },
                new Users
                {
                    Id = "87",
                    FirstName = "Ali",
                    LastName = "Güneş",
                    UserName = "aligunes",
                    RoleId = 1,
                    Bio = "Ali, who’s as bright as the sun, just like guneş in his last name."
                },
                new Users
                {
                    Id = "88",
                    FirstName = "Selin",
                    LastName = "Yalçın",
                    UserName = "selinyalcin",
                    RoleId = 1,
                    Bio = "Selin, known as selinyalcin to many of her friends."
                },
                new Users
                {
                    Id = "89",
                    FirstName = "Burak",
                    LastName = "Doğan",
                    UserName = "burakd",
                    RoleId = 1,
                    Bio = "Burak, a simple guy with the user name burakd."
                },
                new Users
                {
                    Id = "90",
                    FirstName = "Merve",
                    LastName = "Kurt",
                    UserName = "mervekurt",
                    RoleId = 1,
                    Bio = "Merve, whose user name is as sweet as she is."
                },
                new Users
                {
                    Id = "91",
                    FirstName = "Emre",
                    LastName = "Şen",
                    UserName = "emresen",
                    RoleId = 1,
                    Bio = "Emre, who is always happy, just like şen in his name."
                },
                new Users
                {
                    Id = "92",
                    FirstName = "Gökce",
                    LastName = "Akca",
                    UserName = "Gökce",
                    RoleId = 1,
                    Bio = "I am Gökce, nice to meat you ;)"
                },
                new Users
                {
                    Id = "93",
                    FirstName = "Berker",
                    LastName = "Bayar",
                    UserName = "Berker",
                    RoleId = 1,
                    Bio = "Hi I am berker and I also eat food!"
                },
                new Users
                {
                    Id = "94",
                    FirstName = "Adil",
                    LastName = "Oğuz",
                    UserName = "adilOguz",
                    RoleId = 1,
                    Bio = "I am a developer that sometimes require sustenance, so I am here!"
                },
                new Users
                {
                    Id = "95",
                    FirstName = "Adnan",
                    LastName = "Octa",
                    UserName = "OctagonalAdnagonal",
                    RoleId = 1,
                    Bio = "I like catan, board games and Adil!"
                },
                new Users
                {
                    Id = "96",
                    FirstName = "Volkan",
                    LastName = "Kara",
                    UserName = "volkankara",
                    RoleId = 1,
                    Bio = "Volkan, who is as strong and powerful as a volcano, just like the name Volkan."
                },
                new Users
                {
                    Id = "97",
                    FirstName = "Tamer",
                    LastName = "Kucukel",
                    UserName = "kucukel20",
                    RoleId = 1,
                    Bio = "Contrary to popular belief, my hands are average sized."
                },
                new Users
                {
                    Id = "98",
                    FirstName = "Oguzhan",
                    LastName = "Aybar",
                    UserName = "aybar20",
                    RoleId = 1,
                    Bio = "Enginar Sufle is my favourite!"
                },
                new Users
                {
                    Id = "99",
                    FirstName = "Zeynep",
                    LastName = "Kara",
                    UserName = "karaz20",
                    RoleId = 1,
                    Bio = "I like to be mad!"
                },
                new Users
                {
                    Id = "100",
                    FirstName = "Mustafa",
                    LastName = "Kırcı",
                    UserName = "kirci20",
                    RoleId = 1,
                    Bio = "Hi I am mustafa, I am looking for a recipe to spend my life with!"
                }
                );

        }
    }
}
