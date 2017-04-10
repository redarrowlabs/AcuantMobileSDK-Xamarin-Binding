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
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		UIKit.UISegmentedControl CardTypeSegment { get; set; }

		[Outlet]
		UIKit.UITextField KeyEntry { get; set; }

		[Action ("CaptureImagesTapped:")]
		partial void CaptureImagesTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CardTypeSegment != null) {
				CardTypeSegment.Dispose ();
				CardTypeSegment = null;
			}

			if (KeyEntry != null) {
				KeyEntry.Dispose ();
				KeyEntry = null;
			}
		}
	}
}
