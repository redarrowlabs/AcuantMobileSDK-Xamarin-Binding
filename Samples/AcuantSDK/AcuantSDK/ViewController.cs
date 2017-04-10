using System;

using UIKit;
using AcuantMobileSDK;
using Foundation;
using Acr.UserDialogs;
using System.Text;

namespace AcuantMobileSDK_iOS_Sample
{
	public partial class ViewController : UITableViewController,
		IAcuantMobileSDKControllerCapturingDelegate,
	IAcuantMobileSDKControllerProcessingDelegateForMedical
	{
		private string resultText = null;

		private bool _wasValidated;
		private AcuantMobileSDKController instance;

		private UIImage _frontOfCardImage;
		private UIImage _backOfCardImage;
		private AcuantCardType? cardType = null;
		private readonly AcuantCardRegion cardRegion = AcuantCardRegion.UnitedStates;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.TableView.TableFooterView = new UIView();

			CardTypeSegment.Enabled = false;
			CaptureFrontButton.Enabled = false;

			KeyEntry.ShouldReturn = (textField) =>
			{
				KeyEntry.ResignFirstResponder();

				string key = KeyEntry.Text;

				instance = AcuantMobileSDKController.InitAcuantMobileSDKWithLicenseKey(KeyEntry.Text, this);
				SetCardTypeConfigs();

				return true;
			};

			CardTypeSegment.ValueChanged += (sender, e) =>
			{
				SetCardTypeConfigs();
			};

			SetCardTypeConfigs();
		}

		#region Delegate Methods

		[Export("mobileSDKWasValidated:")]
		public void MobileSDKWasValidated(bool wasValidated)
		{
			_wasValidated = wasValidated;

			if (!wasValidated)
			{
				ShowAlert("Error", "Unable to validate Acuant license key.");
			}
			else
			{
				CardTypeSegment.Enabled = true;
				CaptureFrontButton.Enabled = true;
			}
		}

		public void DidCaptureCropImage(UIImage cardImage, bool scanBackSide)
		{
			bool captureForBackOfCard = false;

			if (_frontOfCardImage == null)
			{
				_frontOfCardImage = cardImage;
				FrontImageView.Image = _frontOfCardImage;

				if (cardType == AcuantCardType.MedicalInsuranceCard)
					captureForBackOfCard = true;
			}
			else if (_backOfCardImage == null)
			{
				_backOfCardImage = cardImage;
				BackImageView.Image = _backOfCardImage;
			}

			if (captureForBackOfCard)
			{
				ShowCameraInterface(true);
			}
		}

		public void DidCaptureData(string data)
		{
		}

		[Export("didFailWithError:")]
		public void DidFailWithError(AcuantError error)
		{
			ShowAlert("Error", error.ErrorMessage);
		}

		public void DidFinishValidatingImageWithResult(AcuantMedicalInsuranceCard result)
		{
			UserDialogs.Instance.HideLoading();

			//ShowAlert("Error", result.mes
		}

		[Export("didFinishProcessingCardWithResult:")]
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

			FrontImageView.Image = frontImage;
			BackImageView.Image = backImage;

			resultText = builder.ToString();

			this.PerformSegue("ShowResultSegue", this);
		}

		public void DidFailToCaptureCropImage()
		{
		}

		#endregion

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			var vc = (segue.DestinationViewController as UINavigationController).TopViewController as ShowResultViewController;
			vc.ResultText = this.resultText;
		}

		private void SetCardTypeConfigs()
		{
			switch (this.CardTypeSegment.SelectedSegment)
			{
				case 0:
					cardType = AcuantCardType.MedicalInsuranceCard;
					instance?.SetWidth(1500);
					break;
				case 1:
					cardType = AcuantCardType.DriversLicenseCard;
					bool? isAllowed = instance.IsAssureIDAllowed;
					if (isAllowed.HasValue && isAllowed.Value)
						instance?.SetWidth(2024);
					else
						instance?.SetWidth(1250);
					break;
				case 2:
					cardType = AcuantCardType.PassportCard;
					instance?.SetWidth(1478);
					break;
			}
		}

		partial void CaptureFrontTapped(Foundation.NSObject sender)
		{
			ShowCameraInterface();
		}

		partial void CaptureBackTapped(Foundation.NSObject sender)
		{
			ShowCameraInterface(true);
		}

		partial void ProcessDataTapped(NSObject sender)
		{
			ProcessCard();
		}

		private void ShowCameraInterface(bool isBackSide = false)
		{
			UserDialogs.Instance.Toast(string.Format("Capture {0} of Card", isBackSide ? "Back" : "Front"));

			instance.ShowManualCameraInterfaceInViewController(this, this, cardType.Value, AcuantCardRegion.UnitedStates, isBackSide);
		}

		private void ProcessCard()
		{
			UserDialogs.Instance.ShowLoading("Sending Request...");

			// Obtain the default AcuantCardProcessRequestOptions object for the type of 
			// card you want to process (Driver’s License card for this example)
			var options = AcuantCardProcessRequestOptions.DefaultRequestOptionsForCardType(cardType.Value);

			//Optionally, configure the options to the desired value
			if (cardType.Value == AcuantCardType.DriversLicenseCard)
			{
				options.AutoDetectState = true;
				options.StateID = -1;
				options.ReformatImage = true;
				options.ReformatImageColor = 0;
				options.DPI = 150;
				options.CropImage = false;
				options.FaceDetection = true;
				options.SignatureDetection = true;
				options.Region = cardRegion;
				options.ImageSource = 101;
			}
			else if (cardType.Value == AcuantCardType.MedicalInsuranceCard)
			{
				options.ReformatImage = true;
				options.ReformatImageColor = 0;
				options.DPI = 150;
				options.CropImage = false;
			}
			else if (cardType.Value == AcuantCardType.PassportCard)
			{
				options.ReformatImage = true;
				options.ReformatImageColor = 0;
				options.DPI = 150;
				options.CropImage = true;
				options.FaceDetection = true;
				options.SignatureDetection = true;
				options.ImageSource = 101;
			}

			// perform the request
			instance.ProcessCardImages(
				frontImage: _frontOfCardImage,
				backImage: _backOfCardImage,
				stringData: null,
				@delegate: this,
				options: options);
		}


		private void ShowAlert(string title, string message)
		{
			UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, (a) => { }));

			PresentViewController(alert, true, () => { });
		}
	}
}
