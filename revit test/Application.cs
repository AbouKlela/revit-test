using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;
using System.Drawing;

namespace revit_test
{
    internal class Application : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = RibbonPanel(application);
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            if (panel.AddItem(new PushButtonData("First","First",thisAssemblyPath,"KLELA")) is  PushButton button)
            {
                button.ToolTip = "MOHAMED ABOUKLELA";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "geometry.ico"));
                BitmapImage bitmap = new BitmapImage(); 
                button.LargeImage = bitmap;
            }
            return Result.Succeeded;
        }

        public RibbonPanel RibbonPanel(UIControlledApplication application)
        {
            string tab = "KLELA";
            RibbonPanel ribbonPanel = null;
            try
            {
                application.CreateRibbonPanel(tab);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            try
            {
                application.CreateRibbonPanel(tab, "KLELA");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            List<RibbonPanel> panels = application.GetRibbonPanels(tab);
            foreach (RibbonPanel panel in panels)
            {
                ribbonPanel= panel;
            }
            return ribbonPanel;
        }
    }
}
