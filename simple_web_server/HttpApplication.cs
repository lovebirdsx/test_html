namespace SimpleWebServer {
    class HttpApplication : IHttpHandler {
        public void ProcessRequest(HttpContext context) {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = Path.Combine(baseDir + "SimpleWebServer", context.Request.Url.TrimStart('/'));
            var fileExt = Path.GetExtension(context.Request.Url);

            if (fileExt.Equals(".aspx")) {
                var className = Path.GetFileNameWithoutExtension(context.Request.Url);
                var assembly = typeof(HttpApplication).Assembly;
                var handler = assembly.CreateInstance($"SimpleWebServer.Page.{className}") as IHttpHandler;
                if (handler == null) {
                    Console.WriteLine($"No page for {className}");
                    WriteNotFoundResponse(context.Response);
                } else {
                    handler.ProcessRequest(context);
                }
            } else {
                if (!File.Exists(fileName)) {
                    WriteNotFoundResponse(context.Response);
                } else {
                    context.Response.StateCode = "200";
                    context.Response.StateDescription ="OK";
                    context.Response.ContentType = GetContentType(fileExt);
                    context.Response.Body = File.ReadAllBytes(fileName);
                }
            }
        }

        void WriteNotFoundResponse(HttpResponse response) {
            response.StateCode = "404";
            response.StateDescription = "Not Found";
            response.ContentType = "text/html";
            var notExitHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SimpleWebServer\notfound.html");
            response.Body = File.ReadAllBytes(notExitHtml);
        }

        public string GetContentType(string fileExt) {
            var type = "";
            switch (fileExt) {
                case ".aspx":
                case ".html":
                case ".htm":
                    type = "text/html";
                    break;
                case ".png":
                    type = "image/png";
                    break;
                case ".jpg":
                case ".jpeg":
                    type = "image/jpeg";
                    break;
                case ".css":
                    type = "text/css";
                    break;
                case ".js":
                    type = "application/x-javascript";
                    break;
                default:
                    type = "text/plain; charset=gbk";
                    break;
            }
            return type;
        }
    }
}