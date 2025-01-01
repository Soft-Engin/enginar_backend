using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CityCountryDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_IngredientTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "IngredientTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    PostCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients_Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    PreferenceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients_Preferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Preferences_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredients_Preferences_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DistrictId = table.Column<int>(type: "integer", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<int>(type: "integer", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: true),
                    BannerImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    ProfileImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorId = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    BodyText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Header = table.Column<string>(type: "text", nullable: false),
                    BodyText = table.Column<string>(type: "text", nullable: false),
                    BannerImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    StepImages = table.Column<byte[][]>(type: "bytea[]", nullable: true),
                    ServingSize = table.Column<int>(type: "integer", nullable: false),
                    PreparationTime = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Steps = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Allergens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    PreferenceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Allergens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Allergens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Allergens_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users_Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InitiatorUserId = table.Column<string>(type: "text", nullable: false),
                    TargetUserId = table.Column<string>(type: "text", nullable: false),
                    InteractionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Interactions_AspNetUsers_InitiatorUserId",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Interactions_AspNetUsers_TargetUserId",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Interactions_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events_Requirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    RequirementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events_Requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Requirements_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Requirements_Requirements_RequirementId",
                        column: x => x.RequirementId,
                        principalTable: "Requirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Event_Participations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Event_Participations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Event_Participations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Event_Participations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Header = table.Column<string>(type: "text", nullable: false),
                    BodyText = table.Column<string>(type: "text", nullable: false),
                    BannerImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    RecipeId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blogs_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Bookmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Bookmarks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipe_Bookmarks_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    CommentText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Images = table.Column<byte[][]>(type: "bytea[]", nullable: true),
                    ImagesCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipe_Comments_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipe_Likes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes_Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Ingredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_Recipes_Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    InteractionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Recipes_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Recipes_Interactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Recipes_Interactions_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Recipes_Interactions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blog_Bookmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Bookmarks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_Bookmarks_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blog_Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    CommentText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Images = table.Column<byte[][]>(type: "bytea[]", nullable: true),
                    ImagesCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_Comments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blog_Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_Likes_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users_Blogs_Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    InteractionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Blogs_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Blogs_Interactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Blogs_Interactions_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Blogs_Interactions_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Turkey" });

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Edible plants or their parts, intended for cooking or eating raw.", "Vegetable" },
                    { 2, "Sweet or savory product of a plant that contains seeds and can be eaten as food.", "Fruit" },
                    { 3, "Animal flesh that is eaten as food.", "Meat" },
                    { 4, "Food produced from or containing the milk of mammals.", "Dairy" },
                    { 5, "Small, hard, dry seeds harvested for human or animal consumption.", "Grain" },
                    { 6, "Sea life regarded as food by humans, includes fish and shellfish.", "Seafood" },
                    { 7, "Substance used to flavor food, typically dried seeds, fruits, roots, or bark.", "Spice" },
                    { 8, "Plants with savory or aromatic properties used for flavoring and garnishing food.", "Herb" },
                    { 9, "Dry, edible fruits or seeds that usually have a high fat content.", "Nuts & Seeds" },
                    { 10, "Drinkable liquids other than water, may be hot or cold.", "Beverage" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "ADANA" },
                    { 2, 1, "ADIYAMAN" },
                    { 3, 1, "AFYONKARAHİSAR" },
                    { 4, 1, "AĞRI" },
                    { 5, 1, "AMASYA" },
                    { 6, 1, "ANKARA" },
                    { 7, 1, "ANTALYA" },
                    { 8, 1, "ARTVİN" },
                    { 9, 1, "AYDIN" },
                    { 10, 1, "BALIKESİR" },
                    { 11, 1, "BİLECİK" },
                    { 12, 1, "BİNGÖL" },
                    { 13, 1, "BİTLİS" },
                    { 14, 1, "BOLU" },
                    { 15, 1, "BURDUR" },
                    { 16, 1, "BURSA" },
                    { 17, 1, "ÇANAKKALE" },
                    { 18, 1, "ÇANKIRI" },
                    { 19, 1, "ÇORUM" },
                    { 20, 1, "DENİZLİ" },
                    { 21, 1, "DİYARBAKIR" },
                    { 22, 1, "EDİRNE" },
                    { 23, 1, "ELAZIĞ" },
                    { 24, 1, "ERZİNCAN" },
                    { 25, 1, "ERZURUM" },
                    { 26, 1, "ESKİŞEHİR" },
                    { 27, 1, "GAZİANTEP" },
                    { 28, 1, "GİRESUN" },
                    { 29, 1, "GÜMÜŞHANE" },
                    { 30, 1, "HAKKARİ" },
                    { 31, 1, "HATAY" },
                    { 32, 1, "ISPARTA" },
                    { 33, 1, "MERSİN" },
                    { 34, 1, "İSTANBUL" },
                    { 35, 1, "İZMİR" },
                    { 36, 1, "KARS" },
                    { 37, 1, "KASTAMONU" },
                    { 38, 1, "KAYSERİ" },
                    { 39, 1, "KIRKLARELİ" },
                    { 40, 1, "KIRŞEHİR" },
                    { 41, 1, "KOCAELİ" },
                    { 42, 1, "KONYA" },
                    { 43, 1, "KÜTAHYA" },
                    { 44, 1, "MALATYA" },
                    { 45, 1, "MANİSA" },
                    { 46, 1, "KAHRAMANMARAŞ" },
                    { 47, 1, "MARDİN" },
                    { 48, 1, "MUĞLA" },
                    { 49, 1, "MUŞ" },
                    { 50, 1, "NEVŞEHİR" },
                    { 51, 1, "NİĞDE" },
                    { 52, 1, "ORDU" },
                    { 53, 1, "RİZE" },
                    { 54, 1, "SAKARYA" },
                    { 55, 1, "SAMSUN" },
                    { 56, 1, "SİİRT" },
                    { 57, 1, "SİNOP" },
                    { 58, 1, "SİVAS" },
                    { 59, 1, "TEKİRDAĞ" },
                    { 60, 1, "TOKAT" },
                    { 61, 1, "TRABZON" },
                    { 62, 1, "TUNCELİ" },
                    { 63, 1, "ŞANLIURFA" },
                    { 64, 1, "UŞAK" },
                    { 65, 1, "VAN" },
                    { 66, 1, "YOZGAT" },
                    { 67, 1, "ZONGULDAK" },
                    { 68, 1, "AKSARAY" },
                    { 69, 1, "BAYBURT" },
                    { 70, 1, "KARAMAN" },
                    { 71, 1, "KIRIKKALE" },
                    { 72, 1, "BATMAN" },
                    { 73, 1, "ŞIRNAK" },
                    { 74, 1, "BARTIN" },
                    { 75, 1, "ARDAHAN" },
                    { 76, 1, "IĞDIR" },
                    { 77, 1, "YALOVA" },
                    { 78, 1, "KARABÜK" },
                    { 79, 1, "KİLİS" },
                    { 80, 1, "OSMANİYE" },
                    { 81, 1, "DÜZCE" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CityId", "Name", "PostCode" },
                values: new object[,]
                {
                    { 1, 1, "ALADAĞ", 1757 },
                    { 2, 1, "CEYHAN", 1219 },
                    { 3, 1, "ÇUKUROVA", 2033 },
                    { 4, 1, "FEKE", 1329 },
                    { 5, 1, "İMAMOĞLU", 1806 },
                    { 6, 1, "KARAİSALI", 1437 },
                    { 7, 1, "KARATAŞ", 1443 },
                    { 8, 1, "KOZAN", 1486 },
                    { 9, 1, "POZANTI", 1580 },
                    { 10, 1, "SAİMBEYLİ", 1588 },
                    { 11, 1, "SARIÇAM", 2032 },
                    { 12, 1, "SEYHAN", 1104 },
                    { 13, 1, "TUFANBEYLİ", 1687 },
                    { 14, 1, "YUMURTALIK", 1734 },
                    { 15, 1, "YÜREĞİR", 1748 },
                    { 16, 2, "BESNİ", 1182 },
                    { 17, 2, "ÇELİKHAN", 1246 },
                    { 18, 2, "GERGER", 1347 },
                    { 19, 2, "GÖLBAŞI", 1354 },
                    { 20, 2, "KAHTA", 1425 },
                    { 21, 2, "SAMSAT", 1592 },
                    { 22, 2, "SİNCİK", 1985 },
                    { 23, 2, "TUT", 1989 },
                    { 24, 3, "BAŞMAKÇI", 1771 },
                    { 25, 3, "BAYAT", 1773 },
                    { 26, 3, "BOLVADİN", 1200 },
                    { 27, 3, "ÇAY", 1239 },
                    { 28, 3, "ÇOBANLAR", 1906 },
                    { 29, 3, "DAZKIRI", 1267 },
                    { 30, 3, "DİNAR", 1281 },
                    { 31, 3, "EMİRDAĞ", 1306 },
                    { 32, 3, "EVCİLER", 1923 },
                    { 33, 3, "HOCALAR", 1944 },
                    { 34, 3, "İHSANİYE", 1404 },
                    { 35, 3, "İSCEHİSAR", 1809 },
                    { 36, 3, "KIZILÖREN", 1961 },
                    { 37, 3, "SANDIKLI", 1594 },
                    { 38, 3, "SİNANPAŞA", 1626 },
                    { 39, 3, "SULTANDAĞI", 1639 },
                    { 40, 3, "ŞUHUT", 1664 },
                    { 41, 4, "DİYADİN", 1283 },
                    { 42, 4, "DOĞUBAYAZIT", 1287 },
                    { 43, 4, "ELEŞKİRT", 1301 },
                    { 44, 4, "HAMUR", 1379 },
                    { 45, 4, "PATNOS", 1568 },
                    { 46, 4, "TAŞLIÇAY", 1667 },
                    { 47, 4, "TUTAK", 1691 },
                    { 48, 5, "GÖYNÜCEK", 1363 },
                    { 49, 5, "GÜMÜŞHACIKÖY", 1368 },
                    { 50, 5, "HAMAMÖZÜ", 1938 },
                    { 51, 5, "MERZİFON", 1524 },
                    { 52, 5, "SULUOVA", 1641 },
                    { 53, 5, "TAŞOVA", 1668 },
                    { 54, 6, "AKYURT", 1872 },
                    { 55, 6, "ALTINDAĞ", 1130 },
                    { 56, 6, "AYAŞ", 1157 },
                    { 57, 6, "BALA", 1167 },
                    { 58, 6, "BEYPAZARI", 1187 },
                    { 59, 6, "ÇAMLIDERE", 1227 },
                    { 60, 6, "ÇANKAYA", 1231 },
                    { 61, 6, "ÇUBUK", 1260 },
                    { 62, 6, "ELMADAĞ", 1302 },
                    { 63, 6, "ETİMESGUT", 1922 },
                    { 64, 6, "EVREN", 1924 },
                    { 65, 6, "GÖLBAŞI", 1744 },
                    { 66, 6, "GÜDÜL", 1365 },
                    { 67, 6, "HAYMANA", 1387 },
                    { 68, 6, "KAHRAMANKAZAN", 1815 },
                    { 69, 6, "KALECİK", 1427 },
                    { 70, 6, "KEÇİÖREN", 1745 },
                    { 71, 6, "KIZILCAHAMAM", 1473 },
                    { 72, 6, "MAMAK", 1746 },
                    { 73, 6, "NALLIHAN", 1539 },
                    { 74, 6, "POLATLI", 1578 },
                    { 75, 6, "PURSAKLAR", 2034 },
                    { 76, 6, "SİNCAN", 1747 },
                    { 77, 6, "ŞEREFLİKOÇHİSAR", 1658 },
                    { 78, 6, "YENİMAHALLE", 1723 },
                    { 79, 7, "AKSEKİ", 1121 },
                    { 80, 7, "AKSU", 2035 },
                    { 81, 7, "ALANYA", 1126 },
                    { 82, 7, "DEMRE", 1811 },
                    { 83, 7, "DÖŞEMEALTI", 2036 },
                    { 84, 7, "ELMALI", 1303 },
                    { 85, 7, "FİNİKE", 1333 },
                    { 86, 7, "GAZİPAŞA", 1337 },
                    { 87, 7, "GÜNDOĞMUŞ", 1370 },
                    { 88, 7, "İBRADI", 1946 },
                    { 89, 7, "KAŞ", 1451 },
                    { 90, 7, "KEMER", 1959 },
                    { 91, 7, "KEPEZ", 2037 },
                    { 92, 7, "KONYAALTI", 2038 },
                    { 93, 7, "KORKUTELİ", 1483 },
                    { 94, 7, "KUMLUCA", 1492 },
                    { 95, 7, "MANAVGAT", 1512 },
                    { 96, 7, "MURATPAŞA", 2039 },
                    { 97, 7, "SERİK", 1616 },
                    { 98, 8, "ARDANUÇ", 1145 },
                    { 99, 8, "ARHAVİ", 1147 },
                    { 100, 8, "BORÇKA", 1202 },
                    { 101, 8, "HOPA", 1395 },
                    { 102, 8, "KEMALPAŞA", 2105 },
                    { 103, 8, "MURGUL", 1828 },
                    { 104, 8, "ŞAVŞAT", 1653 },
                    { 105, 8, "YUSUFELİ", 1736 },
                    { 106, 9, "BOZDOĞAN", 1206 },
                    { 107, 9, "BUHARKENT", 1781 },
                    { 108, 9, "ÇİNE", 1256 },
                    { 109, 9, "DİDİM", 2000 },
                    { 110, 9, "EFELER", 2076 },
                    { 111, 9, "GERMENCİK", 1348 },
                    { 112, 9, "İNCİRLİOVA", 1807 },
                    { 113, 9, "KARACASU", 1435 },
                    { 114, 9, "KARPUZLU", 1957 },
                    { 115, 9, "KOÇARLI", 1479 },
                    { 116, 9, "KÖŞK", 1968 },
                    { 117, 9, "KUŞADASI", 1497 },
                    { 118, 9, "KUYUCAK", 1498 },
                    { 119, 9, "NAZİLLİ", 1542 },
                    { 120, 9, "SÖKE", 1637 },
                    { 121, 9, "SULTANHİSAR", 1640 },
                    { 122, 9, "YENİPAZAR", 1724 },
                    { 123, 10, "ALTIEYLÜL", 2077 },
                    { 124, 10, "AYVALIK", 1161 },
                    { 125, 10, "BALYA", 1169 },
                    { 126, 10, "BANDIRMA", 1171 },
                    { 127, 10, "BİGADİÇ", 1191 },
                    { 128, 10, "BURHANİYE", 1216 },
                    { 129, 10, "DURSUNBEY", 1291 },
                    { 130, 10, "EDREMİT", 1294 },
                    { 131, 10, "ERDEK", 1310 },
                    { 132, 10, "GÖMEÇ", 1928 },
                    { 133, 10, "GÖNEN", 1360 },
                    { 134, 10, "HAVRAN", 1384 },
                    { 135, 10, "İVRİNDİ", 1418 },
                    { 136, 10, "KARESİ", 2078 },
                    { 137, 10, "KEPSUT", 1462 },
                    { 138, 10, "MANYAS", 1514 },
                    { 139, 10, "MARMARA", 1824 },
                    { 140, 10, "SAVAŞTEPE", 1608 },
                    { 141, 10, "SINDIRGI", 1619 },
                    { 142, 10, "SUSURLUK", 1644 },
                    { 143, 11, "BOZÜYÜK", 1210 },
                    { 144, 11, "GÖLPAZARI", 1359 },
                    { 145, 11, "İNHİSAR", 1948 },
                    { 146, 11, "OSMANELİ", 1559 },
                    { 147, 11, "PAZARYERİ", 1571 },
                    { 148, 11, "SÖĞÜT", 1636 },
                    { 149, 11, "YENİPAZAR", 1857 },
                    { 150, 12, "ADAKLI", 1750 },
                    { 151, 12, "GENÇ", 1344 },
                    { 152, 12, "KARLIOVA", 1446 },
                    { 153, 12, "KİĞI", 1475 },
                    { 154, 12, "SOLHAN", 1633 },
                    { 155, 12, "YAYLADERE", 1855 },
                    { 156, 12, "YEDİSU", 1996 },
                    { 157, 13, "ADİLCEVAZ", 1106 },
                    { 158, 13, "AHLAT", 1112 },
                    { 159, 13, "GÜROYMAK", 1798 },
                    { 160, 13, "HİZAN", 1394 },
                    { 161, 13, "MUTKİ", 1537 },
                    { 162, 13, "TATVAN", 1669 },
                    { 163, 14, "DÖRTDİVAN", 1916 },
                    { 164, 14, "GEREDE", 1346 },
                    { 165, 14, "GÖYNÜK", 1364 },
                    { 166, 14, "KIBRISCIK", 1466 },
                    { 167, 14, "MENGEN", 1522 },
                    { 168, 14, "MUDURNU", 1531 },
                    { 169, 14, "SEBEN", 1610 },
                    { 170, 14, "YENİÇAĞA", 1997 },
                    { 171, 15, "AĞLASUN", 1109 },
                    { 172, 15, "ALTINYAYLA", 1874 },
                    { 173, 15, "BUCAK", 1211 },
                    { 174, 15, "ÇAVDIR", 1899 },
                    { 175, 15, "ÇELTİKÇİ", 1903 },
                    { 176, 15, "GÖLHİSAR", 1357 },
                    { 177, 15, "KARAMANLI", 1813 },
                    { 178, 15, "KEMER", 1816 },
                    { 179, 15, "TEFENNİ", 1672 },
                    { 180, 15, "YEŞİLOVA", 1728 },
                    { 181, 16, "BÜYÜKORHAN", 1783 },
                    { 182, 16, "GEMLİK", 1343 },
                    { 183, 16, "GÜRSU", 1935 },
                    { 184, 16, "HARMANCIK", 1799 },
                    { 185, 16, "İNEGÖL", 1411 },
                    { 186, 16, "İZNİK", 1420 },
                    { 187, 16, "KARACABEY", 1434 },
                    { 188, 16, "KELES", 1457 },
                    { 189, 16, "KESTEL", 1960 },
                    { 190, 16, "MUDANYA", 1530 },
                    { 191, 16, "MUSTAFAKEMALPAŞA", 1535 },
                    { 192, 16, "NİLÜFER", 1829 },
                    { 193, 16, "ORHANELİ", 1553 },
                    { 194, 16, "ORHANGAZİ", 1554 },
                    { 195, 16, "OSMANGAZİ", 1832 },
                    { 196, 16, "YENİŞEHİR", 1725 },
                    { 197, 16, "YILDIRIM", 1859 },
                    { 198, 17, "AYVACIK", 1160 },
                    { 199, 17, "BAYRAMİÇ", 1180 },
                    { 200, 17, "BİGA", 1190 },
                    { 201, 17, "BOZCAADA", 1205 },
                    { 202, 17, "ÇAN", 1229 },
                    { 203, 17, "ECEABAT", 1293 },
                    { 204, 17, "EZİNE", 1326 },
                    { 205, 17, "GELİBOLU", 1340 },
                    { 206, 17, "GÖKÇEADA", 1408 },
                    { 207, 17, "LAPSEKİ", 1503 },
                    { 208, 17, "YENİCE", 1722 },
                    { 209, 18, "ATKARACALAR", 1765 },
                    { 210, 18, "BAYRAMÖREN", 1885 },
                    { 211, 18, "ÇERKEŞ", 1248 },
                    { 212, 18, "ELDİVAN", 1300 },
                    { 213, 18, "ILGAZ", 1399 },
                    { 214, 18, "KIZILIRMAK", 1817 },
                    { 215, 18, "KORGUN", 1963 },
                    { 216, 18, "KURŞUNLU", 1494 },
                    { 217, 18, "ORTA", 1555 },
                    { 218, 18, "ŞABANÖZÜ", 1649 },
                    { 219, 18, "YAPRAKLI", 1718 },
                    { 220, 19, "ALACA", 1124 },
                    { 221, 19, "BAYAT", 1177 },
                    { 222, 19, "BOĞAZKALE", 1778 },
                    { 223, 19, "DODURGA", 1911 },
                    { 224, 19, "İSKİLİP", 1414 },
                    { 225, 19, "KARGI", 1445 },
                    { 226, 19, "LAÇİN", 1972 },
                    { 227, 19, "MECİTÖZÜ", 1520 },
                    { 228, 19, "OĞUZLAR", 1976 },
                    { 229, 19, "ORTAKÖY", 1556 },
                    { 230, 19, "OSMANCIK", 1558 },
                    { 231, 19, "SUNGURLU", 1642 },
                    { 232, 19, "UĞURLUDAĞ", 1850 },
                    { 233, 20, "ACIPAYAM", 1102 },
                    { 234, 20, "BABADAĞ", 1769 },
                    { 235, 20, "BAKLAN", 1881 },
                    { 236, 20, "BEKİLLİ", 1774 },
                    { 237, 20, "BEYAĞAÇ", 1888 },
                    { 238, 20, "BOZKURT", 1889 },
                    { 239, 20, "BULDAN", 1214 },
                    { 240, 20, "ÇAL", 1224 },
                    { 241, 20, "ÇAMELİ", 1226 },
                    { 242, 20, "ÇARDAK", 1233 },
                    { 243, 20, "ÇİVRİL", 1257 },
                    { 244, 20, "GÜNEY", 1371 },
                    { 245, 20, "HONAZ", 1803 },
                    { 246, 20, "KALE", 1426 },
                    { 247, 20, "MERKEZEFENDİ", 2079 },
                    { 248, 20, "PAMUKKALE", 1871 },
                    { 249, 20, "SARAYKÖY", 1597 },
                    { 250, 20, "SERİNHİSAR", 1840 },
                    { 251, 20, "TAVAS", 1670 },
                    { 252, 21, "BAĞLAR", 2040 },
                    { 253, 21, "BİSMİL", 1195 },
                    { 254, 21, "ÇERMİK", 1249 },
                    { 255, 21, "ÇINAR", 1253 },
                    { 256, 21, "ÇÜNGÜŞ", 1263 },
                    { 257, 21, "DİCLE", 1278 },
                    { 258, 21, "EĞİL", 1791 },
                    { 259, 21, "ERGANİ", 1315 },
                    { 260, 21, "HANİ", 1381 },
                    { 261, 21, "HAZRO", 1389 },
                    { 262, 21, "KAYAPINAR", 2041 },
                    { 263, 21, "KOCAKÖY", 1962 },
                    { 264, 21, "KULP", 1490 },
                    { 265, 21, "LİCE", 1504 },
                    { 266, 21, "SİLVAN", 1624 },
                    { 267, 21, "SUR", 2042 },
                    { 268, 21, "YENİŞEHİR", 2043 },
                    { 269, 22, "ENEZ", 1307 },
                    { 270, 22, "HAVSA", 1385 },
                    { 271, 22, "İPSALA", 1412 },
                    { 272, 22, "KEŞAN", 1464 },
                    { 273, 22, "LALAPAŞA", 1502 },
                    { 274, 22, "MERİÇ", 1523 },
                    { 275, 22, "SÜLOĞLU", 1988 },
                    { 276, 22, "UZUNKÖPRÜ", 1705 },
                    { 277, 23, "AĞIN", 1110 },
                    { 278, 23, "ALACAKAYA", 1873 },
                    { 279, 23, "ARICAK", 1762 },
                    { 280, 23, "BASKİL", 1173 },
                    { 281, 23, "KARAKOÇAN", 1438 },
                    { 282, 23, "KEBAN", 1455 },
                    { 283, 23, "KOVANCILAR", 1820 },
                    { 284, 23, "MADEN", 1506 },
                    { 285, 23, "PALU", 1566 },
                    { 286, 23, "SİVRİCE", 1631 },
                    { 287, 24, "ÇAYIRLI", 1243 },
                    { 288, 24, "İLİÇ", 1406 },
                    { 289, 24, "KEMAH", 1459 },
                    { 290, 24, "KEMALİYE", 1460 },
                    { 291, 24, "OTLUKBELİ", 1977 },
                    { 292, 24, "REFAHİYE", 1583 },
                    { 293, 24, "TERCAN", 1675 },
                    { 294, 24, "ÜZÜMLÜ", 1853 },
                    { 295, 25, "AŞKALE", 1153 },
                    { 296, 25, "AZİZİYE", 1945 },
                    { 297, 25, "ÇAT", 1235 },
                    { 298, 25, "HINIS", 1392 },
                    { 299, 25, "HORASAN", 1396 },
                    { 300, 25, "İSPİR", 1416 },
                    { 301, 25, "KARAÇOBAN", 1812 },
                    { 302, 25, "KARAYAZI", 1444 },
                    { 303, 25, "KÖPRÜKÖY", 1967 },
                    { 304, 25, "NARMAN", 1540 },
                    { 305, 25, "OLTU", 1550 },
                    { 306, 25, "OLUR", 1551 },
                    { 307, 25, "PALANDÖKEN", 2044 },
                    { 308, 25, "PASİNLER", 1567 },
                    { 309, 25, "PAZARYOLU", 1865 },
                    { 310, 25, "ŞENKAYA", 1657 },
                    { 311, 25, "TEKMAN", 1674 },
                    { 312, 25, "TORTUM", 1683 },
                    { 313, 25, "UZUNDERE", 1851 },
                    { 314, 25, "YAKUTİYE", 2045 },
                    { 315, 26, "ALPU", 1759 },
                    { 316, 26, "BEYLİKOVA", 1777 },
                    { 317, 26, "ÇİFTELER", 1255 },
                    { 318, 26, "GÜNYÜZÜ", 1934 },
                    { 319, 26, "HAN", 1939 },
                    { 320, 26, "İNÖNÜ", 1808 },
                    { 321, 26, "MAHMUDİYE", 1508 },
                    { 322, 26, "MİHALGAZİ", 1973 },
                    { 323, 26, "MİHALIÇÇIK", 1527 },
                    { 324, 26, "ODUNPAZARI", 2046 },
                    { 325, 26, "SARICAKAYA", 1599 },
                    { 326, 26, "SEYİTGAZİ", 1618 },
                    { 327, 26, "SİVRİHİSAR", 1632 },
                    { 328, 26, "TEPEBAŞI", 2047 },
                    { 329, 27, "ARABAN", 1139 },
                    { 330, 27, "İSLAHİYE", 1415 },
                    { 331, 27, "KARKAMIŞ", 1956 },
                    { 332, 27, "NİZİP", 1546 },
                    { 333, 27, "NURDAĞI", 1974 },
                    { 334, 27, "OĞUZELİ", 1549 },
                    { 335, 27, "ŞAHİNBEY", 1841 },
                    { 336, 27, "ŞEHİTKAMİL", 1844 },
                    { 337, 27, "YAVUZELİ", 1720 },
                    { 338, 28, "ALUCRA", 1133 },
                    { 339, 28, "BULANCAK", 1212 },
                    { 340, 28, "ÇAMOLUK", 1893 },
                    { 341, 28, "ÇANAKÇI", 1894 },
                    { 342, 28, "DERELİ", 1272 },
                    { 343, 28, "DOĞANKENT", 1912 },
                    { 344, 28, "ESPİYE", 1320 },
                    { 345, 28, "EYNESİL", 1324 },
                    { 346, 28, "GÖRELE", 1361 },
                    { 347, 28, "GÜCE", 1930 },
                    { 348, 28, "KEŞAP", 1465 },
                    { 349, 28, "PİRAZİZ", 1837 },
                    { 350, 28, "ŞEBİNKARAHİSAR", 1654 },
                    { 351, 28, "TİREBOLU", 1678 },
                    { 352, 28, "YAĞLIDERE", 1854 },
                    { 353, 29, "KELKİT", 1458 },
                    { 354, 29, "KÖSE", 1822 },
                    { 355, 29, "KÜRTÜN", 1971 },
                    { 356, 29, "ŞİRAN", 1660 },
                    { 357, 29, "TORUL", 1684 },
                    { 358, 30, "ÇUKURCA", 1261 },
                    { 359, 30, "DERECİK", 2107 },
                    { 360, 30, "ŞEMDİNLİ", 1656 },
                    { 361, 30, "YÜKSEKOVA", 1737 },
                    { 362, 31, "ALTINÖZÜ", 1131 },
                    { 363, 31, "ANTAKYA", 2080 },
                    { 364, 31, "ARSUZ", 2081 },
                    { 365, 31, "BELEN", 1887 },
                    { 366, 31, "DEFNE", 2082 },
                    { 367, 31, "DÖRTYOL", 1289 },
                    { 368, 31, "ERZİN", 1792 },
                    { 369, 31, "HASSA", 1382 },
                    { 370, 31, "İSKENDERUN", 1413 },
                    { 371, 31, "KIRIKHAN", 1468 },
                    { 372, 31, "KUMLU", 1970 },
                    { 373, 31, "PAYAS", 2083 },
                    { 374, 31, "REYHANLI", 1585 },
                    { 375, 31, "SAMANDAĞ", 1591 },
                    { 376, 31, "YAYLADAĞI", 1721 },
                    { 377, 32, "AKSU", 1755 },
                    { 378, 32, "ATABEY", 1154 },
                    { 379, 32, "EĞİRDİR", 1297 },
                    { 380, 32, "GELENDOST", 1341 },
                    { 381, 32, "GÖNEN", 1929 },
                    { 382, 32, "KEÇİBORLU", 1456 },
                    { 383, 32, "SENİRKENT", 1615 },
                    { 384, 32, "SÜTÇÜLER", 1648 },
                    { 385, 32, "ŞARKİKARAAĞAÇ", 1651 },
                    { 386, 32, "ULUBORLU", 1699 },
                    { 387, 32, "YALVAÇ", 1717 },
                    { 388, 32, "YENİŞARBADEMLİ", 2001 },
                    { 389, 33, "AKDENİZ", 2064 },
                    { 390, 33, "ANAMUR", 1135 },
                    { 391, 33, "AYDINCIK", 1766 },
                    { 392, 33, "BOZYAZI", 1779 },
                    { 393, 33, "ÇAMLIYAYLA", 1892 },
                    { 394, 33, "ERDEMLİ", 1311 },
                    { 395, 33, "GÜLNAR", 1366 },
                    { 396, 33, "MEZİTLİ", 2065 },
                    { 397, 33, "MUT", 1536 },
                    { 398, 33, "SİLİFKE", 1621 },
                    { 399, 33, "TARSUS", 1665 },
                    { 400, 33, "TOROSLAR", 2066 },
                    { 401, 33, "YENİŞEHİR", 2067 },
                    { 402, 34, "ADALAR", 1103 },
                    { 403, 34, "ARNAVUTKÖY", 2048 },
                    { 404, 34, "ATAŞEHİR", 2049 },
                    { 405, 34, "AVCILAR", 2003 },
                    { 406, 34, "BAĞCILAR", 2004 },
                    { 407, 34, "BAHÇELİEVLER", 2005 },
                    { 408, 34, "BAKIRKÖY", 1166 },
                    { 409, 34, "BAŞAKŞEHİR", 2050 },
                    { 410, 34, "BAYRAMPAŞA", 1886 },
                    { 411, 34, "BEŞİKTAŞ", 1183 },
                    { 412, 34, "BEYKOZ", 1185 },
                    { 413, 34, "BEYLİKDÜZÜ", 2051 },
                    { 414, 34, "BEYOĞLU", 1186 },
                    { 415, 34, "BÜYÜKÇEKMECE", 1782 },
                    { 416, 34, "ÇATALCA", 1237 },
                    { 417, 34, "ÇEKMEKÖY", 2052 },
                    { 418, 34, "ESENLER", 2016 },
                    { 419, 34, "ESENYURT", 2053 },
                    { 420, 34, "EYÜPSULTAN", 1325 },
                    { 421, 34, "FATİH", 1327 },
                    { 422, 34, "GAZİOSMANPAŞA", 1336 },
                    { 423, 34, "GÜNGÖREN", 2010 },
                    { 424, 34, "KADIKÖY", 1421 },
                    { 425, 34, "KAĞITHANE", 1810 },
                    { 426, 34, "KARTAL", 1449 },
                    { 427, 34, "KÜÇÜKÇEKMECE", 1823 },
                    { 428, 34, "MALTEPE", 2012 },
                    { 429, 34, "PENDİK", 1835 },
                    { 430, 34, "SANCAKTEPE", 2054 },
                    { 431, 34, "SARIYER", 1604 },
                    { 432, 34, "SİLİVRİ", 1622 },
                    { 433, 34, "SULTANBEYLİ", 2014 },
                    { 434, 34, "SULTANGAZİ", 2055 },
                    { 435, 34, "ŞİLE", 1659 },
                    { 436, 34, "ŞİŞLİ", 1663 },
                    { 437, 34, "TUZLA", 2015 },
                    { 438, 34, "ÜMRANİYE", 1852 },
                    { 439, 34, "ÜSKÜDAR", 1708 },
                    { 440, 34, "ZEYTİNBURNU", 1739 },
                    { 441, 35, "ALİAĞA", 1128 },
                    { 442, 35, "BALÇOVA", 2006 },
                    { 443, 35, "BAYINDIR", 1178 },
                    { 444, 35, "BAYRAKLI", 2056 },
                    { 445, 35, "BERGAMA", 1181 },
                    { 446, 35, "BEYDAĞ", 1776 },
                    { 447, 35, "BORNOVA", 1203 },
                    { 448, 35, "BUCA", 1780 },
                    { 449, 35, "ÇEŞME", 1251 },
                    { 450, 35, "ÇİĞLİ", 2007 },
                    { 451, 35, "DİKİLİ", 1280 },
                    { 452, 35, "FOÇA", 1334 },
                    { 453, 35, "GAZİEMİR", 2009 },
                    { 454, 35, "GÜZELBAHÇE", 2018 },
                    { 455, 35, "KARABAĞLAR", 2057 },
                    { 456, 35, "KARABURUN", 1432 },
                    { 457, 35, "KARŞIYAKA", 1448 },
                    { 458, 35, "KEMALPAŞA", 1461 },
                    { 459, 35, "KINIK", 1467 },
                    { 460, 35, "KİRAZ", 1477 },
                    { 461, 35, "KONAK", 1819 },
                    { 462, 35, "MENDERES", 1826 },
                    { 463, 35, "MENEMEN", 1521 },
                    { 464, 35, "NARLIDERE", 2013 },
                    { 465, 35, "ÖDEMİŞ", 1563 },
                    { 466, 35, "SEFERİHİSAR", 1611 },
                    { 467, 35, "SELÇUK", 1612 },
                    { 468, 35, "TİRE", 1677 },
                    { 469, 35, "TORBALI", 1682 },
                    { 470, 35, "URLA", 1703 },
                    { 471, 36, "AKYAKA", 1756 },
                    { 472, 36, "ARPAÇAY", 1149 },
                    { 473, 36, "DİGOR", 1279 },
                    { 474, 36, "KAĞIZMAN", 1424 },
                    { 475, 36, "SARIKAMIŞ", 1601 },
                    { 476, 36, "SELİM", 1614 },
                    { 477, 36, "SUSUZ", 1645 },
                    { 478, 37, "ABANA", 1101 },
                    { 479, 37, "AĞLI", 1867 },
                    { 480, 37, "ARAÇ", 1140 },
                    { 481, 37, "AZDAVAY", 1162 },
                    { 482, 37, "BOZKURT", 1208 },
                    { 483, 37, "CİDE", 1221 },
                    { 484, 37, "ÇATALZEYTİN", 1238 },
                    { 485, 37, "DADAY", 1264 },
                    { 486, 37, "DEVREKANİ", 1277 },
                    { 487, 37, "DOĞANYURT", 1915 },
                    { 488, 37, "HANÖNÜ", 1940 },
                    { 489, 37, "İHSANGAZİ", 1805 },
                    { 490, 37, "İNEBOLU", 1410 },
                    { 491, 37, "KÜRE", 1499 },
                    { 492, 37, "PINARBAŞI", 1836 },
                    { 493, 37, "SEYDİLER", 1984 },
                    { 494, 37, "ŞENPAZAR", 1845 },
                    { 495, 37, "TAŞKÖPRÜ", 1666 },
                    { 496, 37, "TOSYA", 1685 },
                    { 497, 38, "AKKIŞLA", 1752 },
                    { 498, 38, "BÜNYAN", 1218 },
                    { 499, 38, "DEVELİ", 1275 },
                    { 500, 38, "FELAHİYE", 1330 },
                    { 501, 38, "HACILAR", 1936 },
                    { 502, 38, "İNCESU", 1409 },
                    { 503, 38, "KOCASİNAN", 1863 },
                    { 504, 38, "MELİKGAZİ", 1864 },
                    { 505, 38, "ÖZVATAN", 1978 },
                    { 506, 38, "PINARBAŞI", 1576 },
                    { 507, 38, "SARIOĞLAN", 1603 },
                    { 508, 38, "SARIZ", 1605 },
                    { 509, 38, "TALAS", 1846 },
                    { 510, 38, "TOMARZA", 1680 },
                    { 511, 38, "YAHYALI", 1715 },
                    { 512, 38, "YEŞİLHİSAR", 1727 },
                    { 513, 39, "BABAESKİ", 1163 },
                    { 514, 39, "DEMİRKÖY", 1270 },
                    { 515, 39, "KOFÇAZ", 1480 },
                    { 516, 39, "LÜLEBURGAZ", 1505 },
                    { 517, 39, "PEHLİVANKÖY", 1572 },
                    { 518, 39, "PINARHİSAR", 1577 },
                    { 519, 39, "VİZE", 1714 },
                    { 520, 40, "AKÇAKENT", 1869 },
                    { 521, 40, "AKPINAR", 1754 },
                    { 522, 40, "BOZTEPE", 1890 },
                    { 523, 40, "ÇİÇEKDAĞI", 1254 },
                    { 524, 40, "KAMAN", 1429 },
                    { 525, 40, "MUCUR", 1529 },
                    { 526, 41, "BAŞİSKELE", 2058 },
                    { 527, 41, "ÇAYIROVA", 2059 },
                    { 528, 41, "DARICA", 2060 },
                    { 529, 41, "DERİNCE", 2030 },
                    { 530, 41, "DİLOVASI", 2061 },
                    { 531, 41, "GEBZE", 1338 },
                    { 532, 41, "GÖLCÜK", 1355 },
                    { 533, 41, "İZMİT", 2062 },
                    { 534, 41, "KANDIRA", 1430 },
                    { 535, 41, "KARAMÜRSEL", 1440 },
                    { 536, 41, "KARTEPE", 2063 },
                    { 537, 41, "KÖRFEZ", 1821 },
                    { 538, 42, "AHIRLI", 1868 },
                    { 539, 42, "AKÖREN", 1753 },
                    { 540, 42, "AKŞEHİR", 1122 },
                    { 541, 42, "ALTINEKİN", 1760 },
                    { 542, 42, "BEYŞEHİR", 1188 },
                    { 543, 42, "BOZKIR", 1207 },
                    { 544, 42, "CİHANBEYLİ", 1222 },
                    { 545, 42, "ÇELTİK", 1902 },
                    { 546, 42, "ÇUMRA", 1262 },
                    { 547, 42, "DERBENT", 1907 },
                    { 548, 42, "DEREBUCAK", 1789 },
                    { 549, 42, "DOĞANHİSAR", 1285 },
                    { 550, 42, "EMİRGAZİ", 1920 },
                    { 551, 42, "EREĞLİ", 1312 },
                    { 552, 42, "GÜNEYSINIR", 1933 },
                    { 553, 42, "HADİM", 1375 },
                    { 554, 42, "HALKAPINAR", 1937 },
                    { 555, 42, "HÜYÜK", 1804 },
                    { 556, 42, "ILGIN", 1400 },
                    { 557, 42, "KADINHANI", 1422 },
                    { 558, 42, "KARAPINAR", 1441 },
                    { 559, 42, "KARATAY", 1814 },
                    { 560, 42, "KULU", 1491 },
                    { 561, 42, "MERAM", 1827 },
                    { 562, 42, "SARAYÖNÜ", 1598 },
                    { 563, 42, "SELÇUKLU", 1839 },
                    { 564, 42, "SEYDİŞEHİR", 1617 },
                    { 565, 42, "TAŞKENT", 1848 },
                    { 566, 42, "TUZLUKÇU", 1990 },
                    { 567, 42, "YALIHÜYÜK", 1994 },
                    { 568, 42, "YUNAK", 1735 },
                    { 569, 43, "ALTINTAŞ", 1132 },
                    { 570, 43, "ASLANAPA", 1764 },
                    { 571, 43, "ÇAVDARHİSAR", 1898 },
                    { 572, 43, "DOMANİÇ", 1288 },
                    { 573, 43, "DUMLUPINAR", 1790 },
                    { 574, 43, "EMET", 1304 },
                    { 575, 43, "GEDİZ", 1339 },
                    { 576, 43, "HİSARCIK", 1802 },
                    { 577, 43, "PAZARLAR", 1979 },
                    { 578, 43, "SİMAV", 1625 },
                    { 579, 43, "ŞAPHANE", 1843 },
                    { 580, 43, "TAVŞANLI", 1671 },
                    { 581, 44, "AKÇADAĞ", 1114 },
                    { 582, 44, "ARAPGİR", 1143 },
                    { 583, 44, "ARGUVAN", 1148 },
                    { 584, 44, "BATTALGAZİ", 1772 },
                    { 585, 44, "DARENDE", 1265 },
                    { 586, 44, "DOĞANŞEHİR", 1286 },
                    { 587, 44, "DOĞANYOL", 1914 },
                    { 588, 44, "HEKİMHAN", 1390 },
                    { 589, 44, "KALE", 1953 },
                    { 590, 44, "KULUNCAK", 1969 },
                    { 591, 44, "PÜTÜRGE", 1582 },
                    { 592, 44, "YAZIHAN", 1995 },
                    { 593, 44, "YEŞİLYURT", 1729 },
                    { 594, 45, "AHMETLİ", 1751 },
                    { 595, 45, "AKHİSAR", 1118 },
                    { 596, 45, "ALAŞEHİR", 1127 },
                    { 597, 45, "DEMİRCİ", 1269 },
                    { 598, 45, "GÖLMARMARA", 1793 },
                    { 599, 45, "GÖRDES", 1362 },
                    { 600, 45, "KIRKAĞAÇ", 1470 },
                    { 601, 45, "KÖPRÜBAŞI", 1965 },
                    { 602, 45, "KULA", 1489 },
                    { 603, 45, "SALİHLİ", 1590 },
                    { 604, 45, "SARIGÖL", 1600 },
                    { 605, 45, "SARUHANLI", 1606 },
                    { 606, 45, "SELENDİ", 1613 },
                    { 607, 45, "SOMA", 1634 },
                    { 608, 45, "ŞEHZADELER", 2086 },
                    { 609, 45, "TURGUTLU", 1689 },
                    { 610, 45, "YUNUSEMRE", 2087 },
                    { 611, 46, "AFŞİN", 1107 },
                    { 612, 46, "ANDIRIN", 1136 },
                    { 613, 46, "ÇAĞLAYANCERİT", 1785 },
                    { 614, 46, "DULKADİROĞLU", 2084 },
                    { 615, 46, "EKİNÖZÜ", 1919 },
                    { 616, 46, "ELBİSTAN", 1299 },
                    { 617, 46, "GÖKSUN", 1353 },
                    { 618, 46, "NURHAK", 1975 },
                    { 619, 46, "ONİKİŞUBAT", 2085 },
                    { 620, 46, "PAZARCIK", 1570 },
                    { 621, 46, "TÜRKOĞLU", 1694 },
                    { 622, 47, "ARTUKLU", 2088 },
                    { 623, 47, "DARGEÇİT", 1787 },
                    { 624, 47, "DERİK", 1273 },
                    { 625, 47, "KIZILTEPE", 1474 },
                    { 626, 47, "MAZIDAĞI", 1519 },
                    { 627, 47, "MİDYAT", 1526 },
                    { 628, 47, "NUSAYBİN", 1547 },
                    { 629, 47, "ÖMERLİ", 1564 },
                    { 630, 47, "SAVUR", 1609 },
                    { 631, 47, "YEŞİLLİ", 2002 },
                    { 632, 48, "BODRUM", 1197 },
                    { 633, 48, "DALAMAN", 1742 },
                    { 634, 48, "DATÇA", 1266 },
                    { 635, 48, "FETHİYE", 1331 },
                    { 636, 48, "KAVAKLIDERE", 1958 },
                    { 637, 48, "KÖYCEĞİZ", 1488 },
                    { 638, 48, "MARMARİS", 1517 },
                    { 639, 48, "MENTEŞE", 2089 },
                    { 640, 48, "MİLAS", 1528 },
                    { 641, 48, "ORTACA", 1831 },
                    { 642, 48, "SEYDİKEMER", 2090 },
                    { 643, 48, "ULA", 1695 },
                    { 644, 48, "YATAĞAN", 1719 },
                    { 645, 49, "BULANIK", 1213 },
                    { 646, 49, "HASKÖY", 1801 },
                    { 647, 49, "KORKUT", 1964 },
                    { 648, 49, "MALAZGİRT", 1510 },
                    { 649, 49, "VARTO", 1711 },
                    { 650, 50, "ACIGÖL", 1749 },
                    { 651, 50, "AVANOS", 1155 },
                    { 652, 50, "DERİNKUYU", 1274 },
                    { 653, 50, "GÜLŞEHİR", 1367 },
                    { 654, 50, "HACIBEKTAŞ", 1374 },
                    { 655, 50, "KOZAKLI", 1485 },
                    { 656, 50, "ÜRGÜP", 1707 },
                    { 657, 51, "ALTUNHİSAR", 1876 },
                    { 658, 51, "BOR", 1201 },
                    { 659, 51, "ÇAMARDI", 1225 },
                    { 660, 51, "ÇİFTLİK", 1904 },
                    { 661, 51, "ULUKIŞLA", 1700 },
                    { 662, 52, "AKKUŞ", 1119 },
                    { 663, 52, "ALTINORDU", 2103 },
                    { 664, 52, "AYBASTI", 1158 },
                    { 665, 52, "ÇAMAŞ", 1891 },
                    { 666, 52, "ÇATALPINAR", 1897 },
                    { 667, 52, "ÇAYBAŞI", 1900 },
                    { 668, 52, "FATSA", 1328 },
                    { 669, 52, "GÖLKÖY", 1358 },
                    { 670, 52, "GÜLYALI", 1795 },
                    { 671, 52, "GÜRGENTEPE", 1797 },
                    { 672, 52, "İKİZCE", 1947 },
                    { 673, 52, "KABADÜZ", 1950 },
                    { 674, 52, "KABATAŞ", 1951 },
                    { 675, 52, "KORGAN", 1482 },
                    { 676, 52, "KUMRU", 1493 },
                    { 677, 52, "MESUDİYE", 1525 },
                    { 678, 52, "PERŞEMBE", 1573 },
                    { 679, 52, "ULUBEY", 1696 },
                    { 680, 52, "ÜNYE", 1706 },
                    { 681, 53, "ARDEŞEN", 1146 },
                    { 682, 53, "ÇAMLIHEMŞİN", 1228 },
                    { 683, 53, "ÇAYELİ", 1241 },
                    { 684, 53, "DEREPAZARI", 1908 },
                    { 685, 53, "FINDIKLI", 1332 },
                    { 686, 53, "GÜNEYSU", 1796 },
                    { 687, 53, "HEMŞİN", 1943 },
                    { 688, 53, "İKİZDERE", 1405 },
                    { 689, 53, "İYİDERE", 1949 },
                    { 690, 53, "KALKANDERE", 1428 },
                    { 691, 53, "PAZAR", 1569 },
                    { 692, 54, "ADAPAZARI", 2068 },
                    { 693, 54, "AKYAZI", 1123 },
                    { 694, 54, "ARİFİYE", 2069 },
                    { 695, 54, "ERENLER", 2070 },
                    { 696, 54, "FERİZLİ", 1925 },
                    { 697, 54, "GEYVE", 1351 },
                    { 698, 54, "HENDEK", 1391 },
                    { 699, 54, "KARAPÜRÇEK", 1955 },
                    { 700, 54, "KARASU", 1442 },
                    { 701, 54, "KAYNARCA", 1453 },
                    { 702, 54, "KOCAALİ", 1818 },
                    { 703, 54, "PAMUKOVA", 1833 },
                    { 704, 54, "SAPANCA", 1595 },
                    { 705, 54, "SERDİVAN", 2071 },
                    { 706, 54, "SÖĞÜTLÜ", 1986 },
                    { 707, 54, "TARAKLI", 1847 },
                    { 708, 55, "19 MAYIS", 1830 },
                    { 709, 55, "ALAÇAM", 1125 },
                    { 710, 55, "ASARCIK", 1763 },
                    { 711, 55, "ATAKUM", 2072 },
                    { 712, 55, "AYVACIK", 1879 },
                    { 713, 55, "BAFRA", 1164 },
                    { 714, 55, "CANİK", 2073 },
                    { 715, 55, "ÇARŞAMBA", 1234 },
                    { 716, 55, "HAVZA", 1386 },
                    { 717, 55, "İLKADIM", 2074 },
                    { 718, 55, "KAVAK", 1452 },
                    { 719, 55, "LADİK", 1501 },
                    { 720, 55, "SALIPAZARI", 1838 },
                    { 721, 55, "TEKKEKÖY", 1849 },
                    { 722, 55, "TERME", 1676 },
                    { 723, 55, "VEZİRKÖPRÜ", 1712 },
                    { 724, 55, "YAKAKENT", 1993 },
                    { 725, 56, "BAYKAN", 1179 },
                    { 726, 56, "ERUH", 1317 },
                    { 727, 56, "KURTALAN", 1495 },
                    { 728, 56, "PERVARİ", 1575 },
                    { 729, 56, "ŞİRVAN", 1662 },
                    { 730, 56, "TİLLO", 1878 },
                    { 731, 57, "AYANCIK", 1156 },
                    { 732, 57, "BOYABAT", 1204 },
                    { 733, 57, "DİKMEN", 1910 },
                    { 734, 57, "DURAĞAN", 1290 },
                    { 735, 57, "ERFELEK", 1314 },
                    { 736, 57, "GERZE", 1349 },
                    { 737, 57, "SARAYDÜZÜ", 1981 },
                    { 738, 57, "TÜRKELİ", 1693 },
                    { 739, 58, "AKINCILAR", 1870 },
                    { 740, 58, "ALTINYAYLA", 1875 },
                    { 741, 58, "DİVRİĞİ", 1282 },
                    { 742, 58, "DOĞANŞAR", 1913 },
                    { 743, 58, "GEMEREK", 1342 },
                    { 744, 58, "GÖLOVA", 1927 },
                    { 745, 58, "GÜRÜN", 1373 },
                    { 746, 58, "HAFİK", 1376 },
                    { 747, 58, "İMRANLI", 1407 },
                    { 748, 58, "KANGAL", 1431 },
                    { 749, 58, "KOYULHİSAR", 1484 },
                    { 750, 58, "SUŞEHRİ", 1646 },
                    { 751, 58, "ŞARKIŞLA", 1650 },
                    { 752, 58, "ULAŞ", 1991 },
                    { 753, 58, "YILDIZELİ", 1731 },
                    { 754, 58, "ZARA", 1738 },
                    { 755, 59, "ÇERKEZKÖY", 1250 },
                    { 756, 59, "ÇORLU", 1258 },
                    { 757, 59, "ERGENE", 2094 },
                    { 758, 59, "HAYRABOLU", 1388 },
                    { 759, 59, "KAPAKLI", 2095 },
                    { 760, 59, "MALKARA", 1511 },
                    { 761, 59, "MARMARAEREĞLİSİ", 1825 },
                    { 762, 59, "MURATLI", 1538 },
                    { 763, 59, "SARAY", 1596 },
                    { 764, 59, "SÜLEYMANPAŞA", 2096 },
                    { 765, 59, "ŞARKÖY", 1652 },
                    { 766, 60, "ALMUS", 1129 },
                    { 767, 60, "ARTOVA", 1151 },
                    { 768, 60, "BAŞÇİFTLİK", 1883 },
                    { 769, 60, "ERBAA", 1308 },
                    { 770, 60, "NİKSAR", 1545 },
                    { 771, 60, "PAZAR", 1834 },
                    { 772, 60, "REŞADİYE", 1584 },
                    { 773, 60, "SULUSARAY", 1987 },
                    { 774, 60, "TURHAL", 1690 },
                    { 775, 60, "YEŞİLYURT", 1858 },
                    { 776, 60, "ZİLE", 1740 },
                    { 777, 61, "AKÇAABAT", 1113 },
                    { 778, 61, "ARAKLI", 1141 },
                    { 779, 61, "ARSİN", 1150 },
                    { 780, 61, "BEŞİKDÜZÜ", 1775 },
                    { 781, 61, "ÇARŞIBAŞI", 1896 },
                    { 782, 61, "ÇAYKARA", 1244 },
                    { 783, 61, "DERNEKPAZARI", 1909 },
                    { 784, 61, "DÜZKÖY", 1917 },
                    { 785, 61, "HAYRAT", 1942 },
                    { 786, 61, "KÖPRÜBAŞI", 1966 },
                    { 787, 61, "MAÇKA", 1507 },
                    { 788, 61, "OF", 1548 },
                    { 789, 61, "ORTAHİSAR", 2097 },
                    { 790, 61, "SÜRMENE", 1647 },
                    { 791, 61, "ŞALPAZARI", 1842 },
                    { 792, 61, "TONYA", 1681 },
                    { 793, 61, "VAKFIKEBİR", 1709 },
                    { 794, 61, "YOMRA", 1732 },
                    { 795, 62, "ÇEMİŞGEZEK", 1247 },
                    { 796, 62, "HOZAT", 1397 },
                    { 797, 62, "MAZGİRT", 1518 },
                    { 798, 62, "NAZIMİYE", 1541 },
                    { 799, 62, "OVACIK", 1562 },
                    { 800, 62, "PERTEK", 1574 },
                    { 801, 62, "PÜLÜMÜR", 1581 },
                    { 802, 63, "AKÇAKALE", 1115 },
                    { 803, 63, "BİRECİK", 1194 },
                    { 804, 63, "BOZOVA", 1209 },
                    { 805, 63, "CEYLANPINAR", 1220 },
                    { 806, 63, "EYYÜBİYE", 2091 },
                    { 807, 63, "HALFETİ", 1378 },
                    { 808, 63, "HALİLİYE", 2092 },
                    { 809, 63, "HARRAN", 1800 },
                    { 810, 63, "HİLVAN", 1393 },
                    { 811, 63, "KARAKÖPRÜ", 2093 },
                    { 812, 63, "SİVEREK", 1630 },
                    { 813, 63, "SURUÇ", 1643 },
                    { 814, 63, "VİRANŞEHİR", 1713 },
                    { 815, 64, "BANAZ", 1170 },
                    { 816, 64, "EŞME", 1323 },
                    { 817, 64, "KARAHALLI", 1436 },
                    { 818, 64, "SİVASLI", 1629 },
                    { 819, 64, "ULUBEY", 1697 },
                    { 820, 65, "BAHÇESARAY", 1770 },
                    { 821, 65, "BAŞKALE", 1175 },
                    { 822, 65, "ÇALDIRAN", 1786 },
                    { 823, 65, "ÇATAK", 1236 },
                    { 824, 65, "EDREMİT", 1918 },
                    { 825, 65, "ERCİŞ", 1309 },
                    { 826, 65, "GEVAŞ", 1350 },
                    { 827, 65, "GÜRPINAR", 1372 },
                    { 828, 65, "İPEKYOLU", 2098 },
                    { 829, 65, "MURADİYE", 1533 },
                    { 830, 65, "ÖZALP", 1565 },
                    { 831, 65, "SARAY", 1980 },
                    { 832, 65, "TUŞBA", 2099 },
                    { 833, 66, "AKDAĞMADENİ", 1117 },
                    { 834, 66, "AYDINCIK", 1877 },
                    { 835, 66, "BOĞAZLIYAN", 1198 },
                    { 836, 66, "ÇANDIR", 1895 },
                    { 837, 66, "ÇAYIRALAN", 1242 },
                    { 838, 66, "ÇEKEREK", 1245 },
                    { 839, 66, "KADIŞEHRİ", 1952 },
                    { 840, 66, "SARAYKENT", 1982 },
                    { 841, 66, "SARIKAYA", 1602 },
                    { 842, 66, "SORGUN", 1635 },
                    { 843, 66, "ŞEFAATLİ", 1655 },
                    { 844, 66, "YENİFAKILI", 1998 },
                    { 845, 66, "YERKÖY", 1726 },
                    { 846, 67, "ALAPLI", 1758 },
                    { 847, 67, "ÇAYCUMA", 1240 },
                    { 848, 67, "DEVREK", 1276 },
                    { 849, 67, "EREĞLİ", 1313 },
                    { 850, 67, "GÖKÇEBEY", 1926 },
                    { 851, 67, "KİLİMLİ", 2100 },
                    { 852, 67, "KOZLU", 2101 },
                    { 853, 68, "AĞAÇÖREN", 1860 },
                    { 854, 68, "ESKİL", 1921 },
                    { 855, 68, "GÜLAĞAÇ", 1932 },
                    { 856, 68, "GÜZELYURT", 1861 },
                    { 857, 68, "ORTAKÖY", 1557 },
                    { 858, 68, "SARIYAHŞİ", 1866 },
                    { 859, 68, "SULTANHANI", 2106 },
                    { 860, 69, "AYDINTEPE", 1767 },
                    { 861, 69, "DEMİRÖZÜ", 1788 },
                    { 862, 70, "AYRANCI", 1768 },
                    { 863, 70, "BAŞYAYLA", 1884 },
                    { 864, 70, "ERMENEK", 1316 },
                    { 865, 70, "KAZIMKARABEKİR", 1862 },
                    { 866, 70, "SARIVELİLER", 1983 },
                    { 867, 71, "BAHŞILI", 1880 },
                    { 868, 71, "BALIŞEYH", 1882 },
                    { 869, 71, "ÇELEBİ", 1901 },
                    { 870, 71, "DELİCE", 1268 },
                    { 871, 71, "KARAKEÇİLİ", 1954 },
                    { 872, 71, "KESKİN", 1463 },
                    { 873, 71, "SULAKYURT", 1638 },
                    { 874, 71, "YAHŞİHAN", 1992 },
                    { 875, 72, "BEŞİRİ", 1184 },
                    { 876, 72, "GERCÜŞ", 1345 },
                    { 877, 72, "HASANKEYF", 1941 },
                    { 878, 72, "KOZLUK", 1487 },
                    { 879, 72, "SASON", 1607 },
                    { 880, 73, "BEYTÜŞŞEBAP", 1189 },
                    { 881, 73, "CİZRE", 1223 },
                    { 882, 73, "GÜÇLÜKONAK", 1931 },
                    { 883, 73, "İDİL", 1403 },
                    { 884, 73, "SİLOPİ", 1623 },
                    { 885, 73, "ULUDERE", 1698 },
                    { 886, 74, "AMASRA", 1761 },
                    { 887, 74, "KURUCAŞİLE", 1496 },
                    { 888, 74, "ULUS", 1701 },
                    { 889, 75, "ÇILDIR", 1252 },
                    { 890, 75, "DAMAL", 2008 },
                    { 891, 75, "GÖLE", 1356 },
                    { 892, 75, "HANAK", 1380 },
                    { 893, 75, "POSOF", 1579 },
                    { 894, 76, "ARALIK", 1142 },
                    { 895, 76, "KARAKOYUNLU", 2011 },
                    { 896, 76, "TUZLUCA", 1692 },
                    { 897, 77, "ALTINOVA", 2019 },
                    { 898, 77, "ARMUTLU", 2020 },
                    { 899, 77, "ÇINARCIK", 2021 },
                    { 900, 77, "ÇİFTLİKKÖY", 2022 },
                    { 901, 77, "TERMAL", 2026 },
                    { 902, 78, "EFLANİ", 1296 },
                    { 903, 78, "ESKİPAZAR", 1321 },
                    { 904, 78, "OVACIK", 1561 },
                    { 905, 78, "SAFRANBOLU", 1587 },
                    { 906, 78, "YENİCE", 1856 },
                    { 907, 79, "ELBEYLİ", 2023 },
                    { 908, 79, "MUSABEYLİ", 2024 },
                    { 909, 79, "POLATELİ", 2025 },
                    { 910, 80, "BAHÇE", 1165 },
                    { 911, 80, "DÜZİÇİ", 1743 },
                    { 912, 80, "HASANBEYLİ", 2027 },
                    { 913, 80, "KADİRLİ", 1423 },
                    { 914, 80, "SUMBAS", 2028 },
                    { 915, 80, "TOPRAKKALE", 2029 },
                    { 916, 81, "AKÇAKOCA", 1116 },
                    { 917, 81, "CUMAYERİ", 1784 },
                    { 918, 81, "ÇİLİMLİ", 1905 },
                    { 919, 81, "GÖLYAKA", 1794 },
                    { 920, 81, "GÜMÜŞOVA", 2017 },
                    { 921, 81, "KAYNAŞLI", 2031 },
                    { 922, 81, "YIĞILCA", 1730 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DistrictId",
                table: "Addresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Bookmarks_BlogId",
                table: "Blog_Bookmarks",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Bookmarks_UserId",
                table: "Blog_Bookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Comments_BlogId",
                table: "Blog_Comments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Comments_UserId",
                table: "Blog_Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Likes_BlogId",
                table: "Blog_Likes",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Likes_UserId",
                table: "Blog_Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_RecipeId",
                table: "Blogs",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CityId",
                table: "Districts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AddressId",
                table: "Events",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Requirements_EventId",
                table: "Events_Requirements",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Requirements_RequirementId",
                table: "Events_Requirements",
                column: "RequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_TypeId",
                table: "Ingredients",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Preferences_IngredientId",
                table: "Ingredients_Preferences",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Preferences_PreferenceId",
                table: "Ingredients_Preferences",
                column: "PreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Bookmarks_RecipeId",
                table: "Recipe_Bookmarks",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Bookmarks_UserId",
                table: "Recipe_Bookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Comments_RecipeId",
                table: "Recipe_Comments",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Comments_UserId",
                table: "Recipe_Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Likes_RecipeId",
                table: "Recipe_Likes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Likes_UserId",
                table: "Recipe_Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Ingredients_IngredientId",
                table: "Recipes_Ingredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Ingredients_RecipeId",
                table: "Recipes_Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Allergens_PreferenceId",
                table: "User_Allergens",
                column: "PreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Allergens_UserId",
                table: "User_Allergens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Event_Participations_EventId",
                table: "User_Event_Participations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Event_Participations_UserId",
                table: "User_Event_Participations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Blogs_Interactions_BlogId",
                table: "Users_Blogs_Interactions",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Blogs_Interactions_InteractionId",
                table: "Users_Blogs_Interactions",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Blogs_Interactions_UserId",
                table: "Users_Blogs_Interactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Interactions_InitiatorUserId",
                table: "Users_Interactions",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Interactions_InteractionId",
                table: "Users_Interactions",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Interactions_TargetUserId",
                table: "Users_Interactions",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Recipes_Interactions_InteractionId",
                table: "Users_Recipes_Interactions",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Recipes_Interactions_RecipeId",
                table: "Users_Recipes_Interactions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Recipes_Interactions_UserId",
                table: "Users_Recipes_Interactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Blog_Bookmarks");

            migrationBuilder.DropTable(
                name: "Blog_Comments");

            migrationBuilder.DropTable(
                name: "Blog_Likes");

            migrationBuilder.DropTable(
                name: "Events_Requirements");

            migrationBuilder.DropTable(
                name: "Ingredients_Preferences");

            migrationBuilder.DropTable(
                name: "Recipe_Bookmarks");

            migrationBuilder.DropTable(
                name: "Recipe_Comments");

            migrationBuilder.DropTable(
                name: "Recipe_Likes");

            migrationBuilder.DropTable(
                name: "Recipes_Ingredients");

            migrationBuilder.DropTable(
                name: "User_Allergens");

            migrationBuilder.DropTable(
                name: "User_Event_Participations");

            migrationBuilder.DropTable(
                name: "Users_Blogs_Interactions");

            migrationBuilder.DropTable(
                name: "Users_Interactions");

            migrationBuilder.DropTable(
                name: "Users_Recipes_Interactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Requirements");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "IngredientTypes");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
