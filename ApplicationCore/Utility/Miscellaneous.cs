using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ApplicationCore.Utility
{
    public static class Miscellaneous
    {
        public static string GetException(this Exception exception)
            => string.Join("||", exception.Message, exception.StackTrace);

        public static Assembly GetAssemblyByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().
                   SingleOrDefault(assembly => assembly.GetName().Name == name);
        }
    }
}
