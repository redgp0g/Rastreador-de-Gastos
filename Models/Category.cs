using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rastreador_de_Gastos.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Column(TypeName ="nvarchar(50)")]
		[Required(ErrorMessage = "O Título é obrigatório.")]
		public string Title { get; set; }

		[Column(TypeName = "nvarchar(5)")]
		[Display(Name ="Ícone")]
        [Required(ErrorMessage = "O ícone é obrigatório.")]
        public string Icon { get; set; } = "";

		[Column(TypeName = "nvarchar(10)")]
		public string Type { get; set; } = "Despesa";

		[NotMapped]
		public string? TitleWithIcon
		{
			get
			{
				return Icon + " " + Title;
			}
		}
	}
}
