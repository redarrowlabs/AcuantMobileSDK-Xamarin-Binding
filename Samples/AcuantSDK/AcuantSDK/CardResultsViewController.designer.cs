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
	[Register ("CardResultsViewController")]
	partial class CardResultsViewController
	{
		[Outlet]
		UIKit.UILabel OutputLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (OutputLabel != null) {
				OutputLabel.Dispose ();
				OutputLabel = null;
			}
		}
	}
}
