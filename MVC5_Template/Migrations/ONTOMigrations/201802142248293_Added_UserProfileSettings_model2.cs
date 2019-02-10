namespace Auth.Migrations.AuthMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_UserProfileSettings_model2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("auth.User_Profile_Settings", "LocalizationID", c => c.Int(nullable: false));
            DropColumn("auth.User_Profile_Settings", "Localization");
        }
        
        public override void Down()
        {
            AddColumn("auth.User_Profile_Settings", "Localization", c => c.String());
            DropColumn("auth.User_Profile_Settings", "LocalizationID");
        }
    }
}
