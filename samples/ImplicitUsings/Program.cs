using static System.Console;

WriteLine("Extensions added via implicit using");

//Use the files extensions
var home = new FileSystem().Home();

if (!home.Exists()) 
  throw new Exception("You have no home??");

//use the strings extensions.
var name = "A Name ".Trim().Words();

if (name.First() != "A") throw new Exception("First word was 'A'");

