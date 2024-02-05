using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class MAxFileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxFileSize;

        public MAxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-maxFileSize", ErrorMessage);
        }

        public override bool IsValid(object value)
        {
            //tabdil value be file khat aval 
            var file = value as IFormFile; 
            if (file == null) return true;
            return file.Length <= _maxFileSize;
        }
    }
}
