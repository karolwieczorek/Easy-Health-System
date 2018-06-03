using System.Collections.Generic;
using UnityEngine;

namespace EasyHealthSystem.BarsFactory
{    
    [CreateAssetMenu(fileName = "BarsAssetsData", menuName = "Custom/BarsAssetsData", order = 1)]
    public class BarsAssetsData : ScriptableObject
    {
        public Canvas screenSpaceCanvasPrefab;
        public Canvas worldSpaceCanvasPrefab;

        public List<Bar> barsPrefabs;
    }
}