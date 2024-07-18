#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace boshphelm.Save.Editor
{
    [CustomEditor(typeof(SavingWrapper))]
    public class SavingWrapperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Label("Editor Settings..");
            if (GUILayout.Button("Reset Save"))
            {
                SavingWrapper savingWrapper = (SavingWrapper)target;
                if (savingWrapper != null) savingWrapper.Delete();
            }
        }
    }
}
#endif