using Musiction.API.BusinessLogic;
using System.Collections.Generic;
using System.IO;
using Xunit;


namespace Musiction.API.Test
{
    public class PowerPointMergerTest
    {
        [Fact]
        public void CheckNuberOfSlides()
        {
            var FiveSlidesFile = Path.Combine(Directory.GetCurrentDirectory(), @"testFiles\FiveSlides.pptx");
            PowerPointMerger merger = new PowerPointMerger();

            var numberOfSliedes = merger.GetNumberOfSlides(FiveSlidesFile);

            Assert.True(numberOfSliedes == 5);
        }

        [Fact]
        public void MergeFiles()
        {
            //ASSERT
            var test0File = Path.Combine(Directory.GetCurrentDirectory(), @"testFiles\test0.pptx");
            var test1File = Path.Combine(Directory.GetCurrentDirectory(), @"testFiles\test1.pptx");
            var test2File = Path.Combine(Directory.GetCurrentDirectory(), @"testFiles\test2.pptx");

            List<string> files = new List<string>() { test0File, test1File, test2File };
            PowerPointMerger merger = new PowerPointMerger();

            //ACT
            var filePath = merger.Merge(files);

            //DONE           
            Assert.True(filePath != "");
            Assert.True(merger.GetNumberOfSlides(filePath) == 3);
        }
    }
}
