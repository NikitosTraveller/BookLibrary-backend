using BookLibrary.API.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Validators
{
    public class CommentValidator : AbstractValidator<PostCommentRequest>
    {
		public CommentValidator()
		{
			RuleFor(x => x.Message).NotNull().NotEmpty();
			RuleFor(x => x.BookId).GreaterThan(0);
		}
	}
}
