using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.UI.Selection;

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
            /*
                      _____                    _____                    _____                    _____          
                     /\    \                  /\    \                  /\    \                  /\    \         
                    /::\    \                /::\    \                /::\    \                /::\____\        
                    \:::\    \              /::::\    \              /::::\    \              /:::/    /        
                     \:::\    \            /::::::\    \            /::::::\    \            /:::/    /         
                      \:::\    \          /:::/\:::\    \          /:::/\:::\    \          /:::/    /          
                       \:::\    \        /:::/__\:::\    \        /:::/__\:::\    \        /:::/____/           
                       /::::\    \      /::::\   \:::\    \       \:::\   \:::\    \      /::::\    \           
                      /::::::\    \    /::::::\   \:::\    \    ___\:::\   \:::\    \    /::::::\    \   _____  
                     /:::/\:::\    \  /:::/\:::\   \:::\____\  /\   \:::\   \:::\    \  /:::/\:::\    \ /\    \ 
                    /:::/  \:::\____\/:::/  \:::\   \:::|    |/::\   \:::\   \:::\____\/:::/  \:::\    /::\____\
                   /:::/    \::/    /\::/   |::::\  /:::|____|\:::\   \:::\   \::/    /\::/    \:::\  /:::/    /
                  /:::/    / \/____/  \/____|:::::\/:::/    /  \:::\   \:::\   \/____/  \/____/ \:::\/:::/    / 
                 /:::/    /                 |:::::::::/    /    \:::\   \:::\    \               \::::::/    /  
                /:::/    /                  |::|\::::/    /      \:::\   \:::\____\               \::::/    /   
                \::/    /                   |::| \::/____/        \:::\  /:::/    /               /:::/    /    
                 \/____/                    |::|  ~|               \:::\/:::/    /               /:::/    /     
                                            |::|   |                \::::::/    /               /:::/    /      
                                            \::|   |                 \::::/    /               /:::/    /       
                                             \:|   |                  \::/    /                \::/    /        
                                              \|___|                   \/____/                  \/____/         
                                                                                                
             */
            // asssign category filter
            var multicategoryfilter = new ElementMulticategoryFilter(new List<BuiltInCategory> { BuiltInCategory.OST_Lines});
            // assign class filter
            //var multiclassfilter = new ElementMulticlassFilter(new List<Type>() { typeof(DetailLine) });
            //logical and filter
            //var logicalandfilter = new LogicalAndFilter(multiclassfilter, multicategoryfilter);
            // collect the elements 
            var collector = new FilteredElementCollector(doc).WherePasses(multicategoryfilter).WhereElementIsNotElementType().ToElements().ToList();
            // get elements id 
            var eleid = collector.Select(x => x.Id).ToList();
            //select the elements in the doc 
            uidoc.Selection.SetElementIds(eleid);
            //print
            var print = collector.Select(x => x.Id.ToString()).ToList();
            //kp atb3 = new kp(print);
            //atb3.ShowDialog();

            return Result.Succeeded;
        }
    }

}