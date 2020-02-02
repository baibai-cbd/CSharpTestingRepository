namespace ThreadSafeRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntityAs",
                c => new
                    {
                        EntityAId = c.Int(nullable: false, identity: true),
                        SomeName = c.String(),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.EntityAId);
            
            CreateTable(
                "dbo.EntityCs",
                c => new
                    {
                        EntityCId = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Zipcode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntityCId);
            
            CreateTable(
                "dbo.SmallEntityDs",
                c => new
                    {
                        SmallEntityDId = c.Int(nullable: false, identity: true),
                        IsGood = c.Boolean(nullable: false),
                        Guid = c.Guid(nullable: false),
                        SomeInfo = c.String(),
                    })
                .PrimaryKey(t => t.SmallEntityDId);
            
            CreateTable(
                "dbo.XrefBs",
                c => new
                    {
                        XrefBId = c.Int(nullable: false),
                        EntityAId = c.Int(nullable: false),
                        EntityCId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.XrefBId)
                .ForeignKey("dbo.EntityAs", t => t.XrefBId)
                .ForeignKey("dbo.EntityCs", t => t.XrefBId)
                .Index(t => t.XrefBId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.XrefBs", "XrefBId", "dbo.EntityCs");
            DropForeignKey("dbo.XrefBs", "XrefBId", "dbo.EntityAs");
            DropIndex("dbo.XrefBs", new[] { "XrefBId" });
            DropTable("dbo.XrefBs");
            DropTable("dbo.SmallEntityDs");
            DropTable("dbo.EntityCs");
            DropTable("dbo.EntityAs");
        }
    }
}
