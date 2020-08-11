using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using OilfieldCalc2.Models.DrillstringTubulars;

namespace OilfieldCalc2.Models.Validators
{
    public class DrillstringTubularValidator : AbstractValidator<IDrillstringTubular>
    {
        public DrillstringTubularValidator()
        {
            RuleFor(x => x.ItemDescription).NotNull().Length(4, 20);
            RuleFor(x => x.Length.Value).GreaterThan(0)
                .WithMessage("Length must be greater than 0");
            RuleFor(x => x.Length).NotNull()
                .WithMessage("Length must be greater than 0");
            RuleFor(x => x.OutsideDiameter.Value).GreaterThan(0)
                .WithMessage("Outside Diameter must be greater than 0");
            RuleFor(x => x.OutsideDiameter.Value).GreaterThan(x => x.InsideDiameter.Value)
                .WithMessage("Outside Diameter must be greater than Inside Diameter");
            RuleFor(x => x.OutsideDiameter).NotNull();
            RuleFor(x => x.InsideDiameter.Value).GreaterThan(0)
                .WithMessage("Inside Diameter must be greater than 0");
            RuleFor(x => x.InsideDiameter).NotNull();
            RuleFor(x => x.AdjustedWeightPerUnit.Value).GreaterThan(0)
                .WithMessage("Weight must be greater than 0");
            RuleFor(x => x.AdjustedWeightPerUnit).NotNull();
        }
    }
}
