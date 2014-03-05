DotNetCasClient
===============

JASIG DotNetCasClient modified for use with native applications.  

In the updated code, a new event handler method makes POST data available to native applications by inserting the POST body into IIS memory just prior to passing the request to the native IsapiModule. In the case of Illiad interlibrary loan software, this step is needed for openUrl requests.  

The modified CasAuthenticationModule.cs uses a wrapper method introduced with ASP.NET Framework 4.  

The compiled DotNetCaseClient.dll can be found in DotNetCas/bin/Release.

To use the JASIG client and .NET Forms Authentication with a native application, add the following to the web.server 
element of your web.config:

```xml
<system .webserver=""> 
  <modules>
    <remove name="FormsAuthenticationModule"/> 
    <add name="FormsAuthenticationModule" type="System.Web.Security.FormsAuthenticationModule"/>
   <remove name="UrlAuthorization"/> 
   <add name="UrlAuthorization" type="System.Web.Security.UrlAuthorizationModule"/> 
   <remove name="DefaultAuthentication"/>
   <add name="DefaultAuthentication" type="System.Web.Security.DefaultAuthenticationModule"/>
</modules>
```

More JASIG client <a href="https://wiki.jasig.org/display/CASC/.Net+Cas+Client">configuration documentation here</a>.
