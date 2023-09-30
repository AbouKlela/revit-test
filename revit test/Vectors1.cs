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

namespace revit_test
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal partial class Vectors1 : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            var point = new XYZ(4.544696601, 11.973907868, 0.000000000);
            var element = uidoc.PickElements(x => x is FamilyInstance, PickElementsOptionFactory.CreateCurrentDocumentOption());
            var elePoint = element.First().Location as LocationPoint;
            var ele = elePoint.Point;
            var TotalVector = ele + point;




            using (var tr = new Transaction(doc, "zby"))
            {
                tr.Start();

                ElementTransformUtils.CopyElement(doc, element.First().Id, TotalVector);
                var line = point.VisualizeAsLine(doc);
                var line2 = ele.VisualizeAsLine(doc);
                var line3 = TotalVector.VisualizeAsLine(doc);


                tr.Commit();
            }








            return Result.Succeeded;


        }


    }



}