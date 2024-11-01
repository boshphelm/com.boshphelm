using UnityEngine;

namespace Boshphelm.Utility
{
    [System.Serializable]
    public class FormatSettings
    {
        // Default değerler için const tanimlamalari
        private const FormatType DEFAULT_TYPE = FormatType.Value;
        private const string DEFAULT_SEPARATOR = "/";
        private const bool DEFAULT_SHOW_DECIMALS = false;
        private const int DEFAULT_DECIMAL_PLACES = 0;

        [Tooltip("Gösterim formatini belirler (Yüzde, Değer, Etiketli vs.)")]
        public FormatType type;

        [Tooltip("Progress bar'in ne için kullanildiğini belirten etiket (örn: HP, MP, XP)")]
        public string label;

        [Tooltip("Değerin önüne eklenen metin (örn: ⚡ -> ⚡ 75/100)")]
        public string prefix;

        [Tooltip("Değerin sonuna eklenen metin (örn: points -> 75/100 points)")]
        public string suffix;

        [Tooltip("Mevcut değer ile maksimum değer arasindaki ayraç (örn: /, |, of)")]
        public string separator;

        [Tooltip("Ondalik sayi gösterimi (true: 75.5, false: 75)")]
        public bool showDecimals;

        [Tooltip("Gösterilecek ondalik basamak sayisi (1: 75.5, 2: 75.50)")]
        [Range(0, 3)]
        public int decimalPlaces;

        [Tooltip("Özel format şablonu\n" +
                "{0} = Mevcut değer\n" +
                "{1} = Maksimum değer\n" +
                "{2} = Etiket\n" +
                "Örnek: \"{2}: {0} of {1} remaining\"")]
        public string customFormat;

        // Default constructor
        public FormatSettings()
        {
            SetDefaults();
        }

        // Parametreli constructor
        public FormatSettings(FormatType type)
        {
            SetDefaults();
            this.type = type;
        }

        // Tüm parametrelerle constructor
        public FormatSettings(
            FormatType type,
            string label,
            string prefix,
            string suffix,
            string separator,
            bool showDecimals,
            int decimalPlaces,
            string customFormat)
        {
            this.type = type;
            this.label = label ?? string.Empty;
            this.prefix = prefix ?? string.Empty;
            this.suffix = suffix ?? string.Empty;
            this.separator = separator ?? DEFAULT_SEPARATOR;
            this.showDecimals = showDecimals;
            this.decimalPlaces = Mathf.Clamp(decimalPlaces, 0, 3);
            this.customFormat = customFormat ?? string.Empty;
        }

        // Default değerleri ayarlama
        private void SetDefaults()
        {
            type = DEFAULT_TYPE;
            label = string.Empty;
            prefix = string.Empty;
            suffix = string.Empty;
            separator = DEFAULT_SEPARATOR;
            showDecimals = DEFAULT_SHOW_DECIMALS;
            decimalPlaces = DEFAULT_DECIMAL_PLACES;
            customFormat = string.Empty;
        }

        // Builder class
        public class Builder
        {
            private FormatSettings settings;

            public Builder()
            {
                settings = new FormatSettings();
            }

            public Builder(FormatSettings existingSettings)
            {
                settings = existingSettings.Clone();
            }

            public Builder WithType(FormatType type)
            {
                settings.type = type;
                return this;
            }

            public Builder WithLabel(string label)
            {
                settings.label = label ?? string.Empty;
                return this;
            }

            public Builder WithPrefix(string prefix)
            {
                settings.prefix = prefix ?? string.Empty;
                return this;
            }

            public Builder WithSuffix(string suffix)
            {
                settings.suffix = suffix ?? string.Empty;
                return this;
            }

            public Builder WithSeparator(string separator)
            {
                settings.separator = separator ?? DEFAULT_SEPARATOR;
                return this;
            }

            public Builder WithDecimals(bool show = true, int places = 1)
            {
                settings.showDecimals = show;
                settings.decimalPlaces = Mathf.Clamp(places, 0, 3);
                return this;
            }

            public Builder WithCustomFormat(string format)
            {
                settings.customFormat = format ?? string.Empty;
                settings.type = FormatType.Custom;
                return this;
            }

            public FormatSettings Build()
            {
                return settings;
            }
        } 

        // Clone method
        public FormatSettings Clone()
        {
            return new FormatSettings(
                type,
                label,
                prefix,
                suffix,
                separator,
                showDecimals,
                decimalPlaces,
                customFormat
            );
        }
    }
}