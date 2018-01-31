namespace TherapeuticsMDSample.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseItemDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpenseDate = c.DateTime(nullable: false),
                        ExpenseCategory = c.String(maxLength: 50),
                        ExpenseDescription = c.String(maxLength: 250),
                        ExpenseAmount = c.Double(nullable: false),
                        ReceiptPath = c.String(maxLength: 300),
                        RowDeleted = c.Boolean(nullable: false),
                        ExpenseReportId = c.Int(nullable: false),
                        ReceiptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseReportDataModels", t => t.ExpenseReportId, cascadeDelete: false)
                .ForeignKey("dbo.ReceiptDataModels", t => t.ReceiptId, cascadeDelete: false)
                .Index(t => t.ExpenseReportId)
                .Index(t => t.ReceiptId);
            
            CreateTable(
                "dbo.ExpenseReportDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReceiptDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(maxLength: 250),
                        ExpenseReportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseReportDataModels", t => t.ExpenseReportId, cascadeDelete: false)
                .Index(t => t.ExpenseReportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiptDataModels", "ExpenseReportId", "dbo.ExpenseReportDataModels");
            DropForeignKey("dbo.ExpenseItemDataModels", "ReceiptId", "dbo.ReceiptDataModels");
            DropForeignKey("dbo.ExpenseItemDataModels", "ExpenseReportId", "dbo.ExpenseReportDataModels");
            DropIndex("dbo.ReceiptDataModels", new[] { "ExpenseReportId" });
            DropIndex("dbo.ExpenseItemDataModels", new[] { "ReceiptId" });
            DropIndex("dbo.ExpenseItemDataModels", new[] { "ExpenseReportId" });
            DropTable("dbo.ReceiptDataModels");
            DropTable("dbo.ExpenseReportDataModels");
            DropTable("dbo.ExpenseItemDataModels");
        }
    }
}
