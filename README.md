# HAP (Help-A-Peer)
More details on the project can be found on the link [here](https://projects.etc.cmu.edu/help-a-peer/)
This extension hopefully will allow teachers and researchers to gain more insights from online learning environments.
## The tech Used
This setup was run using WPF and C#. The ZOOM SKD has its header files in C++.
We needed the following external libraries for certain functionalities.
* [ModernWPF](https://github.com/Kinnara/ModernWpf)
* [gong-wpf-dragdrop](https://github.com/punker76/gong-wpf-dragdrop)

## To RUN HAP
In order to run HAP you would need an APP key and app secret from ZOOM to be able to do the ZOOM SDK calls.
In order to do this you would need the following
* Visual Studio 2019 or higher
* ZOOM Pro account to be able to get the key and secret. ([Pricing details](https://zoom.us/pricing))
![image](https://user-images.githubusercontent.com/10140799/117348953-d7841c00-ae78-11eb-8691-0ee8df6dc74b.png)

* Windows 10 machine with at least 4 GB of RAM. (Please note that the extension might run on any windows machine but our testing was done on windows 10)
  - Please note that just like ZOOM this is a 32-bit application which might not be supported by the tech community in the long run.



### Adding your own key
1. Open `zoom_sdk_demo` folder
2. Click on `zoom_sdk_demo.sln` to open this in Visual Studio 2019
3. Edit the file called `MainWindow.xaml.cs` to add your key and secret

```
        private string generateJWT()
        {
            Console.WriteLine("Generating token");
            const string secret = "PASTE YOUR SECRET HERE";
            const string key = "PASTE YOUR KEY HERE";
```
4. Rebuild the project to modify the current exe.
   1. Make sure of the following setting when building (Release and ANY CPU)
![image](https://user-images.githubusercontent.com/10140799/117349040-f1256380-ae78-11eb-9cda-17e72f5f70d7.png)



### Running the exe
1. Open the bin folder
2. Click on the file called "zoom_sdk_demo.exe"
3. You would get a series of prompts that will lead to ZOOM being opened embedded in the HAP extension
![image](https://user-images.githubusercontent.com/10140799/117342123-db13a500-ae70-11eb-8f11-d8bd8a6f944b.png)



## In order to make edits to the project

1. open the folder `zoom_sdk_demo`
2. Click on `zoom_sdk_demo.sln` to open this in Visual Studio 2019 or higher
3. The hierachy would look as follows. (Models contains classes) XAML files contain the UI descriptions. Converters help convert aspects from the model to the UI. 

![image](https://user-images.githubusercontent.com/10140799/117349352-4b262900-ae79-11eb-8542-c24074c72274.png)

## The C# wrapper
We were required to add some functionalities to the C# wrapper. We added the following functionalities
* OnCohostChanged callback
* isTalking added to user info
* BreakoutRoom interface V2
* participantID added to user info

All of these edits are working with version v5.5.12509.0330 of the ZOOM SDK.

### How edit the wrapper and expand it
If you need to make edits to the wrapper yourself, please benefit from the following links
* https://bumblebee.com.sg/ref/zoomsdk.php
* https://projects.etc.cmu.edu/help-a-peer/?p=441
* https://devforum.zoom.us/t/how-to-use-the-ibocreator-class-in-c/26548/5

1. open the folder labeled `zoom_sdk_c_sharp_wrap` folder
2. Click on `zoom_sdk_c_sharp_wrap.sln`
3. Edit the files to expand the wrappers functionalities
4. Select `Release` from the build mode

![image](https://user-images.githubusercontent.com/10140799/117349996-077fef00-ae7a-11eb-8e02-088bc9859daf.png)


6. Right click on `zoom_sdk_dotnet_wrap` in the hierarchy and click on `build` or `rebuild`

![image](https://user-images.githubusercontent.com/10140799/117349717-b7a12800-ae79-11eb-9791-b0be87a00e30.png)


6. At the end, this should update your `zoom_sdk_dotnet_wrap.dll`, `zoom_sdk_dotnet_wrap.pdb` and `zoom_sdk_dotnet_wrap.dll.metagen` in the bin folder.

This will allow new functionalities to be present when you edit the HAP project files.


## Other Links and Resources

https://marketplace.zoom.us/docs/sdk/native-sdks/windows/c-sharp-wrapper
https://github.com/Kinnara/ModernWpf

Check the developer's Corner on the updates
https://projects.etc.cmu.edu/help-a-peer/?cat=3
