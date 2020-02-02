namespace ThreadSafeRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LazyLoading : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogID = c.Int(nullable: false, identity: true),
                        BlogSiteGuid = c.Guid(nullable: false),
                        Title = c.String(),
                        AuthorName = c.String(),
                        createdDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BlogID)
                .ForeignKey("dbo.BlogSites", t => t.BlogSiteGuid, cascadeDelete: true)
                .Index(t => t.BlogSiteGuid);
            
            CreateTable(
                "dbo.BlogSites",
                c => new
                    {
                        BlogSiteGuid = c.Guid(nullable: false),
                        BlogSiteName = c.String(),
                        OwnerName = c.String(),
                    })
                .PrimaryKey(t => t.BlogSiteGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "BlogSiteGuid", "dbo.BlogSites");
            DropIndex("dbo.Blogs", new[] { "BlogSiteGuid" });
            DropTable("dbo.BlogSites");
            DropTable("dbo.Blogs");
        }
    }
}
