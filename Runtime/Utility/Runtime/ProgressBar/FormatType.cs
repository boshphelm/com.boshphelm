using UnityEngine;

namespace Boshphelm.Utility
{ 
        public enum FormatType
        {
            [Tooltip("Text göstermez")]
            None,
            
            [Tooltip("Yüzdelik gösterim (örn: 75%)")]
            Percentage,
            
            [Tooltip("Oran gösterimi (örn: 75/100)")]
            Value,
            
            [Tooltip("Etiketli gösterim (örn: Health: 75/100)")]
            ValueWithLabel,
            
            [Tooltip("Sadece mevcut değer (örn: 75)")]
            Short,
            
            [Tooltip("Ayraçlı gösterim (örn: 75 | 100)")]
            ShortWithMax,
            
            [Tooltip("Özel format kullanımı")]
            Custom
        }
     
}