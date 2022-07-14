﻿using FluentValidation;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Validators
{
    public class UpdateStudentRequestValidator:AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentRequestValidator(IStudentRepository studentRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Mobile).NotEmpty().LessThan(20).GreaterThan(8);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGenderAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);
                if (gender != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Please select a valid gender");
            RuleFor(x => x.PostalAddress).NotEmpty();
            RuleFor(x => x.PhysicalAddress).NotEmpty();

        }
    }
}
