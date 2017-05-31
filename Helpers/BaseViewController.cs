using UIKit;

namespace UIStackViewPlayground.Helpers
{
    public class BaseViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if(NavigationController != null)
                NavigationController.NavigationBar.Translucent = false;

            EdgesForExtendedLayout = UIRectEdge.None;

            View.BackgroundColor = UIColor.White;
        }
    }
}