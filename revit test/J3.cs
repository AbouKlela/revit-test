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
    internal class J3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            ICollection<string> print2 = new List<string>();
            var se = uidoc.Selection.PickObjects(ObjectType.LinkedElement);
            foreach ( var x in se )
            {
                var e = doc.GetElement(x.ElementId) as RevitLinkInstance;
                var LinkedDoc =e.GetLinkDocument();
                var linkedEle = LinkedDoc.GetElement(x.LinkedElementId);
                var r = linkedEle.Name;
                var r1 = linkedEle.Id;
                var r2 = linkedEle.Category.Name;

                print2.Add("Element Name: "+r + Environment.NewLine +
                    "Element Catregory : " +  r2 + Environment.NewLine +
                    "Element ID: "  + r1 + Environment.NewLine);
               
            }
            string collectionAsString = string.Join(Environment.NewLine, print2);







            //kp atb3 = new kp();
            //atb3.ShowDialog();

            return Result.Succeeded;
        }
    }

}