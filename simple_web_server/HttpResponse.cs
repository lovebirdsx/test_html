using System.Text;

namespace SimpleWebServer {
    public class HttpResponse {
        public string? StateCode { get; set; }
        public string? StateDescription { get; set; }
        public string? ContentType { get; set; }
        public byte[]? Body { get; set; }

        public byte[] GetResponseHeader() {
            // 注意: 最后需要两个空行
            var header = string.Format(@"HTTP/1.1 {0} {1}
Content-Type: {2}
Accept-Range: bytes
Server: Personal
Date: {3}
Content-Length: {4}

", StateCode, StateDescription, ContentType, string.Format("{0:R}", DateTime.Now), Body?.Length);

            return Encoding.UTF8.GetBytes(header);
        }
    }
}