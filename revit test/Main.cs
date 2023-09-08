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
            // filtered selected elements// 

            IList<Reference> pickedelement = uidoc.Selection.PickObjects(ObjectType.Element);
            ICollection<ElementId> collector = new List<ElementId>();
            foreach (var ay7aga in pickedelement)
            {
                ElementId ele = doc.GetElement(ay7aga).Id;
                collector.Add(ele);
            }
            FilteredElementCollector collector2 = new FilteredElementCollector(doc, collector);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_ElectricalEquipment);

            IList<Element> element = collector2.OfClass(typeof(FamilyInstance)).WherePasses(filter).ToElements();
            string print = null;
            foreach (Element elementItem in element)
            {

                string _1 = elementItem.Name;
                string _2 = elementItem.Id.ToString();
                print += _1 + Environment.NewLine + _2 + Environment.NewLine;


            }

            TaskDialog.Show("info for electrical equipments", print);
            //=========================================================================================
            //code for making all the coloumns top offset is to a certian level and its matrtial is wood//




            return Result.Succeeded;



        }

    }
}
