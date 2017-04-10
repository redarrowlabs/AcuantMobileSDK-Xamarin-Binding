using System;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using AcuantMobileSDK;
using UIKit;

namespace AcuantMobileSDK_iOS_Sample
{
	public class PassaportCardDelegateViewController : BaseDelegateViewController,
	IAcuantMobileSDKControllerProcessingDelegateForPassaport
	{
		public PassaportCardDelegateViewController(string licenseKey)
			: base(licenseKey)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			cardType = AcuantCardType.PassportCard;
			instance?.SetWidth(1478);
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

		public void DidFinishValidatingImageWithResult(AcuantPassaportCard result)
		{
			// todo
		}

		public void DidFinishProcessingCardWithResult(AcuantPassaportCard result)
		{
			UserDialogs.Instance.HideLoading();


			StringBuilder builder = new StringBuilder();
			builder.AppendLine($"First Name - {result.NameFirst}");
			builder.AppendLine($"Middle Name - {result.NameMiddle}");
			builder.AppendLine($"Last Name - {result.NameLast}");
			builder.AppendLine($"Authentication Result - {result.AuthenticationResult}");
			//builder.AppendLine($"nAunthentication Summary - {result.AuthenticationResultSummaryList.Select(x => x.ToString())}");
			builder.AppendLine($"Passport Number - {result.PassportNumber}");
			builder.AppendLine($"Personal Number - {result.PersonalNumber}");
			builder.AppendLine($"Sex - {result.Sex}");
			builder.AppendLine($"Country Long - {result.CountryLong}");
			builder.AppendLine($"Nationality Long - {result.NationalityLong}");
			builder.AppendLine($"DOB Long - {result.DateOfBirth4}");
			builder.AppendLine($"Issue Date Long - {result.IssueDate4}");
			builder.AppendLine($"Expiration Date Long - {result.ExpirationDate4}");
			builder.AppendLine($"Place of Birth - {result.End_POB}");

			SetFrontImage(UIImage.LoadFromData(result.PassportImage));

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
