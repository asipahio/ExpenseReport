namespace TherapeuticsMDSample.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceiptOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExpenseItemDataModels", "ReceiptId", "dbo.ReceiptDataModels");
            DropIndex("dbo.ExpenseItemDataModels", new[] { "ReceiptId" });
            AlterColumn("dbo.ExpenseItemDataModels", "ReceiptId", c => c.Int());
            CreateIndex("dbo.ExpenseItemDataModels", "ReceiptId");
            AddForeignKey("dbo.ExpenseItemDataModels", "ReceiptId", "dbo.ReceiptDataModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseItemDataModels", "ReceiptId", "dbo.ReceiptDataModels");
            DropIndex("dbo.ExpenseItemDataModels", new[] { "ReceiptId" });
            AlterColumn("dbo.ExpenseItemDataModels", "ReceiptId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExpenseItemDataModels", "ReceiptId");
            AddForeignKey("dbo.ExpenseItemDataModels", "ReceiptId", "dbo.ReceiptDataModels", "Id", cascadeDelete: true);
        }
    }
}
