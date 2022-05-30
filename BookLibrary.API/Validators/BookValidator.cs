using BookLibrary.API.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Validators
{
    public class BookValidator : AbstractValidator<UploadBookRequest>
    {
        public BookValidator()
        {
            RuleFor(x => x.FormFile).NotNull();
            RuleFor(x => x.FileName).NotNull().NotEmpty();
            //RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
