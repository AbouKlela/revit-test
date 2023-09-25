using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.UI.Selection;
using System.Collections.ObjectModel;
using revit_test.Extensions;
using System.IO.Packaging;
using revit_test.Extensions.Selection;
using static revit_test.Extensions.Selection.SelectionExtentions;
using System.Windows.Media.Media3D;

namespace revit_test
{
    /*  _    _      _       
       | |  | |    | |      
       | | _| | ___| | __ _ 
       | |/ | |/ _ | |/ _` |
       |   <| |  __| | (_| |
       |_|\_|_|\___|_|\__,_| */

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class hello : IExternalCommand

    {
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            var ele = uidoc.Selection.PickObjects(ObjectType.Element,
                SelectionFilterFactory.CreateElementSelectionFilter(x=> x.Category.Id == new ElementId(BuiltInCategory.OST_ElectricalFixtures)));
            var ele = uidoc.PickElements(x=> x is FamilyInstance , PickElementsOptionFactory.CreateCurrentDocumentOption());


            var count = ele.Select(x => doc.GetElement(x.ElementId).Id).ToList().Distinct();
            var print = string.Join(", ", count);
            TaskDialog.Show("zby", print);

            //var x = uidoc.PickElements(e => e is FamilyInstance, PickElementsOptionFactory.CreateCurrentDocumentOption());
            //TaskDialog.Show("a", x.Count.ToString());
            return Result.Succeeded;


        }
    }

}