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
using static revit_test.Extensions.Print.Print;
using Autodesk.Revit.DB.Electrical;
using revit_test.Extensions.Vectors;

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
            // select the pipes
            var pipe = uidoc.GetSelectedElement().Where(x => x is Pipe);
            // get the offset
            var off = pipe.Select(x => x as Pipe).Select(y => (y.Diameter / 2)).Select(z => z + 50 / 304.8);
            // get the location curve
            var loccur = pipe.Select(x => x.Location as LocationCurve);
            // the shift
            XYZ shift = new XYZ(off.First(), off.First(), off.First());
            // get conduit type category
            var multicategoryfilter = new ElementMulticategoryFilter(new List<BuiltInCategory> { BuiltInCategory.OST_Conduit });
            var conduitype = new FilteredElementCollector(doc).WherePasses(multicategoryfilter)
                .Where(y => y is ConduitType).First().Id;

            List<Connector> conlist = new List<Connector>();

            using (Transaction tr = new Transaction(doc, "codnuit"))
            {
                tr.Start();
                foreach (var curve in loccur)
                {
                    XYZ start = curve.Curve.GetEndPoint(0) + shift;
                    XYZ end = curve.Curve.GetEndPoint(1) + shift;

                    var conduit = Conduit.Create(doc, conduitype, start, end, new ElementId(-1));
                    var connectorset = conduit.ConnectorManager.Connectors;
                    foreach (Connector connector in connectorset)
                    {
                        conlist.Add(connector);
                    }


                }
                tr.Commit();
            }
            List<Connector> PipeConectors = new List<Connector>();
            var PipesConnectors = pipe.Select(x => x as Pipe).Select(y => y.ConnectorManager.Connectors);
            foreach(ConnectorSet pipeconSet in PipesConnectors)
            {
                foreach(Connector pipecon in pipeconSet)
                {
                    PipeConectors.Add(pipecon);
                }

            }
            var PipeConNot = PipeConectors.Where(x => x.IsConnected == false);
            //td.z(PipeConNot.Count().ToString()+Environment.NewLine+"NotConnectedPoints");
            var conlistORD = conlist.OrderByDescending(x => (x.Origin.DistanceTo(PipeConNot.First().Origin))).ToList();
            //td.z(conlistORD.Count().ToString()+Environment.NewLine + "Points Are Ordered");

            using (Transaction tr = new Transaction(doc, "fitting"))
            {
                tr.Start();
                for (int i = 1; i < conlistORD.Count() - 2; i += 2)
                {
                    try
                    {
                        doc.Create.NewElbowFitting(conlistORD[i], conlistORD[i + 1]);
                    }
                    catch (Exception ex)
                    {
                        td.z(ex.ToString());
                        continue;
                    }
                }
                doc.Regenerate();
                tr.Commit();
                tr.Dispose();

            }



            return Result.Succeeded;


        }


    }
}
