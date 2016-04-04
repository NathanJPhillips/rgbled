using Windows.ApplicationModel.Resources;

namespace Porrey.RgbLed.strings
{
    internal static class ResourceManager
    {
        private static readonly ResourceLoader _resourceLoader = new ResourceLoader();

        public static string ExceptionDialogTitle
        {
            get
            {
                return _resourceLoader.GetString("ExceptionDialogTitle");
            }
        }
    }
}
