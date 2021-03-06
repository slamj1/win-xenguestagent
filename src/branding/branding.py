
branding = {
        "toolsName" : "XCP-ng Tools",
        "installerProductName" : "XCP-ng Tools Installer",
        "manufacturer" : "XCP-ng",
        "installerKeyWords" : "XCP-ng Windows Installer",
        "shortTools" : "XenTools",
        "installerServiceName" : "Xen Installer",
        "shortInstallerServiceName": "XenPVInstall",
        "installWizardName" : "Xen Install Wizard",
        "pvtoolsLong" : "XCP-ng Windows PV Tools",
        "pvTools" : "Xen PV Tools",
        "hypervisorAndOs" : "Xen Windows",
        "pvDrivers" : "PV Drivers",
        "driverKeyWords" : "Xen Drivers",
        "driverDescription" : "Xen Windows Drivers",
        "driverComments" : "Paravitualized Windows Drivers For XCP-ng",
        "pvDriversLong" : "Xen PV Drivers for Windows",
        "hypervisor" : "Xen",
        "hypervisorProduct" : "XCP-ng",
        "guestAgentLong" : "XCP-ng Windows Guest Agent",
        "guestAgent" : "Xen Guest Agent",
        "guestServiceShort" : "XenSvc",
        "guestServiceDesc" : "Monitors and provides various metrics to XenStore",
        "vssLong" : "XCP-ng VSS Provider",
        "managementName" : "Windows Management Agent",
        "managementDesc" : "Installation and Update Agent",
        "installAgentShort" : "InstallAgent",
        "installAgentDesc" : "Installs and updates management agent",
        "installAgentRegKey" : R"SOFTWARE\\XCP-ng\\InstallAgent",
        "language" : "1033",
        "languages" : "1033",
        "manufacturerLong" : "XCP-ng",
        "toolsForVMs" : "Tools For Virtual Machines",
        "guestLibDesc" : "Xen Windows Guest Agent Support Library",
        "copyrightGuestLib" : "XCP-ng 2018",
        "copyrightGuestAgent" : "XCP-ng 2018",
        "copyrightXenDpriv" : "XCP-ng 2018",
        "xenDprivDesc" : "XCP-ng Windows Deprivileged Client",

        "setComputerName" : "Set Computer Name",
        "errNoWMI" : "XCP-ng guest Agent cannotfix XenIface WMI interface",
        "GuestAgentLogName" : "XenGuestAgentLog",
        "GuestAgentLogSource" : "XenGuestAgent",
        "setupErr" : "XCP-ng Setup.exe error",
        "processFail" : "Failed to create process %s %x", #commandline #windows error code
        "setupHelp" : "Valid arguments are:\\n /TEST\\n/passive\\n/quiet\\n/norestart\\n/forcerestart",
        "noSystemDir" : "Unable to read system directory",
        "setupLogDir" : "XSToolSetup",
        "copyrightInstallAgent" : "XCP-ng 2018",
        "copyrightBrandSupport" : "XCP-ng 2018",
        "copyrightHelperFunctions" : "XCP-ng 2018",
        "copyrightHardwareDevice" : "XCP-ng 2018",
        "copyrightPInvokeWrap" : "XCP-ng 2018",
        "copyrightPVDriversRemoval" : "XCP-ng 2018",
        "copyrightUninstall" : "XCP-ng 2018",
        "errMSINoMem":"Insufficient memory to allocate msiexec string",
        "errFilePathNoMem":"Insufficient memory to get file path",
        "errNoLogPath":"Can't get logging path",
        "errCmdLineNoMem":"Insufficient memory to allocate cmdline string",
        "errMSIInstallFail":"The MSI Install failed with exit code %d\\nSee %s for more details", #MSI exit code, #Log File Location
        "errDotNetNeeded":"Microsoft .Net Framework 3.5 or higher is required",
        "twoCharBrand":"XS",
        "updater" : "ManagementAgentUpdater",
        "copyrightUpdater" : "XCP-ng",
        "updaterURL" : "",
        "updaterLong" : "Management Agent Auto-Updater",
        "updaterDesc" : "Automatically checks and updates XCP-ng tools",
        "laterVersion" : "A later version of Windows Management Agent is already installed.  Setup will now exit",
        "windowsRequired" : "This version of the XCP-ng Windows Management Agent requires Windows Vista, Windows Server 2008 or Later.  For Windows XP and 2003 try installing XenLegacy.exe",
        "evtServiceStarting" : "Service Starting",
        "evtException" : "Exception: ",
        "evtServiceStopping" : "Service Stopping",
        "evtStopLock" : "Service Stopping (locked)",
        "evtStopShutdown" : "Service Stopping (shutdown)",
        "evtStopNothing" : "Service Stopping (nothing running)",
        "evtStopJoin" : "Service Stopping (joining thread)",
        "evtStopped" : "Service Stopping (done)",
        "32BitNotOn64" : "Please install the 64 bit version of this package on 64 bit systems",
        "allowAutoUpdate" : "YES",
        "allowDriverUpdate" : "NO",
        "allowDriverInstall" : "YES",
        "installAndUpdateTitle" : "Installation and Update Settings",
        "installAndUpdateDesc" : "Click Next to accept recommended settings",
        "ioDesc" : "I/O drivers improve performance, functionality and reliability",
        "ioInstall" : "Install I/O Drivers Now",
        "mgmtDesc" : "The management agent automatically updates itself when new versions are available",
        "mgmtAllow" : "Allow automatic management agent updates",
        "mgmtDisallow" : "Disallow automatic management agent updates",
        "ioUpdDesc" : "The management agent can install I/O drivers when new versions are available",
        "ioUpdAllow" : "Allow automatic I/O driver updates by the management agent",
        "ioUpdDisallow" : "Disallow automatic I/O driver updates by the management agent",
        "updDisclaim" : "Automatic updates may be overridden by pool policies",
        "whqlWarn" : "Customers using Windows Update for I/O driver updates should not select this option",
        "userAgent" : "XCP-ng AutoUpdate",
}

filenames = {
        "legacy" : "XenLegacy.Exe",
        "legacyuninstallerfix" : "xluninstallerfix.exe",
        "driversmsix86" : "CitrixXenDriversX86.msi",
        "driversmsix64" : "CitrixXenDriversX64.msi",
        "vssmsix86" : "CitrixVssX86.msi",
        "vssmsix64" : "CitrixVssX64.msi",
        "guestagentmsix86" : "CitrixGuestAgentX86.msi",
        "guestagentmsix64" : "CitrixGuestAgentX64.msi",
        "installwizard" : "InstallWizard.msi",
        "managementx64" : "managementagentx64.msi",
        "managementx86" : "managementagentx86.msi",
        "setup" : "setup.exe",
        "dpriv" : "XenDpriv.exe",
        "dprivcfg" : "XenDpriv.exe.config",
        "agent" : "XenGuestAgent.exe",
        "agentcfg" : "XenGuestAgent.exe.config",
        "installVSS" : "install-XenProvider.cmd",
        "uninstallVSS" : "uninstall-XenProvider.cmd",
}


resources = {
        "icon" : "xen.ico",
}

bitmaps = "..\\..\\src\\bitmaps"

languagecode = {
        "culture" : "enus",
        "language" : "0x09",
        "sublang" : "0x04",
}

cultures = {
        "default" : "en-us",
        "others" : [],
}


