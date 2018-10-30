using System;

namespace Musiction.API.Resources
{
    public static class MagicString
    {
        public static string FinalFileName => "finaleFile_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".pptx";
        public static string UrlToPptxExport =>
            "https://docs.google.com/presentation/d/{0}/export/pptx";

        public static string ZamzarApiUrl => "https://sandbox.zamzar.com/v1/";
    }
}
