using MZDO.Shared;

namespace MSZDialougeManager
{
    public static class DialogueHelpers
    {
        public static HashSet<int> GetReachableNodes(DialogueTreeDTO tree)
        {
            HashSet<int> reachable = [];
            Queue<int> queue = new();
            Dictionary<int, DialogueNodeDTO> nodesById = tree.nodes.ToDictionary(n => n.id);

            foreach (int startId in tree.startNodeIds)
                queue.Enqueue(startId);

            while (queue.Count > 0)
            {
                int id = queue.Dequeue();
                if (!reachable.Add(id)) continue;

                if (!nodesById.TryGetValue(id, out DialogueNodeDTO? node)) continue;

                foreach (int nextId in node.nextNodeIds)
                    queue.Enqueue(nextId);
            }
            return reachable;
        }
    }
}
