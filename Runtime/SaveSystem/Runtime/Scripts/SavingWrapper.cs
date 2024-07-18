using System.Collections;
using UnityEngine;

namespace boshphelm.Save
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] string defaultSaveFile = "Xsave";
        private void Awake() {
            Load();
        }
        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }

        private void OnApplicationFocus(bool focusStatus)
        {
            // if (!focusStatus) Save();
        }
    }
}