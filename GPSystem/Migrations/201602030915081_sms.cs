namespace GPSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmsModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SenderId = c.String(),
                        RecepientId = c.String(),
                        Text = c.String(maxLength: 120),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SmsModels");
        }
    }
}
