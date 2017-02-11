﻿using System;
using System.Linq;
using System.Threading;

using Xamarin.UITest;

using EntryCustomReturnSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

using EntryCustomReturn.Forms.Plugin.Abstractions;

namespace EntryCustomReturnUITests
{
	public class PickEntryReturnTypePage : BasePage
	{
		#region Constant Fields
		readonly Query _customizableEntry, _entryReturnTypePicker;
		#endregion

		#region Constructors
		public PickEntryReturnTypePage(IApp app, Platform platform) : base(app, platform, PageTitles.PickEntryReturnTypePageTitle)
		{
			_customizableEntry = x => x.Marked(AutomationIdConstants.CustomizableEntryAutomationId);
			_entryReturnTypePicker = x => x.Marked(AutomationIdConstants.EntryReturnTypePickerAutomationId);
		}
		#endregion

		#region Properties
		public string CustomizableEntryPlaceholder => TestHelpers.GetPlaceholderText(app, AutomationIdConstants.CustomizableEntryAutomationId);
		public string PickerText => app.Query(_entryReturnTypePicker)?.FirstOrDefault()?.Text;
		#endregion

		#region Methods
		public void SelectReturnTypeFromPicker(ReturnType returnType)
		{
			SelectReturnTypeFromPicker(returnType, _entryReturnTypePicker);
		}

		public void EnterText(string text)
		{
			app.ClearText(_customizableEntry);
			app.EnterText(_customizableEntry, text);
			app.DismissKeyboard();
		}

		void SelectReturnTypeFromPicker(ReturnType returnType, Query pickerQuery)
		{
			app.WaitForElement(pickerQuery);
			app.Tap(pickerQuery);

			ScrollToPickerLocation(returnType);

			SelectValueFromPicker(returnType);

			app.Screenshot($"Selected Return Type From Picker: {returnType.ToString()}");
		}

		void ScrollToPickerLocation(ReturnType returnType)
		{
			if (OnAndroid)
			{
				app.Query(x => x.Marked("select_dialog_listview").Invoke("smoothScrollToPosition", (int)returnType - 1));
			}
			else
			{
				app.Query(x => x.Class("UIPickerView").Invoke("selectRow", (int)returnType + 1, "inComponent", 0, "animated", true));

				var maxNumberInEnum = Enum.GetValues(typeof(ReturnType)).Length - 1;

				if (returnType == (ReturnType)maxNumberInEnum)
					app.Query(x => x.Class("UIPickerView").Invoke("selectRow", (int)returnType - 1, "inComponent", 0, "animated", true));
			}
		}

		void SelectValueFromPicker(ReturnType returnType)
		{
			if (OniOS && returnType.ToString() == PickerText)
			{
				var nextReturnType = returnType + 2;
				TapPickerValue(nextReturnType);
			}

			TapPickerValue(returnType);

			if (OniOS)
				TapDoneButtonOniOSPicker();

		}

		void TapPickerValue(ReturnType returnType)
		{
			Thread.Sleep(500);

			switch (OnAndroid)
			{
				case true:
					app.Tap(returnType.ToString());
					break;
					
				default:
					switch (returnType)
					{
						case ReturnType.Done:
							var doneQuery = app.Query("Done");
							var donePickerQuery = doneQuery?.Where(x => x.Class == "UILabel").LastOrDefault();

							var donePickerXCoordinate = donePickerQuery?.Rect?.CenterX ?? 500;
							var donePickerYCoordinate = donePickerQuery?.Rect?.CenterY ?? 0;

							app.TapCoordinates(donePickerXCoordinate, donePickerYCoordinate);
							break;

						default:
							app.Tap(returnType.ToString());
							break;
					}
					break;
			}
		}

		void TapDoneButtonOniOSPicker()
		{
			if (OnAndroid)
				return;
			
			var doneQuery = app.Query("Done");

			var pickerDoneButtonQuery = doneQuery?.Where(x => x.Class == "UIButtonLabel")?.FirstOrDefault();

			var pickerDoneButtonX = pickerDoneButtonQuery?.Rect?.CenterX ?? 500;
			var pickerDoneButtonY = pickerDoneButtonQuery?.Rect?.CenterY ?? 0;

			app.TapCoordinates(pickerDoneButtonX, pickerDoneButtonY);

		}
		#endregion
	}
}
