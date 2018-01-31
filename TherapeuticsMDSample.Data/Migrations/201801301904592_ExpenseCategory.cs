namespace TherapeuticsMDSample.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseCategoryDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpenseCategory = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ExpenseItemDataModels", "ExpenseCategoryId", c => c.Int(nullable: true));
            CreateIndex("dbo.ExpenseItemDataModels", "ExpenseCategoryId");
            AddForeignKey("dbo.ExpenseItemDataModels", "ExpenseCategoryId", "dbo.ExpenseCategoryDataModels", "Id", cascadeDelete: true);
            DropColumn("dbo.ExpenseItemDataModels", "ExpenseCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExpenseItemDataModels", "ExpenseCategory", c => c.String(maxLength: 50));
            DropForeignKey("dbo.ExpenseItemDataModels", "ExpenseCategoryId", "dbo.ExpenseCategoryDataModels");
            DropIndex("dbo.ExpenseItemDataModels", new[] { "ExpenseCategoryId" });
            DropColumn("dbo.ExpenseItemDataModels", "ExpenseCategoryId");
            DropTable("dbo.ExpenseCategoryDataModels");
        }
    }
}
