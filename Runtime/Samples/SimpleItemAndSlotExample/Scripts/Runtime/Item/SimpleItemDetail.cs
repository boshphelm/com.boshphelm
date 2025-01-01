using Boshphelm.Items;
using UnityEngine;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    [CreateAssetMenu(menuName = "Boshphelm/Items/SimpleItemDetail", fileName = "SimpleItemDetail")]
    public class SimpleItemDetail : ItemDetail
    {

    }

    [System.Serializable]
    public class SimpleItemDetailQuantity : ItemDetailQuantity<SimpleItemDetail>
    {
    }
}
