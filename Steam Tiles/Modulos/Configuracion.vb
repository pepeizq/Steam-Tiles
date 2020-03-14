Imports FontAwesome.UWP
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.UI

Module Configuracion

    Public Sub Iniciar()

        If ApplicationData.Current.LocalSettings.Values("modo_tiles") Is Nothing Then
            ModoTiles(0, True)
        Else
            ModoTiles(ApplicationData.Current.LocalSettings.Values("modo_tiles"), True)
        End If

        '------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("mostrar_tile_pequeña") Is Nothing Then
            MostrarTilePequeña(True)
        Else
            MostrarTilePequeña(ApplicationData.Current.LocalSettings.Values("mostrar_tile_pequeña"))
        End If

        If ApplicationData.Current.LocalSettings.Values("mostrar_tile_mediana") Is Nothing Then
            MostrarTileMediana(True)
        Else
            MostrarTileMediana(ApplicationData.Current.LocalSettings.Values("mostrar_tile_mediana"))
        End If

        If ApplicationData.Current.LocalSettings.Values("mostrar_tile_ancha") Is Nothing Then
            MostrarTileAncha(True)
        Else
            MostrarTileAncha(ApplicationData.Current.LocalSettings.Values("mostrar_tile_ancha"))
        End If

        If ApplicationData.Current.LocalSettings.Values("mostrar_tile_grande") Is Nothing Then
            MostrarTileGrande(False)
        Else
            MostrarTileGrande(ApplicationData.Current.LocalSettings.Values("mostrar_tile_grande"))
        End If

        '------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("mostrar_tiles_drm") Is Nothing Then
            MostrarTilesDRM(True)
        Else
            MostrarTilesDRM(ApplicationData.Current.LocalSettings.Values("mostrar_tiles_drm"))
        End If

        If ApplicationData.Current.LocalSettings.Values("mostrar_tiles_color") Is Nothing Then
            MostrarTilesColor(True)
        Else
            MostrarTilesColor(ApplicationData.Current.LocalSettings.Values("mostrar_tiles_color"))
        End If

        '------------------------------------------

        'If ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") Is Nothing Then
        '    TileAnchaTitulo(False)
        'Else
        '    TileAnchaTitulo(ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo"))
        'End If

        'If ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") Is Nothing Then
        '    TileGrandeTitulo(True)
        'Else
        '    TileGrandeTitulo(ApplicationData.Current.LocalSettings.Values("tile_grande_titulo"))
        'End If

        'If ApplicationData.Current.LocalSettings.Values("tiles_color_titulo") Is Nothing Then
        '    TilesColorTitulo(0)
        'Else
        '    TilesColorTitulo(ApplicationData.Current.LocalSettings.Values("tiles_color_titulo"))
        'End If

        '------------------------------------------

        'If ApplicationData.Current.LocalSettings.Values("tile_mediana_drm_mostrar") Is Nothing Then
        '    TileMedianaDRMMostrar(False)
        'Else
        '    TileMedianaDRMMostrar(ApplicationData.Current.LocalSettings.Values("tile_mediana_drm_mostrar"))
        'End If

        'If ApplicationData.Current.LocalSettings.Values("tile_ancha_drm_mostrar") Is Nothing Then
        '    TileAnchaDRMMostrar(True)
        'Else
        '    TileAnchaDRMMostrar(ApplicationData.Current.LocalSettings.Values("tile_ancha_drm_mostrar"))
        'End If

        'If ApplicationData.Current.LocalSettings.Values("tile_grande_drm_mostrar") Is Nothing Then
        '    TileGrandeDRMMostrar(True)
        'Else
        '    TileGrandeDRMMostrar(ApplicationData.Current.LocalSettings.Values("tile_grande_drm_mostrar"))
        'End If

        'If ApplicationData.Current.LocalSettings.Values("tiles_drm_icono") Is Nothing Then
        '    TilesDRMIcono(0)
        'Else
        '    TilesDRMIcono(ApplicationData.Current.LocalSettings.Values("tiles_drm_icono"))
        'End If

        'If ApplicationData.Current.LocalSettings.Values("tiles_drm_icono_posicion") Is Nothing Then
        '    TilesDRMIconoPosicion(1)
        'Else
        '    TilesDRMIconoPosicion(ApplicationData.Current.LocalSettings.Values("tiles_drm_icono_posicion"))
        'End If

        '------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("tiles_color_fondo") Is Nothing Then
            TilesColorFondo(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToHex(App.Current.Resources("ColorSecundario")))
        Else
            TilesColorFondo(ApplicationData.Current.LocalSettings.Values("tiles_color_fondo"))
        End If

        '------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_estiramiento") Is Nothing Then
            TilePequeñaImagenEstiramiento(0)
        Else
            TilePequeñaImagenEstiramiento(ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_estiramiento"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_estiramiento") Is Nothing Then
            TileMedianaImagenEstiramiento(1)
        Else
            TileMedianaImagenEstiramiento(ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_estiramiento"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_estiramiento") Is Nothing Then
            TileAnchaImagenEstiramiento(2)
        Else
            TileAnchaImagenEstiramiento(ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_estiramiento"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_estiramiento") Is Nothing Then
            TileGrandeImagenEstiramiento(2)
        Else
            TileGrandeImagenEstiramiento(ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_estiramiento"))
        End If

        '------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_margen") Is Nothing Then
            TilePequeñaImagenMargen(0)
        Else
            TilePequeñaImagenMargen(ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_margen"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_margen") Is Nothing Then
            TileMedianaImagenMargen(0)
        Else
            TileMedianaImagenMargen(ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_margen"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_margen") Is Nothing Then
            TileAnchaImagenMargen(0)
        Else
            TileAnchaImagenMargen(ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_margen"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_margen") Is Nothing Then
            TileGrandeImagenMargen(0)
        Else
            TileGrandeImagenMargen(ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_margen"))
        End If

        '------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaX") Is Nothing And ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaY") Is Nothing Then
            TilePequeñaImagenCoordenadas(0, 0)
        Else
            TilePequeñaImagenCoordenadas(ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaX"), ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaY"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_coordenadaX") Is Nothing And ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_coordenadaY") Is Nothing Then
            TileMedianaImagenCoordenadas(0, 0)
        Else
            TileMedianaImagenCoordenadas(ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_coordenadaX"), ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_coordenadaY"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_coordenadaX") Is Nothing And ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_coordenadaY") Is Nothing Then
            TileAnchaImagenCoordenadas(0, 0)
        Else
            TileAnchaImagenCoordenadas(ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_coordenadaX"), ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_coordenadaY"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_coordenadaX") Is Nothing And ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_coordenadaY") Is Nothing Then
            TileGrandeImagenCoordenadas(0, 0)
        Else
            TileGrandeImagenCoordenadas(ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_coordenadaX"), ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_coordenadaY"))
        End If

        '------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_zoom") Is Nothing Then
            TilePequeñaImagenZoom(1, 0, 0)
        Else
            TilePequeñaImagenZoom(ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_zoom"), ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaX"), ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaY"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_zoom") Is Nothing Then
            TileMedianaImagenZoom(1, 0, 0)
        Else
            TileMedianaImagenZoom(ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_zoom"), ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_coordenadaX"), ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_coordenadaY"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_zoom") Is Nothing Then
            TileAnchaImagenZoom(1, 0, 0)
        Else
            TileAnchaImagenZoom(ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_zoom"), ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_coordenadaX"), ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_coordenadaY"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_zoom") Is Nothing Then
            TileGrandeImagenZoom(1, 0, 0)
        Else
            TileGrandeImagenZoom(ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_zoom"), ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_coordenadaX"), ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_coordenadaY"))
        End If

    End Sub

    Public Sub ModoTiles(modo As Integer, arranque As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("modo_tiles") = modo

        If arranque = True Then
            Dim cbTiles As ComboBox = pagina.FindName("cbConfigModosTiles")
            cbTiles.SelectedIndex = modo
        Else
            If modo = 0 Then
                Steam.Generar(False)
            ElseIf modo = 1 Then
                If Not ApplicationData.Current.LocalSettings.Values("cuenta_steam") Is Nothing Then
                    Dim cuenta As String = ApplicationData.Current.LocalSettings.Values("cuenta_steam")
                    Dim tbCuenta As TextBox = pagina.FindName("tbConfigCuenta")
                    tbCuenta.Text = cuenta
                    Steam.Cuenta(cuenta)
                End If
            End If
        End If

        Dim sp1 As StackPanel = pagina.FindName("spModoTile1")

        If modo = 0 Then
            sp1.Visibility = Visibility.Visible
        Else
            sp1.Visibility = Visibility.Collapsed
        End If

        Dim sp2 As StackPanel = pagina.FindName("spModoTile2")

        If modo = 1 Then
            sp2.Visibility = Visibility.Visible
        Else
            sp2.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub Resetear()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        MostrarTilePequeña(True)
        MostrarTileMediana(True)
        MostrarTileAncha(True)
        MostrarTileGrande(False)

        MostrarTilesDRM(True)
        MostrarTilesColor(True)

        'TileAnchaTitulo(False)
        'TileGrandeTitulo(True)
        'TilesColorTitulo(0)

        'TileMedianaDRMMostrar(False)
        'TileAnchaDRMMostrar(True)
        'TileGrandeDRMMostrar(True)
        'TilesDRMIcono(0)
        'TilesDRMIconoPosicion(1)

        TilesColorFondo(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToHex(App.Current.Resources("ColorSecundario")))

        Dim imagen1 As ImageEx = pagina.FindName("imagenTilePequeñaGenerar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenTilePequeñaEnseñar")
        Dim imagen3 As ImageEx = pagina.FindName("imagenTilePequeñaPersonalizar")

        imagen1.Source = imagen1.Tag
        imagen2.Source = imagen2.Tag
        imagen3.Source = imagen3.Tag

        Dim imagen4 As ImageEx = pagina.FindName("imagenTileMedianaGenerar")
        Dim imagen5 As ImageEx = pagina.FindName("imagenTileMedianaEnseñar")
        Dim imagen6 As ImageEx = pagina.FindName("imagenTileMedianaPersonalizar")

        imagen4.Source = imagen4.Tag
        imagen5.Source = imagen5.Tag
        imagen6.Source = imagen6.Tag

        Dim imagen7 As ImageEx = pagina.FindName("imagenTileAnchaGenerar")
        Dim imagen8 As ImageEx = pagina.FindName("imagenTileAnchaEnseñar")
        Dim imagen9 As ImageEx = pagina.FindName("imagenTileAnchaPersonalizar")

        imagen7.Source = imagen7.Tag
        imagen8.Source = imagen8.Tag
        imagen9.Source = imagen9.Tag

        Dim imagen10 As ImageEx = pagina.FindName("imagenTileGrandeGenerar")
        Dim imagen11 As ImageEx = pagina.FindName("imagenTileGrandeEnseñar")
        Dim imagen12 As ImageEx = pagina.FindName("imagenTileGrandePersonalizar")

        imagen10.Source = imagen10.Tag
        imagen11.Source = imagen11.Tag
        imagen12.Source = imagen12.Tag

        TilePequeñaImagenEstiramiento(0)
        TileMedianaImagenEstiramiento(1)
        TileAnchaImagenEstiramiento(2)
        TileGrandeImagenEstiramiento(2)

        TilePequeñaImagenMargen(0)
        TileMedianaImagenMargen(0)
        TileAnchaImagenMargen(0)
        TileGrandeImagenMargen(0)

        TilePequeñaImagenCoordenadas(0, 0)
        TileMedianaImagenCoordenadas(0, 0)
        TileAnchaImagenCoordenadas(0, 0)
        TileGrandeImagenCoordenadas(0, 0)

        TilePequeñaImagenZoom(1, 0, 0)
        TileMedianaImagenZoom(1, 0, 0)
        TileAnchaImagenZoom(1, 0, 0)
        TileGrandeImagenZoom(1, 0, 0)

    End Sub

    Public Sub MostrarTilePequeña(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("mostrar_tile_pequeña") = valor

        Dim cb As CheckBox = pagina.FindName("cbConfigTilePequeña")
        cb.IsChecked = valor

        Dim sp As StackPanel = pagina.FindName("spConfigTilePequeña")

        If valor = True Then
            sp.Visibility = Visibility.Visible
        Else
            sp.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub MostrarTileMediana(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("mostrar_tile_mediana") = valor

        Dim cb As CheckBox = pagina.FindName("cbConfigTileMediana")
        cb.IsChecked = valor

        Dim sp As StackPanel = pagina.FindName("spConfigTileMediana")

        If valor = True Then
            sp.Visibility = Visibility.Visible
        Else
            sp.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub MostrarTileAncha(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("mostrar_tile_ancha") = valor

        Dim cb As CheckBox = pagina.FindName("cbConfigTileAncha")
        cb.IsChecked = valor

        Dim sp As StackPanel = pagina.FindName("spConfigTileAncha")

        If valor = True Then
            sp.Visibility = Visibility.Visible
        Else
            sp.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub MostrarTileGrande(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("mostrar_tile_grande") = valor

        Dim cb As CheckBox = pagina.FindName("cbConfigTileGrande")
        cb.IsChecked = valor

        Dim sp As StackPanel = pagina.FindName("spConfigTileGrande")

        If valor = True Then
            sp.Visibility = Visibility.Visible
        Else
            sp.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub MostrarTilesDRM(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("mostrar_tiles_drm") = valor

        Dim grid As Grid = pagina.FindName("gridConfigTilesDRM")
        Dim icono As FontAwesome.UWP.FontAwesome = pagina.FindName("iconoConfigTilesDRM")

        If valor = True Then
            grid.Visibility = Visibility.Visible
            icono.Icon = FontAwesomeIcon.CaretUp
        Else
            grid.Visibility = Visibility.Collapsed
            icono.Icon = FontAwesomeIcon.CaretDown
        End If

    End Sub

    Public Sub MostrarTilesColor(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("mostrar_tiles_color") = valor

        Dim sp As StackPanel = pagina.FindName("spConfigTilesColor")
        Dim icono As FontAwesome.UWP.FontAwesome = pagina.FindName("iconoConfigTilesColor")

        If valor = True Then
            sp.Visibility = Visibility.Visible
            icono.Icon = FontAwesomeIcon.CaretUp
        Else
            sp.Visibility = Visibility.Collapsed
            icono.Icon = FontAwesomeIcon.CaretDown
        End If

    End Sub

    'Public Sub TileAnchaTitulo(valor As Boolean)

    '    Dim frame As Frame = Window.Current.Content
    '    Dim pagina As Page = frame.Content

    '    ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = valor

    '    Dim cb As CheckBox = pagina.FindName("cbConfigTileAnchaTitulo")
    '    cb.IsChecked = valor

    '    Dim titulo1 As TextBlock = pagina.FindName("tituloTileAncha")
    '    Dim titulo2 As TextBlock = pagina.FindName("tituloTileAnchaPersonalizar")

    '    If valor = True Then
    '        titulo1.Visibility = Visibility.Visible
    '        titulo2.Visibility = Visibility.Visible
    '    Else
    '        titulo1.Visibility = Visibility.Collapsed
    '        titulo2.Visibility = Visibility.Collapsed
    '    End If

    'End Sub

    'Public Sub TileGrandeTitulo(valor As Boolean)

    '    Dim frame As Frame = Window.Current.Content
    '    Dim pagina As Page = frame.Content

    '    ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") = valor

    '    Dim cb As CheckBox = pagina.FindName("cbConfigTileGrandeTitulo")
    '    cb.IsChecked = valor

    '    Dim titulo1 As TextBlock = pagina.FindName("tituloTileGrande")
    '    Dim titulo2 As TextBlock = pagina.FindName("tituloTileGrandePersonalizar")

    '    If valor = True Then
    '        titulo1.Visibility = Visibility.Visible
    '        titulo2.Visibility = Visibility.Visible
    '    Else
    '        titulo1.Visibility = Visibility.Collapsed
    '        titulo2.Visibility = Visibility.Collapsed
    '    End If

    'End Sub

    'Public Sub TilesColorTitulo(color As Integer)

    '    Dim frame As Frame = Window.Current.Content
    '    Dim pagina As Page = frame.Content

    '    ApplicationData.Current.LocalSettings.Values("tiles_color_titulo") = color

    '    Dim cbColor As ComboBox = pagina.FindName("cbConfigTileTituloColor")
    '    cbColor.SelectedIndex = color

    '    Dim titulo1 As TextBlock = pagina.FindName("tituloTileAncha")
    '    Dim titulo2 As TextBlock = pagina.FindName("tituloTileAnchaPersonalizar")

    '    Dim titulo3 As TextBlock = pagina.FindName("tituloTileGrande")
    '    Dim titulo4 As TextBlock = pagina.FindName("tituloTileGrandePersonalizar")

    '    If color = 0 Then
    '        titulo1.Foreground = New SolidColorBrush(Colors.White)
    '        titulo2.Foreground = New SolidColorBrush(Colors.White)

    '        titulo3.Foreground = New SolidColorBrush(Colors.White)
    '        titulo4.Foreground = New SolidColorBrush(Colors.White)
    '    Else
    '        titulo1.Foreground = New SolidColorBrush(Colors.Black)
    '        titulo2.Foreground = New SolidColorBrush(Colors.Black)

    '        titulo3.Foreground = New SolidColorBrush(Colors.Black)
    '        titulo4.Foreground = New SolidColorBrush(Colors.Black)
    '    End If

    'End Sub

    'Public Sub TileMedianaDRMMostrar(valor As Boolean)

    '    Dim frame As Frame = Window.Current.Content
    '    Dim pagina As Page = frame.Content

    '    ApplicationData.Current.LocalSettings.Values("tile_mediana_drm_mostrar") = valor

    '    Dim cb As CheckBox = pagina.FindName("cbConfigTileMedianaDRM")
    '    cb.IsChecked = valor

    '    Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileMediana")
    '    Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileMedianaPersonalizar")

    '    If valor = True Then
    '        imagen1.Visibility = Visibility.Visible
    '        imagen2.Visibility = Visibility.Visible
    '    Else
    '        imagen1.Visibility = Visibility.Collapsed
    '        imagen2.Visibility = Visibility.Collapsed
    '    End If

    'End Sub

    'Public Sub TileAnchaDRMMostrar(valor As Boolean)

    '    Dim frame As Frame = Window.Current.Content
    '    Dim pagina As Page = frame.Content

    '    ApplicationData.Current.LocalSettings.Values("tile_ancha_drm_mostrar") = valor

    '    Dim cb As CheckBox = pagina.FindName("cbConfigTileAnchaDRM")
    '    cb.IsChecked = valor

    '    Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileAncha")
    '    Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileAnchaPersonalizar")

    '    If valor = True Then
    '        imagen1.Visibility = Visibility.Visible
    '        imagen2.Visibility = Visibility.Visible
    '    Else
    '        imagen1.Visibility = Visibility.Collapsed
    '        imagen2.Visibility = Visibility.Collapsed
    '    End If

    'End Sub

    'Public Sub TileGrandeDRMMostrar(valor As Boolean)

    '    Dim frame As Frame = Window.Current.Content
    '    Dim pagina As Page = frame.Content

    '    ApplicationData.Current.LocalSettings.Values("tile_grande_drm_mostrar") = valor

    '    Dim cb As CheckBox = pagina.FindName("cbConfigTileGrandeDRM")
    '    cb.IsChecked = valor

    '    Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileGrande")
    '    Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileGrandePersonalizar")

    '    If valor = True Then
    '        imagen1.Visibility = Visibility.Visible
    '        imagen2.Visibility = Visibility.Visible
    '    Else
    '        imagen1.Visibility = Visibility.Collapsed
    '        imagen2.Visibility = Visibility.Collapsed
    '    End If

    'End Sub

    'Public Sub TilesDRMIcono(valor As Integer)

    '    Dim frame As Frame = Window.Current.Content
    '    Dim pagina As Page = frame.Content

    '    ApplicationData.Current.LocalSettings.Values("tiles_drm_icono") = valor

    '    Dim cb As ComboBox = pagina.FindName("cbConfigTilesDRMIcono")
    '    cb.SelectedIndex = valor

    '    Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileMediana")
    '    Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileMedianaPersonalizar")

    '    Dim imagen3 As ImageEx = pagina.FindName("imagenDRMTileAncha")
    '    Dim imagen4 As ImageEx = pagina.FindName("imagenDRMTileAnchaPersonalizar")

    '    Dim imagen5 As ImageEx = pagina.FindName("imagenDRMTileGrande")
    '    Dim imagen6 As ImageEx = pagina.FindName("imagenDRMTileGrandePersonalizar")

    '    imagen1.Source = cb.SelectedItem.Source
    '    imagen2.Source = cb.SelectedItem.Source

    '    imagen3.Source = cb.SelectedItem.Source
    '    imagen4.Source = cb.SelectedItem.Source

    '    imagen5.Source = cb.SelectedItem.Source
    '    imagen6.Source = cb.SelectedItem.Source

    'End Sub

    'Public Sub TilesDRMIconoPosicion(valor As Integer)

    '    Dim frame As Frame = Window.Current.Content
    '    Dim pagina As Page = frame.Content

    '    ApplicationData.Current.LocalSettings.Values("tiles_drm_icono_posicion") = valor

    '    Dim cb As ComboBox = pagina.FindName("cbConfigTilesDRMPosicion")
    '    cb.SelectedIndex = valor

    '    Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileMediana")
    '    Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileMedianaPersonalizar")

    '    Dim imagen3 As ImageEx = pagina.FindName("imagenDRMTileAncha")
    '    Dim imagen4 As ImageEx = pagina.FindName("imagenDRMTileAnchaPersonalizar")

    '    Dim imagen5 As ImageEx = pagina.FindName("imagenDRMTileGrande")
    '    Dim imagen6 As ImageEx = pagina.FindName("imagenDRMTileGrandePersonalizar")

    '    If valor = 0 Then
    '        imagen1.HorizontalAlignment = HorizontalAlignment.Left
    '        imagen2.HorizontalAlignment = HorizontalAlignment.Left

    '        imagen3.HorizontalAlignment = HorizontalAlignment.Left
    '        imagen4.HorizontalAlignment = HorizontalAlignment.Left

    '        imagen5.HorizontalAlignment = HorizontalAlignment.Left
    '        imagen6.HorizontalAlignment = HorizontalAlignment.Left
    '    Else
    '        imagen1.HorizontalAlignment = HorizontalAlignment.Right
    '        imagen2.HorizontalAlignment = HorizontalAlignment.Right

    '        imagen3.HorizontalAlignment = HorizontalAlignment.Right
    '        imagen4.HorizontalAlignment = HorizontalAlignment.Right

    '        imagen5.HorizontalAlignment = HorizontalAlignment.Right
    '        imagen6.HorizontalAlignment = HorizontalAlignment.Right
    '    End If

    'End Sub

    Public Sub TilesColorFondo(color As String)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tiles_color_fondo") = color

        Dim picker As ColorPicker = pagina.FindName("colorPickerFondoTiles")
        picker.Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color)

        Dim grid1 As Grid = pagina.FindName("gridTilePequeña")
        Dim grid2 As Grid = pagina.FindName("gridTileMediana")
        Dim grid3 As Grid = pagina.FindName("gridTileAncha")
        Dim grid4 As Grid = pagina.FindName("gridTileGrande")

        grid1.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid2.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid3.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid4.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))

    End Sub

    Public Async Sub TilesCambioImagen(imagen1 As ImageEx, imagen2 As ImageEx, imagen3 As ImageEx)

        Dim ficheroPicker As New FileOpenPicker
        ficheroPicker.FileTypeFilter.Add(".png")
        ficheroPicker.FileTypeFilter.Add(".jpg")
        ficheroPicker.ViewMode = PickerViewMode.List

        Dim ficheroImagen As StorageFile = Await ficheroPicker.PickSingleFileAsync

        Dim bitmap As New BitmapImage

        Try
            Dim stream As FileRandomAccessStream = Await ficheroImagen.OpenAsync(FileAccessMode.Read)
            bitmap.SetSource(stream)

            imagen1.Source = bitmap
            imagen2.Source = bitmap
            imagen3.Source = bitmap
        Catch ex As Exception

        End Try

    End Sub

    Public Sub TilePequeñaImagenEstiramiento(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_estiramiento") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTilePequeñaImagenEstiramiento")
        cb.SelectedIndex = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTilePequeña")

        If valor = 0 Then
            imagen.Stretch = Stretch.None
        ElseIf valor = 1 Then
            imagen.Stretch = Stretch.Uniform
        ElseIf valor = 2 Then
            imagen.Stretch = Stretch.UniformToFill
        ElseIf valor = 3 Then
            imagen.Stretch = Stretch.Fill
        End If

    End Sub

    Public Sub TileMedianaImagenEstiramiento(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_estiramiento") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTileMedianaImagenEstiramiento")
        cb.SelectedIndex = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTileMediana")

        If valor = 0 Then
            imagen.Stretch = Stretch.None
        ElseIf valor = 1 Then
            imagen.Stretch = Stretch.Uniform
        ElseIf valor = 2 Then
            imagen.Stretch = Stretch.UniformToFill
        ElseIf valor = 3 Then
            imagen.Stretch = Stretch.Fill
        End If

    End Sub

    Public Sub TileAnchaImagenEstiramiento(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_estiramiento") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTileAnchaImagenEstiramiento")
        cb.SelectedIndex = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTileAncha")

        If valor = 0 Then
            imagen.Stretch = Stretch.None
        ElseIf valor = 1 Then
            imagen.Stretch = Stretch.Uniform
        ElseIf valor = 2 Then
            imagen.Stretch = Stretch.UniformToFill
        ElseIf valor = 3 Then
            imagen.Stretch = Stretch.Fill
        End If

    End Sub

    Public Sub TileGrandeImagenEstiramiento(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_estiramiento") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTileGrandeImagenEstiramiento")
        cb.SelectedIndex = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTileGrande")

        If valor = 0 Then
            imagen.Stretch = Stretch.None
        ElseIf valor = 1 Then
            imagen.Stretch = Stretch.Uniform
        ElseIf valor = 2 Then
            imagen.Stretch = Stretch.UniformToFill
        ElseIf valor = 3 Then
            imagen.Stretch = Stretch.Fill
        End If

    End Sub

    Public Sub TilePequeñaImagenMargen(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_margen") = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTilePequeña")
        imagen.Margin = New Thickness(valor)

    End Sub

    Public Sub TileMedianaImagenMargen(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_margen") = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTileMediana")
        imagen.Margin = New Thickness(valor)

    End Sub

    Public Sub TileAnchaImagenMargen(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_margen") = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTileAncha")
        imagen.Margin = New Thickness(valor)

    End Sub

    Public Sub TileGrandeImagenMargen(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_margen") = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTileGrande")
        imagen.Margin = New Thickness(valor)

    End Sub

    Public Sub TilePequeñaImagenCoordenadas(x As Integer, y As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaX") = x
        ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaY") = y

        Dim sliderX As Slider = pagina.FindName("sliderConfigTilePequeñaImagenCoordenadasX")
        sliderX.Value = x

        Dim sliderY As Slider = pagina.FindName("sliderConfigTilePequeñaImagenCoordenadasY")
        sliderY.Value = y

        Dim imagen As ImageEx = pagina.FindName("imagenTilePequeña")

        Dim mover As New TranslateTransform With {
            .X = x,
            .Y = y
        }

        imagen.RenderTransform = mover

    End Sub

    Public Sub TileMedianaImagenCoordenadas(x As Integer, y As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_coordenadaX") = x
        ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_coordenadaY") = y

        Dim sliderX As Slider = pagina.FindName("sliderConfigTileMedianaImagenCoordenadasX")
        sliderX.Value = x

        Dim sliderY As Slider = pagina.FindName("sliderConfigTileMedianaImagenCoordenadasY")
        sliderY.Value = y

        Dim imagen As ImageEx = pagina.FindName("imagenTileMediana")

        Dim mover As New TranslateTransform With {
            .X = x,
            .Y = y
        }

        imagen.RenderTransform = mover

    End Sub

    Public Sub TileAnchaImagenCoordenadas(x As Integer, y As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_coordenadaX") = x
        ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_coordenadaY") = y

        Dim sliderX As Slider = pagina.FindName("sliderConfigTileAnchaImagenCoordenadasX")
        sliderX.Value = x

        Dim sliderY As Slider = pagina.FindName("sliderConfigTileAnchaImagenCoordenadasY")
        sliderY.Value = y

        Dim imagen As ImageEx = pagina.FindName("imagenTileAncha")

        Dim mover As New TranslateTransform With {
            .X = x,
            .Y = y
        }

        imagen.RenderTransform = mover

    End Sub

    Public Sub TileGrandeImagenCoordenadas(x As Integer, y As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_coordenadaX") = x
        ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_coordenadaY") = y

        Dim sliderX As Slider = pagina.FindName("sliderConfigTileGrandeImagenCoordenadasX")
        sliderX.Value = x

        Dim sliderY As Slider = pagina.FindName("sliderConfigTileGrandeImagenCoordenadasY")
        sliderY.Value = y

        Dim imagen As ImageEx = pagina.FindName("imagenTileGrande")

        Dim mover As New TranslateTransform With {
            .X = x,
            .Y = y
        }

        imagen.RenderTransform = mover

    End Sub

    Public Sub TilePequeñaImagenZoom(valor As Integer, x As Integer, y As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_zoom") = valor

        Dim slider As Slider = pagina.FindName("sliderConfigTilePequeñaImagenZoom")
        slider.Value = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTilePequeña")

        Dim zoom As New CompositeTransform With {
            .ScaleX = valor,
            .ScaleY = valor,
            .TranslateX = x,
            .TranslateY = y
        }

        imagen.RenderTransform = zoom

    End Sub

    Public Sub TileMedianaImagenZoom(valor As Integer, x As Integer, y As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_zoom") = valor

        Dim slider As Slider = pagina.FindName("sliderConfigTileMedianaImagenZoom")
        slider.Value = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTileMediana")

        Dim zoom As New CompositeTransform With {
            .ScaleX = valor,
            .ScaleY = valor,
            .TranslateX = x,
            .TranslateY = y
        }

        imagen.RenderTransform = zoom

    End Sub

    Public Sub TileAnchaImagenZoom(valor As Integer, x As Integer, y As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_zoom") = valor

        Dim slider As Slider = pagina.FindName("sliderConfigTileAnchaImagenZoom")
        slider.Value = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTileAncha")

        Dim zoom As New CompositeTransform With {
            .ScaleX = valor,
            .ScaleY = valor,
            .TranslateX = x,
            .TranslateY = y
        }

        imagen.RenderTransform = zoom

    End Sub

    Public Sub TileGrandeImagenZoom(valor As Integer, x As Integer, y As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_zoom") = valor

        Dim slider As Slider = pagina.FindName("sliderConfigTileGrandeImagenZoom")
        slider.Value = valor

        Dim imagen As ImageEx = pagina.FindName("imagenTileGrande")

        Dim zoom As New CompositeTransform With {
            .ScaleX = valor,
            .ScaleY = valor,
            .TranslateX = x,
            .TranslateY = y
        }

        imagen.RenderTransform = zoom

    End Sub

End Module
