using System.Runtime.InteropServices;
using System.Text;

namespace EdgeRedirector.Gui.Helpers
{
    public static class PlatformDetection
    {
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentApplicationUserModelId(ref uint applicationUserModelIdLength,
            StringBuilder applicationUserModelId);

        public static bool IsInAppContainer()
        {
            var length = 0U;
            var id = new StringBuilder(0);
            long result = GetCurrentApplicationUserModelId(ref length, id);

            return result != 15703L; // APPMODEL_ERROR_NO_APPLICATION
        }
    }
}
