﻿using System;
using System.Windows.Input;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.PopupLayout;
using TaskTracker.Resources;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.Helpers.Behaviors
{
    class BoardListBehavior : Behavior<SfListView>
    {
        private SfListView _listView;
        private BoardVM _selectedItem;
        private SfPopupLayout _popup;

        public static readonly BindableProperty DeleteItemCommandProperty =
            BindableProperty.Create(nameof(DeleteItemCommand), typeof(ICommand), typeof(BoardListBehavior));

        public ICommand DeleteItemCommand {
            get => (ICommand)GetValue(DeleteItemCommandProperty);
            set => SetValue(DeleteItemCommandProperty, value);
        }

        public static readonly BindableProperty EditItemCommandProperty =
            BindableProperty.Create(nameof(EditItemCommand), typeof(ICommand), typeof(BoardListBehavior));

        public ICommand EditItemCommand {
            get => (ICommand)GetValue(EditItemCommandProperty);
            set => SetValue(EditItemCommandProperty, value);
        }

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
            _popup?.Dismiss();
        }

        private void ListView_ScrollStateChanged(object sender, ScrollStateChangedEventArgs e)
        {
            _popup?.Dismiss();
        }

        private void ListView_ItemHolding(object sender, ItemHoldingEventArgs e)
        {
            _selectedItem = e.ItemData as BoardVM;

            _popup = new SfPopupLayout();

            _popup.PopupView.ShowHeader = false;
            _popup.PopupView.ShowFooter = false;
            _popup.PopupView.HeightRequest = 100;
            _popup.PopupView.WidthRequest = 200;

            _popup.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                var mainStack = new FlexLayout();
                mainStack.Direction = FlexDirection.Column;
                mainStack.BackgroundColor = Color.FromHex("#E0E0E0");

                var deleteButton = new Button()
                {
                    Text = AppResources.Delete,
                    TextColor = Color.Black,
                    HeightRequest = 50,
                    BackgroundColor = Color.FromHex("#E0E0E0")
                };
                deleteButton.Clicked += DeleteButtonClicked;
                deleteButton.VerticalOptions = LayoutOptions.StartAndExpand;

                var editButton = new Button()
                {
                    Text = AppResources.Edit,
                    TextColor = Color.Black,
                    HeightRequest = 50,
                    BackgroundColor = Color.FromHex("#E0E0E0")
                };
                editButton.Clicked += EditButtonClicked;
                deleteButton.VerticalOptions = LayoutOptions.EndAndExpand;

                mainStack.Children.Add(editButton);
                mainStack.Children.Add(deleteButton);

                return mainStack;
            });

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
            _popup.IsOpen = false;
        }

        private void EditButtonClicked(object sender, EventArgs e)
        {
            EditItemCommand?.Execute(_selectedItem);
            _popup.IsOpen = false;
        }

        private void Dismiss()
        {
            _popup.IsVisible = false;
        }
    }
}
