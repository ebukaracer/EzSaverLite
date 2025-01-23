#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Racer.EzSaverLite.Editor
{
    internal class EzSaverLiteEditor : EditorWindow
    {
        private static RemoveRequest _removeRequest;

        private const string ContextMenuPath = "Racer/EzSaverLite/";
        private const string FullContextMenuPath = ContextMenuPath + "Import WebGL Save Plugin";
        private const string RootPath = "Assets/EzSaverLite";
        private const string PluginRootPath = RootPath + "/Plugins";
        private const string PluginPath = PluginRootPath + "/Lss.jslib";
        private const string SamplesPath = "Assets/Samples/EzSaverLite";
        private const string PkgId = "com.racer.ezsaverlite";
        private static readonly string PkgSourcePath = $"Packages/{PkgId}/Plugins";


        [MenuItem(FullContextMenuPath, false)]
        private static void ImportPlugin()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
            {
                Debug.LogWarning("To import this plugin, current build target must be WebGL.");
                return;
            }

            if (File.Exists(PluginPath))
            {
                Debug.Log($"Plugin already imported at: '{PluginRootPath}'");
                SymbolDefiner.DefineSymbol();
                return;
            }

            if (!Directory.Exists(PkgSourcePath))
            {
                Debug.LogError(
                    "Source path is missing. Please ensure this package is installed correctly," +
                    $" otherwise reinstall it.\nNonexistent Path: {PkgSourcePath}");
                return;
            }

            try
            {
                DirUtil.CreateDirectory(RootPath);
                Directory.Move(PkgSourcePath, PluginRootPath);
                DirUtil.MoveMetaFile(PkgSourcePath, PluginRootPath);
                AssetDatabase.Refresh();
                Debug.Log($"Imported successfully at '{PluginRootPath}'");
                SymbolDefiner.DefineSymbol();
            }
            catch (Exception e)
            {
                Debug.LogError(
                    $"An error occurred while importing WebGL Save Plugin: {e.Message}\n{e.StackTrace}");
            }
        }

        [MenuItem(FullContextMenuPath, true)]
        private static bool ValidateImportPlugin()
        {
            return !SymbolDefiner.IsDefined || !File.Exists(PluginPath);
        }

        [MenuItem(ContextMenuPath + "Remove Package(recommended)")]
        private static void RemovePackage()
        {
            _removeRequest = Client.Remove(PkgId);

            EditorApplication.update += RemoveRequest;
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
                    Debug.LogWarning($"Failed to remove package: '{PkgId}'\n{_removeRequest.Error.message}");
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

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void MoveMetaFile(string source, string destination)
        {
            if (!File.Exists(source + ".meta")) return;

            File.Move(source + ".meta", destination + ".meta");
        }

        public static void DeleteEmptyMetaFiles(string directory)
        {
            if (Directory.Exists(directory)) return;

            var metaFile = directory + ".meta";

            if (File.Exists(metaFile))
                File.Delete(metaFile);
        }
    }
}
#endif