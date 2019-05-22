using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard {
    // 능률보카어원 2013
    public class EfficiencyVoca {
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
    }

    // 그림어원 중학
    public class DrawingVocaMs {
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
    }

    // 그림어원 수능, 토익
    public class DrawingVoca {
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
    }

    // 구텐베르크 최빈도 어휘 13000
    public class Voca13000 {
        public string Word { get; set; }            // 단어
        public string Meaning { get; set; }         // 뜻
    }
}
