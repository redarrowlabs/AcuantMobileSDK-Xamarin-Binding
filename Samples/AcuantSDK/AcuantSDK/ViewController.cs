using System;

using UIKit;
using AcuantMobileSDK;
using Foundation;
using Acr.UserDialogs;

namespace AcuantMobileSDK_iOS_Sample
{
	public partial class ViewController : UITableViewController, IAcuantMobileSDKControllerCapturingDelegate, IAcuantMobileSDKControllerProcessingDelegate
	{
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

			if(captureForBackOfCard)
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

		public void DidFinishValidatingImageWithResult(AcuantCardResult result)
		{
			UserDialogs.Instance.HideLoading();

			//ShowAlert("Error", result.mes
		}

		public void DidFinishProcessingCardWithResult(AcuantCardResult result)
		{
			UserDialogs.Instance.HideLoading();

			string message = string.Empty;
			UIImage faceImage;
			UIImage signatureImage;
			UIImage frontImage;
			UIImage backImage;
			if (cardType == AcuantCardType.DriversLicenseCard)
			{
				AcuantDriversLicenseCard data = result as AcuantDriversLicenseCard;
				// TODO: parse data

				//message [NSString stringWithFormat:@"First Name - %@ \nMiddle Name - %@ \nLast Name - 		%@ 	\nName Suffix - %@ \nAuthentication Result - %@ \nAunthentication Summary - %@ 		\nID - 	%@ 	\nLicense - %@ \nDOB Long - %@ \nDOB Short - %@ \nDate Of Birth Local - %@ 		\nIssue 	Date 	Long - %@ \nIssue Date Short - %@ \nIssue Date Local - %@ 		\nExpiration Date Long - 	%@ 	\nExpiration Date Short - %@ \nEye Color - %@ \nHair 		Color - %@ \nHeight - %@ \nWeight 	- 	%@ \nAddress - %@ \nAddress 2 - %@ \nAddress 3 		- %@ \nAddress 4 - %@ \nAddress 5 - %@ 		\nAddress 6  - %@ \nCity - %@ \nZip - %@ \nState - %@ \nCounty - %@ \nCountry Short - 		%@ 	\nCountry Long - %@ \nClass - %@ \nRestriction - %@ \nSex - %@ \nAudit - %@ 		\nEndorsements 	- %@ \nFee - %@ \nCSC - %@ \nSigNum - %@ \nText1 - %@ \nText2 - %@ 		\nText3 - %@ \nType - 	%@ \nDoc Type - %@ \nFather Name - %@ \nMother Name - %@ 		\nNameFirst_NonMRZ - %@ 	\nNameLast_NonMRZ - %@ \nNameLast1 - %@ \nNameLast2 - %@ 		\nNameMiddle_NonMRZ - %@ 	\nNameSuffix_NonMRZ - %@ \nDocument Detected Name - %@ 		\nDocument Detected Name Short - %@ 	\nNationality - %@ \nOriginal - %@ 		\nPlaceOfBirth - %@ \nPlaceOfIssue - %@ \nSocial 		Security - %@ \nIsAddressCorrected - %d \nIsAddressVerified - %d", data.nameFirst, 		data.nameMiddle, data.nameLast, data.nameSuffix,data.authenticationResult,[self 		arrayToString:data.authenticationResultSummaryList], data.licenceId, data.license, 		data.dateOfBirth4, data.dateOfBirth, data.dateOfBirthLocal, data.issueDate4, 		data.issueDate, data.issueDateLocal, data.expirationDate4, data.expirationDate, 		data.eyeColor, data.hairColor, data.height, data.weight, data.address, data.address2, 		data.address3, data.address4, data.address5, data.address6, data.city, data.zip, 		data.state, data.county, data.countryShort, data.idCountry, data.licenceClass, 		data.restriction, data.sex, data.audit, data.endorsements, data.fee, data.CSC, 		data.sigNum, data.text1, data.text2, data.text3, data.type, data.docType, 		data.fatherName, 	data.motherName, data.nameFirst_NonMRZ, data.nameLast_NonMRZ, 		data.nameLast1, 	data.nameLast2, data.nameMiddle_NonMRZ, data.nameSuffix_NonMRZ, 		data.documentDetectedName, 	data.documentDetectedNameShort, data.nationality, 		data.original, data.placeOfBirth, 	data.placeOfIssue, data.socialSecurity, 		data.isAddressCorrected, data.isAddressVerified];

				//if (_region == AcuantCardRegionUnitedStates || _region == AcuantCardRegionCanada) {
				//message = [NSString stringWithFormat:@"%@ \nIsBarcodeRead - %hhd \nIsIDVerified - 			%hhd \nIsOcrRead - %hhd", message, data.isBarcodeRead, data.isIDVerified, 			data.isOcrRead];
				//}

				//faceimage = [UIImage imageWithData:data.faceImage];
				//signatureImage = [UIImage imageWithData:data.signatureImage];
				//frontImage = [UIImage imageWithData:data.licenceImage];
				//backImage = [UIImage imageWithData:data.licenceImageTwo]
			}
			else if (cardType == AcuantCardType.MedicalInsuranceCard)
			{
				AcuantMedicalInsuranceCard data = result as AcuantMedicalInsuranceCard;
				// TODO: parse data

				//message =[NSString stringWithFormat: @"First Name - %@ \nLast Name - %@ \nMiddle Name - %@ \nMemberID - %@ \nGroup No. - %@ \nContract Code - %@ \nCopay ER - %@ \nCopay OV - %@ \nCopay SP - %@ \nCopay UC - %@ \nCoverage - %@ \nDate of Birth - %@ \nDeductible - %@ \nEffective Date - %@ \nEmployer - %@ \nExpire Date - %@ \nGroup Name - %@ \nIssuer Number - %@ \nOther - %@ \nPayer ID - %@ \nPlan Admin - %@ \nPlan Provider - %@ \nPlan Type - %@ \nRX Bin - %@ \nRX Group - %@ \nRX ID - %@ \nRX PCN - %@ \nTelephone - %@ \nWeb - %@ \nEmail - %@ \nAddress - %@ \nCity - %@ \nZip - %@ \nState - %@", data.firstName, data.lastName, data.middleName, data.memberId, data.groupNumber, data.contractCode, data.copayEr, data.copayOv, data.copaySp, data.copayUc, data.coverage, data.dateOfBirth, data.deductible, data.effectiveDate, data.employer, data.expirationDate, data.groupName, data.issuerNumber, data.other, data.payerId, data.planAdmin, data.planProvider, data.planType, data.rxBin, data.rxGroup, data.rxId, data.rxPcn, data.phoneNumber, data.webAddress, data.email, data.fullAddress, data.city, data.zip, data.state];

				//frontImage = [UIImage imageWithData: data.reformattedImage];
				//backImage = [UIImage imageWithData: data.reformattedImageTwo];
			}
			else if (cardType == AcuantCardType.PassportCard)
			{
				AcuantPassaportCard data = result as AcuantPassaportCard;
				// TODO: parse data
				//message =[NSString stringWithFormat: @"First Name - %@ \nMiddle Name - %@ \nLast Name
				//- %@ \nAuthentication Result - %@ \nAunthentication Summary - %@ \nPassport Number -
				//%@ \nPersonal Number - %@ \nSex - %@ \nCountry Long - %@ \nNationality Long - %@
				//\nDOB Long - %@ \nIssue Date Long - %@ \nExpiration Date Long - %@ \nPlace of Birth -
				//%@", data.nameFirst, data.nameMiddle, data.nameLast, data.authenticationResult,[self
				//arrayToString: data.authenticationResultSummaryList], data.passportNumber,
				//data.personalNumber, data.sex, data.countryLong, data.nationalityLong,
				//data.dateOfBirth4, data.issueDate4, data.expirationDate4, data.end_POB];

				//faceimage = [UIImage imageWithData: data.faceImage];
				//frontImage = [UIImage imageWithData: data.passportImage];
			}
		}

		public void DidFailToCaptureCropImage()
		{
		}

		#endregion


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
