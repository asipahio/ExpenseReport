using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TherapeuticsMDSample.Data.DataModels;
using TherapeuticsMDSample.Data.Repositories;

namespace TherapeuticsMDSample.Data.DTO
{
    public class ExpenseReportsDTO
    {
        #region Public Properties
        public List<ExpenseReportDTO> ExpenseReports { get; set; }
        public int TotalReports { get; set; }
        #endregion

        #region Load Data
        public bool Load(int startIndex, int endIndex, string sortBy, string sortOrder, string keyword)
        {
            ExpenseReportRepository _repo = new ExpenseReportRepository();

            int _totalReports;
            List<ExpenseReportDataModel> reports = _repo.GetExpenseReports(startIndex, endIndex, sortBy, sortOrder, keyword, out _totalReports);
            this.TotalReports = _totalReports;

            if (reports != null) { return this.FillFrom(reports); }

            return false;

        }

        private bool FillFrom(List<ExpenseReportDataModel> reports)
        {
            this.ExpenseReports = new List<ExpenseReportDTO>();
            reports.ForEach(t =>
            {
                ExpenseReportDTO dto = new ExpenseReportDTO();
                dto.FillFrom(t);
                this.ExpenseReports.Add(dto);
            });

            return true;
        }
        #endregion
    }
}
