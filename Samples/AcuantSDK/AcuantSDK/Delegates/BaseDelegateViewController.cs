using System;
using Acr.UserDialogs;
using AcuantMobileSDK;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace AcuantMobileSDK_iOS_Sample
{
	public abstract partial class BaseDelegateViewController :
		UIViewController,
		IAcuantMobileSDKControllerCapturingDelegate
	{
		private bool? _wasValidated;
		private bool didAppear = false;
		private bool isFirstLoad = false;

		protected AcuantMobileSDKController instance;
		protected AcuantCardType? cardType;
		protected UIImage _frontOfCardImage;
		protected UIImage _backOfCardImage;

		private readonly string licenseKey;


		//public static BaseDelegateViewController Create<TType>() where TType : BaseDelegateViewController
		//{
		//	var arr = NSBundle.MainBundle.LoadNib(nameof(BaseDelegateViewController), null, null);
		//	var v = Runtime.GetNSObject<TType>(arr.ValueAt(0));

		//	return v;
		//}

		public BaseDelegateViewController(string licenseKey)
			: base(nameof(BaseDelegateViewController), null)
		{
			this.licenseKey = licenseKey;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			instance = AcuantMobileSDKController.InitAcuantMobileSDKWithLicenseKey(licenseKey, this);
		}

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
				ShowCameraInterface(false);
			}
		}

		public virtual void DidCaptureCropImage(UIImage cardImage, bool scanBackSide)
		{
			bool captureForBackOfCard = false;

			if (_frontOfCardImage == null)
			{
				_frontOfCardImage = cardImage;
				FrontImageView.Image = _frontOfCardImage;

				if (cardType.Value == AcuantCardType.MedicalInsuranceCard)
				{
					captureForBackOfCard = true;
				}
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

		public virtual void DidCaptureData(string data)
		{
		}

		public virtual void DidFailToCaptureCropImage()
		{
		}

		[Export("didFailWithError:")]
		public virtual void DidFailWithError(AcuantError error)
		{
			UserDialogs.Instance.HideLoading();

			ShowAlert("Error", error.ErrorMessage);
		}

		public abstract void ProcessCard();

		protected void SetFrontImage(UIImage image)
		{
			FrontImageView.Image = image;
		}

		protected void SetBackImage(UIImage image)
		{
			BackImageView.Image = image;
		}

		protected void ShowResult(string resultText)
		{
			this.PresentViewController(new CardResultsViewController
			{
				ResultText = resultText
			}, true, () => { });
		}

		private void ShowCameraInterface(bool isBackSide = false)
		{
			UserDialogs.Instance.Toast(string.Format("Capture {0} of Card", isBackSide ? "Back" : "Front"));

			instance.ShowManualCameraInterfaceInViewController(this, this, cardType.Value, AcuantCardRegion.UnitedStates, isBackSide);
		}

		private void ShowAlert(string title, string message)
		{
			UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, (a) => { }));

			PresentViewController(alert, true, () => { });
		}

		partial void ProcessDataTapped(NSObject sender)
		{
			ProcessCard();
		}
	}
}

