﻿using System;
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
        public string PRONUNCIATION { get; set; }   // 발음
        public string ORIGIN_APPENDIX { get; set; } // 어원 변화형
        public string VOCABULARY_TAG { get; set; }  // 단어 태그
        public string MEANING_TAG { get; set; }     // 뜻 태그
        public string ORIGIN_EXP_TAG { get; set; }  // 어원 기원 태그
        public string DERIVATIVE_TAG { get; set; }  // 파생어 태그
        public string SENTENCE_TAG { get; set; }    // 예문 태그
        public string MEANING_QUICK { get; set; }   // 뜻 빠른
        public string SOUND_ORD { get; set; }       // 

        public string GetSearchTitle() {
            if (PREFIX_ORD != "0") {
                return $"{VOCABULARY}: {MEANING_TAG.Replace(" / ", " ")}";
            } else {
                return $"{VOCABULARY}: {VOCABULARY_TAG} {MEANING_TAG}";
            }
        }

        public IEnumerable<string> GetSearchTitles() {
            List<string> searchTitles = new List<string>();
            searchTitles.Add(GetSearchTitle());
            var subSearchTitles = DERIVATIVE_TAG
                .Split(new string[]{" / "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(text => text.Replace("^ ", ": ").Replace("^", string.Empty));
            searchTitles.AddRange(subSearchTitles);
            return searchTitles;
        }

        public string GetGroupTitle() {
            return $"Day{DAY_NO}-{int.Parse(PREFIX_GRP)+1}: {VOCABULARY_TAG} {VOCABULARY}";
        }

        public string GetCaptionTitle() {
            return $"Flash Card - Day{DAY_NO}-{int.Parse(PREFIX_GRP)+1} {VOCABULARY_TAG} {VOCABULARY}: {MEANING_TAG}";
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
";
            return html;
        }

        public static string GetHtmlBottom() {
            string html =
$@"</body>
</html>
";
            return html;
        }

        public string GetHtmlTableRow(string color, string bgcolor, string derivecolor) {
            if (this.PREFIX_ORD == "0") {
                string entry = $"<font size=6 color={color}><b>{this.VOCABULARY}</b></font>";
                string origin_appendx = this.ORIGIN_APPENDIX == "" ? "" : $" <font color={color}>({this.ORIGIN_APPENDIX})</font>";
                string etymology = this.MEANING_TAG.Replace(" / ", "<br/>");

                string html = $@"<tr bgcolor={bgcolor}><td width=250>{entry}{origin_appendx}</td><td width=600><font color={color}>{etymology}</font></td></tr>" + "\r\n";
                return html;
            } else {
                string entry = $"<font size=5 color={color}>" + Regex.Replace(this.VOCABULARY_TAG, "\\^(.*?)\\^", "<b>$1</b>") + $"</font>";
                string pronun = (this.PRONUNCIATION == "") ? "" : " [" + this.PRONUNCIATION + "]";
                string origin = (this.ORIGIN_EXP_TAG == "") ? "" : "<br/><font size=2>" + this.ORIGIN_EXP_TAG.Replace(" / ", "<br/>") + "</font>";
                string meaning = this.MEANING_TAG.Replace(" / ", "<br/>");
                string derivative = (this.DERIVATIVE_TAG == "") ? "" : "<hr/>" + $"<font color={derivecolor}>" + Regex.Replace(this.DERIVATIVE_TAG, "\\^(.*?)\\^", "<b>$1</b>").Replace(" / ", "<br/>") + "</font>";
                string sentence = (this.SENTENCE_TAG == "") ? "" : "<hr/>" + Regex.Replace(this.SENTENCE_TAG, "\\^(.*?)\\^", "<b>$1</b>").Replace(" / ", "<br/>  → ");
                
                string html = $@"<tr valign=top><td>{entry}{pronun}{origin}</td><td>{meaning}{derivative}{sentence}</td></tr>";
                return html;
            }
        }
    }
}
