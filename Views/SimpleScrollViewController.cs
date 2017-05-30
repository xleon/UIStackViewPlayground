using System;
using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class SimpleScrollViewController : BaseViewController
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var stackView = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 4,
                LayoutMargins = new UIEdgeInsets(4, 4, 4, 4), // apply padding
                LayoutMarginsRelativeArrangement = true // apply padding
            };

            var scroll = new UIScrollView {TranslatesAutoresizingMaskIntoConstraints = false};
            Add(scroll);

            scroll.AddSubview(stackView);
            scroll.FullSizeOf(View);

            stackView.FullSizeOf(scroll);

            // constraint needed when the UIStackView is inside the UIScrollView
            stackView.WidthAnchor.ConstraintEqualTo(scroll.WidthAnchor).Active = true; 

            for (var i = 0; i < 20; i++)
                stackView.AddArrangedSubview(GetRandomView());
        }

        private static UIView GetRandomView()
        {
            var view = new RoundedView {BackgroundColor = ColorUtil.GetRandomColor()};
            var randomHeight = Random.Next(25, 200);
            view.HeightAnchor.ConstraintEqualTo(randomHeight).Active = true;
            return view;
        }
    }
}