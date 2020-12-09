using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Venflow.Generators
{
    public class Test : Random
    {
        public Random MyProperty { get; }
        public int MyProperty2 { get; set; }
    }

    [Generator]
    public class DatabaseInstantiaterGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }

            var codeBuilder = new StringBuilder();

            codeBuilder.AppendLine("using System;");
            codeBuilder.AppendLine("namespace Venflow.Dynamic");
            codeBuilder.AppendLine("{");

            foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                var rootNode = syntaxTree.GetCompilationUnitRoot();

                var model = context.Compilation.GetSemanticModel(syntaxTree);

                foreach (var decendant in rootNode.DescendantNodes()
                                                  .OfType<ClassDeclarationSyntax>())
                {
                    var classType = model.GetTypeInfo(decendant);

                    if (!classType.HasBaseType("Venflow.Database"))
                        continue;

                    var properties = decendant.ChildNodes()
                                              .OfType<PropertyDeclarationSyntax>()
                                              .Where(x => x.Modifiers.Any(x => x.Value == "public"))
                                              .ToArray();

                    if (properties.Length > 0)
                    {
                        codeBuilder.Append("internal static class Instantiater");
                        codeBuilder.AppendLine("{");
                        codeBuilder.AppendLine($"internal static void {(classType.Type.ContainingNamespace + classType.Type.Name).Replace(".", string.Empty)}()");
                    }

                    foreach (var property in properties)
                    {
                        var identifierName = property.ChildNodes()
                                                     .OfType<IdentifierNameSyntax>()
                                                     .FirstOrDefault();

                        if (identifierName is null ||
                            !model.GetTypeInfo(identifierName).HasBaseType("Venflow.ITable"))
                            continue;
                    }
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }

    public static class TypeInfoExtensions
    {
        public static bool HasBaseType(this TypeInfo typeInfo, string fullName)
        {
            var baseType = typeInfo.Type.BaseType;

            while (baseType is not null)
            {
                if (baseType is null)
                {
                    return false;
                }

                if (baseType.ContainingNamespace + "." + baseType.Name == fullName)
                {
                    return true;
                }

                baseType = baseTypeInfo.Type.BaseType;
            }

            return false;
        }
    }
}
