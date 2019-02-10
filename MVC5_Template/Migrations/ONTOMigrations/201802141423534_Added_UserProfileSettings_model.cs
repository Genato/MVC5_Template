namespace Auth.Migrations.AuthMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_UserProfileSettings_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "auth.User_Profile_Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Localization = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("auth.User_Profile_Settings");
        }
    }
}
