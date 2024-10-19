using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;
using System.Collections.Generic;

namespace Boshphelm.Currencies
{
    public class FloatingCurrencyNotificationManager : MonoBehaviour
    {
        [SerializeField] private FloatingCurrencyNotification _floatingCurrencyPrefab;
        [SerializeField] private Transform _floatingCurrencyParent;
        [SerializeField] private int _maxFloatingNotifications = 3;
        [SerializeField] private float _floatingDuration = 1f;
        [SerializeField] private float _floatingDistance = 50f;

        private ObjectPool<FloatingCurrencyNotification> _floatingCurrencyPool;
        private readonly Queue<FloatingCurrencyNotification> _activeNotifications = new Queue<FloatingCurrencyNotification>();

        private void Awake()
        {
            InitializeObjectPool();
        }

        private void InitializeObjectPool()
        {
            _floatingCurrencyPool = new ObjectPool<FloatingCurrencyNotification>(
                CreateFloatingCurrency,
                (notification) => notification.gameObject.SetActive(true),
                (notification) => notification.gameObject.SetActive(false),
                (notification) => Destroy(notification.gameObject),
                true,
                _maxFloatingNotifications,
                _maxFloatingNotifications
                );
        }

        private FloatingCurrencyNotification CreateFloatingCurrency()
        {
            var notification = Instantiate(_floatingCurrencyPrefab, _floatingCurrencyParent);
            notification.gameObject.SetActive(false);
            return notification;
        }

        public void ShowFloatingCurrencyNotification(CurrencyDataSO currencyData, int amount)
        {
            if (_activeNotifications.Count >= _maxFloatingNotifications)
            {
                var oldestNotification = _activeNotifications.Dequeue();
                oldestNotification.transform.DOKill();
                _floatingCurrencyPool.Release(oldestNotification);
            }

            var notification = _floatingCurrencyPool.Get();
            _activeNotifications.Enqueue(notification);

            notification.SetNotification(currencyData.Icon, amount);
            notification.ResetPosition();

            notification.transform.DOLocalMoveY(_floatingDistance, _floatingDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _activeNotifications.Dequeue();
                    _floatingCurrencyPool.Release(notification);
                });

            notification.CanvasGroup.DOFade(0, _floatingDuration);
        }

    }
}
