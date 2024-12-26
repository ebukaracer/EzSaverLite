#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Racer.EzSaverLite
{
    internal class EzSaverLiteEditorWindow : EditorWindow
    {
        private static RemoveRequest _removeRequest;

        private const string ContextMenuPath = "Racer/EzSaverLite/";
        private const string FullContextMenuPath = ContextMenuPath + "Import WebGL Save Plugin?";
        private const string RootPath = "Assets/EzSaverLite";
        private const string PluginPath = RootPath + "/Plugins";
        private const string SamplesPath = "Assets/Samples/EzSaverLite";
        private const string PkgId = "com.racer.ezsaverlite";
        private static readonly string PkgSourcePath = $"Packages/{PkgId}/Plugins";


        [MenuItem(FullContextMenuPath, false)]
        private static void ImportCore()
        {
            if (Directory.Exists(PluginPath))
            {
                Debug.Log(
                    $"Root directory already exists: '{PluginPath}'" +
                    "\nIf you would like to re-import, remove and reinstall this package.");
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
                Directory.Move(PkgSourcePath, PluginPath);
                DirUtil.DeleteEmptyMetaFiles(PkgSourcePath);
                AssetDatabase.Refresh();
                Debug.Log($"Imported successfully at '{PluginPath}'");
            }
            catch (Exception e)
            {
                Debug.LogError(
                    $"An error occurred while importing WebGL Save Plugin: {e.Message}\n{e.StackTrace}");
            }
        }

        [MenuItem(FullContextMenuPath, true)]
        private static bool ValidateImportCore()
        {
            return !AssetDatabase.IsValidFolder(PluginPath);
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
                    PlayerPrefs.DeleteKey("Highscore");
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