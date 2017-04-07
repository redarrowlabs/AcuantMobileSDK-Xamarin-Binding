## Creating The Binding For AcuantMobileSDK

1) Run git lfs pull

    `git lfs pull`

1) Update Submodules
    git submodule init  
    git submodule update --recursive

2) Copy umbrella header from "extras" file to AcuantMobileSDK headers folder

3) Run objective sharpie

    ```sharpie bind -o output -sdk iphoneos10.2 -n AcuantMobileSDK -framework AcuantMobileSDK/AcuantMobileSDK.Embeddedframework/AcuantMobileSDK.framework```

4) If necessary, update the binding library with the newly generated ApiDefinitions & StructEnums files.