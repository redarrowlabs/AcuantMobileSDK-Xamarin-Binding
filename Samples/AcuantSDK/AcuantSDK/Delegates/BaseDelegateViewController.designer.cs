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
	[Register ("BaseDelegateViewController")]
	partial class BaseDelegateViewController
	{
		[Outlet]
		UIKit.UIImageView BackImageView { get; set; }

		[Outlet]
		UIKit.UIImageView FrontImageView { get; set; }

		[Action ("ProcessDataTapped:")]
		partial void ProcessDataTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (FrontImageView != null) {
				FrontImageView.Dispose ();
				FrontImageView = null;
			}

			if (BackImageView != null) {
				BackImageView.Dispose ();
				BackImageView = null;
			}
		}
	}
}
