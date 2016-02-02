namespace GPSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPostPics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostPictures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PostId = c.Long(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostPictures", "PostId", "dbo.Posts");
            DropIndex("dbo.PostPictures", new[] { "PostId" });
            DropTable("dbo.PostPictures");
        }
    }
}
