# HAP (Help-A-Peer)
More details on the project can be found on the link [here](https://projects.etc.cmu.edu/help-a-peer/)
This exteniosn hopwefully will allow teachers and researchers to gain more insights from online learnign environments. 
## The tech Used
This setup was run using WPF and C#. 
We needed the following exteranl libraries for certain functionalities.
* [ModernWPF](https://github.com/Kinnara/ModernWpf)
* [gong-wpf-dragdrop](https://github.com/punker76/gong-wpf-dragdrop)

## To RUN HAP
In order to run HAP you would need an APP key and app secret from ZOOM to be able to do the ZOOM SDK calls. 
In order to do this you would need the following
* Visual Studio 2019 or higher
* ZOOM Pro account to be able to get the key and secret. ([Pricing details](https://zoom.us/pricing))
* Windows 10 machine with at least 4 GB of RAM. (Please note that the extension might run on any windows machine but our testing was done on windows 10)



### Adding your own key
1. Open `zoom_sdk_demo` folder

### Runing the exe 
1. Open the bin folder
2. Click on the file called "zoom_sdk_demo.exe"
3. You would get a series of prompts that will lead to ZOOM being opened emebedded in the HAP extension
![image](https://user-images.githubusercontent.com/10140799/117342123-db13a500-ae70-11eb-8f11-d8bd8a6f944b.png)



## In order to make edits to the project
The actual Project file is in the ZOOM SDK Demo folder

We decided that we would each seperate branches and work on them as needed. 
We then merge those newly implmented features into master

This is based on the C# ZOOM SDK wrapper


## Other Links and Reources

https://marketplace.zoom.us/docs/sdk/native-sdks/windows/c-sharp-wrapper
https://github.com/Kinnara/ModernWpf

Check the developer's Corner on the updates
https://projects.etc.cmu.edu/help-a-peer/?cat=3
