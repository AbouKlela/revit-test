using Autodesk.Revit.DB;
using revit_test.Extensions.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace revit_test.Extensions
{
    public static class VisualizeTrans
    {
        public static void Visualize(this Transform transform, Document doc, double scale = 3)
        {
            var colors = new List<Color>
            {
                new Color(255,0,0), new Color(0,128,0), new Color(70,65,240)
            };

            var colorToLine = Enumerable.Range(0, 3).Select(x => transform.get_Basis(x)).
                Select(y => Line.CreateBound(transform.Origin, transform.Origin + y * 3)).
                Zip(colors, (x, z) => (Line: x, Color: z)).ToList();
            foreach (var (line,color) in colorToLine)
            {
                var aa = doc.CreateDirectShape_r(new List<GeometryObject> { line});
                var overridegrpahics = new OverrideGraphicSettings();
                overridegrpahics.SetProjectionLineColor(color);
                overridegrpahics.SetProjectionLineWeight(4);
                doc.ActiveView.SetElementOverrides(aa.Id, overridegrpahics);

            }

        }
    }
}
