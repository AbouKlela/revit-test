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
    internal partial class VisualizeGeo : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<Element> ElementList = uidoc.Selection.GetElementIds().Select(x => doc.GetElement(x)).ToList();
            //List<Element> ElementList = uidoc.PickElements(x => x is FamilyInstance, PickElementsOptionFactory.bothDocumentOption());
            var options = new Options() { ComputeReferences = false, DetailLevel = ViewDetailLevel.Fine };





            using (var tr = new Transaction(doc, "zby"))
            {
                tr.Start();
                foreach (Element element in ElementList)
                {
                    var geo1 = element.get_Geometry(options).Where(g => g is Solid);
                    if (geo1.Count() != 0)
                    {
                        List<Solid> geo2 = geo1.Select(geo1 => geo1 as Solid).Where(x => x.Volume > 0).ToList();
                        //td.z(geo2.Count().ToString() + Environment.NewLine + "Geometry Element");
                        foreach (Solid x in geo2)
                        {
                            x.Visualize(doc);
                        }
                    }
                    else
                    {
                        List<Solid> geo3 = element.get_Geometry(options).
                            Select(g => g as GeometryInstance).
                            SelectMany(x => x.GetInstanceGeometry()).
                            Select(y => y as Solid).Where(x => x.Volume > 0).ToList();
                        //td.z(geo3.Count().ToString() + Environment.NewLine + "Geometry Instance");
                        foreach (Solid x in geo3)
                        {
                            x.Visualize(doc);
                        }
                    }

                }





                tr.Commit();
            }


            return Result.Succeeded;


        }


    }



}