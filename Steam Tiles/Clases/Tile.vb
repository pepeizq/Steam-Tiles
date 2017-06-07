Public Class Tile

    Public Property Titulo As String
    Public Property ID As String
    Public Property Enlace As Uri
    Public Property ImagenWide As Uri
    Public Property ImagenLarge As Uri
    Public Property Cliente As String
    Public Property Tile As Tile

    Public Sub New(ByVal titulo As String, ByVal id As String, ByVal enlace As Uri,
                   ByVal imagenWide As Uri, ByVal imagenLarge As Uri,
                   ByVal cliente As String, ByVal tile As Tile)
        Me.Titulo = titulo
        Me.ID = id
        Me.Enlace = enlace
        Me.ImagenWide = imagenWide
        Me.ImagenLarge = imagenLarge
        Me.Cliente = cliente
        Me.Tile = tile
    End Sub
End Class
