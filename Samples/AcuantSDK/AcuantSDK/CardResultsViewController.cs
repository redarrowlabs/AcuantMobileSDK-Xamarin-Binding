using System;
using Foundation;
using UIKit;

namespace AcuantMobileSDK_iOS_Sample
{
	public partial class CardResultsViewController : UIViewController
	{
		public string ResultText { get; set; }


		public CardResultsViewController() 
			: base("CardResultsViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			OutputLabel.Text = ResultText;
		}

		//partial void CloseTapped(NSObject sender)
		//{
		//	this.NavigationController.PopViewController(true);
		//}
	}
}

