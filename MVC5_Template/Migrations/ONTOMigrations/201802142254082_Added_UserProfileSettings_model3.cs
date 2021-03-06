namespace Auth.Migrations.AuthMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_UserProfileSettings_model3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "auth.Localizations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        _Localization = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("auth.Localizations");
        }
    }
}
