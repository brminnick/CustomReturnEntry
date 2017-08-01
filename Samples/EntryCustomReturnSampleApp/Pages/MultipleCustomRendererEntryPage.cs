﻿using Xamarin.Forms;

using EntryCustomReturnSampleApp.Shared;

namespace EntryCustomReturnSampleApp
{
	public class MultipleCustomRendererEntryPage : BaseContentPage<MultipleEntryViewModel>
	{
		#region Constant Fields
		const bool _shouldUseEffects = false;
		#endregion

		#region Constructors
		public MultipleCustomRendererEntryPage(InputViewType inputViewType)
		{
			Title = PageTitles.MultipleEntryPageTitle;

			Padding = ViewHelpers.GetPagePadding();

			Content = ViewHelpers.CreateMultipleEntryPageLayout(inputViewType,false, ViewModel);
		}

		#endregion

		#region Methods
		protected override void SubscribeEventHandlers()
		{
			AreEventHandlersSubscribed = true;
		}

		protected override void UnsubscribeEventHandlers()
		{
			AreEventHandlersSubscribed = false;
		}
		#endregion
	}
}
