DotNetCasClient
===============

JASIG DotNetCasClient modified for use with native applications.  

CAS authentication for Illiad interlibrary loan sofware was the motivating use case for this project.

The JASIG .Net client CasAuthenicationModule is extended to provide a handler for the PreRequestHandlerExecute event. The new handler method makes POST data available to native applications by reinserting the POST body into IIS memory just prior to passing the request to the native IsapiModule. 

In the case of Illiad interlibrary loan software, this step is needed for OpenURL requests. Without it, the bibliographic information in the OpenURL request doesn't reach the Illiad application.  

The modified CasAuthenticationModule.cs uses a wrapper method introduced with ASP.NET Framework 4.  Consequently, this code will not work with prior releases of ASP.NET Framework.  But could be modified to work without too much effort. 

The compiled DotNetCasClient.dll can be found in <a href="https://github.com/mspalti/DotNetCasClient/tree/master/DotNetCasClient/bin/Release">DotNetCas/bin/Release</a>. There are several ways to deploy, but in the case of Illiad, the easiest way is to copy the DoNetCasClient.dll to the Illiad /Bin directory.

To use the JASIG client and .NET Forms Authentication with a native application, add the following to the web.server 
element of your web.config.  See <a href="https://github.com/mspalti/DotNetCasClient/blob/master/Configuration/web.config">sample web.conf file</a> for more details. It's probably easiest to work from this sample file, especially you are using the CAS client with Illiad.

```xml
<system .webserver=""> 
      <modules>
		  <remove name="UrlAuthorization" />
		  <add name="UrlAuthorization" type="System.Web.Security.UrlAuthorizationModule" />
 		  <remove name="FormsAuthenticationModule" />
		  <add name="FormsAuthenticationModule" type="System.Web.Security.FormsAuthenticationModule" />
		  <remove name="DotNetCasClient" />
		  <add name="DotNetCasClient" type="DotNetCasClient.CasAuthenticationModule,DotNetCasClient" />		
    </modules>
```


More JASIG client <a href="https://wiki.jasig.org/display/CASC/.Net+Cas+Client">configuration documentation here</a>.

The project includes Visual Studio Professional 2012 configuration. Mileage will vary. 
