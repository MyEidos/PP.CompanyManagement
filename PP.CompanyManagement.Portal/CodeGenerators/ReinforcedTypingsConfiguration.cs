using PP.CompanyManagement.Core.Shared.Enums;
using PP.CompanyManagement.Portal.ViewModels.Employee;
using Reinforced.Typings.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PP.CompanyManagement.Portal.CodeGenerators
{
    public static class ReinforcedTypingsConfiguration
    {
        public static void Configure(ConfigurationBuilder builder)
        {
            builder.Global(a => a.CamelCaseForProperties().AutoOptionalProperties().UseModules(discardNamespaces: true).RootNamespace("PP.CompanyManagement"));

            var types = Assembly.GetAssembly(typeof(Startup)).GetTypes();
            var coreTypes = Assembly.GetAssembly(typeof(EmployeeType)).GetTypes();
            var viewModels = types
                .Where(t => t.Namespace.StartsWith("PP.CompanyManagement.Portal.ViewModels"))
                .Where(t=> t.IsClass && !t.IsInterface)
                .Where(t=> !t.CustomAttributes.Any(a => a.AttributeType == typeof(CompilerGeneratedAttribute))) // EX: IValidatableObject.
                .ToArray();

            var enums = coreTypes.Where(IsPublicEnum).ToList();
            enums.AddRange(types.Where(IsPublicEnum));

            builder.ExportAsInterfaces(viewModels, config => config.WithPublicProperties());
            builder.ExportAsEnums(enums);

            bool IsPublicEnum(Type t) => t.IsPublic && t.IsSubclassOf(typeof(Enum));
        }
    }
}
