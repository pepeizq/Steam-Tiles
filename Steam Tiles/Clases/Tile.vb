Public Class Tile

    Public Titulo As String
    Public ID As String
    Public Enlace As Uri

    Public ImagenPequeña As Uri
    Public ImagenMediana As Uri
    Public ImagenAncha As Uri
    Public ImagenGrande As Uri

    Public Sub New(ByVal titulo As String, ByVal id As String, ByVal enlace As Uri,
                   ByVal imagenPequeña As Uri, ByVal imagenMediana As Uri, ByVal imagenAncha As Uri, ByVal imagenGrande As Uri)
        Me.Titulo = titulo
        Me.ID = id
        Me.Enlace = enlace

        Me.ImagenPequeña = imagenPequeña
        Me.ImagenMediana = imagenMediana
        Me.ImagenAncha = imagenAncha
        Me.ImagenGrande = imagenGrande
    End Sub
End Class
