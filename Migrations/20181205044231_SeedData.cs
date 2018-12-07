using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaAPI.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes(Name) VALUES('Make-1')");
            migrationBuilder.Sql("INSERT INTO Makes(Name) VALUES('Make-2')");
            migrationBuilder.Sql("INSERT INTO Makes(Name) VALUES('Make-3')");

            
            migrationBuilder.Sql("INSERT INTO Model(Name,MakeId) VALUES('Make1-ModelA',(SELECT Id FROM Makes WHERE Name='Make-1'))");
            migrationBuilder.Sql("INSERT INTO Model(Name,MakeId) VALUES('Make1-ModelB',(SELECT Id FROM Makes WHERE Name='Make-1'))");
            migrationBuilder.Sql("INSERT INTO Model(Name,MakeId) VALUES('Make1-ModelC',(SELECT Id FROM Makes WHERE Name='Make-1'))");

            migrationBuilder.Sql("INSERT INTO Model(Name,MakeId) VALUES('Make2-ModelA',(SELECT Id FROM Makes WHERE Name='Make-2'))");
            migrationBuilder.Sql("INSERT INTO Model(Name,MakeId) VALUES('Make2-ModelB',(SELECT Id FROM Makes WHERE Name='Make-2'))");
            migrationBuilder.Sql("INSERT INTO Model(Name,MakeId) VALUES('Make2-ModelC',(SELECT Id FROM Makes WHERE Name='Make-2'))");

            
            migrationBuilder.Sql("INSERT INTO Model(Name,MakeId) VALUES('Make3-ModelA',(SELECT Id FROM Makes WHERE Name='Make-3'))");
            migrationBuilder.Sql("INSERT INTO Model(Name,MakeId) VALUES('Make3-ModelB',(SELECT Id FROM Makes WHERE Name='Make-3'))");
            migrationBuilder.Sql("INSERT INTO Model(Name,MakeId) VALUES('Make3-ModelC',(SELECT Id FROM Makes WHERE Name='Make-3'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Make-1','Make-2','Make-3')");

        }
    }
}
