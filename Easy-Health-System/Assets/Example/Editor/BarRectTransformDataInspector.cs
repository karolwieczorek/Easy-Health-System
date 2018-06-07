using UnityEditor;
using UnityEngine;

namespace EasyHealthSystem.Example.Editor
{
    [CustomEditor(typeof(BarRectTransformData))]
    public class BarRectTransformDataInspector : UnityEditor.Editor
    {
        BarRectTransformData targetObject;

        void OnEnable()
        {
            targetObject = (BarRectTransformData)target;
            
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            if (RectTransformEditorProvider.rectTransform == null)
                RectTransformEditorProvider.Create(targetObject);
            
            if (RectTransformEditorProvider.rectTransform != null)
            {
                RectTransformEditorProvider.TransformEditor.OnInspectorGUI();
                if (GUI.changed)
                {
                    targetObject.Load(RectTransformEditorProvider.rectTransform);
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }

    internal static class RectTransformEditorProvider
    {
        public static RectTransform rectTransform;
        static GameObject hidenObject;
        static UnityEditor.Editor transformEditor;

        public static UnityEditor.Editor TransformEditor
        {
            get
            {
                if (transformEditor == null)
                    Create();
                return transformEditor;
            }
        }

        public static void Create()
        {
            var hideFlags = HideFlags.DontSave | HideFlags.HideInHierarchy; 
            
            hidenObject =
                EditorUtility.CreateGameObjectWithHideFlags("hidenCanvas", hideFlags);
            
            
            hidenObject.AddComponent<Canvas>();
            var go = EditorUtility.CreateGameObjectWithHideFlags("rect Transform", hideFlags, typeof(RectTransform));
            go.transform.SetParent(hidenObject.transform);
            
            rectTransform = go.GetComponent<RectTransform>();
            
            if (rectTransform != null)
            {
                transformEditor = UnityEditor.Editor.CreateEditor(rectTransform);
            }
        }

        public static void Create(BarRectTransformData data)
        {
            Create();
            data.SetTransform(rectTransform);
        }
    }
}