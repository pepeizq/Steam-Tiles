Public Class Tile

    Public Titulo As String
    Public ID As String
    Public Enlace As String

    Public ImagenIcono As String
    Public ImagenLogo As String
    Public ImagenAnchaReducida As String
    Public ImagenAncha As String
    Public ImagenGrande As String

    Public Sub New(ByVal titulo As String, ByVal id As String, ByVal enlace As String,
                   ByVal imagenIcono As String, ByVal imagenLogo As String, ByVal imagenAnchaReducida As String, ByVal imagenAncha As String, ByVal imagenGrande As String)
        Me.Titulo = titulo
        Me.ID = id
        Me.Enlace = enlace

        Me.ImagenIcono = imagenIcono
        Me.ImagenLogo = imagenLogo
        Me.ImagenAnchaReducida = imagenAnchaReducida
        Me.ImagenAncha = imagenAncha
        Me.ImagenGrande = imagenGrande
    End Sub
End Class
