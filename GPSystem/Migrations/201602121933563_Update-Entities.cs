namespace GPSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Churches", "smsBalance", c => c.Int(nullable: false));
            AddColumn("dbo.Churches", "lastRecharge", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SmsModels", "RecepientId", c => c.String(nullable: false));
            AlterColumn("dbo.SmsModels", "Text", c => c.String(nullable: false, maxLength: 120));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SmsModels", "Text", c => c.String(maxLength: 120));
            AlterColumn("dbo.SmsModels", "RecepientId", c => c.String());
            DropColumn("dbo.Churches", "lastRecharge");
            DropColumn("dbo.Churches", "smsBalance");
        }
    }
}
