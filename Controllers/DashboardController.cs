using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rastreador_de_Gastos.Models;
using System.Globalization;

namespace Rastreador_de_Gastos.Controllers
{
	public class DashboardController : Controller
	{
		private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
		{
			//Last 7 Days
			DateTime dataInicial = _context.Transaction.Min(x => x.Date);
			DateTime dataFinal = _context.Transaction.Max(x => x.Date);	

			List<Transaction> SelectedTransactions = await _context.Transaction
				.Include(x => x.Category)
				.ToListAsync();
			//Total Income
			int TotalIncome = SelectedTransactions
				.Where(i => i.Category.Type == "Renda")
				.Sum(j => j.Amount);
			ViewBag.TotalIncome = TotalIncome.ToString("C0");

            //Total Expense
            int TotalExpense = SelectedTransactions
                .Where(i => i.Category.Type == "Despesa")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

			//Balance
			int Balance = TotalIncome - TotalExpense;
			CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");
			culture.NumberFormat.CurrencyNegativePattern = 1;
			ViewBag.Balance = Balance.ToString("C0");

			//Doughnut Chart - Despesa por Categoria
			ViewBag.DoughnutCharData = SelectedTransactions
				.Where(i => i.Category.Type == "Despesa")
				.GroupBy(j => j.Category.CategoryId)
				.Select(k => new
				{
					categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
					amount = k.Sum(j => j.Amount),
					formattedAmount = k.Sum(j => j.Amount).ToString("C0"),
				})
				.OrderByDescending(l=>l.amount)
				.ToList();

			//Spline Chart - Income vs Expense
			//Renda
			List<SplineChartData> IncomeSummary = SelectedTransactions
				.Where(i => i.Category.Type == "Renda")
				.GroupBy(j => j.Date)
				.Select(k => new SplineChartData()
				{
					day = k.First().Date.ToString("dd-MM"),
					income = k.Sum(l => l.Amount)
				})
				.ToList();

			//Despesa
			List<SplineChartData> ExpenseSummary = SelectedTransactions
				.Where(i => i.Category.Type == "Despesa")
				.GroupBy(j => j.Date)
				.Select(k => new SplineChartData()
				{
					day = k.First().Date.ToString("dd-MM"),
					expense = k.Sum(l => l.Amount)
				})
				.ToList();

			//Combine Income & Expense
			List<string> periodo = new(); // lista de datas

			for (DateTime data = dataInicial; data <= dataFinal; data = data.AddDays(1))
			{
				periodo.Add(data.ToString("dd-MM")); // adiciona a data atual à lista
			}

			ViewBag.SplineChartData = from day in periodo
									  join income in IncomeSummary on day equals income.day into dayIncomeJoined
									  from income in dayIncomeJoined.DefaultIfEmpty()
									  join expense in ExpenseSummary on day equals expense.day into expenseJoined
									  from expense in expenseJoined.DefaultIfEmpty()
									  select new
									  {
										  day,
										  income = income == null ? 0 : income.income,
										  expense = expense == null ? 0 : expense.expense,
									  };

			//Transações Recentes
			ViewBag.RecentTransactions = await _context.Transaction
				.Include(i => i.Category)
				.OrderByDescending(j => j.Date)
				.Take(5)
				.ToListAsync();

			return View();
		}
	}

	public class SplineChartData
	{
		public string day;
		public int income;
		public int expense;
	}
}
