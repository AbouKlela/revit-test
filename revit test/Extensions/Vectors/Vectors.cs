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
        /// To Vizualize Point XYZ IN doc
        /// </summary>
        /// <param POINT TO VISUALIZE="point"></param>
        /// <param DOC TO VISUALIZE IN ="doc"></param>
        public static void Visualize(this XYZ point , Document doc)
        {
            doc.CreateDirectShape(new List<GeometryObject>() { Point.Create(point) });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="origin"></param>
        /// <param name="legnth"></param>
        /// <returns></returns>
        public static Curve AsCurve(this XYZ vector , XYZ origin = null , double? legnth = null) {
            origin??=XYZ.Zero;
            legnth ??= vector.GetLength();
            return Line.CreateBound(origin, origin.MoveAlongVector(vector.Normalize(), legnth.GetValueOrDefault()));
        
        }
        public static void Visualize(this Curve curve , Document document)
        {
            document.CreateDirectShape(new List<GeometryObject> { curve });
        }
        public static XYZ MoveAlongVector(this XYZ pointToMove, XYZ vector, double distance) => pointToMove.Add(vector * distance);
        public static XYZ MoveAlongVector(this XYZ pointToMove, XYZ vector) => pointToMove.Add(vector);
        public static XYZ ToNormalizedVector(this Curve curve)
        {
            return (curve.GetEndPoint(1)- curve.GetEndPoint(0).Normalize());
        }

      
    }
}

