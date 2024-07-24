using Boshphelm.Currencies;
using UnityEngine;

namespace Boshphelm.AreaInteractionSystem
{
    public class AreaInteracter : MonoBehaviour, IAreaInteractible
    {
        [SerializeField] private Wallets.Wallet _wallet;
        [SerializeField] private InteractionTransferRole _interactionRole;
        [SerializeField] private Transform _lootTransferAnimationPosition;

        public InteractionTransferRole GetMyInteractionRole() => _interactionRole;
        private void Start()
        {
            _wallet = FindObjectOfType<Wallets.Wallet>();
        }
        public bool AddPrice(Price price)
        {
            _wallet.Add(price);
            return true;
        }
        public bool RemovePrice(Price price)
        {
            bool canPay = _wallet.CanPayThePrice(price);
            if (!canPay) return false;
            _wallet.Pay(price);
            return true;
        }
        public Transform GetInteractionLootTransferAnimationPosition() => _lootTransferAnimationPosition;
        public void OnEnterInteraction()
        { }
        public void OnExitInteraction()
        { }

    }
}
