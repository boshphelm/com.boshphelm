using UnityEngine;
namespace Boshphelm.Utility
{
    [System.Serializable]
        public class FormatSettings
        {
            [Tooltip("Gösterim formatını belirler (Yüzde, Değer, Etiketli vs.)")]
            public FormatType type = FormatType.Percentage;

            [Tooltip("Progress bar'ın ne için kullanıldığını belirten etiket (örn: HP, MP, XP)")]
            public string label = string.Empty;

            [Tooltip("Değerin önüne eklenen metin (örn: ⚡ -> ⚡ 75/100)")]
            public string prefix = string.Empty;

            [Tooltip("Değerin sonuna eklenen metin (örn: points -> 75/100 points)")]
            public string suffix = string.Empty;

            [Tooltip("Mevcut değer ile maksimum değer arasındaki ayraç (örn: /, |, of)")]
            public string separator = "/";

            [Tooltip("Ondalık sayı gösterimi (true: 75.5, false: 75)")]
            public bool showDecimals = false;

            [Tooltip("Gösterilecek ondalık basamak sayısı (1: 75.5, 2: 75.50)")]
            [Range(0, 3)]
            public int decimalPlaces = 0;

            [Tooltip("Özel format şablonu\n" +
                    "{0} = Mevcut değer\n" +
                    "{1} = Maksimum değer\n" +
                    "{2} = Etiket\n" +
                    "Örnek: \"{2}: {0} of {1} remaining\"")]
            public string customFormat = string.Empty;
        }
}
