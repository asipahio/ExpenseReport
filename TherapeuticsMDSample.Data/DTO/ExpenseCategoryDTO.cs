using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TherapeuticsMDSample.Data.DataModels;
using TherapeuticsMDSample.Data.Repositories;

namespace TherapeuticsMDSample.Data.DTO
{
    public class ExpenseCategoryDTO
    {
        #region Public Properties
        public int Id { get; set; }
        public string ExpenseCategory { get; set; }
        #endregion

        public void FillFrom(ExpenseCategoryDataModel c)
        {
            this.Id = c.Id;
            this.ExpenseCategory = c.ExpenseCategory;
        }

        public List<ExpenseCategoryDTO> GetExpenseCategoryList()
        {
            ExpenseReportRepository _repository = new ExpenseReportRepository();
            List<ExpenseCategoryDataModel> categories = _repository.GetExpenseCategoryList();
            List<ExpenseCategoryDTO> dtos = new List<ExpenseCategoryDTO>();
            categories.ForEach(t =>
            {
                ExpenseCategoryDTO obj = new ExpenseCategoryDTO();
                obj.FillFrom(t);
                dtos.Add(obj);
            });

            return dtos;
        }
    }
}
