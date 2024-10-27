using System; 

namespace Boshphelm.Panel
{
    // Her panel için config
    [Serializable]
    public class PanelConfig 
    {
        public GamePanelType Type;
        public PanelBase Panel; 
        public GamePanelType[] LinkedPanels;  // Bu panel açıldığında otomatik açılacak paneller 
        public GamePanelType[] IncompatiblePanels;  // Bu panel açıkken kapanması gereken paneller
    }
}
