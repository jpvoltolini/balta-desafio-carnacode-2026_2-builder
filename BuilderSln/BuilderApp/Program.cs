using BuilderApp;
using BuilderApp.Builder;

Console.WriteLine("=== Sistema de Relatórios ===");

var report1 = new SalesReportBuilder(
    "Vendas Mensais",           // title
    "PDF",                       // format
    new DateTime(2024, 1, 1),   // startDate
    new DateTime(2024, 1, 31))  // endDate
    .WithHeader("Relatório de Vendas")
    .WithFooter("Confidencial")
    .WithCharts("Bar")
    .WithSummary()
    .AddColumns("Produto", "Quantidade", "Valor")
    .AddFilter("Status=Ativo")
    .SortBy("Valor")
    .GroupBy("Categoria")
    .WithTotals()
    .Page("A4", "Portrait", includePageNumbers: true)
    .Branding("logo.png", "Confidencial")
    .Build();

report1.Generate();


var report2 = new SalesReportBuilder(
    "Relatório Trimestral", 
    "Excel",
    new DateTime(2024, 1, 1), 
    new DateTime(2024, 3, 31))
    .AddColumns("Vendedor", "Região", "Total")
    .WithCharts("Line")
    .WithHeader("Trimestre 1")
    .GroupBy("Região")
    .WithTotals(true)
    .Build();

report2.Generate();


var report3 = new SalesReportBuilder(
    "Vendas Anuais",
    "PDF",
    new DateTime(2024, 1, 1),
    new DateTime(2024, 12, 31))
    .WithHeader("Relatório de Vendas")
    .WithFooter("Confidencial")
    .AddColumns("Produto", "Quantidade", "Valor")
    .WithCharts("Pie")
    .WithTotals()
    .Page("A4", "Portrait")
    .Build();

report3.Generate();
