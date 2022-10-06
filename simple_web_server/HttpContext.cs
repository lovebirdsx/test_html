namespace SimpleWebServer {
    public class HttpContext {
        public HttpRequest Request {get; private set;}
        public HttpResponse Response {get; private set;}

        public HttpContext(string text) {
            Request = new HttpRequest(text);
            Response = new HttpResponse();
        }
    }

    public interface IHttpHandler {
        void ProcessRequest(HttpContext context);
    }
}