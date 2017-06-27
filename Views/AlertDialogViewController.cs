using System;
using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class AlertDialogViewController : BaseDialogViewController<bool>
    {
        private readonly string _title;
        private readonly string _message;
        private readonly string _ok;
        private readonly string _cancel;

        private UIButton _okButton;
        private UIButton _cancelButton;
        private UILabel _titleLabel;
        private UILabel _messageLabel;
        private UIView _container;

        public AlertDialogViewController(
            string title, 
            string message = null, 
            string ok = "OK", 
            string cancel = null)
        {
            _title = title;
            _message = message;
            _ok = ok;
            _cancel = cancel;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _container = new RoundedView(4) { BackgroundColor = UIColor.Blue, ClipsToBounds = true }
                .SetBorder(1, UIColor.White)
                .AddTo(View);

            _container.ActivateConstraints(_container.WidthAnchor.ConstraintLessThanOrEqualTo(View.WidthAnchor, 0.7f));
            _container.CenterIn(View);

            var stack = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Center,
                Spacing = 8

            }.AddTo(_container);

            var buttonStack = new UIStackView
            {
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.Fill,
                Spacing = 12
            };

            _titleLabel = new UILabel
            {
                Text = _title,
                Font = UIFont.BoldSystemFontOfSize(16),
                TextColor = UIColor.White,
                Lines = 0
            };

            _messageLabel = new UILabel
            {
                Text = _message,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(15),
                TextColor = UIColor.White,
                Lines = 0
            };

            var separator = new UIView { BackgroundColor = UIColor.White };

            separator.ConstraintSize(200, 1);

            _okButton = new UIButton();
            _okButton.SetTitle(_ok, UIControlState.Normal);
            _okButton.SetTitleColor(UIColor.White, UIControlState.Normal);

            _cancelButton = new UIButton();
            _cancelButton.SetTitle(_cancel, UIControlState.Normal);
            _cancelButton.SetTitleColor(UIColor.White, UIControlState.Normal);

            stack.AddArrangedSubview(_titleLabel);
            stack.AddArrangedSubview(_messageLabel);
            stack.AddArrangedSubview(separator);
            stack.AddArrangedSubview(buttonStack);
            stack.FullSizeOf(_container, 10);

            buttonStack.AddArrangedSubview(_okButton);
            buttonStack.AddArrangedSubview(_cancelButton);

            _messageLabel.Hidden = _message == null;
            _cancelButton.Hidden = _cancel == null;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _okButton.TouchUpInside += OkButtonOnTouchUpInside;
            _cancelButton.TouchUpInside += CancelButtonOnTouchUpInside;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _okButton.TouchUpInside -= OkButtonOnTouchUpInside;
            _cancelButton.TouchUpInside -= CancelButtonOnTouchUpInside;
        }

        private void OkButtonOnTouchUpInside(object sender, EventArgs eventArgs)
            => Dismiss(true);

        private void CancelButtonOnTouchUpInside(object sender, EventArgs eventArgs)
            => Dismiss();
    }
}
