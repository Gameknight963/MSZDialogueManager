using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSZDialougeManager
{
    public class DialogueNodeDTO
    {
        public int id;
        public int[] nextNodeIds;
        public string dialogueText;
        public string speakerName;
        public float delay;
    }
}
