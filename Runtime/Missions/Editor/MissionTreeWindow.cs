using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Boshphelm.Missions.Editor
{
    public class MissionTreeWindow : EditorWindow
    {
        private MissionTreeView _missionTreeView;
        private InspectorView _inspectorView;
        private MissionTree _missionTree;

        [MenuItem("Boshphelm/UI Toolkit/MissionTreeWindow")]
        public static void OpenWindow()
        {
            var wnd = GetWindow<MissionTreeWindow>();
            wnd.titleContent = new GUIContent("MissionTreeWindow");
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID)
        {
            if (Selection.activeObject is MissionTree)
            {
                OpenWindow();
                return true;
            }
            return false;
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Data/MissionTreeWindow.uxml");
            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Data/MissionTreeWindow.uss");
            root.styleSheets.Add(styleSheet);

            _missionTreeView = root.Q<MissionTreeView>();
            _missionTreeView.OnMissionNodeSelected = OnMissionNodeSelectionChanged;
            _inspectorView = root.Q<InspectorView>();

            OnSelectionChange();
        }

        private void OnEnable()
        {
            Undo.undoRedoPerformed -= OnUndoRedoPerformed;
            Undo.undoRedoPerformed += OnUndoRedoPerformed;
        }
        private void OnUndoRedoPerformed()
        {
            _missionTreeView.PopulateView(_missionTree);
            AssetDatabase.SaveAssets();
        }

        private void OnMissionNodeSelectionChanged(MissionNodeView missionNodeView)
        {
            //Debug.Log("SELECTED MISSION NODEE");
            _inspectorView.UpdateSelection(missionNodeView);
        }

        private void OnSelectionChange()
        {
            var missionTree = Selection.activeObject as MissionTree;
            if (missionTree != null)
            {
                _missionTree = missionTree;
                //Debug.Log("SELECTION ACTIVE OBJECT : " + Selection.activeObject, Selection.activeObject);
                _missionTreeView.PopulateView(_missionTree);
            }
        }
    }

}
