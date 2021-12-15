Public Class Tile

    Public Titulo As String
    Public ID As String
    Public Enlace As String

    Public ImagenIcono As String
    Public ImagenLogo As String
    Public ImagenAncha As String
    Public ImagenGrande As String

    Public Sub New(titulo As String, id As String, enlace As String,
                   imagenIcono As String, imagenLogo As String, imagenAncha As String, imagenGrande As String)
        Me.Titulo = titulo
        Me.ID = id
        Me.Enlace = enlace

        Me.ImagenIcono = imagenIcono
        Me.ImagenLogo = imagenLogo
        Me.ImagenAncha = imagenAncha
        Me.ImagenGrande = imagenGrande
    End Sub
End Class
