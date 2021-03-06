// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace AcuantMobileSDK_iOS_Sample
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			KeyEntry.ShouldReturn = (textField) =>
			{
				KeyEntry.ResignFirstResponder();

				string key = KeyEntry.Text;

				return true;
			};
		}

		partial void CaptureImagesTapped(NSObject sender)
		{
			if (CardTypeSegment.SelectedSegment == 0)
			{
				this.PresentViewController(new MedicalCardDelegateViewController(KeyEntry.Text), true, () => { });
			}
			else if (CardTypeSegment.SelectedSegment == 1)
			{
				this.PresentViewController(new DriversLicenseCardDelegateViewController(KeyEntry.Text), true, () => { });
			}
			else if (CardTypeSegment.SelectedSegment == 2)
			{
				this.PresentViewController(new PassaportCardDelegateViewController(KeyEntry.Text), true, () => { });
			}
		}
	}
}
