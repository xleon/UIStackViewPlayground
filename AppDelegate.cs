using Foundation;
using UIKit;
using UIStackViewPlayground.Views;

namespace UIStackViewPlayground
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            UINavigationBar.Appearance.BarTintColor = UIColor.White;

            var mainView = new UINavigationController(new MenuViewController());
            Window = new UIWindow(UIScreen.MainScreen.Bounds) {RootViewController = mainView};
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}


