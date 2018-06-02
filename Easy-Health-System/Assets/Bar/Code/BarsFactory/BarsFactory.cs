using UnityEngine;

namespace Hypnagogia.Bar.Code.BarsFactory
{
    public static class BarsFactory
    {
        static Canvas parentSceneCanvas;
        
        public static MovingBar CreateMovingBar(BarsAssetsData barsAssetsData)
        {
            if (parentSceneCanvas == null)
                parentSceneCanvas = Object.Instantiate(barsAssetsData.screenSpaceCanvasPrefab);

            return Object.Instantiate(barsAssetsData.barPrefab, parentSceneCanvas.transform) as MovingBar;
        }
    }
}