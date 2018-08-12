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
using System.Management;

namespace XenUpdater
{
    class WmiBase
    {
        private static volatile WmiBase instance = null;
        private static object instancelock = new object();

        public static WmiBase Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                        instance = new WmiBase();
                    return instance;
                }
            }
        }
        private WmiBase()
        {
        }

        private object scopelock = new object();
        private ManagementScope scope = null;
        public ManagementScope Scope
        {
            get
            {
                lock (scopelock)
                {
                    if (scope == null)
                    {
                        scope = new ManagementScope("root\\wmi");
                        scope.Connect();
                    }
                    return scope;
                }
            }
        }

        private object xenbaselock = new object();
        private ManagementObject xenbase = null;
        public ManagementObject XenBase
        {
            get
            {
                lock (xenbaselock)
                {
                    if (xenbase == null)
                    {
                        ManagementPath path = new ManagementPath(BrandSupport.XenVersions.BRANDING_vendorPrefix + "XenStoreBase");
                        ManagementClass cls = new ManagementClass(Scope, path, null);
                        ManagementObjectCollection col = cls.GetInstances();
                        xenbase = GetFirst(col);
                    }
                    return xenbase;
                }
            }
        }

        public static ManagementObject GetFirst(ManagementObjectCollection collection)
        {
            if (collection.Count < 1)
                throw new Exception("No objects found");
            foreach (ManagementObject mobj in collection)
                return mobj;
            throw new Exception("No objects found");
        }

        public static bool IsXDNonPersist
        {
            get
            {
                try
                {
                    ManagementScope ms = new ManagementScope(@"Root\Citrix\DesktopInformation");
                    ms.Connect();
                    ManagementPath mp = new ManagementPath("Citrix_VirtualDesktopInfo");
                    ManagementClass mc = new ManagementClass(ms, mp, null);
                    ManagementObjectCollection moc = mc.GetInstances();
                    if (moc.Count < 1)
                        return false;
                    foreach (ManagementObject mobj in moc)
                    {
                        return !((bool)mobj["OSChangesPersist"]);
                    }
                    return false;
                }
                catch
                {
                    // XD not present or XD in persist mode
                    return false;
                }
            }
        }
    }

    class XenStoreSession : IDisposable
    {
        private ManagementObject Session = null;
        private WmiBase Base = null;
        private string Name;

        bool disposed = false;
        void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Session.InvokeMethod("EndSession", null);
                    }
                    catch { }
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~XenStoreSession()
        {
            Dispose(false);
        }

        public XenStoreSession(string name)
        {
            Base = WmiBase.Instance;
            Name = name;

            // call EndSession if this name already exists
            try 
            {
                ObjectQuery obj = new ObjectQuery(String.Format("SELECT * FROM " + BrandSupport.XenVersions.BRANDING_vendorPrefix + "XenStoreSession WHERE Id=\"Citrix Xen Service: {0}\"", name));
                ManagementObjectSearcher mobs = new ManagementObjectSearcher(Base.Scope, obj); ;
                Session = WmiBase.GetFirst(mobs.Get());
                Session.InvokeMethod("EndSession", null);
            }
            catch
            {
            }

            // create this session
            ManagementBaseObject input = Base.XenBase.GetMethodParameters("AddSession");
            input["ID"] = String.Format("Citrix Xen Service: {0}", name);
            ManagementBaseObject output = Base.XenBase.InvokeMethod("AddSession", input, null);
            UInt32 sessionid = (UInt32)output["SessionId"];
            ObjectQuery query = new ObjectQuery("SELECT * from " + BrandSupport.XenVersions.BRANDING_vendorPrefix + "XenStoreSession WHERE SessionId=" + sessionid.ToString());
            ManagementObjectSearcher objects = new ManagementObjectSearcher(Base.Scope, query);
            Session = WmiBase.GetFirst(objects.Get());
        }

        public string GetValue(string path)
        {
            ManagementBaseObject input = Session.GetMethodParameters("GetValue");
            input["PathName"] = path;
            ManagementBaseObject output = Session.InvokeMethod("GetValue", input, null);
            return (String)output["value"];
        }

        public void SetValue(string path, string value)
        {
            ManagementBaseObject input = Session.GetMethodParameters("SetValue");
            input["PathName"] = path;
            input["value"] = value;
            ManagementBaseObject outparam = Session.InvokeMethod("SetValue", input, null);
        }

        public void RemoveValue(string path)
        {
            ManagementBaseObject input = Session.GetMethodParameters("RemoveValue");
            input["PathName"] = path;
            ManagementBaseObject output = Session.InvokeMethod("RemoveValue", input, null);
        }

        public void Log(string message)
        {
            string[] lines = message.Split('\n');
            foreach (string line in lines)
            {
                System.Diagnostics.Debug.Print("AutoUpdate: " + line + "\n");
                try
                {
                    ManagementBaseObject input = Session.GetMethodParameters("Log");
                    input["Message"] = "AutoUpdate: " + line;
                    ManagementBaseObject output = Session.InvokeMethod("Log", input, null);
                }
                catch
                {
                }
            }
        }
    }

    public interface IXenStoreItemFactory
    {
        ACXenStoreItem newXenStoreItem(string StoreLocation);
        void Log(string message);
    }

    class XenStoreItemFactory : IXenStoreItemFactory
    {
        private XenStoreSession session;

        public XenStoreItemFactory(string SessionName)
        {
            session = new XenStoreSession(SessionName);
        }

        public ACXenStoreItem newXenStoreItem(string StoreLocation)
        {
            return new XenStoreItem(session, StoreLocation);
        }
        
        public void Log(string message) 
        {
            session.Log(message);
        }
    }

    public abstract class ACXenStoreItem
    {
        abstract public bool Exists 
        { 
            get; 
        }
        
        abstract public string Value 
        {
            get;
            set;
        }
        
        public abstract string ValueOrDefault(string def);
        
        abstract public string Path 
        { 
            get; 
        }
        
        public abstract void Remove();
    }

    class XenStoreItem : ACXenStoreItem
    {
        string _path;
        public XenStoreItem(XenStoreSession session, string path)
        {
            _path = path;
            Session = session;
        }

        override public bool Exists
        {
            get
            {
                try
                {
                    // GetValue returns null if key is not present,
                    // empty string ig key is present but empty, or
                    // the value of the string
                    return Session.GetValue(Path) != null;
                }
                catch
                {
                    return false;
                }
            }
        }

        public override string Value
        {
            get
            {
                return Session.GetValue(Path);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "";
                Session.SetValue(Path, value);
            }
        }

        override public string ValueOrDefault(string def)
        {
            try
            {
                return Session.GetValue(Path);
            }
            catch
            {
                return def;
            }
        }

        override public string Path 
        { 
            get
            {
                return _path;
            }
        }

        override public void Remove()
        {
            Session.RemoveValue(Path);
        }

        private XenStoreSession Session;
    }
}
