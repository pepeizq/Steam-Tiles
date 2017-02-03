Public Class Tile

    Private _Titulo As String
    Private _ID As String
    Private _Enlace As Uri
    Private _Imagen As BitmapImage
    Private _ImagenUri As Uri
    Private _Tile As Tile

    Public Property Titulo As String
        Get
            Return _Titulo
        End Get
        Set(ByVal value As String)
            _Titulo = value
        End Set
    End Property

    Public Property ID As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

    Public Property Enlace As Uri
        Get
            Return _Enlace
        End Get
        Set(ByVal value As Uri)
            _Enlace = value
        End Set
    End Property

    Public Property Imagen As BitmapImage
        Get
            Return _Imagen
        End Get
        Set(ByVal value As BitmapImage)
            _Imagen = value
        End Set
    End Property

    Public Property ImagenUri As Uri
        Get
            Return _ImagenUri
        End Get
        Set(ByVal value As Uri)
            _ImagenUri = value
        End Set
    End Property

    Public Property Tile As Tile
        Get
            Return _Tile
        End Get
        Set(ByVal value As Tile)
            _Tile = value
        End Set
    End Property

    Public Sub New(ByVal titulo As String, ByVal id As String, ByVal enlace As Uri, ByVal imagen As BitmapImage,
                   ByVal imagenUri As Uri, ByVal tile As Tile)
        _Titulo = titulo
        _ID = id
        _Enlace = enlace
        _Imagen = imagen
        _ImagenUri = imagenUri
        _Tile = tile
    End Sub
End Class
