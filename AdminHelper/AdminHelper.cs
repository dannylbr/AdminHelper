using System.Diagnostics;
using System.Security.Principal;

namespace AdminHelper;


public static class AdminHelper
{
    public static bool IsAdministrator()
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    public static void RestartAsAdministrator()
    {
        try
        {
            Process.Start(new ProcessStartInfo(Application.ExecutablePath) { Verb = "runas" });
            Application.Exit();
        }
        catch
        {
            MessageBox.Show("Administrator permission required to remove entries.", "Permission Denied",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
