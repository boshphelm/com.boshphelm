using Boshphelm.Utility;

namespace Boshphelm.Shops
{
    public class ShopController : IShopController
    {
        private readonly IShopModel model;
        private readonly IShopView view;
        private readonly ISnap snap;
        private readonly IShopBuyView buyView;

        public ShopController(IShopModel model, IShopView view, ISnap snap, IShopBuyView buyView)
        {
            this.model = model;
            this.view = view;
            this.snap = snap;
            this.buyView = buyView;

            Initialize();
        }

        private void Initialize()
        {
            model.OnModelChanged += HandleModelChanged;
            snap.OnSnappedIndexUpdated += OnSnappedShopItemIndexUpdated;
            RefreshView();
        }

        private void OnSnappedShopItemIndexUpdated(int snappedIndex)
        {
            buyView.RefreshView(model.GetItem(snappedIndex));
        }

        private void HandleModelChanged(ShopItem[] shopItems)
        {
            RefreshView();
        }

        public void RefreshView()
        {
            view.RefreshView(model.Items);
            buyView.RefreshView(model.GetItem(snap.SnappedIndex));
        }

        public void RefreshShopItemView(ShopItem shopItem)
        {
            view.RefreshShopItemView(shopItem);
            buyView.RefreshView(shopItem);
        }
    }
}