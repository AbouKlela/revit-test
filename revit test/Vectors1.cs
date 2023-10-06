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
            var firstvector = new XYZ(2, 0.5, 0);
            var secondvector = new XYZ(1,2,0); 
            var basisZ = XYZ.BasisZ;



            using (var tr = new Transaction(doc, "zby"))
            {
                tr.Start();
                firstvector.AsCurve().Visualize(doc);
                XYZ.BasisZ.AsCurve().Visualize(doc);
                secondvector.AsCurve().Visualize(doc);
                




                tr.Commit();
            }








            return Result.Succeeded;


        }


    }



}