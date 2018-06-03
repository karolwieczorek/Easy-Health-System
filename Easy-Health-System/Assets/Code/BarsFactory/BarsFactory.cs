using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyHealthSystem.BarsFactory
{
    public static class BarsFactory
    {
        static List<ParentCache> cache = new List<ParentCache>();

        public static MovingBar CreateMovingBar(RectTransform parent, Bar barSource)
        {
            if (barSource.gameObject.IsPrefab())
            {
                var targetParent = GetParent(parent);
                return Object.Instantiate(barSource, targetParent) as MovingBar;                
            }
            return barSource as MovingBar;
        }

        static RectTransform GetParent(RectTransform parent)
        {
            RectTransform targetParent = null;
            if (parent.gameObject.IsPrefab())
            {
                var cacheObject = cache.FirstOrDefault(x => x.prefab == parent);
                if (cacheObject != null)
                    targetParent = cacheObject.target;
                if (targetParent == null)
                {
                    targetParent = Object.Instantiate(parent);
                    cache.Add(new ParentCache {prefab = parent, target = targetParent});
                }
            } else
            {
                targetParent = parent;
            }

            return targetParent;
        }

        class ParentCache
        {
            public RectTransform prefab;
            public RectTransform target;
        }
    }
}