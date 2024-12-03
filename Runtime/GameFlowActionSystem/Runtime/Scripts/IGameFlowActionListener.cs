namespace Boshphelm.GFASystem
{
    public interface IGameFlowActionListener
    {
        void AddGameFlowAction(GameFlowAction gameFlowAction);
        void OnGameFlowActionComplete(GameFlowAction gameFlowAction);
    }
}