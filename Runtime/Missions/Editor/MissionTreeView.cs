using System;
using System.Collections.Generic;
using System.Linq;
using Boshphelm.Utility;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Boshphelm.Missions.Editor
{
    public class MissionTreeView : GraphView
    {
        public Action<MissionNodeView> OnMissionNodeSelected;
        private MissionTree _missionTree;

        public new class UxmlFactory : UxmlFactory<MissionTreeView, UxmlTraits>
        {
        }
        public MissionTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Data/MissionTreeWindow.uss");
            styleSheets.Add(styleSheet);

            //Debug.Log("GENERATED MISSION TREE VIEW");
        }
        private void DeregisterAllEvents()
        {
            OnMissionNodeSelected = null;
        }

        private MissionNodeView FindNodeViewByGuid(string guid) => GetNodeByGuid(guid) as MissionNodeView;

        public void PopulateView(MissionTree tree)
        {
            //Debug.Log("MISSION TREE IS : " + tree, tree);
            if (tree == null)
            {
                DeregisterAllEvents();
                return;
            }

            _missionTree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            // Creating Node Views
            tree.Missions.ForEach(CreateNodeView);

            // Creating Edges Between Views
            tree.Missions.ForEach(m =>
            {
                var parentNodeView = FindNodeViewByGuid(m.missionId.ToHexString());
                if (m.requiredMissions == null) return;

                m.requiredMissions.ForEach(r =>
                {
                    var childNodeView = FindNodeViewByGuid(r.missionId.ToHexString());

                    var edge = parentNodeView.input.ConnectTo(childNodeView.output);
                    edge.focusable = true;
                    AddElement(edge);
                });
            });
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
        {
            if (graphviewchange.elementsToRemove != null)
            {
                graphviewchange.elementsToRemove.ForEach(elem =>
                {
                    if (elem is MissionNodeView missionNodeView)
                    {
                        DeleteNodeView(missionNodeView.MissionType);
                    }

                    if (elem is Edge edge)
                    {
                        var parentView = edge.output.node as MissionNodeView;
                        var childView = edge.input.node as MissionNodeView;

                        if (parentView == null || childView == null) return;

                        Undo.RecordObject(childView.MissionType, "Mission Tree (RemoveRequiredMission)");
                        _missionTree.RemoveRequiredMission(childView.MissionType, parentView.MissionType);
                        EditorUtility.SetDirty(childView.MissionType);
                    }
                });
            }

            if (graphviewchange.edgesToCreate != null)
            {
                graphviewchange.edgesToCreate.ForEach(edge =>
                {
                    edge.focusable = true;
                    var parentView = edge.output.node as MissionNodeView;
                    var childView = edge.input.node as MissionNodeView;

                    if (parentView == null || childView == null) return;

                    Undo.RecordObject(childView.MissionType, "Mission Tree (AddRequiredMission)");
                    _missionTree.AddRequiredMission(childView.MissionType, parentView.MissionType);
                    EditorUtility.SetDirty(childView.MissionType);
                });
            }

            return graphviewchange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //base.BuildContextualMenu(evt);

            {
                var types = TypeCache.GetTypesDerivedFrom<MissionType>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name} ", (a) => CreateNode(type));
                }
            }
        }

        private void CreateNode(Type type)
        {
            //var missionType = ScriptableObject.CreateInstance<MissionType>();
            if (_missionTree == null) return;
            //Debug.Log("Mission Tree Not Nulll : " + type);

            var missionType = GenerateMissionType(type);
            if (missionType == null) return;

            CreateNodeView(missionType);
        }

        private MissionType GenerateMissionType(Type type)
        {
            string typeName = type.ToString();
            var missionType = ScriptableObject.CreateInstance(type) as MissionType;
            if (missionType != null)
            {
                missionType.missionId = SerializableGuid.NewGuid();
                missionType.name = typeName;
                Undo.RecordObject(_missionTree, "Mission Tree (CreateMission)");
                _missionTree.Missions.Add(missionType);

                AssetDatabase.AddObjectToAsset(missionType, _missionTree);
                Undo.RegisterCreatedObjectUndo(missionType, "Mission Tree (CreateNode)");
                AssetDatabase.SaveAssets();
                return missionType;
            }

            Debug.LogError("TYPE : " + typeName + ", NOT FOUND IN MISSION TREE", _missionTree);
            return null;
        }

        private void CreateNodeView(MissionType missionType)
        {
            var missionNodeView = new MissionNodeView(missionType);
            missionNodeView.focusable = true;
            missionNodeView.OnMissionNodeSelected = OnMissionNodeSelected;
            AddElement(missionNodeView);
        }

        private void DeleteNodeView(MissionType missionType)
        {
            if (_missionTree == null) return;

            DeleteMissionType(missionType);
        }

        private void DeleteMissionType(MissionType missionType)
        {
            Undo.RecordObject(_missionTree, "Mission Tree (DeleteMission)");
            _missionTree.Missions.Remove(missionType);
            Undo.DestroyObjectImmediate(missionType);
            AssetDatabase.SaveAssets();
        }

    }

}
