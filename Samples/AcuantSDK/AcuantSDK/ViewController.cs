using System;

using UIKit;
using AcuantMobileSDK;

namespace AcuantMobileSDK_iOS_Sample
{
	public partial class ViewController : UITableViewController, IAcuantMobileSDKControllerCapturingDelegate
	{
		//private AcuantMobileSDKController SdkController;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			KeyEntry.ShouldReturn = (textField) =>
			{
				KeyEntry.ResignFirstResponder();

				string key = KeyEntry.Text;

				//SdkController = AcuantMobileSDKController.InitAcuantMobileSDKWithLicenseKey(KeyEntry.Text, this);
				return true;
			};
		}

		partial void CaptureFrontTapped(Foundation.NSObject sender)
		{
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
			
		}

		#endregion
	}
}
