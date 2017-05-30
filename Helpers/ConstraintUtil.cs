using UIKit;

namespace UIStackViewPlayground.Helpers
{
    public static class ConstraintUtil
    {
        public static NSLayoutConstraint[] FullSizeOf(this UIView origin, UIView target, float margin = 0)
        {
            var contraints = new[]
            {
                origin.LeadingAnchor.ConstraintEqualTo(target.LeadingAnchor, margin),
                origin.TrailingAnchor.ConstraintEqualTo(target.TrailingAnchor, -margin),
                origin.TopAnchor.ConstraintEqualTo(target.TopAnchor, margin),
                origin.BottomAnchor.ConstraintEqualTo(target.BottomAnchor, -margin)
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