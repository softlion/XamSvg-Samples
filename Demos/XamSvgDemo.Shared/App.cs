using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamSvgDemo.Shared
{
    //Dummy class used to detect the assembly name
    [Preserve]
    public class App
    {
    }

    /// <summary>
    /// Xamarin linker detects the PreserveAttribute and does not optimize away the unused class
    /// </summary>
    internal sealed class Preserve : Attribute
    {
    }
}
