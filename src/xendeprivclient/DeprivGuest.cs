﻿/* Copyright (c) Citrix Systems Inc.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, 
 * with or without modification, are permitted provided 
 * that the following conditions are met:
 *
 * *   Redistributions of source code must retain the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer.
 * *   Redistributions in binary form must reproduce the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer in the documentation and/or other 
 *     materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
 * SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using XenGuestLib;


[assembly: AssemblyVersion(BrandSupport.XenVersions.Version)]
[assembly: AssemblyFileVersion(BrandSupport.XenVersions.Version)]
[assembly: AssemblyCompanyAttribute(BrandSupport.XenVersions.BRANDING_manufacturerLong)]
[assembly: AssemblyProductAttribute(BrandSupport.XenVersions.BRANDING_toolsForVMs)]
[assembly: AssemblyDescriptionAttribute(BrandSupport.XenVersions.BRANDING_xenDprivDesc)]
[assembly: AssemblyTitleAttribute(BrandSupport.XenVersions.BRANDING_xenDprivDesc)]
[assembly: AssemblyCopyrightAttribute(BrandSupport.XenVersions.BRANDING_copyrightXenDpriv)]

namespace svc_depriv
{
    
    public class ClipboardChangedEventArgs : EventArgs
    {
        public readonly IDataObject DataObject;

        public ClipboardChangedEventArgs(IDataObject dataObject)
        {
            DataObject = dataObject;
        }
    }

    static class DeprivGuest
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            Application.ThreadException += new ThreadExceptionEventHandler(MyCommonExceptionHandlingMethod);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);




            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length >= 1)
            {
                try
                {
                    DummyForm f1 = new DummyForm(args[0]);
                    Application.Run(f1);
                }
                catch 
                {
                    Application.Exit();
                    return;
                }
                
            }
            else
            {
                Application.Exit();
            }

        }

        private static void MyCommonExceptionHandlingMethod(object sender, ThreadExceptionEventArgs t)
        {
            Trace.WriteLine("Depriv exception of type "+ t.Exception.ToString());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Trace.WriteLine("Depriv unhandled exception of type " + ((Exception)e.ExceptionObject).ToString());
        }

    }
}
