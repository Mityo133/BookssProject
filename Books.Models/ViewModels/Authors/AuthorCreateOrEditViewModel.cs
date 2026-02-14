using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Books.Models.ViewModels.Authors
{
    public class AuthorCreateOrEditViewModel
    {
		public Guid? Id { get; set; }

		[Required(ErrorMessage = "Моля, въведете име.")]
		[StringLength(100, ErrorMessage = "Името не може да е над 100 символа.")]
		[Display(Name = "Име на автора")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Описанието е задължително.")]
		[MinLength(10, ErrorMessage = "Описанието трябва да е поне 10 символа.")]
		[Display(Name = "Биография")]
		public string Description { get; set; }

		[Range(1, 120, ErrorMessage = "Възрастта трябва да е между 1 и 120.")]
		[Display(Name = "Възраст")]
		public int Age { get; set; }
	}
}
