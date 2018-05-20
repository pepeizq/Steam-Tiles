Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.UI

Module Configuracion

    Public Sub Iniciar()

        If ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") Is Nothing Then
            TileAnchaTitulo(False)
        Else
            TileAnchaTitulo(ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") Is Nothing Then
            TileGrandeTitulo(True)
        Else
            TileGrandeTitulo(ApplicationData.Current.LocalSettings.Values("tile_grande_titulo"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tiles_color_titulo") Is Nothing Then
            TilesColorTitulo(0)
        Else
            TilesColorTitulo(ApplicationData.Current.LocalSettings.Values("tiles_color_titulo"))
        End If
        '------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("tile_mediana_drm_mostrar") Is Nothing Then
            TileMedianaDRMMostrar(False)
        Else
            TileMedianaDRMMostrar(ApplicationData.Current.LocalSettings.Values("tile_mediana_drm_mostrar"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_ancha_drm_mostrar") Is Nothing Then
            TileAnchaDRMMostrar(False)
        Else
            TileAnchaDRMMostrar(ApplicationData.Current.LocalSettings.Values("tile_ancha_drm_mostrar"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tile_grande_drm_mostrar") Is Nothing Then
            TileGrandeDRMMostrar(True)
        Else
            TileGrandeDRMMostrar(ApplicationData.Current.LocalSettings.Values("tile_grande_drm_mostrar"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tiles_drm_icono") Is Nothing Then
            TilesDRMIcono(0)
        Else
            TilesDRMIcono(ApplicationData.Current.LocalSettings.Values("tiles_drm_icono"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tiles_drm_icono_posicion") Is Nothing Then
            TilesDRMIconoPosicion(1)
        Else
            TilesDRMIconoPosicion(ApplicationData.Current.LocalSettings.Values("tiles_drm_icono_posicion"))
        End If

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
            TileGrandeImagenEstiramiento(1)
        Else
            TileGrandeImagenEstiramiento(ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_estiramiento"))
        End If

        '------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaX") Is Nothing And ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaY") Is Nothing Then
            TilePequeñaImagenCoordenadas(0, 0)
        Else
            TilePequeñaImagenCoordenadas(ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaX"), ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_coordenadaY"))
        End If

    End Sub

    Public Sub Resetear()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = False

        Dim cb1 As CheckBox = pagina.FindName("cbConfigTileAnchaTitulo")
        cb1.IsChecked = False

        ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") = True

        Dim cb2 As CheckBox = pagina.FindName("cbConfigTileGrandeTitulo")
        cb2.IsChecked = True

        ApplicationData.Current.LocalSettings.Values("tiles_color_titulo") = 0

        Dim cb3 As ComboBox = pagina.FindName("cbConfigTileTituloColor")
        cb3.SelectedIndex = 0

        ApplicationData.Current.LocalSettings.Values("tile_mediana_drm_mostrar") = False

        Dim cb4 As CheckBox = pagina.FindName("cbConfigTileMedianaDRM")
        cb4.IsChecked = False

        ApplicationData.Current.LocalSettings.Values("tile_ancha_drm_mostrar") = False

        Dim cb5 As CheckBox = pagina.FindName("cbConfigTileAnchaDRM")
        cb5.IsChecked = False

        ApplicationData.Current.LocalSettings.Values("tile_grande_drm_mostrar") = False

        Dim cb6 As CheckBox = pagina.FindName("cbConfigTileGrandeDRM")
        cb6.IsChecked = True

        ApplicationData.Current.LocalSettings.Values("tiles_drm_icono") = 0

        Dim cb7 As ComboBox = pagina.FindName("cbConfigTilesDRMIcono")
        cb7.SelectedIndex = 0

        ApplicationData.Current.LocalSettings.Values("tiles_drm_icono_posicion") = 1

        Dim cb8 As ComboBox = pagina.FindName("cbConfigTilesDRMPosicion")
        cb8.SelectedIndex = 1

        ApplicationData.Current.LocalSettings.Values("tiles_color_fondo") = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToHex(App.Current.Resources("ColorSecundario"))

        Dim picker As ColorPicker = pagina.FindName("colorPickerFondoTiles")
        picker.Color = App.Current.Resources("ColorSecundario")

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

        ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_estiramiento") = 0

        Dim cb9 As ComboBox = pagina.FindName("cbConfigTilePequeñaImagenEstiramiento")
        cb9.SelectedIndex = 0

        ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_estiramiento") = 1

        Dim cb10 As ComboBox = pagina.FindName("cbConfigTileMedianaImagenEstiramiento")
        cb10.SelectedIndex = 1

        ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_estiramiento") = 2

        Dim cb11 As ComboBox = pagina.FindName("cbConfigTileAnchaImagenEstiramiento")
        cb11.SelectedIndex = 2

        ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_estiramiento") = 1

        Dim cb12 As ComboBox = pagina.FindName("cbConfigTileGrandeImagenEstiramiento")
        cb12.SelectedIndex = 1

    End Sub

    Public Sub TileAnchaTitulo(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = valor

        Dim cb As CheckBox = pagina.FindName("cbConfigTileAnchaTitulo")
        cb.IsChecked = valor

        Dim titulo1 As TextBlock = pagina.FindName("tituloTileAnchaEnseñar")
        Dim titulo2 As TextBlock = pagina.FindName("tituloTileAnchaPersonalizar")

        If valor = True Then
            titulo1.Visibility = Visibility.Visible
            titulo2.Visibility = Visibility.Visible
        Else
            titulo1.Visibility = Visibility.Collapsed
            titulo2.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub TileGrandeTitulo(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") = valor

        Dim cb As CheckBox = pagina.FindName("cbConfigTileGrandeTitulo")
        cb.IsChecked = valor

        Dim titulo1 As TextBlock = pagina.FindName("tituloTileGrandeEnseñar")
        Dim titulo2 As TextBlock = pagina.FindName("tituloTileGrandePersonalizar")

        If valor = True Then
            titulo1.Visibility = Visibility.Visible
            titulo2.Visibility = Visibility.Visible
        Else
            titulo1.Visibility = Visibility.Collapsed
            titulo2.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub TilesColorTitulo(color As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tiles_color_titulo") = color

        Dim cbColor As ComboBox = pagina.FindName("cbConfigTileTituloColor")
        cbColor.SelectedIndex = color

        Dim titulo1 As TextBlock = pagina.FindName("tituloTileAnchaEnseñar")
        Dim titulo2 As TextBlock = pagina.FindName("tituloTileAnchaPersonalizar")

        Dim titulo3 As TextBlock = pagina.FindName("tituloTileGrandeEnseñar")
        Dim titulo4 As TextBlock = pagina.FindName("tituloTileGrandePersonalizar")

        If color = 0 Then
            titulo1.Foreground = New SolidColorBrush(Colors.White)
            titulo2.Foreground = New SolidColorBrush(Colors.White)

            titulo3.Foreground = New SolidColorBrush(Colors.White)
            titulo4.Foreground = New SolidColorBrush(Colors.White)
        Else
            titulo1.Foreground = New SolidColorBrush(Colors.Black)
            titulo2.Foreground = New SolidColorBrush(Colors.Black)

            titulo3.Foreground = New SolidColorBrush(Colors.Black)
            titulo4.Foreground = New SolidColorBrush(Colors.Black)
        End If

    End Sub

    Public Sub TileMedianaDRMMostrar(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_mediana_drm_mostrar") = valor

        Dim cb As CheckBox = pagina.FindName("cbConfigTileMedianaDRM")
        cb.IsChecked = valor

        Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileMedianaEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileMedianaPersonalizar")

        If valor = True Then
            imagen1.Visibility = Visibility.Visible
            imagen2.Visibility = Visibility.Visible
        Else
            imagen1.Visibility = Visibility.Collapsed
            imagen2.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub TileAnchaDRMMostrar(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_ancha_drm_mostrar") = valor

        Dim cb As CheckBox = pagina.FindName("cbConfigTileAnchaDRM")
        cb.IsChecked = valor

        Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileAnchaEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileAnchaPersonalizar")

        If valor = True Then
            imagen1.Visibility = Visibility.Visible
            imagen2.Visibility = Visibility.Visible
        Else
            imagen1.Visibility = Visibility.Collapsed
            imagen2.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub TileGrandeDRMMostrar(valor As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_grande_drm_mostrar") = valor

        Dim cb As CheckBox = pagina.FindName("cbConfigTileGrandeDRM")
        cb.IsChecked = valor

        Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileGrandeEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileGrandePersonalizar")

        If valor = True Then
            imagen1.Visibility = Visibility.Visible
            imagen2.Visibility = Visibility.Visible
        Else
            imagen1.Visibility = Visibility.Collapsed
            imagen2.Visibility = Visibility.Collapsed
        End If

    End Sub

    Public Sub TilesDRMIcono(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tiles_drm_icono") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTilesDRMIcono")
        cb.SelectedIndex = valor

        Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileMedianaEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileMedianaPersonalizar")

        Dim imagen3 As ImageEx = pagina.FindName("imagenDRMTileAnchaEnseñar")
        Dim imagen4 As ImageEx = pagina.FindName("imagenDRMTileAnchaPersonalizar")

        Dim imagen5 As ImageEx = pagina.FindName("imagenDRMTileGrandeEnseñar")
        Dim imagen6 As ImageEx = pagina.FindName("imagenDRMTileGrandePersonalizar")

        imagen1.Source = cb.SelectedItem.Source
        imagen2.Source = cb.SelectedItem.Source

        imagen3.Source = cb.SelectedItem.Source
        imagen4.Source = cb.SelectedItem.Source

        imagen5.Source = cb.SelectedItem.Source
        imagen6.Source = cb.SelectedItem.Source

    End Sub

    Public Sub TilesDRMIconoPosicion(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tiles_drm_icono_posicion") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTilesDRMPosicion")
        cb.SelectedIndex = valor

        Dim imagen1 As ImageEx = pagina.FindName("imagenDRMTileMedianaEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenDRMTileMedianaPersonalizar")

        Dim imagen3 As ImageEx = pagina.FindName("imagenDRMTileAnchaEnseñar")
        Dim imagen4 As ImageEx = pagina.FindName("imagenDRMTileAnchaPersonalizar")

        Dim imagen5 As ImageEx = pagina.FindName("imagenDRMTileGrandeEnseñar")
        Dim imagen6 As ImageEx = pagina.FindName("imagenDRMTileGrandePersonalizar")

        If valor = 0 Then
            imagen1.HorizontalAlignment = HorizontalAlignment.Left
            imagen2.HorizontalAlignment = HorizontalAlignment.Left

            imagen3.HorizontalAlignment = HorizontalAlignment.Left
            imagen4.HorizontalAlignment = HorizontalAlignment.Left

            imagen5.HorizontalAlignment = HorizontalAlignment.Left
            imagen6.HorizontalAlignment = HorizontalAlignment.Left
        Else
            imagen1.HorizontalAlignment = HorizontalAlignment.Right
            imagen2.HorizontalAlignment = HorizontalAlignment.Right

            imagen3.HorizontalAlignment = HorizontalAlignment.Right
            imagen4.HorizontalAlignment = HorizontalAlignment.Right

            imagen5.HorizontalAlignment = HorizontalAlignment.Right
            imagen6.HorizontalAlignment = HorizontalAlignment.Right
        End If

    End Sub

    Public Sub TilesColorFondo(color As String)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tiles_color_fondo") = color

        Dim picker As ColorPicker = pagina.FindName("colorPickerFondoTiles")
        picker.Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color)

        Dim grid1 As Grid = pagina.FindName("gridTilePequeñaEnseñar")
        Dim grid2 As Grid = pagina.FindName("gridTilePequeñaGenerar")
        Dim grid3 As Grid = pagina.FindName("gridTilePequeñaPersonalizar")

        Dim grid4 As Grid = pagina.FindName("gridTileMedianaEnseñar")
        Dim grid5 As Grid = pagina.FindName("gridTileMedianaGenerar")
        Dim grid6 As Grid = pagina.FindName("gridTileMedianaPersonalizar")

        Dim grid7 As Grid = pagina.FindName("gridTileAnchaEnseñar")
        Dim grid8 As Grid = pagina.FindName("gridTileAnchaGenerar")
        Dim grid9 As Grid = pagina.FindName("gridTileAnchaPersonalizar")

        Dim grid10 As Grid = pagina.FindName("gridTileGrandeEnseñar")
        Dim grid11 As Grid = pagina.FindName("gridTileGrandeGenerar")
        Dim grid12 As Grid = pagina.FindName("gridTileGrandePersonalizar")

        grid1.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid2.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid3.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))

        grid4.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid5.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid6.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))

        grid7.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid8.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid9.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))

        grid10.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid11.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))
        grid12.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(color))

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

        Dim imagen1 As ImageEx = pagina.FindName("imagenTilePequeñaEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenTilePequeñaGenerar")
        Dim imagen3 As ImageEx = pagina.FindName("imagenTilePequeñaPersonalizar")

        If valor = 0 Then
            imagen1.Stretch = Stretch.None
            imagen2.Stretch = Stretch.None
            imagen3.Stretch = Stretch.None
        ElseIf valor = 1 Then
            imagen1.Stretch = Stretch.Uniform
            imagen2.Stretch = Stretch.Uniform
            imagen3.Stretch = Stretch.Uniform
        ElseIf valor = 2 Then
            imagen1.Stretch = Stretch.UniformToFill
            imagen2.Stretch = Stretch.UniformToFill
            imagen3.Stretch = Stretch.UniformToFill
        ElseIf valor = 3 Then
            imagen1.Stretch = Stretch.Fill
            imagen2.Stretch = Stretch.Fill
            imagen3.Stretch = Stretch.Fill
        End If

    End Sub

    Public Sub TileMedianaImagenEstiramiento(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_mediana_imagen_estiramiento") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTileMedianaImagenEstiramiento")
        cb.SelectedIndex = valor

        Dim imagen1 As ImageEx = pagina.FindName("imagenTileMedianaEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenTileMedianaGenerar")
        Dim imagen3 As ImageEx = pagina.FindName("imagenTileMedianaPersonalizar")

        If valor = 0 Then
            imagen1.Stretch = Stretch.None
            imagen2.Stretch = Stretch.None
            imagen3.Stretch = Stretch.None
        ElseIf valor = 1 Then
            imagen1.Stretch = Stretch.Uniform
            imagen2.Stretch = Stretch.Uniform
            imagen3.Stretch = Stretch.Uniform
        ElseIf valor = 2 Then
            imagen1.Stretch = Stretch.UniformToFill
            imagen2.Stretch = Stretch.UniformToFill
            imagen3.Stretch = Stretch.UniformToFill
        ElseIf valor = 3 Then
            imagen1.Stretch = Stretch.Fill
            imagen2.Stretch = Stretch.Fill
            imagen3.Stretch = Stretch.Fill
        End If

    End Sub

    Public Sub TileAnchaImagenEstiramiento(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_ancha_imagen_estiramiento") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTileAnchaImagenEstiramiento")
        cb.SelectedIndex = valor

        Dim imagen1 As ImageEx = pagina.FindName("imagenTileAnchaEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenTileAnchaGenerar")
        Dim imagen3 As ImageEx = pagina.FindName("imagenTileAnchaPersonalizar")

        If valor = 0 Then
            imagen1.Stretch = Stretch.None
            imagen2.Stretch = Stretch.None
            imagen3.Stretch = Stretch.None
        ElseIf valor = 1 Then
            imagen1.Stretch = Stretch.Uniform
            imagen2.Stretch = Stretch.Uniform
            imagen3.Stretch = Stretch.Uniform
        ElseIf valor = 2 Then
            imagen1.Stretch = Stretch.UniformToFill
            imagen2.Stretch = Stretch.UniformToFill
            imagen3.Stretch = Stretch.UniformToFill
        ElseIf valor = 3 Then
            imagen1.Stretch = Stretch.Fill
            imagen2.Stretch = Stretch.Fill
            imagen3.Stretch = Stretch.Fill
        End If

    End Sub

    Public Sub TileGrandeImagenEstiramiento(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_grande_imagen_estiramiento") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTileGrandeImagenEstiramiento")
        cb.SelectedIndex = valor

        Dim imagen1 As ImageEx = pagina.FindName("imagenTileGrandeEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenTileGrandeGenerar")
        Dim imagen3 As ImageEx = pagina.FindName("imagenTileGrandePersonalizar")

        If valor = 0 Then
            imagen1.Stretch = Stretch.None
            imagen2.Stretch = Stretch.None
            imagen3.Stretch = Stretch.None
        ElseIf valor = 1 Then
            imagen1.Stretch = Stretch.Uniform
            imagen2.Stretch = Stretch.Uniform
            imagen3.Stretch = Stretch.Uniform
        ElseIf valor = 2 Then
            imagen1.Stretch = Stretch.UniformToFill
            imagen2.Stretch = Stretch.UniformToFill
            imagen3.Stretch = Stretch.UniformToFill
        ElseIf valor = 3 Then
            imagen1.Stretch = Stretch.Fill
            imagen2.Stretch = Stretch.Fill
            imagen3.Stretch = Stretch.Fill
        End If

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

    End Sub

End Module
