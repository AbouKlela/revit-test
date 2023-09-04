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
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using revit_test;

namespace SelectedElementsINFO
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;




            //select the element --- refer----
            //var x = uidoc.Selection.PickObjects(ObjectType.Element);


            // get the element in the code
            //string name2 = "";
            //foreach (var element in x)
            //{
            //    // get element name
            //    var name1 = doc.GetElement(element).Name.ToString();
            //    // get element id
            //    var id = doc.GetElement(element).Id.ToString();
            //    // get category name 
            //    var id2 = doc.GetElement(element).Category.Name.ToString();
            //    // get type id
            //    var typeID = doc.GetElement(element).GetTypeId();
            //    // get type name 
            //    var id4 = doc.GetElement(typeID).Name.ToString();
            //    // get family name
            //    var id5 = doc.GetElement(typeID) as FamilySymbol;
            //    var id6 = id5.Family.Name.ToString();
            //    name2 = name2 + "Instance Name:  " + Environment.NewLine + name1 + Environment.NewLine +
            //        "ID: " + Environment.NewLine + id + Environment.NewLine
            //        + "category:  " + Environment.NewLine + id2 + Environment.NewLine
            //        + "Family Name:  " + Environment.NewLine + id6 + Environment.NewLine
            //        + "Type Name: " + Environment.NewLine +  id4
            //        + Environment.NewLine + "--------------------------" + Environment.NewLine;
            //}


            // show 
            //TaskDialog.Show("Selected Elements INFO.", name2);



            //=========================================================================================
            // collector
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            // filter
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_ElectricalEquipment);
            IList<Element> electrcialeqp = collector.OfClass(typeof(FamilyInstance)).WherePasses(filter).ToElements();

            string print = "Number of electrical equipments = " + electrcialeqp.Count().ToString() + Environment.NewLine;
            foreach(var ele in electrcialeqp)
            {
                var id1= ele.Id;
                var id2 = ele.Name;
                var id3 = ele as FamilySymbol;
                //string id4 = id3.Name.ToString();
                print += id1 + Environment.NewLine + id2 + Environment.NewLine  + Environment.NewLine;




            }

            TaskDialog.Show("info for electrical equipments", print);



            return Result.Succeeded;



        }

    }
}
