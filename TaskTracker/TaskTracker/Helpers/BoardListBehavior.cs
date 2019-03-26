using System;
using System.Windows.Input;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.PopupLayout;
using TaskTracker.Models;
using TaskTracker.Resources;
using Xamarin.Forms;

namespace TaskTracker.Helpers
{
    class BoardListBehavior : Behavior<SfListView>
    {
        private SfListView _listView;
        private Board _selectedItem;
        private SfPopupLayout _popup;

        public ICommand DeleteItemCommand
        {
            get => (ICommand)GetValue(DeleteItemProperty);
            set => SetValue(DeleteItemProperty, value);
        }

        public static readonly BindableProperty DeleteItemProperty =
            BindableProperty.Create("DeleteItemCommand", typeof(ICommand), typeof(ICommand));

        //public ICommand EditItemCommand
        //{
        //    get => (ICommand)GetValue(DeleteItemProperty);
        //    set => SetValue(DeleteItemProperty, value);
        //}

        //public static readonly BindableProperty DeleteItemProperty =
        //    BindableProperty.Create("DeleteItemCommand", typeof(ICommand), typeof(ICommand));

        protected override void OnAttachedTo(SfListView listView)
        {
            _listView = listView;

            _listView.ItemHolding += ListView_ItemHolding;
            _listView.ScrollStateChanged += ListView_ScrollStateChanged;
            _listView.ItemTapped += ListView_ItemTapped;

            base.OnAttachedTo(listView);
        }

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            _popup.Dismiss();
        }

        private void ListView_ScrollStateChanged(object sender, ScrollStateChangedEventArgs e)
        {
            _popup.Dismiss();
        }

        private void ListView_ItemHolding(object sender, ItemHoldingEventArgs e)
        {
            _selectedItem = e.ItemData as Board;

            _popup = new SfPopupLayout();

            _popup.PopupView.HeightRequest = 100;
            _popup.PopupView.WidthRequest = 150;

            _popup.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                var mainStack = new StackLayout();
                mainStack.BackgroundColor = Color.BlanchedAlmond;

                var deleteButton = new Button()
                {
                    Text = AppResources.Delete,
                    HeightRequest = 50,
                    TextColor = Color.Black
                };
                deleteButton.Clicked += DeleteButtonClicked;

                var sortButton = new Button()
                {
                    Text = AppResources.Edit,
                    HeightRequest = 50,
                    TextColor = Color.Black
                };
                sortButton.Clicked += EditButtonClicked;
                mainStack.Children.Add(deleteButton);
                mainStack.Children.Add(sortButton);
                return mainStack;

            });

            _popup.PopupView.ShowHeader = false;
            _popup.PopupView.ShowFooter = false;

            if (e.Position.Y + 100 <= _listView.Height && e.Position.X + 100 > _listView.Width)
                _popup.Show(e.Position.X - 100, e.Position.Y);
            else if (e.Position.Y + 100 > _listView.Height && e.Position.X + 100 < _listView.Width)
                _popup.Show(e.Position.X, e.Position.Y - 100);
            else if (e.Position.Y + 100 > _listView.Height && e.Position.X + 100 > _listView.Width)
                _popup.Show(e.Position.X - 100, e.Position.Y - 100);
            else
                _popup.Show(e.Position.X, e.Position.Y);
        }

        private void DeleteButtonClicked(object sender, EventArgs e)
        {
            DeleteItemCommand?.Execute(_selectedItem);
        }

        private void EditButtonClicked(object sender, EventArgs e)
        {

        }

        private void Dismiss()
        {
            _popup.IsVisible = false;
        }
    }
}
