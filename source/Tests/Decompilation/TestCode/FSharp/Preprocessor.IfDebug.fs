#if DEBUG
    printfn "Debug"
#else
    printfn "Release"
#endif

(* cs

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using <StartupCode$_>;
using Microsoft.FSharp.Core;

[assembly: FSharpInterfaceDataVersion(2, 0, 0)]
[assembly: AssemblyVersion("0.0.0.0")]
[CompilationMapping(SourceConstructFlags.Module)]
public static class @_
{
    [CompilationMapping(SourceConstructFlags.Value)]
    internal static PrintfFormat<Unit, TextWriter, Unit, Unit> format@1
    {
        get
        {
            return $_.format@1;
        }
    }
}
namespace <StartupCode$_>
{
    internal static class $_
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal static readonly PrintfFormat<Unit, TextWriter, Unit, Unit> format@1;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [CompilerGenerated]
        [DebuggerNonUserCode]
        internal static int init@;

        static $_()
        {
            format@1 = new PrintfFormat<Unit, TextWriter, Unit, Unit, Unit>("Debug");
            PrintfModule.PrintFormatLineToTextWriter(Console.Out, @_.format@1);
        }
    }
}

*)