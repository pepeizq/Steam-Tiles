Public Class Tweet

    Private _Contenido As String
    Private _Enlace As Uri
    Private _Fecha As Date

    Public Property Contenido As String
        Get
            Return _Contenido
        End Get
        Set(ByVal value As String)
            _Contenido = value
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

    Public Property Fecha As Date
        Get
            Return _Fecha
        End Get
        Set(ByVal value As Date)
            _Fecha = value
        End Set
    End Property

    Public Sub New(ByVal contenido As String, ByVal enlace As Uri, ByVal fecha As Date)
        _Contenido = contenido
        _Enlace = enlace
        _Fecha = fecha
    End Sub

End Class
