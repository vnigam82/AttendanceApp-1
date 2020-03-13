﻿using System;
using System.Collections.Generic;
using AttendanceApp.ViewModels;
using Xamarin.Forms;

namespace AttendanceApp.CustomViews
{
    public partial class HeaderView : ContentView
    {
        public HeaderView()
        {
            InitializeComponent();
            BindingContext = new HeaderViewModel(Navigation);
            btnBack.IsVisible = IsBackVisible;
            btnHome.IsVisible = IsHomeVisible;
            btnMenu.IsVisible = IsMenuVisible;
            lblTitle.Text = HeaderTitle;
        }
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == HeaderTitleProperty.PropertyName)
            {
                lblTitle.Text = HeaderTitle;
            }
            if (propertyName == IsBackVisibleProperty.PropertyName)
            {
                btnBack.IsVisible = IsBackVisible;
            }
            if (propertyName == IsHomeVisibleProperty.PropertyName)
            {
                btnHome.IsVisible = IsHomeVisible;
            }
            if (propertyName == IsMenuVisibleProperty.PropertyName)
            {
                btnMenu.IsVisible = IsMenuVisible;
            }
        }
        #region Bindable Property

        public static readonly BindableProperty HeaderTitleProperty =
              BindableProperty.Create(nameof(HeaderTitle),
              typeof(string), typeof(HeaderView),
              "",
              BindingMode.TwoWay);
    
        public static readonly BindableProperty IsBackVisibleProperty =
               BindableProperty.Create(nameof(IsBackVisible),
               typeof(bool), typeof(HeaderView),
               false,
               BindingMode.TwoWay);

        public static readonly BindableProperty IsMenuVisibleProperty =
               BindableProperty.Create(nameof(IsMenuVisible),
               typeof(bool), typeof(HeaderView),
               false,
               BindingMode.TwoWay);

        public static readonly BindableProperty IsHomeVisibleProperty =
               BindableProperty.Create(nameof(IsHomeVisible),
               typeof(bool), typeof(HeaderView),
               false,
               BindingMode.TwoWay);

        #endregion

        #region Property
        public string HeaderTitle
        {
            get => (string)GetValue(HeaderTitleProperty);
            set => SetValue(HeaderTitleProperty, value);
        }
        public bool IsBackVisible
        {
            get => (bool)GetValue(IsBackVisibleProperty);
            set => SetValue(IsBackVisibleProperty, value);
        }
        public bool IsMenuVisible
        {
            get => (bool)GetValue(IsMenuVisibleProperty);
            set => SetValue(IsMenuVisibleProperty, value);
        }
        public bool IsHomeVisible
        {
            get => (bool)GetValue(IsHomeVisibleProperty);
            set => SetValue(IsHomeVisibleProperty, value);
        }
        #endregion
    }
}
