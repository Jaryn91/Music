using System;

namespace Musiction.API.Resources
{
    public static class MagicString
    {
        public static string FinalFileName => "finaleFile_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".pptx";
        public static string UrlToPptxExport =>
            "https://docs.google.com/presentation/d/{0}/export/pptx";

        public static string PathToDownloadFileFromGoogleDrive => "http://drive.google.com/uc?id={0}&export=download";
        public static string NoSongSelected => "Nie wybrano żodnej pieśni. Noo... Weź coś wybierz :)!";
        public static string SongWithIdDoesntExist => "Pieśń o id: {0} nie istnieje w bazie.";

        public static string ProblemOucuredDuringSavingSongToDatabase =>
            "Pojawił się problem podczas zapisywania piosenki do bazy danych";

    }
}
