using System;
using System.Collections.Generic;
using UnityEngine;

namespace Boshphelm.Utility
{
    public static class PredefinedAssemblyUtil
    {
        public enum AssemblyType
        {
            AssemblyCSharp,
            AssemblyCSharpEditor,
            AssemblyCSharpFirstPass,
            AssemblyCSharpEditorFirstPass
        }

        private static AssemblyType? GetAssemblyType(string assemblyName)
        {
            return assemblyName switch
            {
                "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
                "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
                "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
                "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
                _ => null
            };
        }

        public static List<Type> GetTypes(Type interfaceType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var assemblyTypes = new Dictionary<AssemblyType, Type[]>();
            var types = new List<Type>();

            for (int i = 0; i < assemblies.Length; i++)
            {
                var assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
                if (assemblyType != null)
                {
//                    Debug.Log("Assembly Name : " + assemblies[i].GetName().Name);
                    assemblyTypes.Add((AssemblyType)assemblyType, assemblies[i].GetTypes());
                }
            }

            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharp], interfaceType, types);
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharpFirstPass], interfaceType, types);

            return types;
        }

        private static void AddTypesFromAssembly(Type[] assembly, Type interfaceType, List<Type> types)
        {
            if (assembly == null) return;

            for (int i = 0; i < assembly.Length; i++)
            {
                Type type = assembly[i];
                if (type != interfaceType && interfaceType.IsAssignableFrom(type))
                {
                    Debug.Log("VALID! --- TYPE : " + type.Name + ", Interface Type : " + interfaceType.Name);
                    types.Add(type);
                }
            }
        }
    }
}