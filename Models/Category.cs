﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rastreador_de_Gastos.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Column(TypeName ="nvarchar(50)")]
		public string Title { get; set; }

		[Column(TypeName = "nvarchar(5 )")]
		public string Icon { get; set; } = "";

		[Column(TypeName = "nvarchar(10)")]
		public string Type { get; set; } = "Despesa";
	}
}
