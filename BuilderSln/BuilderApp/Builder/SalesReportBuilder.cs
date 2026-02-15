using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderApp.Builder
{
    public class SalesReportBuilder
    {
        private readonly SalesReport _report = new SalesReport();

        public SalesReportBuilder(string title, string format, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(title)) 
                throw new ArgumentException("Title é obrigatório.", nameof(title));
            if (string.IsNullOrWhiteSpace(format)) 
                throw new ArgumentException("Format é obrigatório.", nameof(format));
            if (endDate < startDate) 
                throw new ArgumentException("EndDate não pode ser menor que StartDate.");

            _report.Title = title;
            _report.Format = format;
            _report.StartDate = startDate;
            _report.EndDate = endDate;

            _report.Columns ??= new List<string>();
            _report.Filters ??= new List<string>();
        }

        public SalesReportBuilder WithHeader(string headerText)
        {
            _report.IncludeHeader = true;
            _report.HeaderText = headerText;
            return this;
        }

        public SalesReportBuilder WithoutHeader()
        {
            _report.IncludeHeader = false;
            _report.HeaderText = null;
            return this;
        }

        public SalesReportBuilder WithFooter(string footerText)
        {
            _report.IncludeFooter = true;
            _report.FooterText = footerText;
            return this;
        }

        public SalesReportBuilder WithCharts(string chartType)
        {
            _report.IncludeCharts = true;
            _report.ChartType = chartType;
            return this;
        }

        public SalesReportBuilder WithSummary(bool include = true)
        {
            _report.IncludeSummary = include;
            return this;
        }

        public SalesReportBuilder AddColumn(string column)
        {
            if (!string.IsNullOrWhiteSpace(column))
                _report.Columns.Add(column);
            return this;
        }

        public SalesReportBuilder AddColumns(params string[] columns)
        {
            foreach (var c in columns) AddColumn(c);
            return this;
        }

        public SalesReportBuilder AddFilter(string filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
                _report.Filters.Add(filter);
            return this;
        }

        public SalesReportBuilder SortBy(string sortBy)
        {
            _report.SortBy = sortBy;
            return this;
        }

        public SalesReportBuilder GroupBy(string groupBy)
        {
            _report.GroupBy = groupBy;
            return this;
        }

        public SalesReportBuilder WithTotals(bool include = true)
        {
            _report.IncludeTotals = include;
            return this;
        }

        public SalesReportBuilder Page(string size, string orientation = null, bool includePageNumbers = true)
        {
            _report.PageSize = size;
            _report.Orientation = orientation;
            _report.IncludePageNumbers = includePageNumbers;
            return this;
        }

        public SalesReportBuilder Branding(string companyLogo = null, string waterMark = null)
        {
            _report.CompanyLogo = companyLogo;
            _report.WaterMark = waterMark;
            return this;
        }

        public SalesReport Build()
        {
            if (_report.Columns.Count == 0)
                throw new InvalidOperationException("Relatório deve ter pelo menos 1 coluna.");

            if (_report.IncludeHeader && string.IsNullOrWhiteSpace(_report.HeaderText))
                throw new InvalidOperationException("IncludeHeader=true exige HeaderText.");

            if (_report.IncludeFooter && string.IsNullOrWhiteSpace(_report.FooterText))
                throw new InvalidOperationException("IncludeFooter=true exige FooterText.");

            if (_report.IncludeCharts && string.IsNullOrWhiteSpace(_report.ChartType))
                throw new InvalidOperationException("IncludeCharts=true exige ChartType.");

            return _report;
        }

    }
}
