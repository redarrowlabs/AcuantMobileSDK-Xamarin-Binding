using System;

using UIKit;
using AcuantMobileSDK;
using Foundation;

namespace AcuantMobileSDK_iOS_Sample
{
	public partial class ViewController : UITableViewController, IAcuantMobileSDKControllerCapturingDelegate
	{
		private bool _wasValidated;
		private AcuantMobileSDKController instance;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			CardTypeSegment.Enabled = false;
			CaptureFrontButton.Enabled = false;
			CaptureBackButton.Enabled = false;

			KeyEntry.ShouldReturn = (textField) =>
			{
				KeyEntry.ResignFirstResponder();

				string key = KeyEntry.Text;

				instance = AcuantMobileSDKController.InitAcuantMobileSDKWithLicenseKey(KeyEntry.Text, this);
				return true;
			};
		}

		partial void CaptureFrontTapped(Foundation.NSObject sender)
		{
			ShowCameraInterface();
		}

		partial void CaptureBackTapped(Foundation.NSObject sender)
		{
			ShowCameraInterface(true);
		}

		private void ShowCameraInterface(bool isBackSide = false)
		{
			AcuantCardType? cardType = null;
			switch (this.CardTypeSegment.SelectedSegment)
			{
				case 0:
					cardType = AcuantCardType.MedicalInsuranceCard;
					instance.SetWidth(1500);
					break;
				case 1:
					cardType = AcuantCardType.DriversLicenseCard;
					if (instance.IsAssureIDAllowed)
						instance.SetWidth(2024);
					else
						instance.SetWidth(1250);
					break;
				case 2:
					cardType = AcuantCardType.PassportCard;
					instance.SetWidth(1478);
					break;
			}

			instance.ShowManualCameraInterfaceInViewController(this, this, cardType.Value, AcuantCardRegion.UnitedStates, isBackSide);
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
				CaptureBackButton.Enabled = true;
			}
		}

		public void DidCaptureCropImage(UIImage cardImage, bool scanBackSide)
		{
			
		}

		public void DidCaptureData(string data)
		{
			
		}

		public void DidFailWithError(AcuantError error)
		{
			ShowAlert("Error", error.ErrorMessage);
		}

		#endregion

		private void ShowAlert(string title, string message)
		{
			UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, (a) => { }));

			PresentViewController(alert, true, () => { });
		}
	}
}
