using Boshphelm.Commands;
using UnityEngine;

namespace Boshphelm.Panel
{
    public class FailButtonActivater : Command
    {
        [SerializeField] private GameObject _reviveButtonGO;
        [SerializeField] private GameObject _noThanksButtonGO;
        [SerializeField] private float _delayTime = 2;
        private float _timeCount = 0;
        private bool _active = false;

        public override void StartCommand()
        {
            _reviveButtonGO?.SetActive(true);
            if (_noThanksButtonGO == null)
            {
                CompleteCommand();
            }
            else
            {
                _active = true;
            }
        }

        private void Update()
        {
            if (!_active) return;
            _timeCount += Time.deltaTime;

            if (_timeCount >= _delayTime)
            {
                _timeCount = 0;
                _active = false;
                _noThanksButtonGO.SetActive(true);
                CompleteCommand();
            }
        }

        public override void ResetCommand()
        {
            _reviveButtonGO?.SetActive(false);
            _noThanksButtonGO.SetActive(false);
            _timeCount = 0;
        }
    }
}