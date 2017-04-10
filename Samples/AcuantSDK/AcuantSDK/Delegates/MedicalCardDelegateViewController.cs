using System;
using System.Text;
using Acr.UserDialogs;
using AcuantMobileSDK;
using UIKit;

namespace AcuantMobileSDK_iOS_Sample
{
	public class MedicalCardDelegateViewController : BaseDelegateViewController,
	IAcuantMobileSDKControllerProcessingDelegateForMedical
	{
		public MedicalCardDelegateViewController(string licenseKey)
			: base(licenseKey)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			cardType = AcuantCardType.MedicalInsuranceCard;
			instance?.SetWidth(1500);
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

		public void DidFinishValidatingImageWithResult(AcuantMedicalInsuranceCard result)
		{
			// todo
		}

		public void DidFinishProcessingCardWithResult(AcuantMedicalInsuranceCard result)
		{
			UserDialogs.Instance.HideLoading();

			string message = string.Empty;
			UIImage faceImage;
			UIImage signatureImage;
			UIImage frontImage;
			UIImage backImage;

			StringBuilder builder = new StringBuilder();
			builder.AppendLine($"First Name - {result.FirstName}");
			builder.AppendLine($"Middle Name - {result.MiddleName}");
			builder.AppendLine($"Last Name - {result.LastName}");
			builder.AppendLine($"Member ID - {result.MemberId}");
			builder.AppendLine($"Group Number - {result.GroupNumber}");
			builder.AppendLine($"Contract Code - {result.ContractCode}");
			builder.AppendLine($"Copay ER - {result.CopayEr}");
			builder.AppendLine($"Copay OV - {result.CopayOv}");
			builder.AppendLine($"Copay SP - {result.CopaySp}");
			builder.AppendLine($"Copay UC - {result.CopayUc}");
			builder.AppendLine($"Coverage - {result.Coverage}");
			builder.AppendLine($"Date of Birth - {result.DateOfBirth}");
			builder.AppendLine($"Deductible - {result.Deductible}");
			builder.AppendLine($"Effective Date - {result.EffectiveDate}");
			builder.AppendLine($"Employer - {result.Employer}");
			builder.AppendLine($"Expire Date - {result.ExpirationDate}");
			builder.AppendLine($"Group Name - {result.GroupName}");
			builder.AppendLine($"Issuer Number - {result.IssuerNumber}");
			builder.AppendLine($"Other - {result.Other}");
			builder.AppendLine($"Payer ID - {result.PayerId}");
			builder.AppendLine($"Plan Admin - {result.PlanAdmin}");
			builder.AppendLine($"Plan Provider - {result.PlanProvider}");
			builder.AppendLine($"Plan Type - {result.PlanType}");
			builder.AppendLine($"RX Bin - {result.RxBin}");
			builder.AppendLine($"RX Group - {result.RxGroup}");
			builder.AppendLine($"RX ID - {result.RxId}");
			builder.AppendLine($"RX PCN - {result.RxPcn}");
			builder.AppendLine($"Telephone - {result.PhoneNumber}");
			builder.AppendLine($"Web - {result.WebAddress}");
			builder.AppendLine($"Address - {result.FullAddress}");
			builder.AppendLine($"City - {result.City}");
			builder.AppendLine($"Zip - {result.Zip}");
			builder.AppendLine($"State - {result.State}");

			frontImage = UIImage.LoadFromData(result.ReformattedImage);
			backImage = UIImage.LoadFromData(result.ReformattedImageTwo);

			SetFrontImage(frontImage);
			SetBackImage(backImage);

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
