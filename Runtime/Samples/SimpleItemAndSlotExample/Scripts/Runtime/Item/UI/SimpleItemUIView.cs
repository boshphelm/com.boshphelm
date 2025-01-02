using System.Collections;
using System.Collections.Generic;
using Boshphelm.Items;
using UnityEngine;

namespace Boshphelm.Sample.SimpleItemAndSlot
{
    public class SimpleItemUIView<TItem> : MonoBehaviour where TItem : Item
    {
        protected TItem item;

        public bool IsEmpty => item == null;

        public void Setup(TItem item)
        {
            if (item == null) return;

            this.item = item;
        }

    }
}
