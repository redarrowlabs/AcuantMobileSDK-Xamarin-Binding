using System;

using UIKit;
using AcuantMobileSDK;

namespace AcuantSDK
{
	public partial class ViewController : UIViewController
	{
		private AcuantMobileSDKController SdkController;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SdkController = AcuantMobileSDKController.InitAcuantMobileSDKWithLicenseKey(string.Empty, this);
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
