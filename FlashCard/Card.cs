using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace FlashCard {
    public abstract class Voca {
        public abstract string GetTitle();
        public abstract string GetHtml();
        public static Voca[] ReadDeck(Type type, byte[] bytes, string dbFilename, string tableName) {
            string dbFilePath = Application.StartupPath + "\\" + dbFilename + ".sqlite";
            if (File.Exists(dbFilePath) == false) {
                File.WriteAllBytes(dbFilePath, bytes);
            }
            string connStr = $@"Data Source={dbFilePath}";

            var props = type.GetProperties();

            List<Voca> vocaList = new List<Voca>();

            using (var conn = new SqliteConnection(connStr)) {
                conn.Open();
                string sql = $"SELECT * FROM {tableName}";

                var cmd = new SqliteCommand(sql, conn);
                var rdr = cmd.ExecuteReader();
                while (rdr.Read()) {
                    var voca = (Voca)Activator.CreateInstance(type);
                    foreach (var prop in props) {
                        object newType = Convert.ChangeType(rdr[prop.Name], prop.PropertyType);
                        prop.SetValue(voca, newType);
                    }
                    vocaList.Add(voca);
                }
                rdr.Close();
            }

            return vocaList.ToArray();
        }
    }
    
    // 능률보카어원 2013
    public class EfficiencyVoca : Voca {
        public int VOCA_ID { get; set; }            // Voca No.
        public int DAY_NO { get; set; }             // Day No.
        public int PREFIX_GRP { get; set; }         // 어원 그룹 No
        public int PREFIX_ORD { get; set; }         // 어원 단어 순서 No.
        public string VOCABULARY { get; set; }      // 단어
        public string VOCABULARY_TAG { get; set; }  // 단어 태그
        public string MEANING_TAG { get; set; }     // 뜻 태그
        public string MEANING_INDEX { get; set; }   // 뜻 인덱스
        public string ORIGIN_EXP_TAG { get; set; }  // 어원 기원 태그
        public string ORIGIN_APPENDIX { get; set; } // 어원 변화형
        public string DERIVATIVE_TAG { get; set; }  // 파생어 태그
        public string SENTENCE_TAG { get; set; }    // 예문 태그
        public string MEANING_QUICK { get; set; }   // 뜻 빠른
        public int SOUND_ORD { get; set; }          //

        public override string GetTitle() {
            if (PREFIX_ORD != 0) {
                return string.Format("{0}. {1} : {2}", this.VOCA_ID, this.VOCABULARY, this.MEANING_INDEX);
            } else {
                return string.Format("[어원] {0} : {1}", this.VOCABULARY, System.Text.RegularExpressions.Regex.Replace(this.ORIGIN_EXP_TAG, "<.*?>", string.Empty));
            }
        }
        public override string GetHtml() {
            string html =
$@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <title>Document</title>
<style>
{Properties.Resources.main_study}
</style>
</head>
<body>
    <table width=""100%"">
        <tr>
            <td width=""50%"">{(this.PREFIX_ORD == 0 ? this.VOCABULARY : this.VOCABULARY_TAG)}</font>&nbsp{this.ORIGIN_APPENDIX}{this.MEANING_TAG}{this.DERIVATIVE_TAG}</td>
            <td>{this.ORIGIN_EXP_TAG}</td>
        </tr>
        <tr>
            <td colspan=""2"">{(this.SENTENCE_TAG.Replace("opacity:0","opacity:100"))}</td>
        </tr>
    </table>
</body>
</html>";
            return html;
        }
    }

    // 그림어원 중학
    public class DrawingVocaMs : Voca {
         public string CSNUM { get; set; }          // Voca No.
         public string SCNUM { get; set; }          // 챕터 No.
         public string UNITNUM { get; set; }        // 단원 No.
         public string CSSUBNUMBER { get; set; }    // 서브 No.
         public string CSTITLE { get; set; }        // 타이틀 (어원)
         public string WORD_EN1 { get; set; }       // 단어 영어
         public string WORD_EN2 { get; set; }       // 단어 영어 어원
         public string WORD_KR1 { get; set; }       // 단어 한글 어원
         public string WORD_KR2 { get; set; }       // 단어 한글 해석
         public string Q_CORRECT { get; set; }      // 퀴즈 정답
         public string Q_WRONG1 { get; set; }       // 퀴즈 오답1
         public string Q_WRONG2 { get; set; }       // 퀴즈 오답2
         public string Q_WRONG3 { get; set; }       // 퀴즈 오답3
         public string EX_EN { get; set; }          // 예문 영어
         public string EX_EN_COLOR { get; set; }    // 예문 영어 컬러
         public string EX_KR { get; set; }          // 예문 한글 해석
         public string EX_EN2 { get; set; }         // 예문 영어2 
         public string EX_EN_COLOR2 { get; set; }   // 예문 영어 컬러2
         public string EX_KR2 { get; set; }         // 예문 한글 해석2
         public string FILENAME { get; set; }       // 파일이름
         public string VARCHAR_EX { get; set; }     //
         public string VARCHAR_EX2 { get; set; }    //
         public string WORD_STAR { get; set; }      // 단어 별
         public string WORD_PHONETICS { get; set; } // 단어 발음기호
         public string WRONGNUM { get; set; }       // 오답 횟수
         public string BOOKMARK { get; set; }       // 북마크
        public override string GetTitle() {
            return string.Format("{0}. {1} : {2}", this.CSNUM, this.WORD_EN1, this.Q_CORRECT);
        }
        public override string GetHtml() {
            string wordRoot = this.CSTITLE;
            string wordEntry = this.WORD_EN1;
            string pronunciation = this.WORD_PHONETICS;
            string meaning = this.WORD_KR2;
            var etyEngParts = WORD_EN2.Split('^');
            var etyEngParts2 = etyEngParts.Select(word=>word.StartsWith("&") ? "<font color=\"red\">" + word.TrimStart('&') + "</font>" : word);
            var etyEng = string.Join(" + ", etyEngParts2);
            var etyKorParts = WORD_KR1.Split('^');
            var etyKorParts2 = etyKorParts.Select(word=>word.StartsWith("&") ? "<font color=\"red\">" + word.TrimStart('&') + "</font>" : word);
            var etyKor = string.Join(" + ", etyKorParts2);
            var ex1 = this.EX_EN != string.Empty ? this.EX_EN.Replace(this.EX_EN_COLOR, "<font color=\"red\">"+this.EX_EN_COLOR+"</font>") : string.Empty;
            var ex1_kor = this.EX_KR;
            var ex2 = this.EX_EN2 != string.Empty ? this.EX_EN2.Replace(this.EX_EN_COLOR2, "<font color=\"red\">"+this.EX_EN_COLOR2+"</font>") : string.Empty;
            var ex2_kor = this.EX_KR2;
            string html =
$@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <title>Document</title>
<style>
{Properties.Resources.drawing_voca}
</style>
</head>
<body>
{wordRoot}<br/>
</br>
<font size='5'><b>{wordEntry}</b></font> [{pronunciation}]<br/>
{meaning}<br/>
<br/>
{etyEng}<br/>
{etyKor}<br/>
<br/>
{ex2}<br/>
{ex2_kor}<br/>
{ex1}<br/>
{ex1_kor}<br/>
</body>
</html>";
            return html;
        }
    }

    // 그림어원 수능, 토익
    public class DrawingVoca : Voca {
        public int CSNUM { get; set; }              // Voca No.
        public int SCNUM { get; set; }              // 챕터 No.
        public int UNITNUM { get; set; }            // 단원 No.
        public int CSSUBNUMBER { get; set; }        // 서브 No.
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
        public string FILENAME { get; set; }        // 파일이름
        public string VARCHAR_EX { get; set; }      //
        public string VARCHAR_EX2 { get; set; }     //
        public int WORD_STAR { get; set; }          // 단어 별
        public string WORD_PHONETICS { get; set; }  // 단어 발음기호
        public int WRONGNUM { get; set; }           // 오답 횟수
        public int BOOKMARK { get; set; }           // 북마크
        public override string GetTitle() {
            return string.Format("{0}. {1} : {2}", this.CSNUM, this.WORD_EN1, this.Q_CORRECT);
        }
        public override string GetHtml() {
            string wordRoot = this.CSTITLE;
            string wordEntry = this.WORD_EN1;
            string pronunciation = this.WORD_PHONETICS;
            string meaning = this.WORD_KR2;
            var etyEngParts = WORD_EN2.Split('^');
            var etyEngParts2 = etyEngParts.Select(word=>word.StartsWith("&") ? "<font color=\"red\">" + word.TrimStart('&') + "</font>" : word);
            var etyEng = string.Join(" + ", etyEngParts2);
            var etyKorParts = WORD_KR1.Split('^');
            var etyKorParts2 = etyKorParts.Select(word=>word.StartsWith("&") ? "<font color=\"red\">" + word.TrimStart('&') + "</font>" : word);
            var etyKor = string.Join(" + ", etyKorParts2);
            var ex1 = this.EX_EN != string.Empty ? this.EX_EN.Replace(this.EX_EN_COLOR, "<font color=\"red\">"+this.EX_EN_COLOR+"</font>") : string.Empty;
            var ex1_kor = this.EX_KR;
            string html =
$@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <title>Document</title>
<style>
{Properties.Resources.drawing_voca}
</style>
</head>
<body>
{wordRoot}<br/>
</br>
<font size='5'><b>{wordEntry}</b></font> [{pronunciation}]<br/>
{meaning}<br/>
<br/>
{etyEng}<br/>
{etyKor}<br/>
<br/>
{ex1}<br/>
{ex1_kor}<br/>
</body>
</html>";
            return html;
        }
    }

    // 구텐베르크 최빈도 어휘 13000
    public class Voca13000 : Voca {
        public int Id { get; set; }                 // Id
        public string Word { get; set; }            // 단어
        public string Meaning { get; set; }         // 뜻
                public override string GetTitle() {
            return string.Format("{0}. {1} : {2}", this.Id, this.Word, this.Meaning);
        }
        public override string GetHtml() {
            string html =
$@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <title>Document</title>
</head>
<body>
{this.Word}<br/>
{this.Meaning}
</body>
</html>";
            return html;
        }
    }
}
