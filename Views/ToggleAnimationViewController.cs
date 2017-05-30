using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class ToggleAnimationViewController : BaseViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var stack = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.FillEqually,
                Spacing = 4
            };

            Add(stack);

            stack.FullSizeOf(View);

            var addButton = new UIButton(UIButtonType.System);
            addButton.SetTitle("Add view", UIControlState.Normal);
            addButton.TouchUpInside += (s, e) =>
            {
                stack.InsertArrangedSubview(GetAutoDestroyButton(), 1);
                UIView.Animate(0.3, () => stack.LayoutIfNeeded());
            };
            
            stack.AddArrangedSubview(addButton);
        }

        private static UIButton GetAutoDestroyButton()
        {
            var button = new UIButton {BackgroundColor = ColorUtil.GetRandomColor(true), ClipsToBounds = true};
            button.SetTitle("Destroy", UIControlState.Normal);
            button.TouchUpInside += (sender, args) 
                => UIView.Animate(0.3, () => button.Hidden = true, button.RemoveFromSuperview);

            return button;
        }
    }
}