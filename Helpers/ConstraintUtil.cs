using UIKit;

namespace UIStackViewPlayground.Helpers
{
    public static class ConstraintUtil
    {
        public static NSLayoutConstraint[] FullSizeOf(this UIView origin, UIView target, float margin = 0) 
            => origin.FullSizeOf(target, new UIEdgeInsets(margin, margin, margin, margin));

        public static NSLayoutConstraint[] FullSizeOf(this UIView origin, UIView target, UIEdgeInsets edges)
        {
            origin.TranslatesAutoresizingMaskIntoConstraints = false;
            var contraints = new[]
            {
                origin.LeadingAnchor.ConstraintEqualTo(target.LeadingAnchor, edges.Left),
                origin.TrailingAnchor.ConstraintEqualTo(target.TrailingAnchor, -edges.Right),
                origin.TopAnchor.ConstraintEqualTo(target.TopAnchor, edges.Top),
                origin.BottomAnchor.ConstraintEqualTo(target.BottomAnchor, -edges.Bottom)
            };

            NSLayoutConstraint.ActivateConstraints(contraints);
            return contraints;
        }

        public static NSLayoutConstraint[] CenterIn(this UIView origin, UIView target)
        {
            origin.TranslatesAutoresizingMaskIntoConstraints = false;
            var contraints = new[]
            {
                origin.CenterXAnchor.ConstraintEqualTo(target.CenterXAnchor),
                origin.CenterYAnchor.ConstraintEqualTo(target.CenterYAnchor)
            };

            NSLayoutConstraint.ActivateConstraints(contraints);
            return contraints;
        }

        public static NSLayoutConstraint[] ConstraintSize(this UIView origin, float width, float height)
        {
            origin.TranslatesAutoresizingMaskIntoConstraints = false;
            var contraints = new[]
            {
                origin.WidthAnchor.ConstraintEqualTo(width),
                origin.HeightAnchor.ConstraintEqualTo(height)
            };

            NSLayoutConstraint.ActivateConstraints(contraints);
            return contraints;
        }

        public static TView ActivateConstraints<TView>(this TView origin, params NSLayoutConstraint[] constraints) 
            where TView : UIView
        {
            origin.TranslatesAutoresizingMaskIntoConstraints = false;
            NSLayoutConstraint.ActivateConstraints(constraints);
            return origin;
        }

        public static void SubviewsDoNotTranslateAutoresizingMaskIntoConstraints(this UIView origin)
        {
            foreach (var view in origin.Subviews)
            {
                view.TranslatesAutoresizingMaskIntoConstraints = false;
            }
        }
    }
}