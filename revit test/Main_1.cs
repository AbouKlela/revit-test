using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using revit_test;

namespace revit_test
{
    [Transaction(TransactionMode.Manual)]
    internal class Main_1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
                Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
                Document doc = uidoc.Document;

            var selectele = uidoc.Selection.PickObjects(ObjectType.Element).Select(x => doc.GetElement(x)).Select(x=> x.Id).ToList();
            FilteredElementCollector collector = new FilteredElementCollector(doc,selectele);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_ElectricalEquipment);
                  
            var _1 = collector.OfClass(typeof(FamilyInstance)).WherePasses(filter).ToElements().ToList();

            string _2 = _1.Select(x => x.Name).Aggregate((a, b) => a + Environment.NewLine + b);
            TaskDialog.Show("ayklam", _2);
                return Result.Succeeded;
            }
            catch (Exception m)
            {
                TaskDialog.Show("Error", m.Message);
                return Result.Failed;
            }





        }
    }
}
