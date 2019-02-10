namespace Auth.Migrations.AuthMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_property_to_KatalogKljučnihBrojevaOtpada : DbMigration
    {
        public override void Up()
        {
            AddColumn("auth.Katalog_Kljucnih_Brojeva_Otpada", "KeyNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("auth.Katalog_Kljucnih_Brojeva_Otpada", "KeyNumber");
        }
    }
}
