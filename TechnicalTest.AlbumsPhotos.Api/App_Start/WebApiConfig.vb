Imports System.Net.Http.Headers
Imports System.Web.Http

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)

        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        config.Formatters.XmlFormatter.SupportedMediaTypes.Clear()
        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
            New MediaTypeHeaderValue("text/html")
        )
    End Sub
End Module
