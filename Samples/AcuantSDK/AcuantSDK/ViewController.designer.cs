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
		UIKit.UIButton CaptureBackButton { get; set; }

		[Outlet]
		UIKit.UIButton CaptureFrontButton { get; set; }

		[Outlet]
		UIKit.UISegmentedControl CardTypeSegment { get; set; }

		[Outlet]
		UIKit.UITextField KeyEntry { get; set; }

		[Action ("CaptureBackTapped:")]
		partial void CaptureBackTapped (Foundation.NSObject sender);

		[Action ("CaptureFrontTapped:")]
		partial void CaptureFrontTapped (Foundation.NSObject sender);

		[Action ("KeyEntryEditingEnded:")]
		partial void KeyEntryEditingEnded (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (KeyEntry != null) {
				KeyEntry.Dispose ();
				KeyEntry = null;
			}

			if (CaptureBackButton != null) {
				CaptureBackButton.Dispose ();
				CaptureBackButton = null;
			}

			if (CaptureFrontButton != null) {
				CaptureFrontButton.Dispose ();
				CaptureFrontButton = null;
			}

			if (CardTypeSegment != null) {
				CardTypeSegment.Dispose ();
				CardTypeSegment = null;
			}
		}
	}
}
