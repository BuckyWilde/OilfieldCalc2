using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.Validators
{
    public class WellboreTubularValidator : AbstractValidator<IWellboreTubular>
    {
        public WellboreTubularValidator()
        {
            RuleFor(x => x.ItemDescription).NotNull().Length(4, 20);
            RuleFor(x => x.WashoutFactor).InclusiveBetween(0, 100)
                .WithMessage("Washout value must be from 0 to 100");
            RuleFor(x => x.StartDepth.Value).GreaterThanOrEqualTo(0)
                .WithMessage("Start Depth must be non negative");
            RuleFor(x => x.StartDepth).NotNull();            
            RuleFor(x => x.EndDepth.Value).GreaterThan(x => x.StartDepth.Value)
                .WithMessage("End Depth must be greater than Start Depth");
            RuleFor(x => x.EndDepth).NotNull();
            RuleFor(x => x.InsideDiameter.Value).GreaterThan(0)
                .WithMessage("Inside Diameter must be greater than 0");
            RuleFor(x => x.InsideDiameter).NotNull();
        }
    }
}