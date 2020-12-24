using Microsoft.EntityFrameworkCore.Migrations;

namespace CA.ERP.DataAccess.Migrations
{
    public partial class renameUserBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBranch_Branches_BranchId",
                table: "UserBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBranch_Users_UserId",
                table: "UserBranch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBranch",
                table: "UserBranch");

            migrationBuilder.RenameTable(
                name: "UserBranch",
                newName: "UserBranches");

            migrationBuilder.RenameIndex(
                name: "IX_UserBranch_BranchId",
                table: "UserBranches",
                newName: "IX_UserBranches_BranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBranches",
                table: "UserBranches",
                columns: new[] { "UserId", "BranchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBranches_Branches_BranchId",
                table: "UserBranches",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBranches_Users_UserId",
                table: "UserBranches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBranches_Branches_BranchId",
                table: "UserBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBranches_Users_UserId",
                table: "UserBranches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBranches",
                table: "UserBranches");

            migrationBuilder.RenameTable(
                name: "UserBranches",
                newName: "UserBranch");

            migrationBuilder.RenameIndex(
                name: "IX_UserBranches_BranchId",
                table: "UserBranch",
                newName: "IX_UserBranch_BranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBranch",
                table: "UserBranch",
                columns: new[] { "UserId", "BranchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBranch_Branches_BranchId",
                table: "UserBranch",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBranch_Users_UserId",
                table: "UserBranch",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
