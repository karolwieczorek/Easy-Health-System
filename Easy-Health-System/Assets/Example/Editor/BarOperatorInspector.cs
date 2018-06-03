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
                    targetObject.barPrefab = targetObject.barsAssetsData.barsPrefabs[selected];
                }
            }
        }

        void OnEnable()
        {
            targetObject = (BarOperator)target;

            InitSelected();
        }

        void InitSelected()
        {
            if (targetObject.barPrefab != null &&
                targetObject.barsAssetsData.barsPrefabs.Contains(targetObject.barPrefab))
                selected = targetObject.barsAssetsData.barsPrefabs.IndexOf(targetObject.barPrefab);
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
        
        int toolBar;
        enum PrefabTarget
        {
            Examples,
            DirectPrefab
        }
        string[] menuOptions = System.Enum.GetNames (typeof(PrefabTarget));

        void ShowAdvanced()
        {
            toolBar = GUILayout.Toolbar(toolBar, menuOptions);

            switch (toolBar)
            {
                case (int) PrefabTarget.Examples:
                {
                    InitSelected();
                    string[] options = targetObject.barsAssetsData.barsPrefabs.Select(x => x.name).ToArray();
                    Selected = EditorGUILayout.Popup("Bar prefab", Selected, options);
                    break; 
                }
                case (int) PrefabTarget.DirectPrefab:
                {
                    targetObject.barPrefab = EditorGUILayout.ObjectField("Prefab", targetObject.barPrefab, typeof(Bar), true) as Bar;
                    break;
                }
            }
        }
    }
}