using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.Save
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private SavingWrapper _savingWrapper;

        [Header("Listening To...")] [SerializeField] private VoidEventChannel _onSave;

        private void OnEnable()
        {
            _onSave.OnEventRaise += OnSave;
        }

        private void OnDisable()
        {
            _onSave.OnEventRaise -= OnSave;
        }

        private void OnSave()
        {
            _savingWrapper.Save();
        }
    }
}