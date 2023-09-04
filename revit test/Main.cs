using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace revit_test
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //let user selecet the object 
            var obj = uidoc.Selection.PickObjects(ObjectType.PointOnElement);
            foreach ( var element in obj )
            {
              var elle = element.ElementId.ToString();
                TaskDialog.Show("", elle);
            }
            
           


            TaskDialog.Show("element information", "Element Name: " + Environment.NewLine + ele.Name + Environment.NewLine + "element ID" + Environment.NewLine + ele.Id);

            



            //var dialog = new UserControl1(obj);
            //dialog.ShowDialog();


            return Result.Succeeded;



        }
       
    }
}
