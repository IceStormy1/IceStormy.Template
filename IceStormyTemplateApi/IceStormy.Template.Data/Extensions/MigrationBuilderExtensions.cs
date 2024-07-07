using Microsoft.EntityFrameworkCore.Migrations;

namespace IceStormy.Template.Data.Extensions;

public static class MigrationExtensions
{
    public static void SqlFile(this MigrationBuilder migrationBuilder, string sqlFile)
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sqlFile);
        migrationBuilder.Sql(File.ReadAllText(filePath));
    }
}