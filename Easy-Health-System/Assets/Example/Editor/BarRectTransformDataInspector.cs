using UnityEditor;
using UnityEngine;

namespace EasyHealthSystem.Example.Editor
{
    [CustomEditor(typeof(BarRectTransformData))]
    public class BarRectTransformDataInspector : UnityEditor.Editor
    {
        BarRectTransformData targetObject;

        RectTransform rectTransform;
        GameObject hidenObject;
        UnityEditor.Editor transformEditor; 

        void OnEnable()
        {
            targetObject = (BarRectTransformData)target;
            
            hidenObject = new GameObject("hidenCanvas");
            hidenObject.hideFlags = HideFlags.DontSave | HideFlags.HideInHierarchy;
            
            
            hidenObject.AddComponent<Canvas>();
            var go = new GameObject("rect Transform", typeof(RectTransform));
            go.transform.SetParent(hidenObject.transform);
            
            rectTransform = go.GetComponent<RectTransform>();
            
        }

        void OnDisable()
        {
            DestroyImmediate(hidenObject);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            if (rectTransform != null)
            {
                transformEditor = CreateEditor(rectTransform);
            }

            if (transformEditor != null)
            {
                transformEditor.OnInspectorGUI();
                if (GUI.changed)
                {
                    EditorUtility.SetDirty(target);
                }
            }
        }

        
    }
}