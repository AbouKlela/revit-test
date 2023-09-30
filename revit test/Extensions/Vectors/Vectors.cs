using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;

namespace revit_test.Extensions.Vectors
{
    public static class Vectors
    {
        public static void  CreateDirectShape(this Document doc , List<GeometryObject> geometryObjects ,  ElementId CategoryID = null)
        {
            
            CategoryID??= new ElementId(BuiltInCategory.OST_GenericModel);
            DirectShape.CreateElement(doc, CategoryID).SetShape(geometryObjects);
            //                doc.CreateDirectShape(new List<GeometryObject>() { Point.Create(point) });            //

        }

        public static Line VisualizeAsLine (this XYZ Vector , Document doc)
        {
            var line = Line.CreateBound(XYZ.Zero, Vector);
            doc.CreateDirectShape(new List<GeometryObject>() { line});
            return line;
        }
    }
}
