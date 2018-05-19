Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.UI

Module Configuracion

    Public Sub Iniciar()

        If ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_estiramiento") Is Nothing Then
            TilePequeñaImagenEstiramiento(0)
        Else
            TilePequeñaImagenEstiramiento(ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_estiramiento"))
        End If

        If ApplicationData.Current.LocalSettings.Values("tiles_color_fondo") Is Nothing Then
            TilesColorFondo(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToHex(App.Current.Resources("ColorSecundario")))
        Else
            TilesColorFondo(ApplicationData.Current.LocalSettings.Values("tiles_color_fondo"))
        End If




        If ApplicationData.Current.LocalSettings.Values("drm_tile") Is Nothing Then
            MostrarDRM(False)
        Else
            MostrarDRM(ApplicationData.Current.LocalSettings.Values("drm_tile"))
        End If

    End Sub

    Public Sub Resetear()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tiles_color_fondo") = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToHex(App.Current.Resources("ColorSecundario"))

        Dim picker As ColorPicker = pagina.FindName("colorPickerFondoTiles")
        picker.Color = App.Current.Resources("ColorSecundario")

    End Sub

    Public Sub TilePequeñaImagenEstiramiento(valor As Integer)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("tile_pequeña_imagen_estiramiento") = valor

        Dim cb As ComboBox = pagina.FindName("cbConfigTilePequeñaImagenEstiramiento")
        cb.SelectedIndex = valor

        Dim imagen1 As ImageEx = pagina.FindName("imagenTilePequeñaEnseñar")
        Dim imagen2 As ImageEx = pagina.FindName("imagenTilePequeñaGenerar")

        If valor = 0 Then
            imagen1.Stretch = Stretch.None
            imagen2.Stretch = Stretch.None
        ElseIf valor = 1 Then
            imagen1.Stretch = Stretch.Uniform
            imagen2.Stretch = Stretch.Uniform
        ElseIf valor = 2 Then
            imagen1.Stretch = Stretch.UniformToFill
            imagen2.Stretch = Stretch.UniformToFill
        ElseIf valor = 3 Then
            imagen1.Stretch = Stretch.Fill
            imagen2.Stretch = Stretch.Fill
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

    Public Sub MostrarDRM(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("drm_tile") = estado

        Dim cb As CheckBox = pagina.FindName("cbTilesIconos")
        cb.IsChecked = estado

        Dim cbLista As ComboBox = pagina.FindName("cbTilesIconosLista")
        cbLista.SelectedIndex = 0

        If estado = True Then
            cbLista.Visibility = Visibility.Visible
        Else
            cbLista.Visibility = Visibility.Collapsed
        End If

    End Sub

End Module
