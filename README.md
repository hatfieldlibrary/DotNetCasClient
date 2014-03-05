DotNetCasClient
===============

JASIG DotNetCasClient modified for use with native applications.

Added a handler for the PreRequestHandlerExectue event.  The handler method inserts the POST body back into IIS memory, 
making post data available to native applications (e.g. Illiad.dll). This uses a wrapper method introduced with ASP.NET Framework 4.

<code>private static void OnPostRequest(object sender, EventArgs e) {

            HttpContext context = HttpContext.Current;
            
            HttpRequest request = context.Request;
            
            if (request.HttpMethod == "POST")
            
            {
            
                request.InsertEntityBody();
                
            }        
            
        }</code>

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
