Build
================================================================================
Request: A Windows system has VS2010 being installed. 
1. Double click build.release.bat will started to use MSBUILD to compile 
solution and generate all assemlies, executeable and package under ./Output
folder.
2. Or you can open Hibu.Sam.Concordance.sln file using VS2010 and build each 
project with different configuration as needed.

Folder structure as below
================================================================================
|+Data - Testing data
|+Docs - Instruction documents
|+Hibu.Sam.Concordance.Client - Command line client tool project which can be 
	used to post a file to RESTful interface and display returned JSON data
|+Hibu.Sam.Concordance.Models - Common data model classes project
|+Hibu.Sam.Concordance.Parsers - Parser class proejct which takes filestream as 
	input and return extracted sentence and word information
|+Hibu.Sam.Concordance.Utilities - Common utilites and helper class project
|+Hibu.Sam.Concordance.Web - Web application project which exposes a concordance
	generator interface and a web page for submitting file to this interface
|+Hibu.Sam.Concordance.WebApiServer - Self-host web api project which exposes a 
	concordance generator interface 
|+Output - Folder contains compiled output when using build script
|+packages - 3rd part NUGet packages

Files under root folder
================================================================================
./Hibu.Sam.Concordance.sln - VS2010 solution file contains all sub projects
./build.cleanup.bat - Script for cleanup compiled output under each proejcts for 
	Debug and Release configuration
./build.release.bat - Script for compile self-host web api server, command line 
	client tool, and web applicaiton package. Compiled result will be under
	Output folder
./solution cleanup.bat - Script for cleanup all Bin, Obj, Output subfolders
./web.deploy.ps1 - PowerShell script for deploying web application