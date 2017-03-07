Public Class Tile

    Public Property Titulo As String
    Public Property ID As String
    Public Property Enlace As Uri
    Public Property Imagen As BitmapImage
    Public Property ImagenUri As Uri
    Public Property Tile As Tile

    Public Sub New(ByVal titulo As String, ByVal id As String, ByVal enlace As Uri, ByVal imagen As BitmapImage,
                   ByVal imagenUri As Uri, ByVal tile As Tile)
        Me.Titulo = titulo
        Me.ID = id
        Me.Enlace = enlace
        Me.Imagen = imagen
        Me.ImagenUri = imagenUri
        Me.Tile = tile
    End Sub
End Class
