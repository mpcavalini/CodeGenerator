using System;
using System.IO;

namespace CodeGenerator
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Class name:");
            var cclass = Console.ReadLine();

            Console.WriteLine("Project name:");
            var cproject = Console.ReadLine();

            Console.WriteLine($"Class name:{cclass}");
            Console.WriteLine($"Project name:{cproject}");

            var app = new App(cclass, cproject);
            app.Init();
            Console.ReadLine();
        }
    }

    public class App
    {
        public string projectTarget { get; set; }
        public string myClass { get; set; }
        public string myProject { get; set; }
        public string myNamespace { get; set; }

        public App(string cclass, string cproject)
        {
            //projectTarget = @"Z:\dev\netcore\MyGrade\MyGrade\__CC";
            projectTarget = @"Z:\dev\netcore\MyGrade\MyGrade";
            myClass = cclass;
            myProject = cproject;
        }

        public void Init() {
            Console.WriteLine("Init");
            Domain();
            Console.WriteLine("End");
        }

        private void Domain()
        {

            string src = @"\Core\Domain";
            
            string path = CreateFolder(src);
            path = @$"{path}\{myClass}.cs";
            if (!File.Exists(path))
            {

                myNamespace = setNameSpace("MyGrade.Core.Domain");

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("using MyGrade.Core.Domain.Base;");
                    sw.WriteLine($"namespace {myNamespace}");
                    sw.WriteLine("\t{");
                    sw.WriteLine($"\t\tpublic class {myClass} : BaseName");
                    sw.WriteLine("\t\t{");
                    sw.WriteLine("\t\t}");
                    sw.WriteLine("\t}");
                }
            }
            Console.WriteLine($"{myClass} Domain created");
        }

        private string CreateFolder(string src)
        {
            src = projectTarget + setFolder(src);

            Console.WriteLine($"src: {src}");
            if (!Directory.Exists(src))
            {
                Console.WriteLine($"src created: {src}");
                Directory.CreateDirectory(src);
            }

            return src;
        }

        private string setNameSpace(string val)
        {
            return String.IsNullOrEmpty(myProject) ? val : $"{val}.{myProject}";
        }

        private string setFolder(string val)
        {
            return String.IsNullOrEmpty(myProject) ? val : @$"{val}\{myProject}";
        }
    }

}
