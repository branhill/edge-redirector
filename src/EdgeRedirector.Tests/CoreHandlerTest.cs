using EdgeRedirector.Core;
using Xunit;

namespace EdgeRedirector.Tests
{
    public class CoreHandlerTest
    {
        [Theory]
        [InlineData(@"microsoft-edge:https://go.microsoft.com/fwlink/?LinkId=528884",
            @"https://go.microsoft.com/fwlink/?LinkId=528884")]
        [InlineData(
            @"microsoft-edge:?launchContext1=Microsoft.Windows.Cortana_cw5n1h2txyewy&url=https%3A%2F%2Fwww.bing.com%2Fsearch%3Fq%3Dthe%2Bsearch%2Btest%26form%3DWNSGPH%26qs%3DSW%26cvid%3D00000000000000000000000000000000%26pq%3Dthe%2Bsearch%2Btest%26cc%3DUS%26setlang%3Den-US%26nclid%3D00000000000000000000000000000000%26ts%3D0000000000000%26nclidts%3D0000000000%26tsms%3D000",
            @"https://www.bing.com/search?q=the+search+test&form=WNSGPH&qs=SW&cvid=00000000000000000000000000000000&pq=the+search+test&cc=US&setlang=en-US&nclid=00000000000000000000000000000000&ts=0000000000000&nclidts=0000000000&tsms=000")]
        public void GetOriginalUrlTest(string uri, string expected)
        {
            string result = Handler.GetOriginalUrl(uri);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("https://go.microsoft.com/fwlink/?LinkId=528884", "", "https://go.microsoft.com/fwlink/?LinkId=528884")]
        [InlineData("https://go.microsoft.com/fwlink/?LinkId=528884", "https://example.com/search?q=%s", "https://go.microsoft.com/fwlink/?LinkId=528884")]
        [InlineData("https://www.bing.com/search?q=the+search+test&form=WNSGPH&qs=SW&cvid=00000000000000000000000000000000&pq=the+search+test&cc=US&setlang=en-US&nclid=00000000000000000000000000000000&ts=0000000000000&nclidts=0000000000&tsms=000", "https://example.com/search?q=%s", "https://example.com/search?q=the+search+test")]
        public void GetRedirectedUrlTest(string originalUrl, string searchEngine, string expected)
        {
            string result = Handler.GetRedirectedUrl(originalUrl, searchEngine);

            Assert.Equal(result, expected);
        }
    }
}
