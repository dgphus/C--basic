using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelA.DTO.validation;

public class MinAgeAttribute : ValidationAttribute
{
    private readonly int _minAge;

    public MinAgeAttribute(int minAge)
    {
        _minAge = minAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Ngày sinh là trường bắt buộc phải nhập.");
        }

        if (value is DateTime dateTime)
        {
            var age = DateTime.Today.Year - dateTime.Year;
            if (dateTime.Date > DateTime.Today.AddYears(-age)) age--;

            if (age < _minAge)
            {
                return new ValidationResult($"Tuổi phải lớn hơn hoặc bằng {_minAge}.");
            }

            return ValidationResult.Success;
        }

        return new ValidationResult("Ngày sinh không hợp lệ.");
    }
}
