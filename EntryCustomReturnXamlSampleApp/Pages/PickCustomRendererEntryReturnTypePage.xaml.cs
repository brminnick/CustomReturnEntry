﻿using Xamarin.Forms;

using EntryCustomReturn.Forms.Plugin.Abstractions;

using MvvmSamples.Common.Forms;

namespace EntryCustomReturnXamlSampleApp
{
    public partial class PickCustomRendererEntryReturnTypePage : BaseContentPage<PickEntryReturnTypeViewModel>
    {
        public PickCustomRendererEntryReturnTypePage()
        {
            InitializeComponent();

            CustomizableEntry.SetBinding(CustomReturnEntry.ReturnTypeProperty, nameof(ViewModel.EntryReturnType));
        }

        protected override void SubscribeEventHandlers()
        {
            
        }

        protected override void UnsubscribeEventHandlers()
        {
            
        }
    }
}