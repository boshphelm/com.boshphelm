using Boshphelm.Stats;
using DamageNumbersPro;
using UnityEngine;

namespace Boshphelm.Units
{
    [CreateAssetMenu(fileName = "New Damage Type", menuName = "Boshphelm/Damage System/Damage Type")]
    public class DamageType : ScriptableObject
    {
        public string DamageTypeName;
        public Color DamageColor = Color.white;
        public DamageNumberMesh DamageNumberPrefab;
        public StatType DamageStatType;
        public StatType ResistanceStatType;
    }
}
