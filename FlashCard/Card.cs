using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FlashCard {
    // 능률보카어원 2013
    public class EfficiencyVoca {
        public static EfficiencyVoca[] ReadCards(byte[] jsonBuf) {
            var ser = new DataContractJsonSerializer(typeof(EfficiencyVoca[]));
            using (var ms = new MemoryStream(jsonBuf)) {
                return (EfficiencyVoca[])ser.ReadObject(ms);
            }
        }

        public string VOCA_ID { get; set; }         // Voca No.
        public string DAY_NO { get; set; }          // Day No.
        public string PREFIX_GRP { get; set; }      // 어원 그룹 No
        public string PREFIX_ORD { get; set; }      // 어원 단어 순서 No.
        public string VOCABULARY { get; set; }      // 단어
        public string VOCABULARY_TAG { get; set; }  // 단어 태그
        public string MEANING_TAG { get; set; }     // 뜻 태그
        public string ORIGIN_EXP_TAG { get; set; }  // 어원 기원 태그
        public string DERIVATIVE_TAG { get; set; }  // 파생어 태그
        public string SENTENCE_TAG { get; set; }    // 예문 태그
        public string MEANING_QUICK { get; set; }   // 뜻 빠른
        public string SOUND_ORD { get; set; }       // 

        public string GetTitle() {
            if (PREFIX_ORD != "0") {
                return $"{this.VOCABULARY} : {this.MEANING_QUICK}";
            } else {
                return $"d{int.Parse(DAY_NO):00} {this.VOCABULARY_TAG} {this.VOCABULARY} : {this.MEANING_TAG}";
            }
        }

        public static string GetHtmlTop() {
            string html =
$@"<!DOCTYPE html>
<html lang=""ko"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <title>Document</title>
<style>
{Properties.Resources.style}
</style>
</head>
<body>
<table>
";
            return html;
        }

        public static string GetHtmlBottom() {
            string html =
$@"</table>
</body>
</html>
";
            return html;
        }

        public string GetHtmlTableRow() {
            if (this.PREFIX_ORD == "0") {
                string html =
$@"<tr>
<td align=right><b>{this.VOCABULARY}</b></td>
<td>{this.VOCABULARY_TAG} {this.MEANING_TAG.Replace(" / ", "<br/>")}</td>
</tr>
";
                return html;
            } else {
                string html =
$@"<tr>
    <td align=right>{Regex.Replace(this.VOCABULARY_TAG, "\\^(.*?)\\^", "<font color=\"red\">$1</font>")}</td>
    <td>{this.MEANING_TAG.Replace(" / ", "<br/>")}</td>
</tr>
" +
(this.ORIGIN_EXP_TAG == string.Empty ? "" : $@"<tr>
    <td align=right>(원)</td>
    <td>{this.ORIGIN_EXP_TAG.Replace(" / ", "<br/>")}</td>
</tr>
") +
(this.DERIVATIVE_TAG == string.Empty ? "" : $@"<tr>
    <td align=right>(파)</td>
    <td>{this.DERIVATIVE_TAG.Replace(" / ", "<br/>")}</td>
</tr>
") +
(this.SENTENCE_TAG == string.Empty ? "" : $@"<tr>
    <td align=right>(ex)</td>
    <td>{Regex.Replace(this.SENTENCE_TAG, "\\^(.*?)\\^", "<font color=\"red\">$1</font>").Replace(" / ", "<br/>  → ")}</td>
</tr>
");
                return html;
            }
        }
    }
}
