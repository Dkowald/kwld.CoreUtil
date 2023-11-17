using System.IO.Abstractions;
using kwd.CoreUtil.FileSystem;
using static System.Console;


WriteLine("Sample app using core utils");
WriteLine("-----------");

var proj = new FileSystem().Project();
var appData = proj.GetFolder("App_Data");

var readme = appData.GetFile("App_Data", "Readme.md");

WriteLine($"Auto download readme file: {(readme.Exists?"Worked":"Failed")}");

var altName = appData.GetFile("Alt", "AltName.md");
WriteLine($"Auto download with rename: {(altName.Exists?"Worked":"Failed")}");