using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSZDialougeManager
{
    public class NextNodesBoxItem
    {
        public string text;
        public DialogueNodeDTO node;
        public override string ToString() => text;
    }
}
