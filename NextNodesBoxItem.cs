using MZDO;

namespace MSZDialougeManager
{
    public class NextNodesBoxItem
    {
        public string? text;
        public required DialogueNodeDTO node;
        public override string ToString() => text ?? "";
    }
}
