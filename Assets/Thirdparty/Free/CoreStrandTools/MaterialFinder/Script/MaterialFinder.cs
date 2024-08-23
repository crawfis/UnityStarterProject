using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace CoreStrandLLC
{
    public class MaterialFinder : EditorWindow
    {
        private Material materialToFind;
        private List<GameObject> foundObjects = new List<GameObject>();
        private Vector2 scrollPos;

        [MenuItem("Tools/CoreStrandTools/MaterialFinder")]
        public static void ShowWindow()
        {
            GetWindow<MaterialFinder>("Material Finder");
        }

        private void OnGUI()
        {
            GUILayout.Label("Material Finder", EditorStyles.boldLabel);
            materialToFind = (Material)EditorGUILayout.ObjectField("Material", materialToFind, typeof(Material), false);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Find Objects"))
            {
                FindObjects();
            }
            if (GUILayout.Button("Clear"))
            {
                ClearResults();
            }
            GUILayout.EndHorizontal();

            if (foundObjects.Count > 0)
            {
                GUILayout.Label($"Found {foundObjects.Count} objects:", EditorStyles.boldLabel);
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(300)); // Adjust height as needed
                foreach (var obj in foundObjects)
                {
                    EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
                }
                EditorGUILayout.EndScrollView();
            }
        }

        private void FindObjects()
        {
            foundObjects.Clear();
            if (materialToFind == null)
            {
                Debug.LogWarning("No material selected to search for.");
                return;
            }

            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    foreach (var mat in renderer.sharedMaterials)
                    {
                        if (mat == materialToFind)
                        {
                            foundObjects.Add(obj);
                            break;
                        }
                    }
                }
            }

            Debug.Log($"Found {foundObjects.Count} objects using the material {materialToFind.name}.");
        }

        private void ClearResults()
        {
            materialToFind = null;
            foundObjects.Clear();
        }
    }
}