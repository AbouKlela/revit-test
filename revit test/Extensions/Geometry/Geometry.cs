using Autodesk.Revit.DB;
using revit_test.Extensions.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace revit_test.Extensions.Geometry
{
    public static class Geometry
    {
        public static void Visualize(this Solid solid, Document doc)
        {
             doc.CreateDirectShape(new List<GeometryObject> { solid});

        }
    }
}
