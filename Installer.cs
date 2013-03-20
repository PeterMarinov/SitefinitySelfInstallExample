using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using SitefinitySelfInstallExample.Widgets;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using SitefinitySelfInstallExample.Modules;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;

namespace SitefinitySelfInstallExample
{
    public class Installer
    {
        /// <summary>
        /// This is the actual method that is called by ASP.NET even before application start. Sweet!
        /// </summary>
        public static void PreApplicationStart()
        {
            // With this method we subscribe for the Sitefinity Bootstrapper_Initialized event, which is fired after initialization of the Sitefinity application
            Bootstrapper.Initialized += (new EventHandler<ExecutedEventArgs>(Installer.Bootstrapper_Initialized));
        }
        
        /// <summary>
        /// With this method we subscribe for the Sitefinity Bootstrapper_Initiali
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName != "RegisterRoutes" || !Bootstrapper.IsDataInitialized)
            {
                return;
            }

            InstallModule(); // See that method for MODULE installation code
            InstallVirtualPaths(); // See that method for VIRTUAL PATHS installation code
            InstallWidget(); // See that method for WIDGET installation code
        }

        
        /// <summary>
        /// Below you will see how modules can be programmatically installed
        /// </summary>
        private static void InstallModule() {
            // define content view control
            var modulesConfig = Config.Get<SystemConfig>().ApplicationModules;
            AppModuleSettings customFieldsSetting = modulesConfig.Elements.Where(el => el.GetKey().Equals(SampleModule.moduleName)).SingleOrDefault();
            if (customFieldsSetting == null)
            {
                AppModuleSettings moduleConfigElement = new AppModuleSettings(modulesConfig)
                {
                    Name = SampleModule.moduleName,
                    Title = SampleModule.moduleTitle,
                    Description = SampleModule.moduleDescription,
                    Type = typeof(SampleModule).AssemblyQualifiedName,
                    StartupType = StartupType.OnApplicationStart
                };

                // Registerign the module
                modulesConfig.Add(SampleModule.moduleName, moduleConfigElement);

                ConfigManager.GetManager().SaveSection(modulesConfig.Section);
                SystemManager.RestartApplication(false);
            }
        }

        /// <summary>
        /// Below you will see how virtual paths can be programmatically installed
        /// </summary>
        private static void InstallVirtualPaths() {
            SiteInitializer initializer = SiteInitializer.GetInitializer();
            var virtualPathConfig = initializer.Context.GetConfig<VirtualPathSettingsConfig>();
            var EventsCalendarViewConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
            {
                VirtualPath = SampleWidget.sampleWidgetVirtualPath + "*",
                ResolverName = "EmbeddedResourceResolver",
                ResourceLocation = typeof(SampleWidget).Assembly.GetName().Name
            };
            if (!virtualPathConfig.VirtualPaths.ContainsKey(SampleWidget.sampleWidgetVirtualPath + "*"))
            {
                virtualPathConfig.VirtualPaths.Add(EventsCalendarViewConfig);
                Config.GetManager().SaveSection(virtualPathConfig);
            }
        }

        /// <summary>
        /// Registering the widget using the fluent API
        /// </summary>
        private static void InstallWidget()
        {
            App.WorkWith().Module(SampleModule.moduleName).Install()
                .PageToolbox()
                    .ContentSection()
                    .LoadOrAddWidget<SampleWidget>("SampleWidget")
                        .SetTitle("Sample Widget")
                        .SetDescription("This widget was added programatically")
                        .SetCssClass("sfCheckoutIcn")
                .Done();
        }
    }
}
