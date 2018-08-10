using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Diagnostics;

namespace BrandSupport
{
    public static class BrandingControl
    {
        public static string getString(string key)
        {
            try
            {
                XenVersions xv = new XenVersions();
                Type brandinstrings = new XenVersions().GetType();
                FieldInfo bla = brandinstrings.GetField(key);
                string value = (string)bla.GetValue(xv);

                return value;
            }
            catch (Exception)
            {
                return "Unknown Branding " + key;
            }
        }
    }
}
