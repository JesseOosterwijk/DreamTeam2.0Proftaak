using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProftaakApplicatieDiabetes.Models
{
    public class CalcModel
    {
        public int userBSN { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Your current weight must be numeral and can't be 0.")]
        [Display(Name = "Current weight")]
        [Required(ErrorMessage = "Your current weight is required to calculate the insuline amount.")]
        public double Weight { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Your total carbs income must be numeral and can't be 0.")]
        [Display(Name = "Total carbs income")]
        [Required(ErrorMessage = "The total carbs income is required to calculate the insuline amount")]
        public double TotalCarbs { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Your current blood sugar must be numeral and can't be 0.")]
        [Display(Name = "Current blood sugar")]
        [Required(ErrorMessage = "Your current blood sugar is required to calculate te insuine amount.")]
        public double CurrentBloodsugar { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Your targeted blood sugar must be numeral and can't be 0.")]
        [Display(Name = "Target blood sugar")]
        [Required(ErrorMessage = "The targeted blood sugar is required to calculate the insuline amount.")]
        public double TargetBloodSugar { get; set; }

        public double Result { get; set; }
    }
}
