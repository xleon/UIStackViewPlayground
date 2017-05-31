using UIKit;

namespace UIStackViewPlayground.Helpers
{
    public static class ConstraintUtil
    {
        public static NSLayoutConstraint[] FullSizeOf(this UIView origin, UIView target, float margin = 0) 
            => origin.FullSizeOf(target, new UIEdgeInsets(margin, margin, margin, margin));

        public static NSLayoutConstraint[] FullSizeOf(this UIView origin, UIView target, UIEdgeInsets edges)
        {
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
            var contraints = new[]
            {
                origin.CenterXAnchor.ConstraintEqualTo(target.CenterXAnchor),
                origin.CenterYAnchor.ConstraintEqualTo(target.CenterYAnchor)
            };

            NSLayoutConstraint.ActivateConstraints(contraints);
            return contraints;
        }
    }
}