using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TherapeuticsMDSample.Data.DataModels
{
    public class ExpenseReportDataModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<ExpenseItemDataModel> ExpenseItems { get; set; }

        public virtual List<ReceiptDataModel> Receipts { get; set; }
    }
}
