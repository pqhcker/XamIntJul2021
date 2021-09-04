using System;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Controls;

namespace XamIntJul2021.AppBase.Behaviors
{
    public class ViewUnfocusBehavior : Behavior<VisualElement>
    {
        protected override void OnAttachedTo(VisualElement bindable)
        {
            bindable.Unfocused += Bindable_Unfocused;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            bindable.Unfocused -= Bindable_Unfocused;
        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
                entry.Text = entry.Text?.ToUpperInvariant();

            if (sender is VisualElement element && element.GetParentPage() is BindedPage bindedPage)
                bindedPage.UnfocusSave();
        }
    }

    public static class ViewExtensions
    {
        public static Page GetParentPage(this VisualElement element)
        {
            var parent = element.Parent;

            while(parent is not null)
            {
                parent = parent.Parent;
                if(parent is Page page)
                {
                    return page;
                }
            }

            return null;
        }
    }
}
