using System;
using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class SimpleScrollViewController : BaseViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create and add Views

            var stackView = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                // make the inner views to fill the whole available width
                Alignment = UIStackViewAlignment.Fill, 
                Distribution = UIStackViewDistribution.EqualSpacing,
                // margin between views
                Spacing = 4, 
                // apply padding between the parent view (the scroll) and the stack
                LayoutMargins = new UIEdgeInsets(4, 4, 4, 4),
                // this will make padding actually work
                LayoutMarginsRelativeArrangement = true 
            };

            var scroll = new UIScrollView();
            Add(scroll);

            scroll.AddSubview(stackView);

            // Add some views to the stack with random height and color
            // Notice that we use AddArrangedSubview rather than the former AddSubview
            // for UIStackView to manage it. Otherwise it won´t work
            for (var i = 0; i < 20; i++)
                stackView.AddArrangedSubview(GetRandomView());

            // Layout Views

            // For AutoLayout to work, all views and nested views need to set its 
            // "TranslatesAutoresizingMaskIntoConstraints" property to true. It´s easy
            // to forget it so I created an extension method that will set it to the view 
            // and its subviews
            scroll.EnableAutoLayout();

            // "FullSizeOf" is a method extension to set leading, trailing, 
            // bottom and top constraints
            scroll.FullSizeOf(View); 
            stackView.FullSizeOf(scroll);

            // constraint needed when the UIStackView is inside the UIScrollView
            stackView.WidthAnchor.ConstraintEqualTo(scroll.WidthAnchor).Active = true;
        }

        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        private static UIView GetRandomView()
        {
            var view = new UIView();
            view.BackgroundColor = ColorUtil.GetRandomColor();
            view.Layer.CornerRadius = 8;

            var randomHeight = Random.Next(25, 200);
            view.HeightAnchor.ConstraintEqualTo(randomHeight).Active = true;
            return view;
        }
    }
}