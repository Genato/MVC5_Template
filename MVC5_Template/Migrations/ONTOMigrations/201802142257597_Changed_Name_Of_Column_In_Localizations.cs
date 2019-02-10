namespace Auth.Migrations.AuthMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Name_Of_Column_In_Localizations : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "auth.Localizations", newName: "Localization");
            RenameColumn(table: "auth.Localization", name: "_Localization", newName: "Localization");
        }
        
        public override void Down()
        {
            RenameColumn(table: "auth.Localization", name: "Localization", newName: "_Localization");
            RenameTable(name: "auth.Localization", newName: "Localizations");
        }
    }
}
