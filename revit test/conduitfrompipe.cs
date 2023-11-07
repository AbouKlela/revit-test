using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using revit_test.Extensions.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using revit_test.Extensions.Print;
using Autodesk.Revit.DB.Electrical;

namespace revit_test
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal partial class conduitfrompipe : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Autodesk.Revit.DB.Document doc = uidoc.Document;

            var pipe = uidoc.GetSelectedElement().Where(x=>x is Pipe);
            var off = pipe.Select(x => x as Pipe).Select(y => (y.Diameter/2)).Select(z=>z+50/304.8);
            var loccur = pipe.Select(x => x.Location as LocationCurve);
            XYZ shift = new XYZ(off.First(), off.First(), off.First());
            var multicategoryfilter = new ElementMulticategoryFilter(new List<BuiltInCategory> { BuiltInCategory.OST_Conduit });
            var conduitype = new FilteredElementCollector(doc).WherePasses(multicategoryfilter)
                .Where(y => y is ConduitType).First().Id;
            foreach (var curve in loccur)
            {
                XYZ start = curve.Curve.GetEndPoint(0) + shift;
                XYZ end = curve.Curve.GetEndPoint(1) + shift;

                using (Transaction tr = new Transaction(doc, "codnuit"))
                {
                    tr.Start();
                    var conduit = Conduit.Create(doc, conduitype, start, end, new ElementId(-1));
                    var connectorset = conduit.ConnectorManager.Connectors;
                    Connector con1 = null;
                    Connector con2 = null;
                    List<Connector> conlist = new List<Connector>();
                    foreach (Connector connector in connectorset)
                    {
                        conlist.Add(connector);
                    }
                    con1 = conlist[0];
                    con2 = conlist[1];
                    Print.td.z(conlist.Count().ToString());
                    tr.Commit();
                    tr.Dispose();

                }
            }





            return Result.Succeeded;


        }


    }
}
