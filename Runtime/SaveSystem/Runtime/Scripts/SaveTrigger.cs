using Boshphelm.BHUtility;
using UnityEngine;

namespace boshphelm.Save
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private SavingWrapper _savingWrapper;

        [Header("Listening To...")]
        [SerializeField] private VoidEventChannelSO _onSave;

        private void OnEnable()
        {
            _onSave.onEventRaised += OnSave;
        }

        private void OnDisable()
        {
            _onSave.onEventRaised -= OnSave;
        }

        private void OnSave()
        {
            _savingWrapper.Save();
        }
    }
}