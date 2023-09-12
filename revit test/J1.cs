using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using revit_test;

namespace revit_test
{
    [Transaction(TransactionMode.Manual)]
    internal class J1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            var multicategoryfilter = new ElementMulticategoryFilter(new List<BuiltInCategory> { BuiltInCategory.OST_Conduit ,BuiltInCategory.OST_ConduitFitting});
            var multiclassfilter = new ElementMulticlassFilter(new List<Type>() { typeof(Conduit) , typeof(FamilyInstance)});
            var collector = new FilteredElementCollector(doc).WherePasses(multiclassfilter).WherePasses(multicategoryfilter).WhereElementIsNotElementType().ToElements().ToList();
            var eleid = collector.Select(x=> x.Id).ToList();
            var print = collector.Select(x=> x.Id.ToString()).ToList();
            uidoc.Selection.SetElementIds(eleid);



            
            kp atb3 = new kp(print);
            atb3.ShowDialog();

            return Result.Succeeded;


        }
    }

}