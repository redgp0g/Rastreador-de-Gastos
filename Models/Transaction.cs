using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rastreador_de_Gastos.Models
{
	public class Transaction
	{
		[Key] 
		public int TransactionId { get; set; }

		[Range(1, int.MaxValue,ErrorMessage = "Por favor, selecione uma categoria.")]

		[Display(Name = "Categoria")]
		public int CategoryId { get; set; }
		public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A Quantidade deve ser maior do que zero.")]
        public int Amount { get; set; }
		[Column(TypeName = "nvarchar(75)")]
		public string? Note { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;

		[NotMapped]
		public string? CategoryTitleWithIcon { 
			get 
			{
				return Category == null ? "" : Category.Icon + " " + Category.Title;
			} 
		}

		[NotMapped]
		public string? FormattedAmount
		{
			get
			{
				return ((Category == null || Category.Type=="Despesa" )? "- " : "+ ") + Amount.ToString("C0");
			}
		}
	}
}
