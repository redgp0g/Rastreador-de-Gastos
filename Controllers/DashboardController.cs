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
			DateTime StartDate = DateTime.Now.AddDays(-6);
			DateTime EndDate = DateTime.Today;	

			List<Transaction> SelectedTransactions = await _context.Transaction
				.Include(x => x.Category)
				.Where(y => y.Date >= StartDate && y.Date <= EndDate)
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
				.ToList();

            return View();
		}
	}
}
