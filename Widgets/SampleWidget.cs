using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace SitefinitySelfInstallExample.Widgets
{
    public class SampleWidget : SimpleView
    {
        protected override void InitializeControls(GenericContainer container)
        {
            this.WidgetLabel.Text = this.labelValue;
        }

        public override string LayoutTemplatePath
        {
            get
            {
                return sampleWidgetTempaltePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        public string LabelValue
        {
            get
            {
                return this.labelValue;
            }
            set
            {
                this.labelValue = value;
            }
        }

        public Label WidgetLabel
        {
            get
            {
                return this.Container.GetControl<Label>("widgetLabel", true);
            }
        }

        private string labelValue = "I am a text from the widget";
        public static readonly string sampleWidgetVirtualPath = "~/SampleWidget/";
        private readonly string sampleWidgetTempaltePath = sampleWidgetVirtualPath + "SitefinitySelfInstallExample.Widgets.SampleWidgetTemplate.ascx";
    }
}
