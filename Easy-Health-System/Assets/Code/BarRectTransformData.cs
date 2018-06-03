using UnityEngine;

namespace EasyHealthSystem
{
    [CreateAssetMenu(fileName = "BarRectTransformData", menuName = "Custom/BarRectTransformData", order = 1)]
    public class BarRectTransformData : ScriptableObject
    {
        public Vector3 anchoredPosition;
        public Vector2 sizeDelta;
        
        public Vector2 anchorsMin;
        public Vector2 anchorsMax;
        
        public Vector2 pivot;
        
        public Vector3 rotationEuler;
        public Vector3 localScale;

        public bool useColor;
        public Color barColor;

        public Color? Color
        {
            get
            {
                if (useColor)
                    return barColor;
                return null;
            }
        }
        

        public void SetTransform(RectTransform rectTransform)
        {
            rectTransform.anchoredPosition3D = anchoredPosition;
            rectTransform.sizeDelta = sizeDelta;
            rectTransform.anchorMin = anchorsMin;
            rectTransform.anchorMax = anchorsMax;
            rectTransform.pivot = pivot;
            rectTransform.rotation = Quaternion.Euler(rotationEuler);
            rectTransform.localScale = localScale;
        }

        public void Load(RectTransform rectTransform)
        {
            anchoredPosition = rectTransform.anchoredPosition3D;
            sizeDelta = rectTransform.sizeDelta;
            anchorsMin = rectTransform.anchorMin;
            anchorsMax = rectTransform.anchorMax;
            pivot = rectTransform.pivot;
            rotationEuler = rectTransform.rotation.eulerAngles;
            localScale = rectTransform.localScale;
        }
    }
}