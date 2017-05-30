using System;
using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class AccordionViewController : BaseViewController
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        private UIStackView _stackView;
        private UIView _visibleContent;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _stackView = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Fill,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 1
            };

            var scroll = new UIScrollView { TranslatesAutoresizingMaskIntoConstraints = false };
            Add(scroll);

            scroll.AddSubview(_stackView);
            scroll.FullSizeOf(View);

            _stackView.FullSizeOf(scroll);
            _stackView.WidthAnchor.ConstraintEqualTo(scroll.WidthAnchor).Active = true;

            for (var i = 0; i < 25; i++)
            {
                _stackView.AddArrangedSubview(GetButton($"Category {i + 1}", i));
                _stackView.AddArrangedSubview(GetContent($"Child of category {i + 1}", i + 100));
            }
        }

        private UIButton GetButton(string title, int tag)
        {
            var button = new UIButton
            {
                BackgroundColor = UIColor.Blue,
                HorizontalAlignment = UIControlContentHorizontalAlignment.Left,
                ContentEdgeInsets = new UIEdgeInsets(4, 10, 4, 10),
                ClipsToBounds = true,
                Tag = tag
            };

            button.SetTitle(title, UIControlState.Normal);

            button.TouchUpInside += (sender, args) =>
            {
                var content = View.ViewWithTag(((UIButton) sender).Tag + 100);
                UIView.Animate(0.3, () =>
                {
                    if (_visibleContent != null)
                        _visibleContent.Hidden = true;

                    if(!Equals(_visibleContent, content))
                        content.Hidden = !content.Hidden;

                }, () => _visibleContent = content.Hidden ? null : content);
            };

            return button;
        }

        private static UILabel GetContent(string title, int tag)
        {
            var content = new UILabel
            {
                Text = title,
                Lines = 0,
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.LightGray,
                Tag = tag,
                Hidden = true
            };

            content.HeightAnchor.ConstraintEqualTo(Random.Next(40, 250)).Active = true;
            return content;
        }
    }
}