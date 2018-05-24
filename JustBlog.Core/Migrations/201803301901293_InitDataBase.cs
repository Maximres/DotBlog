namespace JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        UrlSlug = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        ShortDescription = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Meta = c.String(nullable: false, maxLength: 1000),
                        UrlSlug = c.String(nullable: false, maxLength: 200),
                        Published = c.Boolean(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        UrlSlug = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        Post_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_Id, t.Tag_Id })
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.Post_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.PostTags", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.PostTags", "Post_Id", "dbo.Posts");
            DropIndex("dbo.PostTags", new[] { "Tag_Id" });
            DropIndex("dbo.PostTags", new[] { "Post_Id" });
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropTable("dbo.PostTags");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
        }
    }
}
