Public Class SteamCuenta

    Public Property ID64 As String
    Public Property NombreUrl As String
    Public Property Nombre As String
    Public Property Avatar As String

    Public Sub New(ByVal id64 As String, ByVal nombreurl As String, ByVal nombre As String, ByVal avatar As String)
        Me.ID64 = id64
        Me.NombreUrl = nombreurl
        Me.Nombre = nombre
        Me.Avatar = avatar
    End Sub

End Class