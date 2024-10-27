namespace Boshphelm.Panel
{
    public interface IPanel
    {
        void Open();
        void Close();
        bool IsOpen { get; } 
    }

}
