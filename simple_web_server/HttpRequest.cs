using System.Text.RegularExpressions;

namespace SimpleWebServer {
    public class HttpRequest {
        public string HttpMethod { get; private set; }
        public string Url { get; private set; }
        public string HttpVersion { get; private set; }
        public Dictionary<string, string> HeaderMap { get; private set; }
        public Dictionary<string, string> BodyMap { get; private set; }

        public HttpRequest(string text) {
            var lines = Regex.Split(text, "\r\n|\r|\n");
            var firstLineTockens = lines[0].Split(' ');
            HttpMethod = firstLineTockens[0];
            Url = firstLineTockens[1];
            HttpVersion = firstLineTockens[2];
            HeaderMap = new Dictionary<string, string>();
            BodyMap = new Dictionary<string, string>();
        }
    }
}