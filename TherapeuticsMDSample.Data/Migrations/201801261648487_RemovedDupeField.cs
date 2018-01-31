namespace TherapeuticsMDSample.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDupeField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ExpenseItemDataModels", "ReceiptPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExpenseItemDataModels", "ReceiptPath", c => c.String(maxLength: 300));
        }
    }
}
