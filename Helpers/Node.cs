using System.Collections.Generic;

namespace UIStackViewPlayground.Helpers
{
    public class Node
    {
        public string Name { get; set; }
        public IList<Node> Children { get; set; }
    }
}