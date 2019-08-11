using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace FlashCard {
    public class Settings {
        public int lastIndex { get; set; } = 0;
        public Point windowLocation { get; set; } = Point.Empty;
        public Size windowSize { get; set; } = new Size(600, 300);
        public bool autoChange { get; set; } = false;
        
        public static Settings FromJson(string json) {
            var ser = new DataContractJsonSerializer(typeof(Settings));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json))) {
                var obj = (Settings)ser.ReadObject(ms);
                return obj;
            }
        }

        public string ToJson() {
            var ser = new DataContractJsonSerializer(typeof(Settings));
            using (var ms = new MemoryStream())
            using (var writer = JsonReaderWriterFactory.CreateJsonWriter(ms, Encoding.UTF8, true, true)) {
                ser.WriteObject(writer, this);
                writer.Flush();
                var json = Encoding.UTF8.GetString(ms.ToArray());
                return json;
            }
        }

        public static string HttpGetJson() {
            string url = "https://api.jsonbin.io/b/5ce6bb555302fd1986c60aa0";
            string responseText = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 30 * 1000; // 30초
            request.Headers.Add("secret-key", "$2a$10$u9ScRpYp.bxUrZOpoYNYc.eWSfzDrfB2PksK5EUlS97QNKrRO77GG"); // 헤더 추가 방법

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse()) {
                HttpStatusCode status = resp.StatusCode;
                Console.WriteLine(status);  // 정상이면 "OK"

                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream)) {
                    responseText = sr.ReadToEnd();
                }
            }
            return responseText;
        }

        public static void HttpPutJson(string json) {
            string url = "https://api.jsonbin.io/b/5ce6bb555302fd1986c60aa0";
            string data = json;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.Timeout = 30 * 1000;
            request.ContentType = "application/json";
            request.Headers.Add("secret-key", "$2a$10$u9ScRpYp.bxUrZOpoYNYc.eWSfzDrfB2PksK5EUlS97QNKrRO77GG");
            request.Headers.Add("versioning", "false");
            
            // POST할 데이타를 Request Stream에 쓴다
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            request.ContentLength = bytes.Length; // 바이트수 지정

            using (Stream reqStream = request.GetRequestStream()) {
                reqStream.Write(bytes, 0, bytes.Length);
            }

            // Response 처리
            string responseText = string.Empty;
            using (WebResponse resp = request.GetResponse()) {
                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream)) {
                    responseText = sr.ReadToEnd();
                }
            }
        }

        public static Settings Load() {
            try {
                string json = Settings.HttpGetJson();
                return Settings.FromJson(json);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                return new Settings();
            }
        }

        public void Save() {
            try {
                string json = this.ToJson();
                Settings.HttpPutJson(json);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
