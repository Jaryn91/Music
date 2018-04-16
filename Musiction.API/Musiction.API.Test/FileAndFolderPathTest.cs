using Musiction.API.BusinessLogic;
using Xunit;

namespace Musiction.API.Test
{
    public class FileAndFolderPathTest
    {
        [Fact]
        public void CheckNuberOfSlides()
        {
            var fileAndFolderPath = new FileAndFolderPathsCreator();
            var songName = "Barka";

            var path = fileAndFolderPath.GetZipFilePath(songName);

            Assert.True(path != null);
        }

    }
}
