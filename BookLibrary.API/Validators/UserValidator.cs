using BookLibrary.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Validators
{
    public class UserValidator : AbstractValidator<UserViewModel>
    {
		public UserValidator()
		{
			RuleFor(x => x.LastName).NotNull().NotEmpty();
			RuleFor(x => x.FirstName).NotNull().NotEmpty();
			RuleFor(x => x.Username).MinimumLength(4);
			RuleFor(x => x.Password).MinimumLength(6);
		}
	}
}
