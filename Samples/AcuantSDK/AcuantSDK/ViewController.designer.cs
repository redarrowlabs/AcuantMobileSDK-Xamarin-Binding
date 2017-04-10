// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AcuantMobileSDK_iOS_Sample
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIImageView BackImageView { get; set; }

		[Outlet]
		UIKit.UIButton CaptureFrontButton { get; set; }

		[Outlet]
		UIKit.UISegmentedControl CardTypeSegment { get; set; }

		[Outlet]
		UIKit.UIImageView FrontImageView { get; set; }

		[Outlet]
		UIKit.UITextField KeyEntry { get; set; }

		[Outlet]
		UIKit.UIButton ProcessDataButton { get; set; }

		[Action ("CaptureBackTapped:")]
		partial void CaptureBackTapped (Foundation.NSObject sender);

		[Action ("CaptureFrontTapped:")]
		partial void CaptureFrontTapped (Foundation.NSObject sender);

		[Action ("ProcessDataTapped:")]
		partial void ProcessDataTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CaptureFrontButton != null) {
				CaptureFrontButton.Dispose ();
				CaptureFrontButton = null;
			}

			if (CardTypeSegment != null) {
				CardTypeSegment.Dispose ();
				CardTypeSegment = null;
			}

			if (KeyEntry != null) {
				KeyEntry.Dispose ();
				KeyEntry = null;
			}

			if (ProcessDataButton != null) {
				ProcessDataButton.Dispose ();
				ProcessDataButton = null;
			}

			if (BackImageView != null) {
				BackImageView.Dispose ();
				BackImageView = null;
			}

			if (FrontImageView != null) {
				FrontImageView.Dispose ();
				FrontImageView = null;
			}
		}
	}
}
