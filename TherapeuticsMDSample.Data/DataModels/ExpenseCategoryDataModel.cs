using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TherapeuticsMDSample.Data.DataModels
{
    public class ExpenseCategoryDataModel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string ExpenseCategory { get; set; }
    }
}
