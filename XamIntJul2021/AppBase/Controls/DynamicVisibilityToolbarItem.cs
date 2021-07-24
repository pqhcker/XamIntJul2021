using System;
using Xamarin.Forms;

namespace XamIntJul2021.AppBase.Controls
{
    public class DynamicVisibilityToolbarItem : ToolbarItem
    {
        public static BindableProperty IsVisibleProperty =
            BindableProperty.Create(nameof(IsVisible), typeof(bool),
                typeof(DynamicVisibilityToolbarItem), false, BindingMode.TwoWay, propertyChanged: OnVisibleChanged);

        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        private static void OnVisibleChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var toolBarItem = bindableObject as DynamicVisibilityToolbarItem;

            if (oldValue != newValue && toolBarItem.Parent is not null)
            {
                var toolBarItems = ((ContentPage)toolBarItem.Parent).ToolbarItems;

                if ((bool)newValue && !toolBarItems.Contains(toolBarItem))
                    toolBarItems.Add(toolBarItem);
                else if (!(bool)newValue && toolBarItems.Contains(toolBarItem))
                    toolBarItems.Remove(toolBarItem);

            }
        }

        protected override void OnParentSet()
        {
            if (Parent is not null)
                OnVisibleChanged(this, !IsVisible, IsVisible);
        }
    }
}
