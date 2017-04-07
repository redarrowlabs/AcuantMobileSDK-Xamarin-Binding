## Binding The Library
1) Run git lfs pull in the AcuantMobileSDK directly. This will ensure all of the large files have been downloaded.

    `git lfs pull`

2) Copy the custom umbrella header file from "extras" to "AcuantMobileSDK/AcuantMobileSDK.embeddedframework/AcuantMobileSDK.framework/Headers".

    This is a custom header file written to make the binding process with objective sharpie easier. The AcuantMobileSDK for iOS doesn't have an umbrella header defined, so objective-sharpie doesn't know how to bind the framework on the fly.

3) Run the following command in terminal to bind the framework. This will output the ApiDefinitions and StructAndEnums files to be used in the C# binding project.

    `sharpie bind -o output -sdk iphoneos10.2 -n AcuantMobileSDK -framework AcuantMobileSDK/AcuantMobileSDK.Embeddedframework/AcuantMobileSDK.framework`

