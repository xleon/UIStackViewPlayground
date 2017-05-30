using System;
using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class MenuViewController : BaseViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.Title = "UIStackView Playground";
            View.BackgroundColor = UIColor.White;

            var menu = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Center,
                Spacing = 8
            };
            
            Add(menu);

            menu.CenterIn(View);

            menu.AddArrangedSubview(GetButton("Fill equally", typeof(FillEquallyViewController)));
            menu.AddArrangedSubview(GetButton("Simple scroll", typeof(SimpleScrollViewController)));
            menu.AddArrangedSubview(GetButton("Nested stacks", typeof(NestedStacksViewController)));
            menu.AddArrangedSubview(GetButton("Toggle animated", typeof(ToggleAnimationViewController)));
            menu.AddArrangedSubview(GetButton("Accordion", typeof(AccordionViewController)));
            menu.AddArrangedSubview(GetButton("Tree", typeof(TreeViewController)));
        }

        private UIButton GetButton(string title, Type controllerType)
        {
            var button = new UIButton(UIButtonType.System);
            button.SetTitle(title, UIControlState.Normal);
            button.TouchUpInside += (sender, args) =>
            {
                var controller = (UIViewController) Activator.CreateInstance(controllerType);
                controller.NavigationItem.Title = title;
                controller.View.BackgroundColor = UIColor.White;
                controller.EdgesForExtendedLayout = UIRectEdge.None;

                NavigationController.PushViewController(controller, true);
            };

            return button;
        }
    }
}