using UnityEngine.UIElements;

namespace Boshphelm.Missions.Editor
{
    public class InspectorView : VisualElement
    {
        private UnityEditor.Editor _editor;

        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits>
        {
        }

        public InspectorView()
        {

        }

        public void UpdateSelection(MissionNodeView missionNodeView)
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(_editor);
            _editor = UnityEditor.Editor.CreateEditor(missionNodeView.MissionType);
            var container = new IMGUIContainer(() =>
            {
                if (_editor.target)
                {
                    _editor.OnInspectorGUI();
                }
            });
            Add(container);
        }
    }
}
