using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard {
    public class Card {
        public int VOCA_ID { get; set; }
        public int DAY_NO { get; set; }
        public int PREFIX_GRP { get; set; }
        public int PREFIX_ORD { get; set; }
        public string VOCABULARY { get; set; }
        public string VOCABULARY_TAG { get; set; }
        public string MEANING_TAG { get; set; }
        public string MEANING_INDEX { get; set; }
        public string ORIGIN_EXP_TAG { get; set; }
        public string ORIGIN_APPENDIX { get; set; }
        public string DERIVATIVE_TAG { get; set; }
        public string SENTENCE_TAG { get; set; }
        public string MEANING_QUICK { get; set; }
        public int SOUND_ORD { get; set; }
    }
}
