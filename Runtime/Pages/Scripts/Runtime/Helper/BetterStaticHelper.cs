using TheraBytes.BetterUi;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Boshphelm.Pages
{
    public static class BetterStaticHelper
    {
        [Preserve]
        public static float MyStaticMethod(Component caller, Vector2 optimizedResolution, Vector2 actualResolution, float optimizedDpi, float actualDpi)
        {
            var gridLayoutGroup = caller.GetComponent<GridLayoutGroup>();
            var betterGridLayoutGroup = caller.GetComponent<BetterGridLayoutGroup>();

            float optimizedXValue = betterGridLayoutGroup.CellSizer.OptimizedSize.x;
            float calculatedXValue = gridLayoutGroup.cellSize.x;
            float multiplier = calculatedXValue / optimizedXValue;

            return multiplier * 1.5f;
        }
    }
}
