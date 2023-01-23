using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Basic_Games_Shelf.DOMAINE.ValidationAttributes
{
    public class MinValueAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly double _minValue;

        public MinValueAttribute(double minValue)
        {
            _minValue = minValue;
            ErrorMessage = "Enter a value greater than or equal to " + _minValue;
        }

        public MinValueAttribute(int minValue)
        {
            _minValue = minValue;
            ErrorMessage = "Enter a value greater than or equal to " + _minValue;
        }

        public override bool IsValid(object value)
        {
            return Convert.ToDouble(value) >= _minValue;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = ErrorMessage;
            rule.ValidationParameters.Add("min", _minValue);
            rule.ValidationParameters.Add("max", Double.MaxValue);
            rule.ValidationType = "range";
            yield return rule;
        }

    }
}
