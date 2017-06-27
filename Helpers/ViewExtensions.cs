using UIKit;

namespace UIStackViewPlayground.Helpers
{
    public static class ViewExtensions
    {
        public static TView AddTo<TView>(this TView origin, UIView target) 
            where TView : UIView
        {
            target.AddSubview(origin);
            return origin;
        }

        public static UIButton SetButtonTitle(this UIButton button, string title)
        {
            button.SetTitle(title, UIControlState.Normal);
            return button;
        }

        public static UIView SetBorder(this UIView view, float borderWidth, UIColor borderColor)
        {
            view.Layer.BorderColor = borderColor.CGColor;
            view.Layer.BorderWidth = borderWidth;
            return view;
        }
    }
}
