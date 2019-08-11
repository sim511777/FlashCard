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
    public abstract class Voca {
        public abstract string GetTitle();
        public abstract string GetHtml();
        public static Voca[] ReadDeck(Type type, byte[] jsonBuf) {
            var ser = new DataContractJsonSerializer(type);
            using (var ms = new MemoryStream(jsonBuf)) {
                return (Voca[])ser.ReadObject(ms);
            }
        }
    }

    // 능률보카어원 2013
    public class EfficiencyVoca : Voca {
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

        public override string GetTitle() {
            if (PREFIX_ORD != "0") {
                return $"{this.VOCA_ID}. {this.VOCABULARY} : {this.MEANING_QUICK}";
            } else {
                return $"{this.VOCA_ID}. {this.VOCABULARY} : {this.MEANING_TAG}";
            }
        }
        public override string GetHtml() {
            if (this.PREFIX_ORD == "0") {
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
<font size='5'><b>{this.VOCABULARY}</b></font><br/>
<br/>
{this.MEANING_TAG.Replace(" / ", "<br/>")}
</body>
</html>";
                return html;
            } else {
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
<font size='5'><b>{Regex.Replace(this.VOCABULARY_TAG, "\\^(.*?)\\^", "<font color=\"red\">$1</font>")}</b></font><br/>
<br/>
{this.MEANING_TAG.Replace(" / ", "<br/>")}<br/>
<br/>
{this.ORIGIN_EXP_TAG.Replace(" / ", "<br/>")}<br/>
<br/>
(파생어)<br/>
{this.DERIVATIVE_TAG.Replace(" / ", "<br/>")}<br/>
<br/>
(예문)<br/>
{Regex.Replace(this.SENTENCE_TAG, "\\^(.*?)\\^", "<font color=\"red\">$1</font>").Replace(" / ", "<br/>  → ")}
</body>
</html>";
                return html;
            }
        }
    }

    // 그림어원 중학
    public class DrawingVoca : Voca {
        public string LEVEL { get; set; }           // 수준 중학/수능/토익
        public string CSNUM { get; set; }           // Voca No.
        public string SCNUM { get; set; }           // 챕터 No.
        public string UNITNUM { get; set; }         // 단원 No.
        public string CSSUBNUMBER { get; set; }     // 서브 No.
        public string CSTITLE { get; set; }         // 타이틀 (어원)
        public string WORD_EN1 { get; set; }        // 단어 영어
        public string WORD_EN2 { get; set; }        // 단어 영어 어원
        public string WORD_KR1 { get; set; }        // 단어 한글 어원
        public string WORD_KR2 { get; set; }        // 단어 한글 해석
        public string Q_CORRECT { get; set; }       // 퀴즈 정답
        public string Q_WRONG1 { get; set; }        // 퀴즈 오답1
        public string Q_WRONG2 { get; set; }        // 퀴즈 오답2
        public string Q_WRONG3 { get; set; }        // 퀴즈 오답3
        public string EX_EN { get; set; }           // 예문 영어
        public string EX_EN_COLOR { get; set; }     // 예문 영어 컬러
        public string EX_KR { get; set; }           // 예문 한글 해석
        public string EX_EN2 { get; set; }          // 예문 영어2 
        public string EX_EN_COLOR2 { get; set; }    // 예문 영어 컬러2
        public string EX_KR2 { get; set; }          // 예문 한글 해석2
        public string FILENAME { get; set; }        // 파일이름
        public string VARCHAR_EX { get; set; }      //
        public string VARCHAR_EX2 { get; set; }     //
        public string WORD_STAR { get; set; }       // 단어 별
        public string WORD_PHONETICS { get; set; }  // 단어 발음기호
        public string WRONGNUM { get; set; }        // 오답 횟수
        public string BOOKMARK { get; set; }        // 북마크
        public override string GetTitle() {
            return $"[{levelNamesShort[this.LEVEL]}]{this.CSNUM}. {this.WORD_EN1} : {this.Q_CORRECT}";
        }
        private static Dictionary<string, string> levelNames = new Dictionary<string, string>{
            { "0", ""},
            { "1", "중학"},
            { "2", "수능"},
            { "3", "토익"},
        };
        private static Dictionary<string, string> levelNamesShort = new Dictionary<string, string>{
            { "0", ""},
            { "1", "중"},
            { "2", "수"},
            { "3", "토"},
        };
        public override string GetHtml() {
            string wordRoot = this.CSTITLE;
            string wordEntry = this.WORD_EN1;
            string levelWord = levelNames[this.LEVEL];
            int.TryParse(this.WORD_STAR, out int starNum);
            levelWord += string.Join("", Enumerable.Repeat("★", starNum));
            string pronunciation = this.WORD_PHONETICS;
            string meaning = this.WORD_KR2;
            var etyEngParts = WORD_EN2.Split('^');
            var etyEngParts2 = etyEngParts.Select(word => word.StartsWith("&") ? "<font color=\"red\">" + word.TrimStart('&') + "</font>" : word);
            var etyEng = string.Join(" + ", etyEngParts2);
            var etyKorParts = WORD_KR1.Split('^');
            var etyKorParts2 = etyKorParts.Select(word => word.StartsWith("&") ? "<font color=\"red\">" + word.TrimStart('&') + "</font>" : word);
            var etyKor = string.Join(" + ", etyKorParts2);
            var ex1 = this.EX_EN != string.Empty ? this.EX_EN.Replace(this.EX_EN_COLOR, "<font color=\"red\">" + this.EX_EN_COLOR + "</font>") : string.Empty;
            var ex1_kor = this.EX_KR;
            var ex2 = this.EX_EN2 != string.Empty ? this.EX_EN2.Replace(this.EX_EN_COLOR2, "<font color=\"red\">" + this.EX_EN_COLOR2 + "</font>") : string.Empty;
            var ex2_kor = this.EX_KR2;
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
{wordRoot}<br/>
<br/>
<font size='5'><b>{wordEntry}</b></font><sup><font color=""gray"">{levelWord}</font></sup><br/>
[{pronunciation}]<br/>
<br/>
{meaning}<br/>
<br/>
{etyEng}<br/>
→ {etyKor}<br/>
<br/>
(예문)<br/>
{ex1}<br/>
→ {ex1_kor}<br/>
{ex2}<br/>
→ {ex2_kor}<br/>
</body>
</html>";
            return html;
        }
    }
}
