Imports Newtonsoft.Json

Public Class SteamAPI

    <JsonProperty("data")>
    Public Datos As SteamDatos

End Class

Public Class SteamDatos

    <JsonProperty("name")>
    Public Titulo As String

    <JsonProperty("header_image")>
    Public Imagen As String

    <JsonProperty("steam_appid")>
    Public ID As String

    <JsonProperty("background")>
    Public Fondo As String

End Class
