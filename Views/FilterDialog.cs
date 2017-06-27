using System;
using System.Collections.Generic;
using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class FilterDialog : BaseDialogViewController<Dictionary<string, bool>>
    {
        private readonly Dictionary<string, bool> _options;
        private UIView _container;
        private UIButton _okButton;
        private UIStackView _stack;

        public FilterDialog(Dictionary<string, bool> options)
        {
            _options = options;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _container = new UIView { BackgroundColor = UIColor.White }
                .AddTo(View);

            _container.ActivateConstraints(
                _container.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
                _container.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
                _container.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
                _container.HeightAnchor.ConstraintEqualTo(120)
            );

            _okButton = new UIButton(UIButtonType.System)
                .AddTo(_container)
                .SetButtonTitle("Done");

            _okButton.ActivateConstraints(
                _okButton.RightAnchor.ConstraintEqualTo(_container.RightAnchor, -12),
                _okButton.TopAnchor.ConstraintEqualTo(_container.TopAnchor, 4)
            );

            var scroll = new UIScrollView
            {
                ContentInset = new UIEdgeInsets(0, 10, 0, 10),
                ShowsHorizontalScrollIndicator = false,
                Bounces = false

            }.AddTo(_container);

            scroll.FullSizeOf(_container, new UIEdgeInsets(35, 0, 0, 0));

            _stack = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 4

            }.AddTo(scroll);

            _stack.FullSizeOf(scroll);

            foreach (var option in _options)
            {
                _stack.AddArrangedSubview(new ToggleButton(option.Key, option.Value, 50, 75));
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _okButton.TouchUpInside += OkButtonOnTouchUpInside;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _okButton.TouchUpInside -= OkButtonOnTouchUpInside;
        }

        private void OkButtonOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            foreach (var view in _stack.ArrangedSubviews)
            {
                var button = (ToggleButton) view;
                _options[button.OptionKey] = button.Selected;
            }

            Dismiss(_options);
        }
    }

    public class ToggleButton : UIButton
    {
        public string OptionKey { get; set; }

        public ToggleButton(string optionKey, bool selected, float width, float height)
        {
            OptionKey = optionKey;

            WidthAnchor.ConstraintEqualTo(width).Active = true;
            HeightAnchor.ConstraintEqualTo(height).Active = true;

            Layer.CornerRadius = 5;

            Selected = selected;
            TouchUpInside += (sender, args) => Selected = !Selected;
        }

        public override bool Selected
        {
            get => base.Selected;
            set
            {
                BackgroundColor = value ? UIColor.Blue : UIColor.LightGray;
                base.Selected = value;
            }
        }
    }
}
