namespace Auth.Migrations.AuthMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "auth.Katalog_Kljucnih_Brojeva_Otpada",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("auth.Katalog_Kljucnih_Brojeva_Otpada");
        }
    }
}
