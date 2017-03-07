Public Class FeedRSS

    Public Property Titulo As String
    Public Property Enlace As Uri

    Public Sub New(ByVal titulo As String, ByVal enlace As Uri)
        Me.Titulo = titulo
        Me.Enlace = enlace
    End Sub

End Class
