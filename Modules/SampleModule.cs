using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Modules;
using SitefinitySelfInstallExample.Widgets;

namespace SitefinitySelfInstallExample.Modules
{
    public class SampleModule : ModuleBase
    {
        protected override Telerik.Sitefinity.Configuration.ConfigSection GetModuleConfig()
        {
            return new ConfigSection();
        }

        public override void Install(Telerik.Sitefinity.Abstractions.SiteInitializer initializer)
        {
            // Create the module page and populate it with a widget
            initializer.Installer.CreateModulePage(LandingPageId, moduleName)
                .PlaceUnder(CommonNode.System)
                        .EnableViewState()
                        .SetOrdinal(2)
                        .SetTitle("Sample Module Page")
                        .SetUrlName("SampleModulePage")
                        .ShowInNavigation()
                        .AddControl(new SampleWidget() { LabelValue = "I am a backend widget" })
                        .Done();
        }

        public override Guid LandingPageId
        {
            get { return new Guid(landingPageIdString); }
        }

        public override Type[] Managers
        {
            get
            {
                return new Type[0];
            }
        }

        public override void Upgrade(Telerik.Sitefinity.Abstractions.SiteInitializer initializer, Version upgradeFrom)
        {
            // do nothing at this point. It's just a dummy module
        }

        public static readonly string moduleName = "SampleModule";
        public static readonly string moduleTitle = "Sample Module";
        public static readonly string moduleDescription = "This module was registered automatically";
        private static readonly string landingPageIdString = "277bf391-dc18-4aa2-bbb2-e6e4191647dc";
    }
}
