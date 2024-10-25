using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Boshphelm.Missions.Editor
{
    public class MissionNodeView : Node
    {
        public System.Action<MissionNodeView> OnMissionNodeSelected;
        public MissionType MissionType;

        public Port input;
        public Port output;

        public MissionNodeView(MissionType missionType) : base("Assets/Editor/Data/MissionNodeView.uxml")
        {
            MissionType = missionType;
            title = missionType.name;
            viewDataKey = MissionType.missionId.ToHexString();

            style.left = MissionType.Position.x;
            style.top = MissionType.Position.y;

            CreateInputPorts();
            CreateOutputPorts();

            var descriptionLabel = this.Q<Label>("description");
            descriptionLabel.bindingPath = "description";
            descriptionLabel.Bind(new SerializedObject(missionType));

            var titleLabel = this.Q<Label>("title-label");
            titleLabel.bindingPath = "missionName";
            titleLabel.Bind(new SerializedObject(missionType));
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);

            Undo.RecordObject(MissionType, "Mission Tree (Set Position)");

            MissionType.Position.x = newPos.xMin;
            MissionType.Position.y = newPos.yMin;

            EditorUtility.SetDirty(MissionType);
        }

        public void CreateInputPorts()
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(bool));

            input.portName = "";
            input.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(input);
        }

        public void CreateOutputPorts()
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));

            output.portName = "";
            output.style.flexDirection = FlexDirection.ColumnReverse;
            outputContainer.Add(output);
        }

        public override void OnSelected()
        {
            base.OnSelected();

            OnMissionNodeSelected?.Invoke(this);
        }
    }
}
