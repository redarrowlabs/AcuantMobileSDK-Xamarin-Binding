using System;

using UIKit;
using AcuantMobileSDK;

namespace AcuantMobileSDK_iOS_Sample
{
	public partial class ViewController : UITableViewController, IAcuantMobileSDKControllerCapturingDelegate
	{
		private bool _wasValidated;
		private AcuantMobileSDKController SdkController;

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

				SdkController = AcuantMobileSDKController.InitAcuantMobileSDKWithLicenseKey(KeyEntry.Text, this);
				return true;
			};
		}

		partial void CaptureFrontTapped(Foundation.NSObject sender)
		{
			SdkController.ShowManualCameraInterfaceInViewController(this, this, AcuantCardType.MedicalInsuranceCard, AcuantCardRegion.UnitedStates, false);
		}

		partial void CaptureBackTapped(Foundation.NSObject sender)
		{

		}

		partial void KeyEntryEditingEnded(Foundation.NSObject sender)
		{
		}

		#region Delegate Methods

		public void DidCaptureCropImage(UIImage cardImage, bool scanBackSide)
		{
		}

		public void DidCaptureData(string data)
		{
		}

		public void DidFailWithError(AcuantError error)
		{
		}

		public void MobileSDKWasValidated(bool wasValidated)
		{
			_wasValidated = wasValidated;

			if (!wasValidated)
			{
				UIAlertController alert = UIAlertController.Create("Error", "Unable to validate Acuant license key.", UIAlertControllerStyle.Alert);
				alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, (a) => { }));

				PresentViewController(alert, true, () => { });
			}
			else
			{
				CardTypeSegment.Enabled = true;
				CaptureFrontButton.Enabled = true;
				CaptureBackButton.Enabled = true;
			}
		}

		#endregion
	}
}
