using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DefaultData : Migration
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Allergens_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Interactions_AspNetUsers_TargetUserId",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Interactions_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Recipes_Interactions_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Recipes_Interactions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Blogs_Interactions_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Blogs_Interactions_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Preferences",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Includes all milk products, such as milk, cheese, yogurt, butter, and whey.", "Milk" },
                    { 2, "Includes chicken eggs and any products containing eggs, such as baked goods and mayonnaise.", "Eggs" },
                    { 3, "Includes all finned fish such as bass, cod, salmon, tuna, and anchovies.", "Fish" },
                    { 4, "Includes shrimp, crab, lobster, prawns, and crayfish.", "Crustacean Shellfish" },
                    { 5, "Includes almonds, walnuts, pecans, cashews, macadamia nuts, and hazelnuts. Excludes peanuts.", "Tree Nuts" },
                    { 6, "Includes peanuts and peanut-containing products, such as peanut butter and peanut oil.", "Peanuts" },
                    { 7, "Includes foods containing wheat gluten, such as bread, pasta, and cereals.", "Wheat" },
                    { 8, "Includes soy and soy-containing products, such as tofu, soy sauce, and edamame.", "Soybeans" },
                    { 9, "Includes sesame seeds, sesame oil, and products containing sesame, such as tahini.", "Sesame" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Default user role", "User" },
                    { 2, "Admin role", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BannerImage", "Bio", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImage", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, null, null, null, "b1979add-89f2-4da5-a6d1-022f18b4b629", "Users", null, false, "Abagail", "Hess", false, null, null, null, null, null, false, null, 1, "c3210e90-e5ba-45a8-b05c-b437484c6204", false, "ButterflyGirl" },
                    { "10", 0, null, null, null, "64b1baf6-c01d-43bb-be2f-4f62a8a588bc", "Users", null, false, "Elijah", "Harris", false, null, null, null, null, null, false, null, 1, "d3a40818-9c9d-4e3e-95dc-997719eb74b5", false, "Crazy_Eagle46" },
                    { "100", 0, null, null, null, "86116db1-104b-4bc8-bdbc-5bd66bca833f", "Users", null, false, "Kemal", "Yardımcı", false, null, null, null, null, null, false, null, 1, "7d5d01e6-b4f8-414d-a351-22d399739095", false, "kemalyardimci" },
                    { "11", 0, null, null, null, "dc1c3476-041b-44f2-b9ae-d779115ac755", "Users", null, false, "Michael", "Jones", false, null, null, null, null, null, false, null, 1, "b8658363-99c5-44a6-a9e9-d5f80df902e0", false, "Mj123" },
                    { "12", 0, null, null, null, "3667aa12-6e3c-4647-b01d-bd38781af663", "Users", null, false, "Amelia", "Johnson", false, null, null, null, null, null, false, null, 1, "9fb10d47-7776-4130-b251-c5caa9bd4a9b", false, "AmeliaJ" },
                    { "13", 0, null, null, null, "b2917006-f13f-4e54-9374-e464bebc92f1", "Users", null, false, "Liam", "Williams", false, null, null, null, null, null, false, null, 1, "64e65067-542f-4e93-b218-8f18dbbbbb14", false, "Liam123" },
                    { "14", 0, null, null, null, "9b422ccb-2467-4efa-beb4-21826c5a0a81", "Users", null, false, "Alexia", "Hernandez", false, null, null, null, null, null, false, null, 1, "81d3730c-0e53-4d43-9694-f0f19cca8af0", false, "SteepJay" },
                    { "15", 0, null, null, null, "f9d585b9-7672-475a-bf38-1ac4d0067a9f", "Users", null, false, "Oliver", "Whitehead", false, null, null, null, null, null, false, null, 1, "f8a150c0-724e-4822-9f2f-9a3099eb43ac", false, "EarthDawn" },
                    { "16", 0, null, null, null, "dcfe87cb-9d1b-412d-8f90-8d1443724fb8", "Users", null, false, "Amanda", "Johnson", false, null, null, null, null, null, false, null, 1, "a0131ced-bb66-4123-8ba6-438fadddbe2d", false, "amandajohnson123" },
                    { "17", 0, null, null, null, "52cf86da-c75c-430c-af5b-44ba75f55f19", "Users", null, false, "Hayden", "Russo", false, null, null, null, null, null, false, null, 1, "807c9463-afb6-437f-82bd-108ab5f2e475", false, "HappyButterfly76" },
                    { "18", 0, null, null, null, "df10f521-8443-4d69-8b4f-40f09881cee6", "Users", null, false, "Eleanor", "Bolton", false, null, null, null, null, null, false, null, 1, "86cb10db-f782-4449-bfd4-e92b412ca97b", false, "ebolton" },
                    { "19", 0, null, null, null, "95e489e5-a3ce-4434-83c3-48e7e4812795", "Users", null, false, "Edward", "Owens", false, null, null, null, null, null, false, null, 1, "0e09edeb-e3fb-4c09-b310-fb706ad38c5c", false, "eowens53" },
                    { "2", 0, null, null, null, "52e7456a-7df1-44fe-8764-ea7981d3b3a4", "Users", null, false, "Aiden", "Smith", false, null, null, null, null, null, false, null, 1, "d37d2fff-687e-43a2-82ae-0d54fd1a488f", false, "aiden.smith123" },
                    { "20", 0, null, null, null, "57b47e0d-1523-47fc-b213-ec6be3f5a644", "Users", null, false, "Anna", "Harris", false, null, null, null, null, null, false, null, 1, "648a1499-af05-4be2-9b42-5b6661669268", false, "anna_harris1996" },
                    { "21", 0, null, null, null, "7f4c7a6a-e308-40fa-8639-79d5cd13615b", "Users", null, false, "Omar", "Dejesus", false, null, null, null, null, null, false, null, 1, "0a57d278-e053-444c-baa4-e1bf38b30976", false, "CuddlyMuffin" },
                    { "22", 0, null, null, null, "b4f21c67-86b2-4d05-bce6-9156591dd332", "Users", null, false, "Sarah", "Johnson", false, null, null, null, null, null, false, null, 1, "2771e7a4-2ceb-4c5c-88d8-b5f0b5456531", false, "sarahj23" },
                    { "23", 0, null, null, null, "74457ce0-9b58-43a8-9a4c-810fb65413a8", "Users", null, false, "Cody", "Bailey", false, null, null, null, null, null, false, null, 1, "e5956c14-1020-4908-b398-a22c832f6ac5", false, "c.bailey69" },
                    { "24", 0, null, null, null, "5ea068c6-3315-4def-8bd1-5a9c004cb0f9", "Users", null, false, "Jack", "Black", false, null, null, null, null, null, false, null, 1, "9711da2d-27c7-4190-8fa4-0a69d446e608", false, "jblack" },
                    { "25", 0, null, null, null, "b0128907-471a-4d34-a543-71349250a052", "Users", null, false, "Metin", "Hikaye", false, null, null, null, null, null, false, null, 1, "9483dd7f-ec1c-490d-bb08-d31cf68e0002", false, "sallyrooney39" },
                    { "26", 0, null, null, null, "c6620f67-35c0-4d0b-af0b-0dad20295e70", "Users", null, false, "Tuncay", "Taşkıran", false, null, null, null, null, null, false, null, 1, "1676664f-2a4f-4d4e-911f-547e8ba335d6", false, "Futbol" },
                    { "27", 0, null, null, null, "b9c39edc-7bd6-4257-b208-d14bf38cf98c", "Users", null, false, "Adnan", "Kumandan", false, null, null, null, null, null, false, null, 1, "9439c2a0-301b-452d-98a7-e50fd9f8310a", false, "lordoglu" },
                    { "28", 0, null, null, null, "99cedebe-ccdf-491f-ba21-14271e5c55b6", "Users", null, false, "Mustafa", "Sandalye", false, null, null, null, null, null, false, null, 1, "4016c45a-16bf-4981-81bd-4a5c0cd82a4a", false, "mustii2024" },
                    { "29", 0, null, null, null, "4cd4f1bd-9a49-4805-8edc-d17d7a5e89d3", "Users", null, false, "Adil", "Salamura", false, null, null, null, null, null, false, null, 1, "a69de262-22f9-446a-a39a-b609c0dd1afc", false, "tursuanime321" },
                    { "3", 0, null, null, null, "4b8e33c9-4ecf-4253-aead-50dc0d70ed2b", "Users", null, false, "Ada", "Medina", false, null, null, null, null, null, false, null, 1, "d5cd02f0-9ab2-48a4-b893-aaa3eecf0a21", false, "unicorn98" },
                    { "30", 0, null, null, null, "887621ff-d427-4234-9f37-7d925dc94d3c", "Users", null, false, "Tamer", "Koltuk", false, null, null, null, null, null, false, null, 1, "e1071eb0-e9f8-499b-896d-430ac6964154", false, "dockhrr" },
                    { "31", 0, null, null, null, "a1e97a91-6649-4e28-ad20-65fe22a782ac", "Users", null, false, "Berker", "Gardırop", false, null, null, null, null, null, false, null, 1, "5629c96d-1601-4690-857c-3d980b9c0c81", false, "benkerr" },
                    { "32", 0, null, null, null, "077fd031-0702-442a-9470-5de16ee915d4", "Users", null, false, "Gökçe", "Masa", false, null, null, null, null, null, false, null, 1, "41970df7-f8b1-4dcc-8c1e-6cddd5105921", false, "sekai321" },
                    { "33", 0, null, null, null, "75b98032-898f-4485-a135-38fe024904fb", "Users", null, false, "Oğuzhan", "Avize", false, null, null, null, null, null, false, null, 1, "6999d90e-4439-4e9a-94d4-db46fd10ac6f", false, "gakimarp" },
                    { "34", 0, null, null, null, "f1fed353-2548-4e9a-bd48-b61534388aae", "Users", null, false, "Zeynep", "Cam", false, null, null, null, null, null, false, null, 1, "08031908-5c34-49bc-97a1-909fb0e2e59b", false, "zeyneo" },
                    { "35", 0, null, null, null, "c0cf2272-edae-45d4-8406-703efa028ec4", "Users", null, false, "Nicholas", "Garza", false, null, null, null, null, null, false, null, 1, "a8e2aafa-3db6-4875-a979-f31688b669bc", false, "Rattling_Cymbal301" },
                    { "36", 0, null, null, null, "f81e5023-a96b-455c-9258-db379dafff0c", "Users", null, false, "Aurora", "McLaughlin", false, null, null, null, null, null, false, null, 1, "95bd30a7-d7db-4e0f-b4da-eab8b2dab1ca", false, "goldensunrise23" },
                    { "37", 0, null, null, null, "cf2d2412-0bdc-4893-85ba-2f84dcefb0dc", "Users", null, false, "Antler", "Hawaii", false, null, null, null, null, null, false, null, 1, "cc7c4adf-fb0b-4836-982d-786b9b181370", false, "Salamura" },
                    { "38", 0, null, null, null, "1bfb9770-c5eb-446a-b1b5-d2a1a657d4a0", "Users", null, false, "Amelia", "Roberts", false, null, null, null, null, null, false, null, 1, "d4e531e9-8926-4ab1-ac6d-97a788d73eef", false, "ProudBird456" },
                    { "39", 0, null, null, null, "2cc2ce80-2b1d-4885-b25d-9a1d68f8d957", "Users", null, false, "Ethan", "Murray", false, null, null, null, null, null, false, null, 1, "95085ada-6474-489d-ad44-8939f8d32397", false, "TemptingLunch321" },
                    { "4", 0, null, null, null, "6d528c43-0cee-441a-9d04-2ae8a7b5566c", "Users", null, false, "Maria", "Conner", false, null, null, null, null, null, false, null, 1, "8e94dc6f-a4b8-40fc-bc80-0f5c7e859c10", false, "mconner" },
                    { "40", 0, null, null, null, "f03a9830-4183-4e64-a7b9-b594cc2192aa", "Users", null, false, "Grant", "Koffer", false, null, null, null, null, null, false, null, 1, "a79dce13-ec02-49b9-bd81-b88a1125390d", false, "Joltik" },
                    { "41", 0, null, null, null, "a4e71dff-d2c6-48f7-98fa-3d9863173c6f", "Users", null, false, "Leila", "Patel", false, null, null, null, null, null, false, null, 1, "03453c65-b725-4777-8863-23bad3e3909f", false, "LPatel123" },
                    { "42", 0, null, null, null, "634f271e-4404-430b-a19d-77ab1dcb11c9", "Users", null, false, "Oliver", "Larson", false, null, null, null, null, null, false, null, 1, "c7ed1739-bf0e-4814-85f7-bac2e59124d5", false, "ocelot_tiger34" },
                    { "43", 0, null, null, null, "7b1636c1-4aad-4f84-8b42-f04fb2bc5644", "Users", null, false, "Kayden", "Hinton", false, null, null, null, null, null, false, null, 1, "2c46f034-f248-44f5-a684-6cd167855b00", false, "goldenrose77" },
                    { "44", 0, null, null, null, "f6716e61-f695-45d7-8baf-7f945c78e430", "Users", null, false, "Catherine", "Schiller", false, null, null, null, null, null, false, null, 1, "1d367f23-cc1e-4d47-bb0b-6ed89f3861e3", false, "unknown_cat22" },
                    { "45", 0, null, null, null, "052e30db-5bc6-4761-b1ac-069753dfb5c8", "Users", null, false, "Samuel", "Coleman", false, null, null, null, null, null, false, null, 1, "f5e5ca1e-c1fc-4c63-8929-029d2756cd90", false, "spoonedDew25" },
                    { "46", 0, null, null, null, "4c8fc8e6-2cfe-4b15-afdd-aa663891deb6", "Users", null, false, "İrfan", "Hakan", false, null, null, null, null, null, false, null, 1, "a094f1ab-4713-4f13-acd2-6901fa9f83bc", false, "hakanto" },
                    { "47", 0, null, null, null, "a27d681f-efff-4c25-864a-af3fa1425b9c", "Users", null, false, "Ali", "Yaman", false, null, null, null, null, null, false, null, 1, "0ab9657c-42d7-4519-a5a7-1daf8847abd1", false, "zedmain123" },
                    { "48", 0, null, null, null, "e816a190-84cb-43f6-8905-597c55989532", "Users", null, false, "Bruce", "Ramirez", false, null, null, null, null, null, false, null, 1, "2ac6df58-f76c-47c7-9ef3-e6dfede43b41", false, "b3729301" },
                    { "49", 0, null, null, null, "775c7eaf-1e96-4746-b2cb-c8fadc956a71", "Users", null, false, "Sophia", "Larson", false, null, null, null, null, null, false, null, 1, "b782d81c-4fe0-4e7a-a033-3204390899a5", false, "sleepykangaroo93" },
                    { "5", 0, null, null, null, "61d64e15-945a-49fc-9a92-8c875b0a6a95", "Users", null, false, "Amelia", "Kim", false, null, null, null, null, null, false, null, 1, "34a56ca1-954b-4c42-86c7-9c41635a9532", false, "ameliakim1987" },
                    { "50", 0, null, null, null, "5a893d91-cd21-436d-bcb5-3ec8815d951c", "Users", null, false, "Dylan", "Fisher", false, null, null, null, null, null, false, null, 1, "d33a9952-a2c1-4589-b29f-a30b0ac8d66b", false, "LovelyLove52" },
                    { "51", 0, null, null, null, "4bcde6da-e853-4392-97fa-f2a6367eeca2", "Users", null, false, "Gabrielle", "Rau", false, null, null, null, null, null, false, null, 1, "9388e362-02a2-4fdc-b1be-604cff5f75eb", false, "gabi.rau66" },
                    { "52", 0, null, null, null, "131e5ed5-8b69-4fb7-8e88-574777122f1f", "Users", null, false, "Mia", "Hansen", false, null, null, null, null, null, false, null, 1, "2ccf3055-49bb-4c7c-a11a-f22832d7e7e5", false, "HappyPanda845" },
                    { "53", 0, null, null, null, "0e97eb37-ebb8-4025-a81a-e6a4c8c72fcf", "Users", null, false, "Brandon", "McLaughlin", false, null, null, null, null, null, false, null, 1, "cf27000a-cca9-4cea-bae9-d8f0ddc9b449", false, "RelievedPizza981" },
                    { "54", 0, null, null, null, "90310985-bf62-4cca-97f8-20fcd96bf96c", "Users", null, false, "Frank", "Morris", false, null, null, null, null, null, false, null, 1, "3c703309-ffa5-4c32-83c3-cf7d5b942c0e", false, "fmorris01" },
                    { "55", 0, null, null, null, "2e4c3506-6cc3-421d-8363-0cc9a23cd5c2", "Users", null, false, "Wyatt", "Lyons", false, null, null, null, null, null, false, null, 1, "4137621f-12ca-4481-9aa2-b5a1fe68997d", false, "wyattlyons07" },
                    { "56", 0, null, null, null, "6ead7942-3cd0-4122-873d-8b26f37e591d", "Users", null, false, "Rhonda", "Jackson", false, null, null, null, null, null, false, null, 1, "c5ae7fa1-1865-406a-af18-c0e6a7f7939d", false, "glowing_guitar_930" },
                    { "57", 0, null, null, null, "1b741602-85bb-43bf-9827-10e43eab6cdb", "Users", null, false, "Emma", "Garcia", false, null, null, null, null, null, false, null, 1, "7bb8f486-93b2-42a5-9558-386baf012619", false, "emma_garcia234" },
                    { "58", 0, null, null, null, "fecd35da-a1d7-45c7-85f5-5b89dd508db8", "Users", null, false, "Isabella", "King", false, null, null, null, null, null, false, null, 1, "2b4e6165-28f5-4e08-aab4-1bb65fcee60c", false, "goldenpig645" },
                    { "59", 0, null, null, null, "39e57398-b751-417d-877d-9d547a6f607c", "Users", null, false, "Celeste", "Koch", false, null, null, null, null, null, false, null, 1, "837dfc62-015e-493a-b072-b15f22476413", false, "esteekoch678" },
                    { "6", 0, null, null, null, "e807112a-ea2b-47cd-8e1e-e4056c289b8d", "Users", null, false, "Adah", "Langley", false, null, null, null, null, null, false, null, 1, "14d6f48a-d8b4-40af-a5f8-24884572f2b2", false, "Aarushi01" },
                    { "60", 0, null, null, null, "9003e388-996f-41ae-b0a2-7b718c2b9a16", "Users", null, false, "Mary", "Olsen", false, null, null, null, null, null, false, null, 1, "9a6029d1-39c0-4254-8a6f-592aa300e43e", false, "Heaven328" },
                    { "61", 0, null, null, null, "d56d398c-b85c-45b0-997d-ba78f65dc662", "Users", null, false, "Savannah", "Walker", false, null, null, null, null, null, false, null, 1, "ddc86f57-9af6-46c5-bc86-09588482ee7d", false, "SavvyWalker1973" },
                    { "62", 0, null, null, null, "f88d0b56-03ac-4d51-ac41-d0d617b33bf1", "Users", null, false, "Rex", "Christiansen", false, null, null, null, null, null, false, null, 1, "55c31b23-1ed5-4bd9-ae5d-623871b3bffe", false, "RChr0321" },
                    { "63", 0, null, null, null, "d024c906-93d2-47be-932f-c29b79864a54", "Users", null, false, "Mohammad", "Lynch", false, null, null, null, null, null, false, null, 1, "5033aeda-572e-4690-b166-3cbef59d4278", false, "mohdlynch614" },
                    { "64", 0, null, null, null, "925fb8ea-0090-44ce-98df-fddb08174ac5", "Users", null, false, "Jack", "Williams", false, null, null, null, null, null, false, null, 1, "df7935bb-1e10-4321-a3f5-1b69e16dad50", false, "jackwil123" },
                    { "65", 0, null, null, null, "987fff85-b307-4aba-a915-2efd51056efb", "Users", null, false, "Aiden", "Smith", false, null, null, null, null, null, false, null, 1, "48ac256b-7366-4612-9605-5290edf80276", false, "as9876" },
                    { "66", 0, null, null, null, "f4212d79-91b8-415b-b387-d39fcf5ba858", "Users", null, false, "Emily", "Flores", false, null, null, null, null, null, false, null, 1, "710c69b1-d948-4f09-ba87-c545c21ba4d5", false, "LilTiger7523" },
                    { "67", 0, null, null, null, "c62f3cee-0cc3-4b49-a3d2-3eccad6c3bd1", "Users", null, false, "Joseph", "O'Hara", false, null, null, null, null, null, false, null, 1, "97ec8d17-446b-4bb2-ad17-3b1bd5ea5e3a", false, "bold111" },
                    { "68", 0, null, null, null, "4068e892-3ab2-4eda-bc61-0d42f831de36", "Users", null, false, "Sandrah", "Rios", false, null, null, null, null, null, false, null, 1, "c5b786d9-48d7-4222-94dc-c85f14373630", false, "sandrahrios743" },
                    { "69", 0, null, null, null, "7d899c99-2f00-4b51-af0e-6f9338057234", "Users", null, false, "John", "Doe", false, null, null, null, null, null, false, null, 1, "cc0cb14f-2d35-478c-b632-15b56b97ad27", false, "jdoe123" },
                    { "7", 0, null, null, null, "2f0bfb77-bd40-4568-8eb1-2ddba2713a1c", "Users", null, false, "Braydon", "Moore", false, null, null, null, null, null, false, null, 1, "20a2ef0e-d7a0-47a3-8af4-697069ac5637", false, "nutritious_strawberries" },
                    { "70", 0, null, null, null, "14e2de62-c536-4c6f-b4ad-d91df210eeb6", "Users", null, false, "Ethan", "Tran", false, null, null, null, null, null, false, null, 1, "8802cd78-cf9a-4917-98d1-c5c5954ac588", false, "ethtran" },
                    { "71", 0, null, null, null, "35485dd6-89a9-4b85-9bad-e92f4379d07c", "Users", null, false, "Catherine", "Schiller", false, null, null, null, null, null, false, null, 1, "adf54f96-1051-4454-b1eb-b902be3a245a", false, "unknown_cat22" },
                    { "72", 0, null, null, null, "b6d4854a-dea2-4fc6-b308-4315c913f333", "Users", null, false, "Ava", "Simmons", false, null, null, null, null, null, false, null, 1, "e4075b88-0d4b-4b30-96a4-296d0e127413", false, "ava.simmons96" },
                    { "73", 0, null, null, null, "f2e7ff35-f8df-4bd0-875c-a19c0dc21c7e", "Users", null, false, "Ahmet", "Yılmaz", false, null, null, null, null, null, false, null, 1, "cf2c5e17-7a05-4e07-a616-cec4a8818dff", false, "ahmetyilmaz" },
                    { "74", 0, null, null, null, "c411e023-ee8e-4945-b09c-da0024248653", "Users", null, false, "Ayşe", "Öztürk", false, null, null, null, null, null, false, null, 1, "84137c5e-b4cb-4cc5-86fb-f6b9170b4854", false, "ayseoz" },
                    { "75", 0, null, null, null, "8b2f4d4d-e564-4327-9948-630092868775", "Users", null, false, "Mehmet", "Kaya", false, null, null, null, null, null, false, null, 1, "24ed09b2-dae6-4d57-b648-e32b801696f0", false, "mkaya123" },
                    { "76", 0, null, null, null, "8a5d30af-4f08-46b4-9775-9b3677c01cd4", "Users", null, false, "Fatma", "Demir", false, null, null, null, null, null, false, null, 1, "d26a57f4-2313-4925-88eb-3a3be54f1e28", false, "fatmademir" },
                    { "77", 0, null, null, null, "2c942d7c-731f-4633-926f-c94c49aa7f3a", "Users", null, false, "Mustafa", "Çelik", false, null, null, null, null, null, false, null, 1, "c19749e0-6183-42d9-b565-bb466ce5ba7a", false, "mcelik34" },
                    { "78", 0, null, null, null, "07bc13d5-ed76-4228-8891-90c6b718983d", "Users", null, false, "Zeynep", "Şahin", false, null, null, null, null, null, false, null, 1, "27df2b17-ee7b-417f-9000-253b3ec8de49", false, "zeynepsahin" },
                    { "79", 0, null, null, null, "eb9477a1-8071-4096-b9b6-9f301e332d1b", "Users", null, false, "İbrahim", "Arslan", false, null, null, null, null, null, false, null, 1, "1ec1fca7-3560-4074-b3f4-384886cabfaa", false, "ibrahimars" },
                    { "8", 0, null, null, null, "193e2f6a-1713-48e3-92c2-bc809a1cf3c8", "Users", null, false, "Priscilla", "Erickson", false, null, null, null, null, null, false, null, 1, "0bf30c9e-ab42-4efa-9804-b00a8f39c565", false, "mookie13" },
                    { "80", 0, null, null, null, "aac356dd-cfd8-4c9a-9235-9e6278c62d8c", "Users", null, false, "Emine", "Yıldız", false, null, null, null, null, null, false, null, 1, "6a1282fa-c553-4cb0-8e32-fd5e6a7412d5", false, "emineyildiz" },
                    { "81", 0, null, null, null, "59a34e3f-24ac-4766-81fe-51a470dd9662", "Users", null, false, "Osman", "Aydın", false, null, null, null, null, null, false, null, 1, "0b45cf66-489d-4345-8a4f-98f2ad6cff52", false, "osmanaydin" },
                    { "82", 0, null, null, null, "41ab506a-f12f-4f4f-80a8-bb43215d0bc4", "Users", null, false, "Elif", "Erdoğan", false, null, null, null, null, null, false, null, 1, "73bd3700-9db3-4d6b-a5e9-357f5511910a", false, "eliferd" },
                    { "83", 0, null, null, null, "5574089f-f97f-4a82-986f-368c6e0c4e14", "Users", null, false, "Hüseyin", "Özdemir", false, null, null, null, null, null, false, null, 1, "a615bb63-0641-48e5-9a26-85dfd798e1d4", false, "hozdemir" },
                    { "84", 0, null, null, null, "8398b611-6057-4f6d-b664-2e2112160c9b", "Users", null, false, "Hatice", "Korkmaz", false, null, null, null, null, null, false, null, 1, "0bdf0e26-046e-4f5c-8d9d-a0c7b6f3afca", false, "hatice_k" },
                    { "85", 0, null, null, null, "7f3ac58c-7e3f-416b-ab06-05ac0bb92332", "Users", null, false, "Murat", "Çetin", false, null, null, null, null, null, false, null, 1, "50abe69b-1f30-4ee3-97a6-d65cd7747cb2", false, "muratcetin" },
                    { "86", 0, null, null, null, "75440988-3683-45b0-972b-a2eb94551699", "Users", null, false, "Esra", "Koç", false, null, null, null, null, null, false, null, 1, "ee27e14f-4097-47b2-adfd-b0b278e150bb", false, "esrakoc" },
                    { "87", 0, null, null, null, "1f74a18a-693b-4b0e-8a1c-231b1a1ad1cb", "Users", null, false, "Ali", "Güneş", false, null, null, null, null, null, false, null, 1, "1d8ebaa3-f96d-4a48-97a4-5ee71de7f3c9", false, "aligunes" },
                    { "88", 0, null, null, null, "b9d9802e-6d4a-4db7-a6cc-b697191aaa9e", "Users", null, false, "Selin", "Yalçın", false, null, null, null, null, null, false, null, 1, "c0610f6c-00ff-4cb2-9999-07b0647a8e97", false, "selinyalcin" },
                    { "89", 0, null, null, null, "a3ad8472-e2ef-47fc-8287-edfb8bd17baa", "Users", null, false, "Burak", "Doğan", false, null, null, null, null, null, false, null, 1, "c8169d96-2108-42d4-ac01-9803e9ffa579", false, "burakd" },
                    { "9", 0, null, null, null, "395e92c8-091b-4138-a8fc-0f268d1cb352", "Users", null, false, "Elizabeth", "Pham", false, null, null, null, null, null, false, null, 1, "bf59070f-4e96-49d8-88bd-00ee3f54f65d", false, "Agreeable_Owl_31" },
                    { "90", 0, null, null, null, "aa5f2cb3-62c4-460c-9012-ce77ad908a0a", "Users", null, false, "Merve", "Kurt", false, null, null, null, null, null, false, null, 1, "ac57a38a-2ffc-4906-9443-75584e8f39cd", false, "mervekurt" },
                    { "91", 0, null, null, null, "76620104-0d92-49db-9a45-7161465a824b", "Users", null, false, "Emre", "Şen", false, null, null, null, null, null, false, null, 1, "efd4c33c-bb7f-4f62-9d0c-82e1279bb046", false, "emresen" },
                    { "92", 0, null, null, null, "b761bc97-d0a5-4e27-9399-f8ba5f394a2f", "Users", null, false, "Gizem", "Aslan", false, null, null, null, null, null, false, null, 1, "cb1f32af-e866-4f70-8e03-4667b2b5d63a", false, "gizemaslan" },
                    { "93", 0, null, null, null, "8ae48bf8-d6f3-4a3e-96ee-1e321da94eb9", "Users", null, false, "Serkan", "Özer", false, null, null, null, null, null, false, null, 1, "df3725f2-1049-4339-ac33-51a614cad2b7", false, "serkanozer" },
                    { "94", 0, null, null, null, "890068d3-219a-43ef-bb4e-d9721cc846a0", "Users", null, false, "Deniz", "Aktaş", false, null, null, null, null, null, false, null, 1, "6cd0c2c6-7ee2-4d02-b00f-57c45c5a6b6f", false, "denizaktas" },
                    { "95", 0, null, null, null, "476a4222-fd72-42bd-9b56-cca6524a85c4", "Users", null, false, "Cansu", "Akar", false, null, null, null, null, null, false, null, 1, "400c13b0-f07f-4b6a-aefd-2a0dcb361160", false, "cansuakar" },
                    { "96", 0, null, null, null, "8710da9c-79b5-41db-b5f6-a410ddae3007", "Users", null, false, "Volkan", "Kara", false, null, null, null, null, null, false, null, 1, "e153f39f-c219-41a2-82ff-90062c7bcba6", false, "volkankara" },
                    { "97", 0, null, null, null, "fb8639f8-fcd9-498d-8adb-9d3fc8b37f1c", "Users", null, false, "Melis", "Tuncer", false, null, null, null, null, null, false, null, 1, "15891daf-0d9f-4945-8d48-6a9b8e44718e", false, "melistuncer" },
                    { "98", 0, null, null, null, "3944f51b-31bb-489e-8ab6-45184a0d6928", "Users", null, false, "Kaan", "Polat", false, null, null, null, null, null, false, null, 1, "48f336f6-ba61-4cd6-a3ec-ca5768f6c487", false, "kaanpolat" },
                    { "99", 0, null, null, null, "224742e4-6b39-4837-a673-0f7d898cd1a3", "Users", null, false, "Ebru", "Altın", false, null, null, null, null, null, false, null, 1, "11b4b1a3-e01c-4990-a514-ca2487d4f91e", false, "ebrualtin" }
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
                table: "Ingredients",
                columns: new[] { "Id", "Image", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, null, "Carrots", 1 },
                    { 2, null, "Potatoes", 1 },
                    { 3, null, "Onions", 1 },
                    { 4, null, "Garlic", 1 },
                    { 5, null, "Bell peppers", 1 },
                    { 6, null, "Tomatoes", 1 },
                    { 7, null, "Broccoli", 1 },
                    { 8, null, "Spinach", 1 },
                    { 9, null, "Kale", 1 },
                    { 10, null, "Zucchini", 1 },
                    { 11, null, "Eggplant", 1 },
                    { 12, null, "Mushrooms", 1 },
                    { 13, null, "Cauliflower", 1 },
                    { 14, null, "Asparagus", 1 },
                    { 15, null, "Green beans", 1 },
                    { 16, null, "Cabbage", 1 },
                    { 17, null, "Sweet potatoes", 1 },
                    { 18, null, "Corn", 1 },
                    { 19, null, "Peas", 1 },
                    { 20, null, "Leeks", 1 },
                    { 21, null, "Apples", 1 },
                    { 22, null, "Oranges", 1 },
                    { 23, null, "Bananas", 1 },
                    { 24, null, "Strawberries", 1 },
                    { 25, null, "Blueberries", 1 },
                    { 26, null, "Lemons", 1 },
                    { 27, null, "Limes", 1 },
                    { 28, null, "Grapes", 1 },
                    { 29, null, "Mangoes", 1 },
                    { 30, null, "Pineapples", 1 },
                    { 31, null, "Cherries", 1 },
                    { 32, null, "Watermelon", 1 },
                    { 33, null, "Peaches", 1 },
                    { 34, null, "Plums", 1 },
                    { 35, null, "Raspberries", 1 },
                    { 36, null, "Blackberries", 1 },
                    { 37, null, "Pears", 1 },
                    { 38, null, "Cantaloupe", 1 },
                    { 39, null, "Pomegranates", 1 },
                    { 40, null, "Figs", 1 },
                    { 41, null, "Chicken", 1 },
                    { 42, null, "Beef", 1 },
                    { 43, null, "Pork", 1 },
                    { 44, null, "Lamb", 1 },
                    { 45, null, "Salmon", 1 },
                    { 46, null, "Shrimp", 1 },
                    { 47, null, "Tuna", 1 },
                    { 48, null, "Eggs", 1 },
                    { 49, null, "Tofu", 1 },
                    { 50, null, "Tempeh", 1 },
                    { 51, null, "Lentils", 1 },
                    { 52, null, "Chickpeas", 1 },
                    { 53, null, "Black beans", 1 },
                    { 54, null, "Kidney beans", 1 },
                    { 55, null, "Ground turkey", 1 },
                    { 56, null, "Sardines", 1 },
                    { 57, null, "Scallops", 1 },
                    { 58, null, "Halibut", 1 },
                    { 59, null, "Duck", 1 },
                    { 60, null, "Lobster", 1 },
                    { 61, null, "Milk", 1 },
                    { 62, null, "Butter", 1 },
                    { 63, null, "Cheese", 1 },
                    { 64, null, "Yogurt", 1 },
                    { 65, null, "Cream", 1 },
                    { 66, null, "Sour cream", 1 },
                    { 67, null, "Cream cheese", 1 },
                    { 68, null, "Cottage cheese", 1 },
                    { 69, null, "Feta cheese", 1 },
                    { 70, null, "Ricotta", 1 },
                    { 71, null, "Rice", 1 },
                    { 72, null, "Pasta", 1 },
                    { 73, null, "Quinoa", 1 },
                    { 74, null, "Couscous", 1 },
                    { 75, null, "Bread", 1 },
                    { 76, null, "Oats", 1 },
                    { 77, null, "Tortillas", 1 },
                    { 78, null, "Barley", 1 },
                    { 79, null, "Polenta", 1 },
                    { 80, null, "Cornmeal", 1 },
                    { 81, null, "Almonds", 1 },
                    { 82, null, "Walnuts", 1 },
                    { 83, null, "Pecans", 1 },
                    { 84, null, "Cashews", 1 },
                    { 85, null, "Peanuts", 1 },
                    { 86, null, "Sesame seeds", 1 },
                    { 87, null, "Sunflower seeds", 1 },
                    { 88, null, "Chia seeds", 1 },
                    { 89, null, "Flaxseeds", 1 },
                    { 90, null, "Pumpkin seeds", 1 },
                    { 91, null, "Basil", 1 },
                    { 92, null, "Oregano", 1 },
                    { 93, null, "Thyme", 1 },
                    { 94, null, "Rosemary", 1 },
                    { 95, null, "Cilantro", 1 },
                    { 96, null, "Dill", 1 },
                    { 97, null, "Parsley", 1 },
                    { 98, null, "Mint", 1 },
                    { 99, null, "Turmeric", 1 },
                    { 100, null, "Cumin", 1 },
                    { 101, null, "Paprika", 1 },
                    { 102, null, "Cayenne pepper", 1 },
                    { 103, null, "Black pepper", 1 },
                    { 104, null, "Cinnamon", 1 },
                    { 105, null, "Nutmeg", 1 },
                    { 106, null, "Cardamom", 1 },
                    { 107, null, "Ginger", 1 },
                    { 108, null, "Coriander", 1 },
                    { 109, null, "Bay leaves", 1 },
                    { 110, null, "Cloves", 1 },
                    { 111, null, "Olive oil", 1 },
                    { 112, null, "Vegetable oil", 1 },
                    { 113, null, "Coconut oil", 1 },
                    { 114, null, "Sesame oil", 1 },
                    { 115, null, "Avocado oil", 1 },
                    { 116, null, "Apple cider vinegar", 1 },
                    { 117, null, "White vinegar", 1 },
                    { 118, null, "Balsamic vinegar", 1 },
                    { 119, null, "Rice vinegar", 1 },
                    { 120, null, "Red wine vinegar", 1 },
                    { 121, null, "Sugar", 1 },
                    { 122, null, "Honey", 1 },
                    { 123, null, "Maple syrup", 1 },
                    { 124, null, "Agave nectar", 1 },
                    { 125, null, "Molasses", 1 },
                    { 126, null, "Stevia", 1 },
                    { 127, null, "Corn syrup", 1 },
                    { 128, null, "Coconut sugar", 1 },
                    { 129, null, "Date syrup", 1 },
                    { 130, null, "Monk fruit sweetener", 1 },
                    { 131, null, "Soy sauce", 1 },
                    { 132, null, "Worcestershire sauce", 1 },
                    { 133, null, "Hot sauce", 1 },
                    { 134, null, "Mustard", 1 },
                    { 135, null, "Ketchup", 1 },
                    { 136, null, "Mayonnaise", 1 },
                    { 137, null, "Miso paste", 1 },
                    { 138, null, "Tahini", 1 },
                    { 139, null, "Yeast", 1 },
                    { 140, null, "Peanut butter", 1 },
                    { 141, null, "Almond butter", 1 },
                    { 142, null, "Vanilla extract", 1 },
                    { 143, null, "Cocoa powder", 1 },
                    { 144, null, "Baking soda", 1 },
                    { 145, null, "Baking powder", 1 },
                    { 146, null, "Cornstarch", 1 },
                    { 147, null, "Gelatin", 1 },
                    { 148, null, "Chicken stock", 1 },
                    { 149, null, "Beef stock", 1 },
                    { 150, null, "Vegetable stock", 1 }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "BannerImage", "BodyText", "CreatedAt", "Header", "RecipeId", "UserId" },
                values: new object[,]
                {
                    { 1, null, "If there was a list of every entity that ever existed, The art of food presentation would rank TOP5 no doubt. VOL#1", new DateTime(2024, 12, 31, 0, 17, 54, 0, DateTimeKind.Utc), "THE ART OF FOOD PRESENTATION: A Comprehensive Anal", null, "61" },
                    { 2, null, "If there was a list of every entity that ever existed, Cooking with seasonal vegetables would rank TOP5 no doubt. VOL#2", new DateTime(2024, 12, 31, 0, 18, 1, 0, DateTimeKind.Utc), "COOKING WITH SEASONAL VEGETABLES: A Comprehensive ", null, "88" },
                    { 3, null, "If there was a list of every entity that ever existed, The best coffee brands would rank TOP5 no doubt. VOL#3", new DateTime(2024, 12, 31, 0, 18, 8, 0, DateTimeKind.Utc), "THE BEST COFFEE BRANDS: A Comprehensive Analysis, ", null, "44" },
                    { 4, null, "If there was a list of every entity that ever existed, Homemade jams and preserves would rank TOP5 no doubt. VOL#4", new DateTime(2024, 12, 31, 0, 18, 13, 0, DateTimeKind.Utc), "HOMEMADE JAMS AND PRESERVES: A Comprehensive Analy", null, "72" },
                    { 5, null, "If there was a list of every entity that ever existed, Food photography would rank TOP5 no doubt. VOL#5", new DateTime(2024, 12, 31, 0, 18, 20, 0, DateTimeKind.Utc), "FOOD PHOTOGRAPHY: A Comprehensive Analysis, VOL. 5", null, "39" },
                    { 6, null, "If there was a list of every entity that ever existed, The art of food presentation would rank TOP5 no doubt. VOL#6", new DateTime(2024, 12, 31, 0, 18, 26, 0, DateTimeKind.Utc), "THE ART OF FOOD PRESENTATION: A Comprehensive Anal", null, "57" },
                    { 7, null, "If there was a list of every entity that ever existed, Best BBQ Techniques would rank TOP5 no doubt. VOL#7", new DateTime(2024, 12, 31, 0, 18, 31, 0, DateTimeKind.Utc), "BEST BBQ TECHNIQUES: A Comprehensive Analysis, VOL", null, "1" },
                    { 8, null, "In a world of culinary delights, there's solace to be found in comfort foods—those dishes that evoke a sense of home, warmth, and nostalgia. But what if we ventured beyond our familiar culinary horizons and explored the comforting flavors of other cultures? From the hearty stews of Europe to the spicy curries of Asia, every corner of the globe holds its own culinary gems that offer a unique and comforting experience. Let's embark on a gastronomic journey and discover some of the world's most comforting foods:\n\n1. Osso Buco (Italy): Slow-braised veal shanks in a rich tomato-based sauce, perfect for a chilly evening.\n2. Pad Thai (Thailand): A stir-fried noodle dish with a harmonious balance of sweet, sour, and savory flavors.\n3. Chicken Tikka Masala (India): A creamy, aromatic curry featuring tender chicken marinated in yogurt and spices.\n4. Bœuf Bourguignon (France): A classic French beef stew with red wine, mushrooms, and bacon, perfect for a cozy dinner party.\n5. Moqueca (Brazil): A flavorful seafood stew cooked in a coconut milk broth, reflecting the vibrancy of Brazilian cuisine.\n\nExploring these different dishes not only satisfies our taste buds but also connects us to diverse cultures and their culinary traditions. Whether it's the heartwarming comfort of a steaming bowl of stew or the spicy indulgence of a curry, these foods have the power to transport us to different corners of the world.\n\nSo, next time you're seeking solace in food, don't be afraid to step out of your comfort zone. Embrace the culinary wonders that await you beyond your borders and discover the comforting embrace of foods around the globe.", new DateTime(2024, 12, 31, 0, 18, 37, 0, DateTimeKind.Utc), "EMBRACE THE COMFORT OF FOODS AROUND THE GLOBE", null, "37" },
                    { 9, null, "If there was a list of every entity that ever existed, The World of Spicy Foods would rank TOP5 no doubt. VOL#8", new DateTime(2024, 12, 31, 0, 18, 43, 0, DateTimeKind.Utc), "THE WORLD OF SPICY FOODS: A Comprehensive Analysis", null, "67" },
                    { 10, null, "If there was a list of every entity that ever existed, Coffee brewing methods would rank TOP5 no doubt. VOL#9", new DateTime(2024, 12, 31, 0, 18, 48, 0, DateTimeKind.Utc), "COFFEE BREWING METHODS: A Comprehensive Analysis, ", null, "11" }
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

            migrationBuilder.InsertData(
                table: "Ingredients_Preferences",
                columns: new[] { "Id", "IngredientId", "PreferenceId" },
                values: new object[,]
                {
                    { 1, 61, 1 },
                    { 2, 62, 1 },
                    { 3, 63, 1 },
                    { 4, 64, 1 },
                    { 5, 65, 1 },
                    { 6, 66, 1 },
                    { 7, 67, 1 },
                    { 8, 68, 1 },
                    { 9, 69, 1 },
                    { 10, 70, 1 },
                    { 11, 48, 2 },
                    { 12, 136, 2 },
                    { 13, 45, 3 },
                    { 14, 47, 3 },
                    { 15, 56, 3 },
                    { 16, 58, 3 },
                    { 17, 46, 4 },
                    { 18, 57, 4 },
                    { 19, 60, 4 },
                    { 20, 81, 5 },
                    { 21, 82, 5 },
                    { 22, 83, 5 },
                    { 23, 84, 5 },
                    { 24, 141, 5 },
                    { 25, 85, 6 },
                    { 26, 140, 6 },
                    { 27, 72, 7 },
                    { 28, 75, 7 },
                    { 29, 49, 8 },
                    { 30, 50, 8 },
                    { 31, 131, 8 },
                    { 32, 137, 8 },
                    { 33, 86, 9 },
                    { 34, 114, 9 },
                    { 35, 138, 9 }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "DistrictId", "Name", "Street" },
                values: new object[,]
                {
                    { 1, 1, "1", "AKÖREN" },
                    { 2, 1, "1", "MANSURLU" },
                    { 3, 1, "1", "SİNANPAŞA" },
                    { 4, 1, "1", "BAŞPINAR" },
                    { 5, 1, "1", "AKPINAR" },
                    { 6, 1, "1", "BOZTAHTA" },
                    { 7, 1, "1", "BÜYÜKSOFULU" },
                    { 8, 1, "1", "CERİTLER" },
                    { 9, 1, "1", "DARILIK" },
                    { 10, 1, "1", "DAYILAR" },
                    { 11, 1, "1", "DÖLEKLİ" },
                    { 12, 1, "1", "EĞNER" },
                    { 13, 1, "1", "GERDİBİ" },
                    { 14, 1, "1", "GÖKÇE" },
                    { 15, 1, "1", "EBRİŞİM" },
                    { 16, 1, "1", "KABASAKAL" },
                    { 17, 1, "1", "KARAHAN" },
                    { 18, 1, "1", "KICAK" },
                    { 19, 1, "1", "KIŞLAK" },
                    { 20, 1, "1", "KIZILDAM" },
                    { 21, 1, "1", "KÖKEZ" },
                    { 22, 1, "1", "KÖPRÜCÜK" },
                    { 23, 1, "1", "KÜP" },
                    { 24, 1, "1", "MADENLİ" },
                    { 25, 1, "1", "MAZILIK" },
                    { 26, 1, "1", "TOPALLI" },
                    { 27, 1, "1", "UZUNKUYU" },
                    { 28, 1, "1", "POSYAĞBASAN" },
                    { 29, 1, "1", "GİREĞİYENİKÖY" },
                    { 30, 1, "1", "YETİMLİ" },
                    { 31, 1, "1", "YÜKSEKÖREN" },
                    { 32, 2, "1", "AĞAÇPINAR" },
                    { 33, 2, "1", "ALTIKARA" },
                    { 34, 2, "1", "AZİZLİ" },
                    { 35, 2, "1", "BÜYÜKBURHANİYE" },
                    { 36, 2, "1", "ÇAKALDERE" },
                    { 37, 2, "1", "ÇEVRETEPE" },
                    { 38, 2, "1", "ÇİÇEKLİ" },
                    { 39, 2, "1", "ÇİFTLİKLER" },
                    { 40, 2, "1", "ÇOKÇAPINAR" },
                    { 41, 2, "1", "DEĞİRMENDERE" },
                    { 42, 2, "1", "DOKUZTEKNE" },
                    { 43, 2, "1", "DURHASANDEDE" },
                    { 44, 2, "1", "DUTLUPINAR" },
                    { 45, 2, "1", "ERENLER" },
                    { 46, 2, "1", "GÜNDOĞAN" },
                    { 47, 2, "1", "VEYSİYE " },
                    { 48, 2, "1", "HAMİDİYE" },
                    { 49, 2, "1", "HAMİTBEY" },
                    { 50, 2, "1", "HAMİTBEYBUCAĞI" },
                    { 51, 2, "1", "İMRAN" },
                    { 52, 2, "1", "MERCİN" },
                    { 53, 2, "1", "İSALI" },
                    { 54, 2, "1", "KILIÇKAYA" },
                    { 55, 2, "1", "KIZILDERE" },
                    { 56, 2, "1", "KÖPRÜLÜ" },
                    { 57, 2, "1", "KÖRKUYU" },
                    { 58, 2, "1", "KUZUCAK" },
                    { 59, 2, "1", "KÜÇÜKBURHANİYE" },
                    { 60, 2, "1", "KÜÇÜKMANGIT" },
                    { 61, 2, "1", "NARLIK" },
                    { 62, 2, "1", "SAĞIRLAR" },
                    { 63, 2, "1", "SELİMİYE" },
                    { 64, 2, "1", "SİRKELİ" },
                    { 65, 2, "1", "SOĞUKPINAR" },
                    { 66, 2, "1", "TOKTAMIŞ" },
                    { 67, 2, "1", "YELLİBEL" },
                    { 68, 2, "1", "NAZIMBEY YENİKÖY" },
                    { 69, 2, "1", "YILANKALE" },
                    { 70, 2, "1", "ADAPINAR" },
                    { 71, 2, "1", "ALTIGÖZBEKİRLİ" },
                    { 72, 2, "1", "BURHANLI" },
                    { 73, 2, "1", "CEYHANBEKİRLİ" },
                    { 74, 2, "1", "ÇATAKLI" },
                    { 75, 2, "1", "DAĞISTAN" },
                    { 76, 2, "1", "DEĞİRMENLİ" },
                    { 77, 2, "1", "DİKİLİTAŞ" },
                    { 78, 2, "1", "EKİNYAZI" },
                    { 79, 2, "1", "ELMAGÖLÜ" },
                    { 80, 2, "1", "GÜNLÜCE" },
                    { 81, 2, "1", "IRMAKLI" },
                    { 82, 2, "1", "KARAKAYALI" },
                    { 83, 2, "1", "TATARLI" },
                    { 84, 2, "1", "YALAK" },
                    { 85, 2, "1", "AĞAÇLI" },
                    { 86, 2, "1", "AKDAM" },
                    { 87, 2, "1", "BAŞÖREN" },
                    { 88, 2, "1", "CAMUZAĞILI" },
                    { 89, 2, "1", "ÇATALHÜYÜK" },
                    { 90, 2, "1", "TUMLU" },
                    { 91, 2, "1", "GÜMÜRDÜLÜ" },
                    { 92, 2, "1", "ISIRGANLI" },
                    { 93, 2, "1", "İNCEYER" },
                    { 94, 2, "1", "KIVRIKLI" },
                    { 95, 2, "1", "SARIBAHÇE" },
                    { 96, 2, "1", "SOYSALLI" },
                    { 97, 2, "1", "TATLIKUYU" },
                    { 98, 2, "1", "ÜÇDUTYEŞİLOVA" },
                    { 99, 2, "1", "YEŞİLBAHÇE" },
                    { 100, 2, "1", "YEŞİLDAM" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AddressId", "BodyText", "CreatedAt", "CreatorId", "Date", "Title" },
                values: new object[,]
                {
                    { 1, 69, "Indulge in an evening of seductive desserts with a guided tasting crafted for every palate.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "10", new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Dessert Delights" },
                    { 2, 49, "Join us for a sizzling BBQ battle, where culinary masters fire up their grills for mouthwatering cre", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "3", new DateTime(2025, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Grill Master ShowDown" },
                    { 3, 30, "Indulge in a culinary extravaganza where flavors dance on your palate.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "46", new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Gastronomic Symphony" },
                    { 4, 93, "Indulge in a culinary extravaganza! Join us for a feast of tantalizing dishes, where street food art", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Savor the Flavors: Street Food Market" },
                    { 5, 27, "Capture the beauty of food with expert tips and techniques for stunning food photography.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "49", new DateTime(2025, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Clicks: Food Photography Workshop" },
                    { 6, 67, "Join us for a tantalizing wine tasting journey, where flavors dance with aromas", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "81", new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Unveiling the Vine's Secrets" },
                    { 7, 2, "Street food market with flavors from around the globe.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "15", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Savor the Street" },
                    { 8, 91, "Craft authentic pasta from scratch with our interactive workshop. Discover secrets and indulge in yo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "66", new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Perfection in the Kitchen" },
                    { 9, 38, "Indulge in global flavors at our vibrant Street Food Market!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "72", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Taste the World" },
                    { 10, 57, "Explore a world of flavors at our International Food Fair!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "43", new DateTime(2025, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Odyssey" },
                    { 11, 21, "Indulge in a symphony of flavors as we present an array of delectable desserts, crafted with love an", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "20", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Sweet Indulgence: Dessert Extravaganza" },
                    { 12, 19, "Immerse yourself in the art of sushi making with expert guidance. Craft your own delicious creations", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "54", new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Sushi Sensations: A Culinary Journey" },
                    { 13, 34, "Join us for a hands-on pasta making workshop. Discover the art of crafting delicious fresh pasta fro", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "8", new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Perfection" },
                    { 14, 1, "Indulge in a culinary journey as street food vendors from around the globe gather to tantalize your ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "49", new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Taste the World on Every Corner" },
                    { 15, 48, "Get ready for a feast of epic proportions! Join us for a grilling competition where BBQ masters show", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "57", new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Sizzling Showdown: BBQ Extravaganza" },
                    { 16, 18, "Savor flavors from every corner, as culinary artisans gather to showcase their finest creations.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "47", new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Crossroads: A Global Feast" },
                    { 17, 56, "Join us for an unforgettable Chef's Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "57", new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Chef's Table Dinner Culinary Experience" },
                    { 18, 78, "An exquisite culinary journey awaits, where master chefs unveil their most tantalizing creations.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "91", new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Feast of Flavors" },
                    { 19, 38, "Join us for an unforgettable Street Food Market food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "35", new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Street Food Market Culinary Experience" },
                    { 20, 7, "Embark on a tantalizing culinary journey as local and international chefs showcase their masterful d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "19", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Expedition: Flavors from Afar" },
                    { 21, 74, "Join us for an unforgettable Farm-to-Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "8", new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Farm-to-Table Dinner Culinary Experience" },
                    { 22, 1, "Join us for an unforgettable Sushi Making Class food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "85", new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Sushi Making Class Culinary Experience" },
                    { 23, 5, "Join us for an unforgettable Baking Masterclass food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "26", new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Baking Masterclass Culinary Experience" },
                    { 24, 90, "Join us for an unforgettable Pasta Making Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "77", new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Making Workshop Culinary Experience" },
                    { 25, 31, "Join us for an unforgettable Pasta Making Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "43", new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Making Workshop Culinary Experience" },
                    { 26, 96, "Join us for an unforgettable International Food Fair food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "33", new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), "International Food Fair Culinary Experience" },
                    { 27, 1, "Join us for an unforgettable Food Photography Class food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "38", new DateTime(2025, 4, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Food Photography Class Culinary Experience" },
                    { 28, 18, "Join us for an unforgettable Sushi Making Class food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "62", new DateTime(2025, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Sushi Making Class Culinary Experience" },
                    { 29, 61, "Join us for an unforgettable Food Photography Class food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "38", new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Food Photography Class Culinary Experience" },
                    { 30, 98, "Join us for an unforgettable Pasta Making Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "92", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Making Workshop Culinary Experience" },
                    { 31, 45, "Join us for an unforgettable Craft Beer Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "34", new DateTime(2025, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Craft Beer Tasting Culinary Experience" },
                    { 32, 37, "Join us for an unforgettable Street Food Market food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "27", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Street Food Market Culinary Experience" },
                    { 33, 85, "Join us for an unforgettable Pasta Making Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "20", new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Making Workshop Culinary Experience" },
                    { 34, 80, "Join us for an unforgettable Craft Beer Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "34", new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Craft Beer Tasting Culinary Experience" },
                    { 35, 62, "Join us for an unforgettable Sushi Making Class food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "71", new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Sushi Making Class Culinary Experience" },
                    { 36, 31, "Join us for an unforgettable Pasta Making Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "49", new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Making Workshop Culinary Experience" },
                    { 37, 55, "Join us for an unforgettable Craft Beer Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "37", new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Craft Beer Tasting Culinary Experience" },
                    { 38, 74, "Join us for an unforgettable Food Festival food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "58", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Food Festival Culinary Experience" },
                    { 39, 91, "Join us for an unforgettable International Food Fair food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "87", new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Utc), "International Food Fair Culinary Experience" },
                    { 40, 21, "Join us for an unforgettable Chocolate Making Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "77", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Chocolate Making Workshop Culinary Experience" },
                    { 41, 54, "Join us for an unforgettable Cooking Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "26", new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Cooking Workshop Culinary Experience" },
                    { 42, 12, "Join us for an unforgettable Craft Beer Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "77", new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Craft Beer Tasting Culinary Experience" },
                    { 43, 23, "Join us for an unforgettable Craft Beer Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "67", new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Craft Beer Tasting Culinary Experience" },
                    { 44, 89, "Join us for an unforgettable Food Festival food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "65", new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Food Festival Culinary Experience" },
                    { 45, 65, "Join us for an unforgettable Food Festival food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "78", new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Food Festival Culinary Experience" },
                    { 46, 20, "Join us for an unforgettable Dessert Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "17", new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Dessert Tasting Culinary Experience" },
                    { 47, 30, "Join us for an unforgettable Farm-to-Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "82", new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Farm-to-Table Dinner Culinary Experience" },
                    { 48, 40, "Join us for an unforgettable Street Food Market food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "85", new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Street Food Market Culinary Experience" },
                    { 49, 47, "Join us for an unforgettable Craft Beer Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "93", new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Craft Beer Tasting Culinary Experience" },
                    { 50, 85, "Join us for an unforgettable BBQ Competition food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "72", new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), "BBQ Competition Culinary Experience" },
                    { 51, 86, "Join us for an unforgettable Craft Beer Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "87", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Craft Beer Tasting Culinary Experience" },
                    { 52, 13, "Join us for an unforgettable Cooking Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "92", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Cooking Workshop Culinary Experience" },
                    { 53, 81, "Join us for an unforgettable BBQ Competition food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "34", new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "BBQ Competition Culinary Experience" },
                    { 54, 45, "Join us for an unforgettable BBQ Competition food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "98", new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), "BBQ Competition Culinary Experience" },
                    { 55, 81, "Join us for an unforgettable Sushi Making Class food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "45", new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Sushi Making Class Culinary Experience" },
                    { 56, 90, "Join us for an unforgettable Farm-to-Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "31", new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Farm-to-Table Dinner Culinary Experience" },
                    { 57, 64, "Join us for an unforgettable Chocolate Making Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "6", new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Chocolate Making Workshop Culinary Experience" },
                    { 58, 29, "Join us for an unforgettable Dessert Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "89", new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Dessert Tasting Culinary Experience" },
                    { 59, 33, "Join us for an unforgettable Baking Masterclass food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "45", new DateTime(2025, 3, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Baking Masterclass Culinary Experience" },
                    { 60, 86, "Join us for an unforgettable Wine Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "16", new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Wine Tasting Culinary Experience" },
                    { 61, 73, "Join us for an unforgettable Chef's Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "58", new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Chef's Table Dinner Culinary Experience" },
                    { 62, 66, "Join us for an unforgettable Chef's Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "49", new DateTime(2025, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Chef's Table Dinner Culinary Experience" },
                    { 63, 60, "Join us for an unforgettable Pasta Making Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "41", new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Making Workshop Culinary Experience" },
                    { 64, 65, "Join us for an unforgettable International Food Fair food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "79", new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), "International Food Fair Culinary Experience" },
                    { 65, 35, "Join us for an unforgettable Street Food Market food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "86", new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Street Food Market Culinary Experience" },
                    { 66, 74, "Join us for an unforgettable Pasta Making Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "65", new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Making Workshop Culinary Experience" },
                    { 67, 16, "Join us for an unforgettable BBQ Competition food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "81", new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "BBQ Competition Culinary Experience" },
                    { 68, 97, "Join us for an unforgettable International Food Fair food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "42", new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), "International Food Fair Culinary Experience" },
                    { 69, 89, "Join us for an unforgettable Farm-to-Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "88", new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Farm-to-Table Dinner Culinary Experience" },
                    { 70, 16, "Join us for an unforgettable International Food Fair food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "100", new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Utc), "International Food Fair Culinary Experience" },
                    { 71, 25, "Learn food photography secrets from a pro and elevate your culinary shots.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "98", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Canvas: Food Photography Masterclass" },
                    { 72, 54, "Join us for an unforgettable Chef's Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "70", new DateTime(2025, 9, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Chef's Table Dinner Culinary Experience" },
                    { 73, 9, "Unleash your inner chocolatier and craft delectable treats!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "91", new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Chocolate's Sweet Symphony" },
                    { 74, 82, "Join us for an exquisite evening of wine tasting, savoring the perfect pairings with delectable culi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "62", new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Unveiling the Flavors of Wine and Cuisine" },
                    { 75, 58, "Savor global flavors & culinary delights from around the world!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "60", new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Street Food Extravaganza" },
                    { 76, 32, "Indulge in an intimate dining experience. Watch our chefs craft a masterpiece before your eyes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "85", new DateTime(2025, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "An Evening at the Chef's Table" },
                    { 77, 12, "Join renowned chefs for cooking demos and hands-on workshops exploring flavors from around the globe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "89", new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Creations Festival" },
                    { 78, 83, "Embark on a culinary journey through diverse flavors, cooking demos, and hands-on experiences.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "1", new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Extravaganza" },
                    { 79, 36, "Join us for a culinary journey where farm-fresh flavors take center stage.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "71", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Feast from the Farm" },
                    { 80, 17, "Indulge in culinary delights from around the globe at our vibrant Street Food Market.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "58", new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Savor the Street" },
                    { 81, 92, "Indulge in a hands-on adventure, crafting exquisite chocolates from scratch!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "85", new DateTime(2025, 8, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Chocolate Crafting Extravaganza" },
                    { 82, 71, "Savor the freshest flavors as local ingredients dance on your plate.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "92", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Odyssey: Farm-to-Fork Feast" },
                    { 83, 97, "Join our hands-on workshop and learn the art of crafting authentic Italian pasta from scratch.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "70", new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Making Mastery" },
                    { 84, 43, "Join us as we master the art of crafting fresh, homemade pasta from scratch.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "69", new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta Proficiency Workshop" },
                    { 85, 100, "Capture the art of cooking through the lens. Learn techniques to elevate your food photography skill", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "67", new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Canvas: Food Photography Masterclass" },
                    { 86, 91, "Savor a weekend of exquisite cuisine, cooking demos, and culinary workshops.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "30", new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Delights" },
                    { 87, 48, "Embark on a culinary adventure, exploring diverse cuisines and tantalizing tastes from every corner ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "22", new DateTime(2025, 9, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Savor the World's Flavors" },
                    { 88, 96, "Witness culinary artistry unfold as our chef paints flavors on your plate.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "46", new DateTime(2025, 8, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Culinary Canvas: An Immersive Dining Experience" },
                    { 89, 71, "Grab your forks and gather 'round for a sizzling BBQ competition where pitmasters ignite taste buds.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "66", new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Smoky 'Cue Showdown" },
                    { 90, 87, "Join us for an unforgettable Cooking Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "48", new DateTime(2025, 5, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Cooking Workshop Culinary Experience" },
                    { 91, 28, "Join us for an unforgettable Cooking Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "96", new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Cooking Workshop Culinary Experience" },
                    { 92, 75, "Join us for an unforgettable Chef's Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "24", new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Chef's Table Dinner Culinary Experience" },
                    { 93, 36, "Join us for an unforgettable Food Festival food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "46", new DateTime(2025, 7, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Food Festival Culinary Experience" },
                    { 94, 89, "Join us for an unforgettable BBQ Competition food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "66", new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), "BBQ Competition Culinary Experience" },
                    { 95, 98, "Join us for an unforgettable Craft Beer Tasting food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "45", new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Craft Beer Tasting Culinary Experience" },
                    { 96, 97, "Join us for an unforgettable Cooking Workshop food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "58", new DateTime(2025, 7, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Cooking Workshop Culinary Experience" },
                    { 97, 31, "Join us for an unforgettable Chef's Table Dinner food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "56", new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Chef's Table Dinner Culinary Experience" },
                    { 98, 39, "Join us for an unforgettable Baking Masterclass food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "52", new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Baking Masterclass Culinary Experience" },
                    { 99, 77, "Join us for an unforgettable Food Photography Class food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "83", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Food Photography Class Culinary Experience" },
                    { 100, 60, "Join us for an unforgettable International Food Fair food experience!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "28", new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), "International Food Fair Culinary Experience" }
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
