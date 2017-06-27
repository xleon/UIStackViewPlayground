using System.Threading.Tasks;
using UIKit;

namespace UIStackViewPlayground.Helpers
{
    public class BaseDialogViewController<TResult> 
        : UIViewController
    {
        private TaskCompletionSource<TResult> _tsc;
        private UIViewController _parent;
        private UIView _overlay;
        private UIView _hitArea;
        private UITapGestureRecognizer _gesture;
        private bool _animated;

        public Task<TResult> ShowAsync(UIViewController parentViewController, bool animated = true)
        {
            _animated = animated;

            ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            _tsc = new TaskCompletionSource<TResult>();

            _parent = parentViewController;
            _parent.PresentViewController(this, animated, null);

            return _tsc.Task;
        }

        public void Dismiss(TResult result = default(TResult))
            => _parent.DismissViewController(_animated, () => _tsc.SetResult(result));

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Clear;

            _overlay = new UIView
            {
                BackgroundColor = UIColor.Black,
                Alpha = 0.0f,
                UserInteractionEnabled = true,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            _parent.View.AddSubview(_overlay);
            _overlay.FullSizeOf(_parent.View);

            _hitArea = new UIView
            {
                BackgroundColor = UIColor.Clear,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            View.InsertSubview(_hitArea, 0);
            _hitArea.FullSizeOf(View);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _gesture = new UITapGestureRecognizer(() => Dismiss()) { CancelsTouchesInView = false };
            _hitArea.AddGestureRecognizer(_gesture);

            if (_animated)
                UIView.Animate(0.3f, () => _overlay.Alpha = 0.7f);
            else
                _overlay.Alpha = 0.7f;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            _hitArea.RemoveGestureRecognizer(_gesture);
            _overlay.RemoveFromSuperview();

            _gesture.Dispose();
            _gesture = null;
        }
    }
}
