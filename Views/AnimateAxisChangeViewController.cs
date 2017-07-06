using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class AnimateAxisChangeViewController : BaseViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var stackView = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.FillEqually,
                Spacing = 4
            };

            Add(stackView);

            stackView.FullSizeOf(View, 4);

            for (var i = 0; i < 3; i++)
                stackView.AddArrangedSubview(new RoundedView { BackgroundColor = ColorUtil.GetRandomColor()});

            var axisButton = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (s, e) =>
            {
                UIView.Animate(0.3, () =>
                {
                    stackView.Axis = stackView.Axis == UILayoutConstraintAxis.Vertical
                        ? UILayoutConstraintAxis.Horizontal
                        : UILayoutConstraintAxis.Vertical;

                    stackView.LayoutIfNeeded();
                });
            });

            NavigationItem.SetRightBarButtonItem(axisButton, true);
        }
    }
}