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
        /// <summary>
        /// Create an Object To Create a Direct Shape and Visualize it as any Geometry Object
        /// </summary>
        /// <param Current Document="doc"></param>
        /// <param The Geometry Object you wanna Visualize="geometryObjects"></param>
        /// <param Optional param for the Category of the created Object="CategoryID"></param>
        ///                                             Call                                                   
        ///               doc.CreateDirectShape(new List<GeometryObject>() { Point.Create(point) });            
        public static void CreateDirectShape(this Document doc, List<GeometryObject> geometryObjects, ElementId CategoryID = null)
        {

            CategoryID ??= new ElementId(BuiltInCategory.OST_GenericModel);
            DirectShape.CreateElement(doc, CategoryID).SetShape(geometryObjects);

        }
        /// <summary>
        /// Visualize Vector As a Line from Centre Point Of The project
        /// </summary>
        /// <param Vector You Wanna Visualize ="Vector"></param>
        /// <param Current Document="doc"></param>
        ///                                             Call                                                    
        ///                                  Vector.VisualizeAsLine(doc)                                        
        public static void VisualizeAsLine(this XYZ Vector, Document doc)
        {
            var line = Line.CreateBound(XYZ.Zero, Vector);
            doc.CreateDirectShape(new List<GeometryObject>() { line });


        }
    }
}
