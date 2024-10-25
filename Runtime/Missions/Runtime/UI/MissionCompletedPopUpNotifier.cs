using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Boshphelm.Missions
{
    public class MissionCompletedPopUpNotifier : MonoBehaviour
    { 
        [SerializeField] private GameObject _maskGO; 
        [SerializeField] private Image _popUp;
        [SerializeField] private Vector3 _targetPos;
        private Vector3 _initPos; 
        DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> _animation;
        private void Start() => _initPos = _popUp.transform.localPosition;
        public void Show()
        { 
            if(_animation != null) return;

            _maskGO.SetActive(true); 
            _animation = _popUp.transform.DOLocalMove(_targetPos, 1).SetEase(Ease.OutBack).OnComplete(()=>
            {
                _popUp.transform.DOLocalMove(_initPos, 1).SetDelay(1f).OnComplete(()=>
                {
                    _maskGO.SetActive(false); 
                    _animation = null;
                });
            });
        }
    }
}