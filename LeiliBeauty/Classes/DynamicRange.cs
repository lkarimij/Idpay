using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeiliBeauty
{
    public class DynamicRange : ValidationAttribute, IClientValidatable
    {
        private readonly string _minPropertyName;
        private readonly string _maxPropertyName;

        public DynamicRange(string minPropName, string maxPropName)
        {
            _minPropertyName = minPropName;
            _maxPropertyName = maxPropName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var minProperty = validationContext.ObjectType.GetProperty(_minPropertyName);
            var maxProperty = validationContext.ObjectType.GetProperty(_maxPropertyName);

            if (minProperty == null)
                return new ValidationResult(string.Format("Unknown property {0}", _minPropertyName));

            if (maxProperty == null)
                return new ValidationResult(string.Format("Unknown property {0}", _maxPropertyName));

            var minValue = (int)minProperty.GetValue(validationContext.ObjectInstance, null);
            var maxValue = (int)maxProperty.GetValue(validationContext.ObjectInstance, null);

            var currentValue = (int)value;

            if (currentValue < minValue || currentValue > maxValue)
            {
                return new ValidationResult(string.Format(ErrorMessage, minValue, maxValue));
            }

            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "dynamicrange",
                ErrorMessage = ErrorMessage
            };

            rule.ValidationParameters["minvalueproperty"] = _minPropertyName;
            rule.ValidationParameters["maxvalueproperty"] = _maxPropertyName;
            yield return rule;
        }
    }
}