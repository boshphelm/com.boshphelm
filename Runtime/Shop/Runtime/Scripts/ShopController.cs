using System;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Shops
{
    public class ShopController
    {
        private readonly ShopModel _model;
        private readonly ShopView _view;
        private readonly Snap _snap;
        private readonly ShopBuyView _buyView;

        public ShopItem[] Items => _model.ShopItems.items;

        private ShopController(ShopModel model, ShopView view, Snap snap, ShopBuyView buyView)
        {
            Debug.Assert(model != null, "MODEL IS NULL");

            _model = model;
            _view = view;
            _snap = snap;
            _buyView = buyView;

            // TODO : Initialize.
            Initialize();
        }

        private void Initialize()
        {
            _model.OnModelChanged += HandleModelChanged;
            _snap.OnSnappedIndexUpdated += OnSnappedShopItemIndexUpdated;
            RefreshView();
        }

        private void OnSnappedShopItemIndexUpdated(int snappedIndex)
        {
            _buyView.RefreshView(_model.ShopItems[snappedIndex]);
        }

        private void HandleModelChanged(ShopItem[] shopItems)
        {
            // TODO: Refresh view.
            RefreshView();
        }

        public void RefreshView()
        {
            //Debug.Log("SHOP ITEM COUNT : " + _model.ShopItems.Count);
            for (int i = 0; i < _model.ShopItems.Count; i++)
            {
                ShopItem shopItem = _model.Get(i);
                if (shopItem != null)
                {
                    // TODO: View Slot Set item.id, item.details.icon, item.quantity
                }
                else
                {
                    Debug.LogError($"Shop Item Index : {i} IS NULL");
                }
            }

            _view.RefreshView(_model.ShopItems.items);
            _buyView.RefreshView(_model.ShopItems[_snap.SnappedIndex]);
        }

        public void RefreshShopItemView(ShopItem shopItem)
        {
            _view.RefreshShopItemView(shopItem);
            _buyView.RefreshView(shopItem);
        }


        #region Builder

        public class Builder
        {
            private ShopItem[] _shopItems;
            private readonly ShopView _view;
            private readonly Snap _snap;
            private readonly ShopBuyView _buyView;

            public Builder(ShopView view, Snap snap, ShopBuyView buyView)
            {
                _view = view;
                _snap = snap;
                _buyView = buyView;
            }

            public Builder WithStartingItems(ShopItem[] shopItems)
            {
                _shopItems = shopItems;
                return this;
            }

            public ShopController Build()
            {
                ShopModel model = _shopItems != null
                    ? new ShopModel(_shopItems)
                    : new ShopModel(Array.Empty<ShopItem>());

                return new ShopController(model, _view, _snap, _buyView);
            }
        }

        #endregion
    }
}