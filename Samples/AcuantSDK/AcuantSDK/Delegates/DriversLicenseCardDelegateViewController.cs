using System;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using AcuantMobileSDK;
using UIKit;

namespace AcuantMobileSDK_iOS_Sample
{
	public class DriversLicenseCardDelegateViewController : BaseDelegateViewController,
	IAcuantMobileSDKControllerProcessingDelegateForDriversLicense
	{
		public DriversLicenseCardDelegateViewController(string licenseKey)
			: base(licenseKey)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			cardType = AcuantCardType.DriversLicenseCard;

			bool? isAllowed = instance.IsAssureIDAllowed;
			if (isAllowed.HasValue && isAllowed.Value)
				instance?.SetWidth(2024);
			else
				instance?.SetWidth(1250);
		}

		public override void ProcessCard()
		{
			UserDialogs.Instance.ShowLoading("Sending Request...");

			// Obtain the default AcuantCardProcessRequestOptions object for the type of 
			// card you want to process (Driver’s License card for this example)
			var options = AcuantCardProcessRequestOptions.DefaultRequestOptionsForCardType(cardType.Value);

			options.ReformatImage = true;
			options.ReformatImageColor = 0;
			options.DPI = 150;
			options.CropImage = false;


			// perform the request
			instance.ProcessCardImages(
				frontImage: _frontOfCardImage,
				backImage: _backOfCardImage,
				stringData: null,
				@delegate: this,
				options: options);
		}

		public void DidFinishValidatingImageWithResult(AcuantDriversLicenseCard result)
		{
			// todo
		}

		public void DidFinishProcessingCardWithResult(AcuantDriversLicenseCard result)
		{
			UserDialogs.Instance.HideLoading();


			StringBuilder builder = new StringBuilder();
			//TODO: there are more properties for a drivers license
			builder.AppendLine($"First Name - {result.NameFirst}");
			builder.AppendLine($"Middle Name - {result.NameMiddle}");
			builder.AppendLine($"Last Name - {result.NameLast}");
			builder.AppendLine($"Authentication Result - {result.AuthenticationResult}");
			//builder.AppendLine($"nAunthentication Summary - {result.AuthenticationResultSummaryList.Select(x => x.ToString())}");

			builder.AppendLine($"Sex - {result.Sex}");
			builder.AppendLine($"DOB Long - {result.DateOfBirth4}");
			builder.AppendLine($"Issue Date Long - {result.IssueDate4}");
			builder.AppendLine($"Expiration Date Long - {result.ExpirationDate4}");

			SetFrontImage(UIImage.LoadFromData(result.LicenceImage));
			SetBackImage(UIImage.LoadFromData(result.LicenceImageTwo));

			ShowResult(builder.ToString());
		}

		private void ShowAlert(string title, string message)
		{
			UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, (a) => { }));

			PresentViewController(alert, true, () => { });
		}
	}
}
