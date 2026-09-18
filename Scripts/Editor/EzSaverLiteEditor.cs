using System.IO;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Racer.EzSaverLite.Scripts.Editor
{
    internal static class EzSaverLiteEditor
    {
        private static RemoveRequest _removeRequest;

        private const string ContextMenuPath = "Racer/EzSaverLite/";
        private const string FullContextMenuPath = ContextMenuPath + "Import WebGL Save Plugin (force)";
        private const string RootPath = "Assets/EzSaverLite";

        private const string PluginRootPath = RootPath + "/Plugins";
        private const string PluginAssetsPath = PluginRootPath + "/Lss.jslib";
        private const string SamplesPath = "Assets/Samples/EzSaverLite";

        private const string PkgId = "com.racer.ezsaverlite";
        private const string AssetPkgId = "EzSaverLite.unitypackage";


        [MenuItem(FullContextMenuPath, false)]
        private static void ImportPlugin()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
            {
                Debug.LogError("To import this plugin, current build target must be WebGL.");
                return;
            }

            var path = $"Packages/{PkgId}/Dependencies~/Package/{AssetPkgId}";

            if (File.Exists(path))
            {
                AssetDatabase.ImportPackage(path, true);
                AssetDatabase.importPackageCompleted += DefineSymbol;
            }
            else
                EditorUtility.DisplayDialog("Missing Package File", $"{AssetPkgId} not found in the package.", "OK");
        }


        [MenuItem(ContextMenuPath + "Remove Package (recommended)")]
        private static void RemovePackage()
        {
            _removeRequest = Client.Remove(PkgId);
            EditorApplication.update += RemoveRequest;
        }

        private static void DefineSymbol(string pkgName = null)
        {
            if (File.Exists(PluginAssetsPath))
                SymbolDefiner.DefineSymbol();
            else
                Debug.LogError($"Failed to define symbol, plugin file not found at: {PluginAssetsPath}");

            AssetDatabase.importPackageCompleted -= DefineSymbol;
        }

        private static void RemoveRequest()
        {
            if (!_removeRequest.IsCompleted) return;

            switch (_removeRequest.Status)
            {
                case StatusCode.Success:
                {
                    AssetDatabase.DeleteAsset(RootPath);
                    AssetDatabase.DeleteAsset(SamplesPath);
                    AssetDatabase.Refresh();
                    SymbolDefiner.UnDefineSymbol();

                    break;
                }
                case >= StatusCode.Failure:
                    Debug.LogError($"Failed to remove package: '{PkgId}'\n{_removeRequest.Error.message}");
                    break;
            }

            EditorApplication.update -= RemoveRequest;
        }
    }
}