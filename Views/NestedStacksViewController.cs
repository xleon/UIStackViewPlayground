using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class NestedStacksViewController : BaseViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Main scrolled stack

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

            var scroll = new UIScrollView { TranslatesAutoresizingMaskIntoConstraints = false };
            Add(scroll);

            scroll.AddSubview(stackView);
            scroll.FullSizeOf(View);

            stackView.FullSizeOf(scroll);
            stackView.WidthAnchor.ConstraintEqualTo(scroll.WidthAnchor).Active = true;

            // Nested stacks

            var nested1 = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.FillEqually,
                Spacing = 4
            };

            var nested2 = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 4
            };

            var nested3 = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.FillProportionally,
                Spacing = 4
            };

            var nested4 = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Top,
                Distribution = UIStackViewDistribution.EqualCentering,
                Spacing = 4
            };

            var nested5 = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 4
            };

            for (var i = 0; i < 3; i++)
            {
                nested1.AddArrangedSubview(GetRandomView());
                nested2.AddArrangedSubview(GetRandomView());
                nested3.AddArrangedSubview(GetRandomView());
                nested4.AddArrangedSubview(GetRandomView(50, 15 * (i + 1)));
            }

            stackView.AddArrangedSubview(GetNestedStackContainer("Fill Equally", nested1));
            stackView.AddArrangedSubview(GetNestedStackContainer("Equal Spacing", nested2));
            stackView.AddArrangedSubview(GetNestedStackContainer("Fill Proportionally", nested3));
            
            for(var i = 0; i < 30; i++)
                nested5.AddArrangedSubview(GetRandomView());

            var nestedScroll = new UIScrollView { TranslatesAutoresizingMaskIntoConstraints = false };
            nestedScroll.AddSubview(nested5);

            stackView.AddArrangedSubview(GetNestedStackContainer("Nested stack with scroll", nestedScroll));

            nested5.FullSizeOf(nestedScroll);
            nested5.HeightAnchor.ConstraintEqualTo(nestedScroll.HeightAnchor).Active = true;

            stackView.AddArrangedSubview(GetNestedStackContainer("Align Top", nested4));
        }

        private static UIView GetNestedStackContainer(string title, UIView stackView)
        {
            var view = new RoundedView {BackgroundColor = UIColor.LightGray};
            view.Layer.BorderColor = UIColor.Gray.CGColor;
            view.HeightAnchor.ConstraintEqualTo(110).Active = true;

            var label = new UILabel {Text = title, TranslatesAutoresizingMaskIntoConstraints = false};
            view.Add(label);
            view.Add(stackView);

            NSLayoutConstraint.ActivateConstraints(new []
            {
                label.LeadingAnchor.ConstraintEqualTo(view.LeadingAnchor, 10),
                label.TrailingAnchor.ConstraintEqualTo(view.TrailingAnchor, -10),
                label.TopAnchor.ConstraintEqualTo(view.TopAnchor, 8),
                label.HeightAnchor.ConstraintEqualTo(22),

                stackView.LeadingAnchor.ConstraintEqualTo(label.LeadingAnchor),
                stackView.TrailingAnchor.ConstraintEqualTo(label.TrailingAnchor),
                stackView.TopAnchor.ConstraintEqualTo(label.BottomAnchor, 5),
                stackView.BottomAnchor.ConstraintEqualTo(view.BottomAnchor, -8)
            });

            return view;
        }

        private static UIView GetRandomView(float width = 50, float? height = null)
        {
            var view = new RoundedView { BackgroundColor = ColorUtil.GetRandomColor() };
            view.WidthAnchor.ConstraintEqualTo(width).Active = true;

            if (height != null)
                view.HeightAnchor.ConstraintEqualTo(height.Value).Active = true;

            return view;
        }
    }
}