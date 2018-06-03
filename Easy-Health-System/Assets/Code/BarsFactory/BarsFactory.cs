using UnityEngine;

namespace EasyHealthSystem.BarsFactory
{
    public static class BarsFactory
    {
        static Canvas parentSceneCanvas;
        
        public static MovingBar CreateMovingBar(BarsAssetsData barsAssetsData, Bar barPrefab)
        {
            if (parentSceneCanvas == null)
                parentSceneCanvas = Object.Instantiate(barsAssetsData.screenSpaceCanvasPrefab);

            return Object.Instantiate(barPrefab, parentSceneCanvas.transform) as MovingBar;
        }
    }
}