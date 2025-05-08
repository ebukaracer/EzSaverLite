#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Racer.EzSaverLite.Editor
{
    internal static class EzSaverLiteEditor
    {
        private static RemoveRequest _removeRequest;

        private const string ContextMenuPath = "Racer/EzSaverLite/";
        private const string FullContextMenuPath = ContextMenuPath + "Import WebGL Save Plugin(Force)";
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

            var packagePath = $"Packages/{PkgId}/Plugins/{AssetPkgId}";

            if (File.Exists(packagePath))
            {
                AssetDatabase.ImportPackage(packagePath, true);
                AssetDatabase.importPackageCompleted += DefineSymbol;
            }
            else
                EditorUtility.DisplayDialog("Missing Package File", $"{AssetPkgId} not found in the package.", "OK");
        }


        [MenuItem(ContextMenuPath + "Remove Package(recommended)")]
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
                    DirUtil.DeleteDirectory(RootPath);
                    DirUtil.DeleteDirectory(SamplesPath);
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

    internal static class DirUtil
    {
        public static void DeleteDirectory(string path)
        {
            if (!Directory.Exists(path)) return;

            Directory.Delete(path, true);
            DeleteEmptyMetaFiles(path);
        }

        private static void DeleteEmptyMetaFiles(string directory)
        {
            if (Directory.Exists(directory)) return;

            var metaFile = directory + ".meta";

            if (File.Exists(metaFile))
                File.Delete(metaFile);
        }
    }
}
#endif