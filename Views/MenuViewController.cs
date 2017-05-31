﻿using System;
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

            var menu = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Center,
                Spacing = 8
            };
            
            Add(menu);

            menu.CenterIn(View);

            menu.AddArrangedSubview(GetButton("Animate axis change", typeof(AnimateAxixChangeViewController)));
            menu.AddArrangedSubview(GetButton("Simple scroll", typeof(SimpleScrollViewController)));
            menu.AddArrangedSubview(GetButton("Nested stacks", typeof(NestedStacksViewController)));
            menu.AddArrangedSubview(GetButton("Toggle animated", typeof(ToggleAnimationViewController)));
            menu.AddArrangedSubview(GetButton("Accordion", typeof(AccordionViewController)));
            menu.AddArrangedSubview(GetButton("Tree", typeof(TreeViewController)));
            menu.AddArrangedSubview(GetButton("Form states", typeof(FormStatesViewController)));
        }

        private UIButton GetButton(string title, Type controllerType)
        {
            var button = new UIButton(UIButtonType.System);
            button.SetTitle(title, UIControlState.Normal);
            button.TouchUpInside += (sender, args) =>
            {
                var controller = (UIViewController) Activator.CreateInstance(controllerType);
                controller.NavigationItem.Title = title;

                NavigationController.PushViewController(controller, true);
            };

            return button;
        }
    }
}