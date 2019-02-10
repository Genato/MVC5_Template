namespace MVC5_Template.Migrations.MVC5_TemplateMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "mvc5template.Localization",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Localization = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "mvc5template.User_Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        LocalizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("mvc5template.User_Settings");
            DropTable("mvc5template.Localization");
        }
    }
}
