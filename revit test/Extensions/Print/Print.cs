using Autodesk.Revit.UI;
using System;

namespace revit_test.Extensions.Print;

public static class Print
{
    public class print
    {

        public print(string print)
        {
            TaskDialog.Show("ALO HL TSM3NY ?", print);
        }

    }
    public static class td
    {
        public static print z(string print)
        {
            return new print(print);
        }
    }

}