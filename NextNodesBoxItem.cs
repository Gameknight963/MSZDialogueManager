using MZDO.Shared;

namespace MSZDialougeManager
{
    public class NextNodesBoxItem
    {
        public string? text;
        public DialogueNodeDTO? node;
        public override string ToString() => text ?? "";
    }
}
