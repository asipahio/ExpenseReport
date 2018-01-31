using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TherapeuticsMDSample.Data.DataModels
{
    public class ReceiptDataModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        public int ExpenseReportId { get; set; }
        [ForeignKey("ExpenseReportId")]
        public ExpenseReportDataModel ExpenseReport { get; set; }

        public virtual List<ExpenseItemDataModel> ExpenseItems { get; set; }
    }
}
