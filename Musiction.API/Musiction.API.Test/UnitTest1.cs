using Musiction.API.BusinessLogic;
using Xunit;


namespace Musiction.API.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            PptxToJpgConverter aa = new PptxToJpgConverter();
            var file = @"1.pptx";
            aa.Convert(file);
        }
    }
}
