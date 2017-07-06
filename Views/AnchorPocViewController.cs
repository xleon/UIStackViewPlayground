using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class AnchorPocViewController : BaseViewController
    {
        private NSLayoutConstraint[] _small;
        private NSLayoutConstraint[] _big;
        private bool _isBig;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var box = new RoundedView { BackgroundColor = UIColor.Blue };
            View.Add(box);
            box.CenterIn(View);

            _small = box.ConstraintSize(50, 50);
            _big = box.ConstraintSize(150, 300, false);

            box.AddGestureRecognizer(new UITapGestureRecognizer(ChangeState) { CancelsTouchesInView = false });
        }

        private void ChangeState()
        {
            if (_isBig)
                View.ChangeState(_big, _small, 0.3);
            else
                View.ChangeState(_small, _big, 0.3);

            _isBig = !_isBig;
        }
    }
}
