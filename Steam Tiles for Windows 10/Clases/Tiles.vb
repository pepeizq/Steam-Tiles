Public Class Tiles

    Public _titulo As String

    Public Property titulo As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            _titulo = value
        End Set
    End Property

    Public _id As String

    Public Property id As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property

    Public _enlace As Uri

    Public Property enlace As Uri
        Get
            Return _enlace
        End Get
        Set(ByVal value As Uri)
            _enlace = value
        End Set
    End Property

    Public _imagen As BitmapImage

    Public Property imagen As BitmapImage
        Get
            Return _imagen
        End Get
        Set(ByVal value As BitmapImage)
            _imagen = value
        End Set
    End Property

    Public _imagenUri As Uri

    Public Property imagenUri As Uri
        Get
            Return _imagenUri
        End Get
        Set(ByVal value As Uri)
            _imagenUri = value
        End Set
    End Property

    Public Sub New(ByVal tit As String, ByVal id As String, ByVal enl As Uri, ByVal img As BitmapImage, ByVal uri As Uri)
        _titulo = tit
        _id = id
        _enlace = enl
        _imagen = img
        _imagenUri = uri
    End Sub
End Class
