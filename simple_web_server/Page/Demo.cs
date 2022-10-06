using System.Text;

namespace SimpleWebServer.Page {
    public class Demo : IHttpHandler {
        public void ProcessRequest(HttpContext context) {
            var sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("<head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'/><title>DemoPage</title></head>");
            sb.Append("<body style='margin:10px auto;text-align:center;'>");
            sb.Append("<h1>用户列表</h1>");
            sb.Append("<table align='center' cellpadding='1' cellspacing='1'><thead><tr><td>ID</td><td>名字</td></tr></thead>");
            sb.Append("<tbody>");
            sb.Append(GetDataList());
            sb.Append("</tbody></table>");
            sb.Append(string.Format("<h3>Time: {0}</h3>", DateTime.Now.ToString()));
            sb.Append("</body>");
            sb.Append("</html>");

            var response = context.Response;
            response.Body = Encoding.UTF8.GetBytes(sb.ToString());
            response.StateCode = "200";
            response.ContentType = "text/html";
            response.StateDescription = "OK";
        }

        private string GetDataList() {
            var sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append("<td>10001</td>");
            sb.Append("<td>Lovebird</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>10002</td>");
            sb.Append("<td>Firebird</td>");
            sb.Append("</tr>");
            return sb.ToString();
        }
    }
}