using System;
using System.Collections;
using System.Collections.Generic;
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
    internal class conduit : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIApplication uiapp = commandData.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Application app = uiapp.Application;
                Document doc = uidoc.Document;
                var selectele = uidoc.Selection.PickObjects(ObjectType.Element);
                IList<Element> ele = new List<Element>();
                foreach (var heloo in selectele)
                {
                    ele.Append(doc.GetElement(heloo));
                }
                IList<Parameter> param = new List<Parameter>();
                foreach (var x in ele)
                {
                    param.Append(x.LookupParameter("Elevation From Level"));
                }
                using (Transaction trans = new Transaction(doc, "edit parameter"))
                {
                    trans.Start();
                    foreach (var x in param)
                    {
                        try
                        {
                            x.Set(1250 / 304.8);
                        }
                        catch (Exception m )
                        {

                            TaskDialog.Show("zby",m.Message);
                            return Result.Failed;
                        }
                        
                    }
                    trans.Commit(); 
                 
                }
                    return Result.Succeeded;

            }


            catch (Exception m)
            {
                TaskDialog.Show("Error", m.Message);
                return Result.Failed;
            }





        }
    }

}