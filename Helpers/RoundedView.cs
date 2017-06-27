using UIKit;

namespace UIStackViewPlayground.Helpers
{
    public class RoundedView : UIView
    {
        public RoundedView(float radius = 8)
        {
            Layer.CornerRadius = radius;
        }
    }
}