using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EasyHealthSystem.Example.Editor
{
    [CustomEditor(typeof(BarOperator))]
    public class BarOperatorInspector : UnityEditor.Editor
    {
        BarOperator targetObject;
        int selected = -1;
        int Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                    targetObject.barSource = targetObject.barsAssetsData.barsPrefabs[selected];
                }
            }
        }

        void OnEnable()
        {
            targetObject = (BarOperator)target;

            InitSelectedBar();
            InitSelectedCanvas();
        }

        void InitSelectedBar()
        {
            if (targetObject.barSource != null &&
                targetObject.barsAssetsData.barsPrefabs.Contains(targetObject.barSource))
                selected = targetObject.barsAssetsData.barsPrefabs.IndexOf(targetObject.barSource);
            else
                selected = -1;
        }

        bool foldout = true;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foldout = EditorGUILayout.Foldout(foldout, "Advanced", true);
            if (foldout)
                ShowAdvanced();
        }

        void ShowAdvanced()
        {
            BarTargetArea();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            if (targetObject.barSource != null)
            {
                if (targetObject.IsBarPrefab())
                    CanvasTargetArea();
            }
        }
        
        int prefabTargetIndex;
        enum PrefabTarget
        {
            Examples,
            Direct
        }

        readonly string[] prefabTargetMenuOptions = Enum.GetNames (typeof(PrefabTarget));

        void BarTargetArea()
        {
            EditorGUILayout.LabelField("Prefab target:");
            
            prefabTargetIndex = GUILayout.Toolbar(prefabTargetIndex, prefabTargetMenuOptions);
            switch (prefabTargetIndex)
            {
                case (int) PrefabTarget.Examples:
                {
                    InitSelectedBar();
                    string[] options = targetObject.barsAssetsData.barsPrefabs.Select(x => x.name).ToArray();
                    Selected = EditorGUILayout.Popup("Bar prefab", Selected, options);
                    break;
                }
                case (int) PrefabTarget.Direct:
                {
                    targetObject.barSource =
                        EditorGUILayout.ObjectField("Bar source", targetObject.barSource, typeof(Bar), true) as Bar;
                    string label = targetObject.barSource == null ? "source is null" :
                        targetObject.IsBarPrefab() ? "bar is prefab" : "bar is on scene"; 
                    EditorGUILayout.LabelField(label);
                    break;
                }
            }
        }
        
        int canvasTargetIndex;
        enum CanvasTarget
        {
            Screen,
            Scene,
            Direct
        }

        readonly string[] canvasTargetMenuOptions = Enum.GetNames (typeof(CanvasTarget));
        void CanvasTargetArea()
        {
            EditorGUILayout.LabelField("Canvas target:");
            canvasTargetIndex = GUILayout.Toolbar(canvasTargetIndex, canvasTargetMenuOptions);
            switch (canvasTargetIndex)
            {
                case (int)CanvasTarget.Screen:
                    targetObject.parentRectTransformSource = targetObject.barsAssetsData.screenSpaceCanvasPrefab;
                    break;
                case (int)CanvasTarget.Scene:
                    targetObject.parentRectTransformSource = targetObject.barsAssetsData.worldSpaceCanvasPrefab;
                    break;
                case (int)CanvasTarget.Direct:
                    targetObject.parentRectTransformSource =
                        EditorGUILayout.ObjectField("parent", targetObject.parentRectTransformSource, typeof(RectTransform), true) as RectTransform;
                    break;
            }
        }

        void InitSelectedCanvas()
        {
            if (targetObject.parentRectTransformSource == targetObject.barsAssetsData.screenSpaceCanvasPrefab)
                canvasTargetIndex = 0;
            else if (targetObject.parentRectTransformSource == targetObject.barsAssetsData.worldSpaceCanvasPrefab)
                canvasTargetIndex = 1;
            else
                canvasTargetIndex = 2;

        }
    }
}