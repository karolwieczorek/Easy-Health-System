using UnityEngine;

namespace EasyHealthSystem {
    public static class GameObjectExtensions
    {
        public static bool IsPrefab(this GameObject go)
        {
            return go.scene.name == null;
        }
    }
}