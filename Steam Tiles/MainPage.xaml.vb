Imports FontAwesome.UWP
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_Loaded(sender As Object, e As RoutedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("Tiles"), FontAwesomeIcon.Home, 0))
        nvPrincipal.MenuItems.Add(New NavigationViewItemSeparator)
        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("Config"), FontAwesomeIcon.Cog, 1))

    End Sub

    Private Sub Nv_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        Dim item As TextBlock = args.InvokedItem

        If Not item Is Nothing Then
            If item.Text = recursos.GetString("Tiles") Then
                GridVisibilidad(gridTiles, item.Text)
            ElseIf item.Text = recursos.GetString("Config") Then
                GridVisibilidad(gridConfig, item.Text)
            End If
        End If

    End Sub

    Private Sub Nv_ItemFlyout(sender As NavigationViewItem, args As TappedRoutedEventArgs)

        FlyoutBase.ShowAttachedFlyout(sender)

    End Sub

    Private Sub Page_Loaded(sender As FrameworkElement, args As Object)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        MasCosas.Generar()

        Dim recursos As New Resources.ResourceLoader()

        GridVisibilidad(gridTiles, recursos.GetString("Tiles"))
        nvPrincipal.IsPaneOpen = False

        Steam.Generar(False)
        Configuracion.Iniciar()

        '--------------------------------------------------------

        Dim transpariencia As New UISettings
        TransparienciaEfectosFinal(transpariencia.AdvancedEffectsEnabled)
        AddHandler transpariencia.AdvancedEffectsEnabledChanged, AddressOf TransparienciaEfectosCambia

    End Sub

    Private Sub TransparienciaEfectosCambia(sender As UISettings, e As Object)

        TransparienciaEfectosFinal(sender.AdvancedEffectsEnabled)

    End Sub

    Private Async Sub TransparienciaEfectosFinal(estado As Boolean)

        Await Dispatcher.RunAsync(CoreDispatcherPriority.High, Sub()
                                                                   If estado = True Then
                                                                       gridAñadirTile.Background = App.Current.Resources("GridAcrilico")
                                                                       gridPersonalizarTiles.Background = App.Current.Resources("GridAcrilico")
                                                                       gridConfig.Background = App.Current.Resources("GridAcrilico")
                                                                       gridConfigTiles.Background = App.Current.Resources("GridTituloBackground")
                                                                   Else
                                                                       gridAñadirTile.Background = New SolidColorBrush(Colors.LightGray)
                                                                       gridPersonalizarTiles.Background = New SolidColorBrush(Colors.LightGray)
                                                                       gridConfig.Background = New SolidColorBrush(Colors.LightGray)
                                                                       gridConfigTiles.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                                                                   End If
                                                               End Sub)

    End Sub

    Private Sub GridVisibilidad(grid As Grid, tag As String)

        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - " + tag

        gridAñadirTile.Visibility = Visibility.Collapsed
        gridPersonalizarTiles.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    Private Sub UsuarioEntraBoton2(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        Dim boton As Button = sender
        Dim grid As Grid = boton.Content
        Dim icono As FontAwesome.UWP.FontAwesome = grid.Children(1)

        If icono.Visibility = Visibility.Visible Then
            icono.Foreground = New SolidColorBrush(Colors.White)
        End If

    End Sub

    Private Sub UsuarioSaleBoton2(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        Dim boton As Button = sender
        Dim grid As Grid = boton.Content
        Dim icono As FontAwesome.UWP.FontAwesome = grid.Children(1)

        If icono.Visibility = Visibility.Visible Then
            icono.Foreground = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        End If

    End Sub

    'TILES-----------------------------------------------------------------------------

    Private Sub BotonAñadirTile_Click(sender As Object, e As RoutedEventArgs) Handles botonAñadirTile.Click

        Dim tile As Tile = botonAñadirTile.Tag
        Tiles.Generar(tile)

    End Sub

    Private Sub BotonPersonalizarTiles_Click(sender As Object, e As RoutedEventArgs) Handles botonPersonalizarTiles.Click

        gridAñadirTile.Visibility = Visibility.Collapsed
        gridPersonalizarTiles.Visibility = Visibility.Visible

    End Sub

    Private Sub BotonVolverPersonalizacion_Click(sender As Object, e As RoutedEventArgs) Handles botonVolverPersonalizacion.Click

        gridAñadirTile.Visibility = Visibility.Visible
        gridPersonalizarTiles.Visibility = Visibility.Collapsed

    End Sub

    Private Sub BotonResetearPersonalizacion_Click(sender As Object, e As RoutedEventArgs) Handles botonResetearPersonalizacion.Click

        Configuracion.Resetear

    End Sub

    Private Sub CbConfigTilePequeña_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigTilePequeña.Checked

        Configuracion.MostrarTilePequeña(cbConfigTilePequeña.IsChecked)

    End Sub

    Private Sub CbConfigTilePequeña_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigTilePequeña.Unchecked

        Configuracion.MostrarTilePequeña(cbConfigTilePequeña.IsChecked)

    End Sub

    Private Sub CbConfigTileMediana_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileMediana.Checked

        Configuracion.MostrarTileMediana(cbConfigTileMediana.IsChecked)

    End Sub

    Private Sub CbConfigTileMediana_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileMediana.Unchecked

        Configuracion.MostrarTileMediana(cbConfigTileMediana.IsChecked)

    End Sub

    Private Sub CbConfigTileAncha_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileAncha.Checked

        Configuracion.MostrarTileAncha(cbConfigTileAncha.IsChecked)

    End Sub

    Private Sub CbConfigTileAncha_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileAncha.Unchecked

        Configuracion.MostrarTileAncha(cbConfigTileAncha.IsChecked)

    End Sub

    Private Sub CbConfigTileGrande_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileGrande.Checked

        Configuracion.MostrarTileGrande(cbConfigTileGrande.IsChecked)

    End Sub

    Private Sub CbConfigTileGrande_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileGrande.Unchecked

        Configuracion.MostrarTileGrande(cbConfigTileGrande.IsChecked)

    End Sub

    Private Sub BotonConfigTilePequeñaBuscarImagen_Click(sender As Object, e As RoutedEventArgs) Handles botonConfigTilePequeñaBuscarImagen.Click

        Configuracion.TilesCambioImagen(imagenTilePequeñaGenerar, imagenTilePequeñaEnseñar, imagenTilePequeñaPersonalizar)

    End Sub

    Private Sub BotonConfigTileMedianaBuscarImagen_Click(sender As Object, e As RoutedEventArgs) Handles botonConfigTileMedianaBuscarImagen.Click

        Configuracion.TilesCambioImagen(imagenTileMedianaGenerar, imagenTileMedianaEnseñar, imagenTileMedianaPersonalizar)

    End Sub

    Private Sub BotonConfigTileAnchaBuscarImagen_Click(sender As Object, e As RoutedEventArgs) Handles botonConfigTileAnchaBuscarImagen.Click

        Configuracion.TilesCambioImagen(imagenTileAnchaGenerar, imagenTileAnchaEnseñar, imagenTileAnchaPersonalizar)

    End Sub

    Private Sub BotonConfigTileGrandeBuscarImagen_Click(sender As Object, e As RoutedEventArgs) Handles botonConfigTileGrandeBuscarImagen.Click

        Configuracion.TilesCambioImagen(imagenTileGrandeGenerar, imagenTileGrandeEnseñar, imagenTileGrandePersonalizar)

    End Sub

    Private Sub CbConfigTileAnchaTitulo_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileAnchaTitulo.Checked

        Configuracion.TileAnchaTitulo(cbConfigTileAnchaTitulo.IsChecked)

    End Sub

    Private Sub CbConfigTileAnchaTitulo_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileAnchaTitulo.Unchecked

        Configuracion.TileAnchaTitulo(cbConfigTileAnchaTitulo.IsChecked)

    End Sub

    Private Sub CbConfigTileGrandeTitulo_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileGrandeTitulo.Checked

        Configuracion.TileGrandeTitulo(cbConfigTileGrandeTitulo.IsChecked)

    End Sub

    Private Sub CbConfigTileGrandeTitulo_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileGrandeTitulo.Unchecked

        Configuracion.TileGrandeTitulo(cbConfigTileGrandeTitulo.IsChecked)

    End Sub

    Private Sub CbConfigTileTituloColor_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigTileTituloColor.SelectionChanged

        Try
            Configuracion.TilesColorTitulo(cbConfigTileTituloColor.SelectedIndex)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BotonConfigTilesDRM_Click(sender As Object, e As RoutedEventArgs) Handles botonConfigTilesDRM.Click

        If gridConfigTilesDRM.Visibility = Visibility.Visible Then
            Configuracion.MostrarTilesDRM(False)
        Else
            Configuracion.MostrarTilesDRM(True)
        End If

    End Sub

    Private Sub CbConfigTileMedianaDRM_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileMedianaDRM.Checked

        Configuracion.TileMedianaDRMMostrar(cbConfigTileMedianaDRM.IsChecked)

    End Sub

    Private Sub CbConfigTileMedianaDRM_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileMedianaDRM.Unchecked

        Configuracion.TileMedianaDRMMostrar(cbConfigTileMedianaDRM.IsChecked)

    End Sub

    Private Sub CbConfigTileAnchaDRM_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileAnchaDRM.Checked

        Configuracion.TileAnchaDRMMostrar(cbConfigTileAnchaDRM.IsChecked)

    End Sub

    Private Sub CbConfigTileAnchaDRM_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileAnchaDRM.Unchecked

        Configuracion.TileAnchaDRMMostrar(cbConfigTileAnchaDRM.IsChecked)

    End Sub

    Private Sub CbConfigTileGrandeDRM_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileGrandeDRM.Checked

        Configuracion.TileGrandeDRMMostrar(cbConfigTileGrandeDRM.IsChecked)

    End Sub

    Private Sub CbConfigTileGrandeDRM_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigTileGrandeDRM.Unchecked

        Configuracion.TileGrandeDRMMostrar(cbConfigTileGrandeDRM.IsChecked)

    End Sub

    Private Sub CbConfigTilesDRMIcono_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigTilesDRMIcono.SelectionChanged

        Try
            Configuracion.TilesDRMIcono(cbConfigTilesDRMIcono.SelectedIndex)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CbConfigTilesDRMPosicion_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigTilesDRMPosicion.SelectionChanged

        Try
            Configuracion.TilesDRMIconoPosicion(cbConfigTilesDRMPosicion.SelectedIndex)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BotonConfigTilesColor_Click(sender As Object, e As RoutedEventArgs) Handles botonConfigTilesColor.Click

        If spConfigTilesColor.Visibility = Visibility.Visible Then
            Configuracion.MostrarTilesColor(False)
        Else
            Configuracion.MostrarTilesColor(True)
        End If

    End Sub

    Private Sub ColorPickerFondoTiles_ColorChanged(sender As ColorPicker, args As ColorChangedEventArgs) Handles colorPickerFondoTiles.ColorChanged

        Configuracion.TilesColorFondo(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToHex(colorPickerFondoTiles.Color))

    End Sub

    Private Sub CbConfigTilePequeñaImagenEstiramiento_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigTilePequeñaImagenEstiramiento.SelectionChanged

        Configuracion.TilePequeñaImagenEstiramiento(cbConfigTilePequeñaImagenEstiramiento.SelectedIndex)

    End Sub

    Private Sub CbConfigTileMedianaImagenEstiramiento_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigTileMedianaImagenEstiramiento.SelectionChanged

        Configuracion.TileMedianaImagenEstiramiento(cbConfigTileMedianaImagenEstiramiento.SelectedIndex)

    End Sub

    Private Sub CbConfigTileAnchaImagenEstiramiento_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigTileAnchaImagenEstiramiento.SelectionChanged

        Configuracion.TileAnchaImagenEstiramiento(cbConfigTileAnchaImagenEstiramiento.SelectedIndex)

    End Sub

    Private Sub CbConfigTileGrandeImagenEstiramiento_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigTileGrandeImagenEstiramiento.SelectionChanged

        Configuracion.TileGrandeImagenEstiramiento(cbConfigTileGrandeImagenEstiramiento.SelectedIndex)

    End Sub

    Private Sub SliderConfigTilePequeñaImagenMargen_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTilePequeñaImagenMargen.ValueChanged

        Configuracion.TilePequeñaImagenMargen(sliderConfigTilePequeñaImagenMargen.Value)

    End Sub

    Private Sub SliderConfigTileMedianaImagenMargen_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileMedianaImagenMargen.ValueChanged

        Configuracion.TileMedianaImagenMargen(sliderConfigTileMedianaImagenMargen.Value)

    End Sub

    Private Sub SliderConfigTileAnchaImagenMargen_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileAnchaImagenMargen.ValueChanged

        Configuracion.TileAnchaImagenMargen(sliderConfigTileAnchaImagenMargen.Value)

    End Sub

    Private Sub SliderConfigTileGrandeImagenMargen_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileGrandeImagenMargen.ValueChanged

        Configuracion.TileGrandeImagenMargen(sliderConfigTileGrandeImagenMargen.Value)

    End Sub

    Private Sub SliderConfigTilePequeñaImagenCoordenadasX_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTilePequeñaImagenCoordenadasX.ValueChanged

        Configuracion.TilePequeñaImagenCoordenadas(sliderConfigTilePequeñaImagenCoordenadasX.Value, sliderConfigTilePequeñaImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTilePequeñaImagenCoordenadasY_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTilePequeñaImagenCoordenadasY.ValueChanged

        Configuracion.TilePequeñaImagenCoordenadas(sliderConfigTilePequeñaImagenCoordenadasX.Value, sliderConfigTilePequeñaImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTileMedianaImagenCoordenadasX_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileMedianaImagenCoordenadasX.ValueChanged

        Configuracion.TileMedianaImagenCoordenadas(sliderConfigTileMedianaImagenCoordenadasX.Value, sliderConfigTileMedianaImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTileMedianaImagenCoordenadasY_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileMedianaImagenCoordenadasY.ValueChanged

        Configuracion.TileMedianaImagenCoordenadas(sliderConfigTileMedianaImagenCoordenadasX.Value, sliderConfigTileMedianaImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTileAnchaImagenCoordenadasX_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileAnchaImagenCoordenadasX.ValueChanged

        Configuracion.TileAnchaImagenCoordenadas(sliderConfigTileAnchaImagenCoordenadasX.Value, sliderConfigTileAnchaImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTileAnchaImagenCoordenadasY_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileAnchaImagenCoordenadasY.ValueChanged

        Configuracion.TileAnchaImagenCoordenadas(sliderConfigTileAnchaImagenCoordenadasX.Value, sliderConfigTileAnchaImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTileGrandeImagenCoordenadasX_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileGrandeImagenCoordenadasX.ValueChanged

        Configuracion.TileGrandeImagenCoordenadas(sliderConfigTileGrandeImagenCoordenadasX.Value, sliderConfigTileGrandeImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTileGrandeImagenCoordenadasY_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileGrandeImagenCoordenadasY.ValueChanged

        Configuracion.TileGrandeImagenCoordenadas(sliderConfigTileGrandeImagenCoordenadasX.Value, sliderConfigTileGrandeImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTilePequeñaImagenZoom_SelectionChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTilePequeñaImagenZoom.ValueChanged

        If sliderConfigTilePequeñaImagenZoom.Value = 0 Then
            sliderConfigTilePequeñaImagenZoom.Value = 1
        End If

        Configuracion.TilePequeñaImagenZoom(sliderConfigTilePequeñaImagenZoom.Value, sliderConfigTilePequeñaImagenCoordenadasX.Value, sliderConfigTilePequeñaImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTileMedianaImagenZoom_SelectionChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileMedianaImagenZoom.ValueChanged

        If sliderConfigTileMedianaImagenZoom.Value = 0 Then
            sliderConfigTileMedianaImagenZoom.Value = 1
        End If

        Configuracion.TileMedianaImagenZoom(sliderConfigTileMedianaImagenZoom.Value, sliderConfigTileMedianaImagenCoordenadasX.Value, sliderConfigTileMedianaImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTileAnchaImagenZoom_SelectionChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileAnchaImagenZoom.ValueChanged

        If sliderConfigTileAnchaImagenZoom.Value = 0 Then
            sliderConfigTileAnchaImagenZoom.Value = 1
        End If

        Configuracion.TileAnchaImagenZoom(sliderConfigTileAnchaImagenZoom.Value, sliderConfigTileAnchaImagenCoordenadasX.Value, sliderConfigTileAnchaImagenCoordenadasY.Value)

    End Sub

    Private Sub SliderConfigTileGrandeImagenZoom_SelectionChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderConfigTileGrandeImagenZoom.ValueChanged

        If sliderConfigTileGrandeImagenZoom.Value = 0 Then
            sliderConfigTileGrandeImagenZoom.Value = 1
        End If

        Configuracion.TileGrandeImagenZoom(sliderConfigTileGrandeImagenZoom.Value, sliderConfigTileGrandeImagenCoordenadasX.Value, sliderConfigTileGrandeImagenCoordenadasY.Value)

    End Sub

    'CONFIG-----------------------------------------------------------------------------

    Private Sub BotonAñadirCarpetaSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonAñadirCarpetaSteam.Click

        Steam.Generar(True)

    End Sub

    Private Sub BotonBorrarCarpetasSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonBorrarCarpetasSteam.Click

        Steam.Borrar()

    End Sub

End Class
