using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TherapeuticsMDSample.Data.DataModels
{
    public class ExpenseItemDataModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime ExpenseDate { get; set; }

        [StringLength(250)]
        public string ExpenseDescription { get; set; }

        public double ExpenseAmount { get; set; }

        public bool RowDeleted { get; set; }

        public int ExpenseCategoryId { get; set; }
        [ForeignKey("ExpenseCategoryId")]
        public ExpenseCategoryDataModel ExpenseCategory { get; set; }

        public int ExpenseReportId { get; set; }
        [ForeignKey("ExpenseReportId")]
        public ExpenseReportDataModel ExpenseReport { get; set; }

        public int? ReceiptId { get; set; }
        [ForeignKey("ReceiptId")]
        public ReceiptDataModel Receipt { get; set; }
    }
}
