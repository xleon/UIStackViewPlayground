using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class TreeViewController : BaseViewController
    {
        private UIStackView _stackView;
        private List<Node> _tree;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _stackView = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 0
            };

            var scroll = new UIScrollView { TranslatesAutoresizingMaskIntoConstraints = false };
            Add(scroll);

            scroll.AddSubview(_stackView);
            scroll.FullSizeOf(View);

            _stackView.FullSizeOf(scroll);
            _stackView.WidthAnchor.ConstraintEqualTo(scroll.WidthAnchor).Active = true;

            _tree = GetTree();
            _tree.ForEach(node => _stackView.AddArrangedSubview(new NodeView(node, ToggleNode)));
        }

        private void ToggleNode(NodeView nodeView)
        {
            if (nodeView.Expanded)
                CollapseNode(nodeView);
            else
                ExpandNode(nodeView);
        }

        private void ExpandNode(NodeView nodeView)
        {
            if (!nodeView.Expanded && nodeView.Node.Children?.Count > 0)
            {
                var index = _stackView.ArrangedSubviews.ToList().IndexOf(nodeView);

                foreach (var node in nodeView.Node.Children)
                {
                    var view = new NodeView(node, ToggleNode, nodeView.Level + 1);
                    _stackView.InsertArrangedSubview(view, (nuint)(++index));
                }

                nodeView.Expanded = true;
            }
        }

        private void CollapseNode(NodeView nodeView, bool destroy = false)
        {
            if (nodeView.Node.Children?.Count > 0)
            {
                foreach (var child in nodeView.Node.Children)
                {
                    var view = _stackView.ArrangedSubviews
                        .FirstOrDefault(x => ((NodeView)x).Node.Equals(child)) as NodeView;

                    if (view != null)
                    {
                        CollapseNode(view, true);
                    }
                }
            }

            if(destroy)
                nodeView.RemoveFromSuperview();

            nodeView.Expanded = false;
        }

        private static List<Node> GetTree() => new List<Node>
        {
            new Node
            {
                Name = "Projects",
                Children = new List<Node>
                {
                    new Node { Name = "Project 1" },
                    new Node
                    {
                        Name = "Project 2",
                        Children = new List<Node>
                        {
                            new Node { Name = "SubProject 1" },
                            new Node { Name = "SubProject 2" },
                            new Node { Name = "SubProject 3" },
                            new Node { Name = "SubProject 4" },
                            new Node
                            {
                                Name = "SubProject 5",
                                Children = new List<Node>
                                {
                                    new Node { Name = "Member 1" },
                                    new Node { Name = "Member 2" },
                                    new Node { Name = "Member 3" },
                                    new Node { Name = "Member 4" },
                                    new Node { Name = "Member 5" }
                                }
                            }
                        }
                    },
                    new Node { Name = "Project 3" },
                    new Node { Name = "Project 4" },
                    new Node { Name = "Project 5" },
                    new Node { Name = "Project 6" },
                    new Node { Name = "Project 7" },
                }
            },
            new Node
            {
                Name = "To do",
                Children = new List<Node>
                {
                    new Node { Name = "Task 1" },
                    new Node { Name = "Task 2" },
                    new Node { Name = "Task 3" },
                    new Node { Name = "Task 4" },
                    new Node { Name = "Task 5" },
                    new Node { Name = "Task 6" },
                    new Node { Name = "Task 7" },
                    new Node { Name = "Task 8" },
                    new Node { Name = "Task 9" },
                    new Node { Name = "Task 10" },
                }
            },
            new Node
            {
                Name = "Days",
                Children = new List<Node>
                {
                    new Node { Name = "Day 1" },
                    new Node { Name = "Day 2" },
                    new Node { Name = "Day 3" },
                    new Node { Name = "Day 4" },
                    new Node { Name = "Day 5" },
                    new Node { Name = "Day 6" },
                    new Node { Name = "Day 7" },
                }
            },
        };
    }

    public class Node
    {
        public string Name { get; set; }
        public List<Node> Children { get; set; }
    }

    public class NodeView : UIButton
    {
        public Node Node { get; }
        public uint Level { get; }

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                _expanded = value;

                if (Node.Children?.Count > 0)
                    _icon.Image = UIImage.FromBundle(_expanded ? "less" : "plus");
            }
        }

        private readonly Action<NodeView> _toggleAction;
        private UIImageView _icon;
        private bool _expanded;

        private const uint Indent = 18;

        public NodeView(Node node, Action<NodeView> toggleAction, uint level = 0)
        {
            Level = level;
            Node = node;
            _toggleAction = toggleAction;

            Initialize();
        }

        private void Initialize()
        {
            SetTitleColor(UIColor.Black, UIControlState.Normal);
            SetTitle(Node.Name, UIControlState.Normal);

            HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            ContentEdgeInsets = new UIEdgeInsets(3, Indent + Indent * Level, 3, 5);

            TouchUpInside += delegate { _toggleAction?.Invoke(this); };

            if (Node.Children?.Count > 0)
            {
                _icon = new UIImageView
                {
                    ContentMode = UIViewContentMode.ScaleAspectFill,
                    Image = UIImage.FromBundle("plus"),
                    Frame = new CGRect(4 + Indent * Level, 7, 14, 14)
                };

                Add(_icon);
            }
        }
    }
}