using System;
using System.Management.Automation;

namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) {
            using (var ps = PowerShell.Create()) {
                ps.AddScript(@"Get-ChildItem c:\");
                var result = ps.Invoke();
                foreach (var r in result) {
                    Console.WriteLine(r);

                    Console.WriteLine("...which is a...");
                    Console.WriteLine(r.BaseObject.GetType().FullName);

                    Console.WriteLine("");
                }
            }
        }
    }
}
