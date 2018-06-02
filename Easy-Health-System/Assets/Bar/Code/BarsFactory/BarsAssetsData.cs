using UnityEngine;

namespace Hypnagogia.Bar.Code.BarsFactory
{    
    [CreateAssetMenu(fileName = "BarsAssetsData", menuName = "Custom/BarsAssetsData", order = 1)]
    public class BarsAssetsData : ScriptableObject
    {
        public Canvas screenSpaceCanvasPrefab;
        public Canvas worldSpaceCanvasPrefab;

        public Bar barPrefab;
    }
}