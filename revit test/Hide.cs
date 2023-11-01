using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static revit_test.Extensions.Selection.SelectionExtentions;
using static revit_test.Extensions.Print.Print;
using static revit_test.Extensions.Vectors.Vectors;
using revit_test.Extensions.Vectors;
using revit_test.Extensions.Geometry;
using System;

namespace revit_test
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal partial class Hide : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Category roomseparationline = Category.GetCategory(doc, BuiltInCategory.OST_RoomSeparationLines);





            using (Transaction transaction = new Transaction(doc, "Turn Off Visibility of Room Separation Line"))
            {
                transaction.Start();

                doc.ActiveView.SetCategoryHidden(roomseparationline.Id, true);

                transaction.Commit();
            }

            uidoc.RefreshActiveView();

            return Result.Succeeded;






        }


    }



}