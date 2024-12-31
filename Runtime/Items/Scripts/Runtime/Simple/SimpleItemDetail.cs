using UnityEngine;

namespace Boshphelm.Items
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
