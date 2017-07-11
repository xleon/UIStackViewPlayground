using System;
using System.Collections.Generic;
using System.Diagnostics;
using UIKit;
using UIStackViewPlayground.Helpers;

namespace UIStackViewPlayground.Views
{
    public class MenuViewController : BaseViewController
    {
        private Dictionary<string, bool> _options = new Dictionary<string, bool>
        {
            {"Op1", false},
            {"Op2", true},
            {"Op3", true},
            {"Op4", false},
            {"Op5", false},
            {"Op6", true},
            {"Op7", false},
            {"Op8", false},
        };

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.Title = "UIStackView Playground";

            var menu = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Center,
                Spacing = 8
            };
            
            Add(menu);

            menu.CenterIn(View);

            menu.AddArrangedSubview(GetButton("Anchor constraints POC", typeof(AnchorPocViewController)));
            menu.AddArrangedSubview(GetButton("Animate axis change", typeof(AnimateAxisChangeViewController)));
            menu.AddArrangedSubview(GetButton("Simple scroll", typeof(SimpleScrollViewController)));
            menu.AddArrangedSubview(GetButton("Nested stacks", typeof(NestedStacksViewController)));
            menu.AddArrangedSubview(GetButton("Toggle animated", typeof(ToggleAnimationViewController)));
            menu.AddArrangedSubview(GetButton("Accordion", typeof(AccordionViewController)));
            menu.AddArrangedSubview(GetButton("Tree", typeof(TreeViewController)));
            
            menu.AddArrangedSubview(GetButton("Alert dialog", action: async() =>
            {
                const string message = "This alert can change its visual state with a combination of 2 stacks. " +
                                       "One vertical and one horizontal for the buttons. \n\n" +
                                       "Click an option and see it in action";

                var alert = new AlertDialogViewController("Choose an option", message, "Accept", "Cancel");
                var result = await alert.ShowAsync(this) ? "Accepted" : "Cancelled";
                await new AlertDialogViewController(result).ShowAsync(this, false);
            }));

            menu.AddArrangedSubview(GetButton("Filter dialog", action: async () =>
            {
                var filterDialog = new FilterDialog(_options);
                var result = await filterDialog.ShowAsync(this);
                if (result == null) return;

                foreach (var option in result)
                    Debug.WriteLine($"{option.Key}: {option.Value}");

                _options = result;
            }));
        }

        private UIButton GetButton(string title, Type controllerType = null, Action action = null)
        {
            var button = new UIButton(UIButtonType.System);
            button.SetTitle(title, UIControlState.Normal);
            button.TouchUpInside += (sender, args) =>
            {
                if (action == null)
                {
                    var controller = (UIViewController)Activator.CreateInstance(controllerType);
                    controller.NavigationItem.Title = title;

                    NavigationController.PushViewController(controller, true);
                }
                else
                {
                    action();
                }
            };

            return button;
        }
    }
}