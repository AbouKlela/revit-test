using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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
    internal class J2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            using (var tr = new Transaction(doc, "ElementSelectionFilter"))
            {
            var ele = uidoc.Selection.GetElementIds().Select(x => doc.GetElement(x));
            tr.Start();
            var param = ele.Select(x => x.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS));
            foreach(var x in param)
                {
                    x.Set("KLELA");
                }
            tr.Commit();
            }

           



            //kp atb3 = new kp(param);
            //atb3.ShowDialog();

            return Result.Succeeded;
        }
    }

}