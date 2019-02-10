namespace Auth.Migrations.AuthMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_UserProfileSettings_model1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("auth.User_Profile_Settings", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("auth.User_Profile_Settings", "UserID", c => c.Int(nullable: false));
        }
    }
}
