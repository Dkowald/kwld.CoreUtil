# Overview
MSBuild helpers

[<-Home](../Home.md)  

### __DownloadFile__ build item

Using this you can download a file as part of the build.

```xml
<ItemGroup>
  <DownloadFile OutFolder="App_Data" Include="https://raw.githubusercontent.com/Dkowald/kwld.CoreUtil/master/Readme.md"/>
</ItemGroup>
```

The above will download the readme.md for this project from git-hub,
and copy it to ./App_Data/readme.md

The build task only downloads the file if it doesn't already exist.

The following Metadata is used:

|Metadata   |Description   |Default|
| --------- | ------------ | ---- |
|OutFolder  |project folder|Project directory: _$(MSBuildProjectDirectory)_|
|OutFileName|filename      |Match filename of the item: _%(Filename)%(Extension)_|

For finer control, you can explicity set the metadata as desired:
```xml
<DownloadFile Include="https://raw.githubusercontent.com/Dkowald/kwld.CoreUtil/master/Readme.md">
  <OutFolder>App_Data/Other</OutFolder>
  <OutFileName>%(Filename).download.%(Extension)</OutFileName>
</DownloadFile>
```

By default, the msbuild build task is added as part of the design-time build.
If causes problems for your use-case; over-ride as below:
```xml
<PropertyGroup>
	<TriggerDownloadFileTarget>Build</TriggerDownloadFileTarget>
</PropertyGroup>
```