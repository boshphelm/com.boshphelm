using UnityEngine;

namespace Boshphelm.Utility
{
    public partial class ProgressFormatter : IProgressFormatter
    { 
        private readonly FormatSettings settings;

        public ProgressFormatter(FormatSettings settings)
        {
            this.settings = settings;
        }

        public string Format(float current, float target)
        {
            if (settings.type == FormatType.None)
                return string.Empty;

            string formattedValue = settings.type switch
            {
                FormatType.Percentage => FormatPercentage(current, target),
                FormatType.Value => FormatValue(current, target),
                FormatType.ValueWithLabel => FormatValueWithLabel(current, target),
                FormatType.Short => FormatShort(current),
                FormatType.ShortWithMax => FormatShortWithMax(current, target),
                FormatType.Custom => FormatCustom(current, target),
                _ => FormatValue(current, target)
            };

            return $"{settings.prefix}{formattedValue}{settings.suffix}";
        }

        private string FormatNumber(float value)
        {
            return settings.showDecimals 
                ? value.ToString($"F{settings.decimalPlaces}") 
                : Mathf.RoundToInt(value).ToString();
        }

        private string FormatPercentage(float current, float target)
        {
            float percentage = target > 0 ? (current / target) * 100f : 0f;
            return $"{FormatNumber(percentage)}%";
        }

        private string FormatValue(float current, float target)
        {
            return $"{FormatNumber(current)}{settings.separator}{FormatNumber(target)}";
        }

        private string FormatValueWithLabel(float current, float target)
        {
            string label = !string.IsNullOrEmpty(settings.label) ? $"{settings.label}: " : "";
            return $"{label}{FormatValue(current, target)}";
        }

        private string FormatShort(float current)
        {
            return FormatNumber(current);
        }

        private string FormatShortWithMax(float current, float target)
        {
            return $"{FormatNumber(current)} | {FormatNumber(target)}";
        }

        private string FormatCustom(float current, float target)
        {
            if (string.IsNullOrEmpty(settings.customFormat))
                return FormatValue(current, target);

            return string.Format(settings.customFormat, 
                FormatNumber(current), 
                FormatNumber(target), 
                settings.label);
        }
    }
}