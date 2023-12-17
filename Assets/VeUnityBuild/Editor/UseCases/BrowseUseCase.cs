using UnityEditor;
using UnityEngine;

namespace VeUnityBuild.Editor.UseCases
{
    public class BrowseUseCase
    {
        public static string Browse()
        {
            var selectedPath = EditorUtility.OpenFolderPanel("Select Folder", "", "");
            if (string.IsNullOrEmpty(selectedPath))
            {
                return string.Empty;
            }

            var dataPath = Application.dataPath;
            if (selectedPath.StartsWith(dataPath))
            {
                return $"Assets{selectedPath[dataPath.Length..]}";
            }

            Debug.LogError("The selected folder is not part of the Assets folder.");
            return string.Empty;
        }
    }
}
