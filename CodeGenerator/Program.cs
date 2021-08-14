using System;
using System.IO;

namespace CodeGenerator
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Project prefix:");
            var cprojectPrefix = "MyGrade";// Console.ReadLine();

            Console.WriteLine("Class name:");
            var cclass = Console.ReadLine();

            Console.WriteLine("Project name:");
            var cproject = Console.ReadLine();

            Console.WriteLine($"Class name:{cclass}");
            Console.WriteLine($"Project name:{cproject}");
            Console.WriteLine($"Project prefix:{cprojectPrefix}");

            new App(cclass, cproject, cprojectPrefix);

            //Console.ReadLine();
        }
    }

    public class App
    {
        public string projectTarget { get; set; }
        public string projectPrefix { get; set; }
        public string myClass { get; set; }
        public string myProject { get; set; }
        public string myNamespace { get; set; }

        public App(string cclass, string cproject, string cprojectPrefix)
        {
            //projectTarget = @"Z:\dev\netcore\MyGrade\MyGrade\__CC";
            projectTarget = @"C:\Users\Cavas\Documents\sites\MyGrade\MyGrade";
            projectPrefix = cprojectPrefix;
            myClass = cclass;
            myProject = cproject;

            Init();
        }

        public void Init() {
            Console.WriteLine("Init");
            Domain();
            RepositoryInterface();
            EntityConfiguration();
            Repository();
            Console.WriteLine("End");
        }

        private void Domain()
        {

            string src = @"\Core\Domain";
            
            string path = CreateFolder(src);
            path = @$"{path}\{myClass}.cs";
            if (!File.Exists(path))
            {

                myNamespace = setNameSpace("Core.Domain");

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("using MyGrade.Core.Domain.Base;");
                    sw.WriteLine($"namespace {myNamespace}");
                    sw.WriteLine("{");
                    sw.WriteLine($"\tpublic class {myClass} : BaseName");
                    sw.WriteLine("\t{");
                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                }
            }
            Console.WriteLine($"{myClass} Domain created");
        }

        private void RepositoryInterface()
        {

            string src = @"\Core\Repositories";

            string path = CreateFolder(src);
            path = @$"{path}\I{myClass}Repository.cs";
            if (!File.Exists(path))
            {

                myNamespace = setNameSpace("Core.Repositories");

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine($"using {setNameSpace("Core.Domain")};");
                    sw.WriteLine($"namespace {myNamespace}");
                    sw.WriteLine("{");
                    sw.WriteLine($"\tpublic interface I{myClass}Repository : IRepository<{myClass}>");
                    sw.WriteLine("\t{");
                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                }
            }
            Console.WriteLine($"{myClass} RepositoryInterface created");
        }

        private void EntityConfiguration()
        {

            string src = @"\Persistence\EntityConfigurations";

            string path = CreateFolder(src);
            path = @$"{path}\{myClass}Configuration.cs";
            if (!File.Exists(path))
            {

                myNamespace = setNameSpace("Persistence.EntityConfigurations");

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine($"using {setNameSpace("Core.Domain")};");
                    sw.WriteLine($"using System.Data.Entity.ModelConfiguration;");
                    sw.WriteLine($"namespace {myNamespace}");
                    sw.WriteLine("{");
                    sw.WriteLine($"\tpublic class {myClass}Configuration : EntityTypeConfiguration<{myClass}>");
                    sw.WriteLine("\t{");
                    sw.WriteLine($"\t\tpublic {myClass}Configuration()");
                    sw.WriteLine("\t\t{");
                    sw.WriteLine("\t\t}");
                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                }
            }
            Console.WriteLine($"{myClass} EntityConfiguration created");
        }

        private void Repository()
        {

            string src = @"\Persistence\Repositories";

            string path = CreateFolder(src);
            path = @$"{path}\{myClass}Repository.cs";
            if (!File.Exists(path))
            {

                myNamespace = setNameSpace("Persistence.Repositories");

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine($"using {setNameSpace("Core.Domain")};");
                    sw.WriteLine($"using {setNameSpace("Core.Repositories")};");
                    sw.WriteLine($"using System.Data.Entity;");
                    sw.WriteLine($"using System.Linq;");
                    sw.WriteLine($"namespace {myNamespace}");
                    sw.WriteLine("{");
                    sw.WriteLine($"\tpublic class {myClass}Repository : Repository<{myClass}>, I{myClass}Repository");
                    sw.WriteLine("\t{");
                    sw.WriteLine($"\t\tpublic {myClass}Repository(PlutoContext context) : base(context)");
                    sw.WriteLine("\t\t{");
                    sw.WriteLine("\t\t}");
                    sw.WriteLine($"\t\tpublic PlutoContext PlutoContext");
                    sw.WriteLine("\t\t{");
                    sw.WriteLine("\t\t\tget { return Context as PlutoContext; }");
                    sw.WriteLine("\t\t}");
                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                }
            }
            Console.WriteLine($"{myClass} Repository created");
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

            return String.IsNullOrEmpty(myProject) ? $"{projectPrefix}.{val}" : $"{projectPrefix}.{val}.{myProject}";
        }

        private string setFolder(string val)
        {
            return String.IsNullOrEmpty(myProject) ? val : @$"{val}\{myProject}";
        }

    }

}
