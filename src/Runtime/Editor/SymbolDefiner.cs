#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Racer.EzSaverLite.Editor
{
    internal static class SymbolDefiner
    {
        private const string CustomSymbol = "USE_LSS";
        private static List<string> _definedSymbols = new();


        public static void DefineSymbol()
        {
            if (IsDefined)
            {
                Debug.Log($"[{CustomSymbol}] symbol already defined.");
                return;
            }

            _definedSymbols = DefinedSymbols;
            _definedSymbols.Add(CustomSymbol);

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, _definedSymbols.ToArray());
            Debug.Log($"[{CustomSymbol}] symbol is being defined..");
        }

        public static void UnDefineSymbol()
        {
            if (!IsDefined)
                return;

            _definedSymbols = DefinedSymbols;
            _definedSymbols.Remove(CustomSymbol);

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, _definedSymbols.ToArray());
        }

        private static List<string> DefinedSymbols
        {
            get
            {
                var scriptingDefineSymbolsForGroup =
                    PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL);

                return scriptingDefineSymbolsForGroup.Split(';').ToList();
            }
        }

        public static bool IsDefined => DefinedSymbols.Contains(CustomSymbol);
    }
}
#endif