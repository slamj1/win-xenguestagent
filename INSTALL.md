To install the XCP-ng Windows XenGuestAgent onto a XCP-ng Windows 
guest VM:

*    Install your choice od .net 3.5 or .net 4 or greater on the VM
*    Install xeniface.sys and xenbus.sys on the guest VM
*    Create a directory "c:\\Program Files\\XCP-ng\\XenTools" (the installdir)
*    If you want to use .Net 4.x , copy XenGuestAgent.exe.Config into the 
     installdir
*    If you want to use .Net 4.x , copy XenDpriv.exe.Config into the 
     installdir
*    Copy XenGuestAgent.exe into the installdir
*    Copy XenGuestLib.dll into the installdir
*    Copy XenDpriv.exe into the installdir

*    Set the following registry entries
        
     `
     HKLM\\Software\\XCP-ng\\XenTools\\MajorVersion DWORD 6
     HKLM\\Software\\XCP-ng\\XenTools\\MinorVersion DWORD 2
     HKLM\\Software\\XCP-ng\\XenTools\\MicroVersion DWORD 0
     HKLM\\Software\\XCP-ng\\XenTools\\BuildVersion DWORD 20
     HKLM\\Software\\XCP-ng\\XenTools\\InstallDir STRING "C:\\Program Files\\XCP-ng\\XenTools"
     HKLM\\SYSTEM\\CurrentControlSet\\Control\\ServicesPipeTimeout DWORD 300000
     `

*    Run the following command

     `sc create XenSvc binPath= "c:\\Program Files\\XCP-ng\\XenTools\\xenguestagent.exe" type= own start = auto depend= WinMgmt DisplayName= "XCP-ng Xen Guest Agent"`

*    Reboot the VM
