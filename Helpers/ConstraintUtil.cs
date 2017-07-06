using System.Linq;
using UIKit;

namespace UIStackViewPlayground.Helpers
{
    public static class ConstraintUtil
    {
        public static NSLayoutConstraint[] FullSizeOf(this UIView origin, 
            UIView target, float margin = 0, bool activate = true) 
            => origin.FullSizeOf(target, new UIEdgeInsets(margin, margin, margin, margin), activate);

        public static NSLayoutConstraint[] FullSizeOf(this UIView origin, 
            UIView target, UIEdgeInsets edges, bool activate = true)
        {
            origin.TranslatesAutoresizingMaskIntoConstraints = false;
            var contraints = new[]
            {
                origin.LeadingAnchor.ConstraintEqualTo(target.LeadingAnchor, edges.Left),
                origin.TrailingAnchor.ConstraintEqualTo(target.TrailingAnchor, -edges.Right),
                origin.TopAnchor.ConstraintEqualTo(target.TopAnchor, edges.Top),
                origin.BottomAnchor.ConstraintEqualTo(target.BottomAnchor, -edges.Bottom)
            };

            if(activate)
                NSLayoutConstraint.ActivateConstraints(contraints);

            return contraints;
        }

        public static NSLayoutConstraint[] CenterIn(this UIView origin, 
            UIView target, bool activate = true)
        {
            origin.TranslatesAutoresizingMaskIntoConstraints = false;
            var contraints = new[]
            {
                origin.CenterXAnchor.ConstraintEqualTo(target.CenterXAnchor),
                origin.CenterYAnchor.ConstraintEqualTo(target.CenterYAnchor)
            };

            if (activate)
                NSLayoutConstraint.ActivateConstraints(contraints);

            return contraints;
        }

        public static NSLayoutConstraint[] ConstraintSize(this UIView origin, 
            float width, float height, bool activate = true)
        {
            origin.TranslatesAutoresizingMaskIntoConstraints = false;
            var contraints = new[]
            {
                origin.WidthAnchor.ConstraintEqualTo(width),
                origin.HeightAnchor.ConstraintEqualTo(height)
            };

            if (activate)
                NSLayoutConstraint.ActivateConstraints(contraints);

            return contraints;
        }

        public static void ChangeState(this UIView parentView, 
            NSLayoutConstraint[] before, 
            NSLayoutConstraint[] after, 
            double duration = 0)
        {
            parentView.LayoutIfNeeded();

            NSLayoutConstraint.DeactivateConstraints(before);
            NSLayoutConstraint.ActivateConstraints(after);

            if (duration > 0)
            {
                UIView.Animate(duration, parentView.LayoutIfNeeded);
            }
        }

        public static TView ActivateConstraints<TView>(this TView origin, 
            params NSLayoutConstraint[] constraints) 
            where TView : UIView
        {
            origin.TranslatesAutoresizingMaskIntoConstraints = false;
            NSLayoutConstraint.ActivateConstraints(constraints); 
            return origin;
        }

        public static void EnableAutoLayout(this UIView origin)
        {
            origin.TranslatesAutoresizingMaskIntoConstraints = false;

            foreach (var view in origin.Subviews)
            {
                view.TranslatesAutoresizingMaskIntoConstraints = false;
            }
        }
    }
}