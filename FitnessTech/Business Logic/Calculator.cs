using FitnessTech.Models;

namespace FitnessTech.Business_Logic
{
    public static class Calculator
    {

        public static double BmrCalculator(BMI bmi)
        {
            var bmrResult = bmi.Gender == Gender.Male
                ? 66 + (13.7 * bmi.Weight) + (5 * bmi.Height) - (6.8 * bmi.Age)
                : 655 + (9.6 * bmi.Weight) + (1.8 * bmi.Height) - (4.7 * bmi.Age);
            return bmrResult;
        }

        public static double TdeeCalculator(TDEE tdee)
        {
            double activityRate;
            var activityLevel = tdee.ActivityLevel;
            switch (activityLevel)
            {
                case ActivityLevel.Sedentary:
                    activityRate = 1.2;
                    break;
                case ActivityLevel.Light:
                    activityRate = 1.375;
                    break;
                case ActivityLevel.Moderate:
                    activityRate = 1.55;
                    break;
                case ActivityLevel.Heavy:
                    activityRate = 1.725;
                    break;
                case ActivityLevel.Athlete:
                    activityRate = 1.9;
                    break;
                default:
                    activityRate = 0.0;
                    break;
            }
            var bmrResult = tdee.Gender == Gender.Male
                ? 66 + (13.7 * tdee.Weight) + (5 * tdee.Height) - (6.8 * tdee.Age)
                : 655 + (9.6 * tdee.Weight) + (1.8 * tdee.Height) - (4.7 * tdee.Age);

            var tdeeResult = 0.0;
            tdeeResult += bmrResult * activityRate;
            return tdeeResult;
        }

        public static double MacroCalculator(Macro macro)
        {
            double activityRate;
            var activityLevel = macro.ActivityLevel;
            switch (activityLevel)
            {
                case ActivityLevel.Sedentary:
                    activityRate = 1.2;
                    break;
                case ActivityLevel.Light:
                    activityRate = 1.375;
                    break;
                case ActivityLevel.Moderate:
                    activityRate = 1.55;
                    break;
                case ActivityLevel.Heavy:
                    activityRate = 1.725;
                    break;
                case ActivityLevel.Athlete:
                    activityRate = 1.9;
                    break;
                default:
                    activityRate = 0.0;
                    break;
            }
            var bmrResult = macro.Gender == Gender.Male
                ? 66 + (13.7 * macro.Weight) + (5 * macro.Height) - (6.8 * macro.Age)
                : 655 + (9.6 * macro.Weight) + (1.8 * macro.Height) - (4.7 * macro.Age);

            var tdeeResult = bmrResult * activityRate;

            double percentageCalories;
            var percentage = macro.Percentage;
            switch (percentage)
            {
                case Percentage.Aggressive:
                    percentageCalories = 25;
                    break;
                case Percentage.Reckless:
                    percentageCalories = 20;
                    break;
                case Percentage.Suggested:
                    percentageCalories = 15;
                    break;
                default:
                    percentageCalories = 0;
                    break;
            }

            double goalCalories;
            var goal = macro.Goal;
            double macroResult;

            switch (goal)
            {                  
                case Goal.Musclegain:
                case Goal.WeightGain:
                    goalCalories = CalculatePercentage(tdeeResult, percentageCalories);
                    macroResult = tdeeResult + goalCalories;

                    break;
                case Goal.Weightloss:
                    goalCalories = CalculatePercentage(tdeeResult, percentageCalories);
                    macroResult = tdeeResult - goalCalories;
                    break;
                case Goal.Maintenance:
                    macroResult = tdeeResult;
                    break;
                default:
                    macroResult = tdeeResult;
                    break;
            }

            return macroResult;
        }

        public static double CalculatePercentage(double value, double percent)
        {   
            var percentDivision = value % 100;
            var result = percentDivision * percent;
            return result;
        }
    }
}
