# Xamarin Binding for Acuant Mobile SDK

This project is a Xamarin binding for the Acuant Mobile SDK.

Current contains bindings for the [iOS SDK](https://github.com/Acuant/AcuantiOSMobileSDK).

## Things to be aware of.

I ran into an issue with the different card result types in the iOS SDK. The native sdk passes a base class (AcuantCardResult) into the delegate methods. In Obj-C you can cast that object to the appropriate subclass (AcuantPassaportCard, AcuantMedicalInsuranceCard or AcuantDriversLicenseCard ). But that caused some issues with the C# bindings.

To get around this, there are 3 different processing delegates that you can implement. One for each card type.

* IAcuantMobileSDKControllerProcessingDelegateForDriversLicense
* IAcuantMobileSDKControllerProcessingDelegateForPassaport
* IAcuantMobileSDKControllerProcessingDelegateForMedical

This will give you 3 different possible methods signatures for consuming the results.

See the sample project for details on this. 

As a result of this customization, you can only have a single delegate defined per card result type. So you cannot use the same delegate to handle both a AcuantPassaportCard and AcuantMedicalInsuranceCard for example.


