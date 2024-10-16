using ManageGologin.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Runtime_Processor
{
    public class FormRuntimeExecutor
    {
        Assembly assembly;
        public List<string> ChoosedClasses { get; set; }
        public FormRuntimeExecutor() {
            assembly=Assembly.GetExecutingAssembly();
        }
        public void Execute(IWebDriver webDriver, Profiles profiles)
        {
            var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(Executor.InterfaceExecutor.IExecutor)))
                                           .Aggregate(new List<Type>(), (list, type) =>
                                           {
                                               if (ChoosedClasses.Contains(type.Name))
                                               {
                                                   list.Add(type);
                                               }
                                               return list;
                                           })
                ;
            object[] objects = [webDriver, profiles ];
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type,args:objects);
                var method = type.GetMethod("Execute");
                method.Invoke(instance, null);
            }
        }
    }
}
