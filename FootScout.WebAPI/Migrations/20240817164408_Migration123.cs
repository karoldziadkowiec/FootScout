using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootScout.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfMatches = table.Column<int>(type: "int", nullable: false),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    AdditionalAchievements = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerFeet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FootName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFeet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Min = table.Column<double>(type: "float", nullable: false),
                    Max = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User2Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_AspNetUsers_User1Id",
                        column: x => x.User1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_AspNetUsers_User2Id",
                        column: x => x.User2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClubHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerPositionId = table.Column<int>(type: "int", nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    League = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AchievementsId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubHistories_Achievements_AchievementsId",
                        column: x => x.AchievementsId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubHistories_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubHistories_PlayerPositions_PlayerPositionId",
                        column: x => x.PlayerPositionId,
                        principalTable: "PlayerPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubAdvertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerPositionId = table.Column<int>(type: "int", nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    League = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SalaryRangeId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClubMemberId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubAdvertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubAdvertisements_AspNetUsers_ClubMemberId",
                        column: x => x.ClubMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubAdvertisements_PlayerPositions_PlayerPositionId",
                        column: x => x.PlayerPositionId,
                        principalTable: "PlayerPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubAdvertisements_SalaryRanges_SalaryRangeId",
                        column: x => x.SalaryRangeId,
                        principalTable: "SalaryRanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAdvertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerPositionId = table.Column<int>(type: "int", nullable: false),
                    League = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    PlayerFootId = table.Column<int>(type: "int", nullable: false),
                    SalaryRangeId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAdvertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerAdvertisements_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAdvertisements_PlayerFeet_PlayerFootId",
                        column: x => x.PlayerFootId,
                        principalTable: "PlayerFeet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAdvertisements_PlayerPositions_PlayerPositionId",
                        column: x => x.PlayerPositionId,
                        principalTable: "PlayerPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAdvertisements_SalaryRanges_SalaryRangeId",
                        column: x => x.SalaryRangeId,
                        principalTable: "SalaryRanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteClubAdvertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubAdvertisementId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteClubAdvertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteClubAdvertisements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteClubAdvertisements_ClubAdvertisements_ClubAdvertisementId",
                        column: x => x.ClubAdvertisementId,
                        principalTable: "ClubAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubAdvertisementId = table.Column<int>(type: "int", nullable: false),
                    OfferStatusId = table.Column<int>(type: "int", nullable: false),
                    PlayerPositionId = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    PlayerFootId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerOffers_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerOffers_ClubAdvertisements_ClubAdvertisementId",
                        column: x => x.ClubAdvertisementId,
                        principalTable: "ClubAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerOffers_OfferStatuses_OfferStatusId",
                        column: x => x.OfferStatusId,
                        principalTable: "OfferStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerOffers_PlayerFeet_PlayerFootId",
                        column: x => x.PlayerFootId,
                        principalTable: "PlayerFeet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerOffers_PlayerPositions_PlayerPositionId",
                        column: x => x.PlayerPositionId,
                        principalTable: "PlayerPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerAdvertisementId = table.Column<int>(type: "int", nullable: false),
                    OfferStatusId = table.Column<int>(type: "int", nullable: false),
                    PlayerPositionId = table.Column<int>(type: "int", nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    League = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClubMemberId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubOffers_AspNetUsers_ClubMemberId",
                        column: x => x.ClubMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubOffers_OfferStatuses_OfferStatusId",
                        column: x => x.OfferStatusId,
                        principalTable: "OfferStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubOffers_PlayerAdvertisements_PlayerAdvertisementId",
                        column: x => x.PlayerAdvertisementId,
                        principalTable: "PlayerAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubOffers_PlayerPositions_PlayerPositionId",
                        column: x => x.PlayerPositionId,
                        principalTable: "PlayerPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoritePlayerAdvertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerAdvertisementId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritePlayerAdvertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritePlayerAdvertisements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritePlayerAdvertisements_PlayerAdvertisements_PlayerAdvertisementId",
                        column: x => x.PlayerAdvertisementId,
                        principalTable: "PlayerAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_User1Id",
                table: "Chats",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_User2Id",
                table: "Chats",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ClubAdvertisements_ClubMemberId",
                table: "ClubAdvertisements",
                column: "ClubMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubAdvertisements_PlayerPositionId",
                table: "ClubAdvertisements",
                column: "PlayerPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubAdvertisements_SalaryRangeId",
                table: "ClubAdvertisements",
                column: "SalaryRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubHistories_AchievementsId",
                table: "ClubHistories",
                column: "AchievementsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClubHistories_PlayerId",
                table: "ClubHistories",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubHistories_PlayerPositionId",
                table: "ClubHistories",
                column: "PlayerPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubOffers_ClubMemberId",
                table: "ClubOffers",
                column: "ClubMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubOffers_OfferStatusId",
                table: "ClubOffers",
                column: "OfferStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubOffers_PlayerAdvertisementId",
                table: "ClubOffers",
                column: "PlayerAdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubOffers_PlayerPositionId",
                table: "ClubOffers",
                column: "PlayerPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteClubAdvertisements_ClubAdvertisementId",
                table: "FavoriteClubAdvertisements",
                column: "ClubAdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteClubAdvertisements_UserId",
                table: "FavoriteClubAdvertisements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePlayerAdvertisements_PlayerAdvertisementId",
                table: "FavoritePlayerAdvertisements",
                column: "PlayerAdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePlayerAdvertisements_UserId",
                table: "FavoritePlayerAdvertisements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAdvertisements_PlayerFootId",
                table: "PlayerAdvertisements",
                column: "PlayerFootId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAdvertisements_PlayerId",
                table: "PlayerAdvertisements",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAdvertisements_PlayerPositionId",
                table: "PlayerAdvertisements",
                column: "PlayerPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAdvertisements_SalaryRangeId",
                table: "PlayerAdvertisements",
                column: "SalaryRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOffers_ClubAdvertisementId",
                table: "PlayerOffers",
                column: "ClubAdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOffers_OfferStatusId",
                table: "PlayerOffers",
                column: "OfferStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOffers_PlayerFootId",
                table: "PlayerOffers",
                column: "PlayerFootId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOffers_PlayerId",
                table: "PlayerOffers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOffers_PlayerPositionId",
                table: "PlayerOffers",
                column: "PlayerPositionId");
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
                name: "ClubHistories");

            migrationBuilder.DropTable(
                name: "ClubOffers");

            migrationBuilder.DropTable(
                name: "FavoriteClubAdvertisements");

            migrationBuilder.DropTable(
                name: "FavoritePlayerAdvertisements");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PlayerOffers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "PlayerAdvertisements");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "ClubAdvertisements");

            migrationBuilder.DropTable(
                name: "OfferStatuses");

            migrationBuilder.DropTable(
                name: "PlayerFeet");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PlayerPositions");

            migrationBuilder.DropTable(
                name: "SalaryRanges");
        }
    }
}
