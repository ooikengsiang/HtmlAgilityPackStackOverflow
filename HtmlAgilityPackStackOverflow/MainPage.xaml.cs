using HtmlAgilityPack;
using System.Linq;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HtmlAgilityPackStackOverflow
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // create request to connect with the server
            var client = new HttpClient();
            var response = await client.GetAsync("https://store.channelfireball.com/products/search?q=narset&c=1");
            if (response.IsSuccessStatusCode)
            {
                // get the HTTP respond content in string
                var respondBytes = await response.Content.ReadAsByteArrayAsync();
                var respondContent = System.Text.Encoding.UTF8.GetString(respondBytes, 0, respondBytes.Length - 1);

                // load string to HTML in order to extract the data
                var doc = new HtmlDocument();
                doc.LoadHtml(respondContent);

                // get each row results
                var rowNodes = doc.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", null) == "meta");
                if (rowNodes != null)
                {
                    // process each row and find the card we want
                    foreach (var rowNode in rowNodes)
                    {
                        var cardNameNode = rowNode.Descendants("a").FirstOrDefault(x => x.GetAttributeValue("itemprop", null) == "url");
                        if (cardNameNode != null)
                        {
                        }
                    }
                }
            }
        }
    }
}
