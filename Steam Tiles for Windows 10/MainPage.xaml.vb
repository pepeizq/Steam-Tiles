Imports Microsoft.Toolkit.Uwp.Notifications
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core
Imports Windows.UI.Notifications
Imports Windows.UI.StartScreen

Public NotInheritable Class MainPage
    Inherits Page

    Private Async Sub Page_Loaded(sender As FrameworkElement, args As Object)

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar

        barra.BackgroundColor = Colors.DimGray
        barra.ForegroundColor = Colors.White
        barra.InactiveForegroundColor = Colors.White
        barra.ButtonBackgroundColor = Colors.DimGray
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonInactiveForegroundColor = Colors.White

        '--------------------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        botonPrincipal.Label = recursos.GetString("Tiles")
        botonConfig.Label = recursos.GetString("Boton Config")
        botonVotar.Label = recursos.GetString("Boton Votar")
        botonCompartir.Label = recursos.GetString("Boton Compartir")
        botonContacto.Label = recursos.GetString("Boton Contactar")
        botonMasApps.Label = recursos.GetString("Boton Web")

        commadBarTop.DefaultLabelPosition = CommandBarDefaultLabelPosition.Right

        tbConfig.Text = recursos.GetString("Boton Config")
        buttonConfigApp.Content = recursos.GetString("App")
        tbDirectoriosSteam.Text = recursos.GetString("Directorio")
        buttonAñadirCarpetaSteamTexto.Text = recursos.GetString("Boton Añadir")
        tbCarpetasAñadidasSteam.Text = recursos.GetString("Carpetas Añadidas")
        tbCarpetasAvisoSteam.Text = recursos.GetString("Carpetas Aviso")
        buttonBorrarCarpetasTexto.Text = recursos.GetString("Boton Borrar")

        cbTilesTitulo.Content = recursos.GetString("Tile Titulo")
        cbTilesBranding.Content = recursos.GetString("Tile Logo")
        sliderTilesOverlay.Header = recursos.GetString("Tile Overlay")
        cbTilesCirculo.Content = recursos.GetString("Tile Circulo")

        tbTwitterConfig.Text = recursos.GetString("Twitter")

        '--------------------------------------------------------

        Await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Sub()
                                                                     Listado.Generar(False)
                                                                 End Sub)

        Config.Generar()
        Twitter.Generar()

    End Sub

    Private Sub GridVisibilidad(grid As Grid)

        gridTiles.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed
        gridWebContacto.Visibility = Visibility.Collapsed
        gridWeb.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Sub botonPrincipal_Click(sender As Object, e As RoutedEventArgs) Handles botonPrincipal.Click

        GridVisibilidad(gridTiles)

    End Sub

    Private Sub botonConfig_Click(sender As Object, e As RoutedEventArgs) Handles botonConfig.Click

        GridVisibilidad(gridConfig)
        GridConfigVisibilidad(gridConfigApp, buttonConfigApp)

    End Sub

    Private Async Sub botonVotar_Click(sender As Object, e As RoutedEventArgs) Handles botonVotar.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    Private Sub botonCompartir_Click(sender As Object, e As RoutedEventArgs) Handles botonCompartir.Click

        Dim datos As DataTransferManager = DataTransferManager.GetForCurrentView()
        AddHandler datos.DataRequested, AddressOf MainPage_DataRequested
        DataTransferManager.ShowShareUI()

    End Sub

    Private Sub MainPage_DataRequested(sender As DataTransferManager, e As DataRequestedEventArgs)

        Dim request As DataRequest = e.Request
        request.Data.SetText("Download: https://www.microsoft.com/store/apps/9nblggh51sb3")
        request.Data.Properties.Title = "Steam Tiles"
        request.Data.Properties.Description = "Add Tiles for your Steam games in the Start Menu of Windows 10"

    End Sub

    Private Sub botonContacto_Click(sender As Object, e As RoutedEventArgs) Handles botonContacto.Click

        GridVisibilidad(gridWebContacto)

    End Sub

    Private Sub botonMasApps_Click(sender As Object, e As RoutedEventArgs) Handles botonMasApps.Click

        GridVisibilidad(gridWeb)

    End Sub

    'AÑADIRCARPETA-----------------------------------------------------------------------------

    Private Async Sub buttonAñadirCarpetaSteam_Click(sender As Object, e As RoutedEventArgs) Handles buttonAñadirCarpetaSteam.Click

        Await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Sub()
                                                                     Listado.Generar(True)
                                                                 End Sub)

    End Sub

    'CBTILES-----------------------------------------------------------------------------

    Private Sub cbTilesTitulo_Checked(sender As Object, e As RoutedEventArgs) Handles cbTilesTitulo.Checked

        ApplicationData.Current.LocalSettings.Values("titulotile") = "on"
        Config.Generar()

    End Sub

    Private Sub cbTilesTitulo_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbTilesTitulo.Unchecked

        ApplicationData.Current.LocalSettings.Values("titulotile") = "off"
        Config.Generar()

    End Sub

    Private Sub cbTilesBranding_Checked(sender As Object, e As RoutedEventArgs) Handles cbTilesBranding.Checked

        ApplicationData.Current.LocalSettings.Values("logotile") = "on"
        Config.Generar()

    End Sub

    Private Sub cbTilesBranding_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbTilesBranding.Unchecked

        ApplicationData.Current.LocalSettings.Values("logotile") = "off"
        Config.Generar()

    End Sub

    Private Sub sliderTilesOverlay_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderTilesOverlay.ValueChanged

        ApplicationData.Current.LocalSettings.Values("overlaytile") = sliderTilesOverlay.Value

    End Sub

    Private Sub cbTilesCirculo_Checked(sender As Object, e As RoutedEventArgs) Handles cbTilesCirculo.Checked

        ApplicationData.Current.LocalSettings.Values("circulotile") = "on"
        Config.Generar()

    End Sub

    Private Sub cbTilesCirculo_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbTilesCirculo.Unchecked

        ApplicationData.Current.LocalSettings.Values("circulotile") = "off"
        Config.Generar()

    End Sub

    '-----------------------------------------------------------------------------

    Private Async Sub gridviewTiles_ItemClick(sender As Object, e As ItemClickEventArgs) Handles gridViewTiles.ItemClick

        Dim tile As Tile = e.ClickedItem

        Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("headersteam.png", CreationCollisionOption.GenerateUniqueName)
        Dim downloader As BackgroundDownloader = New BackgroundDownloader()
        Dim descarga As DownloadOperation = downloader.CreateDownload(tile.ImagenUri, ficheroImagen)
        Await descarga.StartAsync

        Dim nuevaTile As SecondaryTile = New SecondaryTile(tile.ID, tile.Titulo, "steam://rungameid/" + tile.ID, New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute), TileSize.Wide310x150)

        nuevaTile.VisualElements.Wide310x150Logo = New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute)
        nuevaTile.VisualElements.Square310x310Logo = New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute)

        Await nuevaTile.RequestCreateAsync()

        Dim imagen As AdaptiveImage = New AdaptiveImage
        imagen.Source = "ms-appdata:///local/" + ficheroImagen.Name
        imagen.HintRemoveMargin = True
        imagen.HintAlign = AdaptiveImageAlign.Stretch

        If ApplicationData.Current.LocalSettings.Values("circulotile") = "on" Then
            imagen.HintCrop = AdaptiveImageCrop.Circle
        Else
            imagen.HintCrop = AdaptiveImageCrop.Default
        End If

        Dim fondoImagen As TileBackgroundImage = New TileBackgroundImage
        fondoImagen.Source = "ms-appdata:///local/" + ficheroImagen.Name
        fondoImagen.HintOverlay = Integer.Parse(ApplicationData.Current.LocalSettings.Values("overlaytile"))

        If ApplicationData.Current.LocalSettings.Values("circulotile") = "on" Then
            fondoImagen.HintCrop = AdaptiveImageCrop.Circle
        Else
            fondoImagen.HintCrop = AdaptiveImageCrop.Default
        End If

        '-----------------------

        Dim contenidoWile As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoWile.BackgroundImage = fondoImagen

        Dim tileWide As TileBinding = New TileBinding
        tileWide.Content = contenidoWile

        '-----------------------

        Dim contenidoSmall As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoSmall.Children.Add(imagen)

        Dim tileSmall As TileBinding = New TileBinding
        tileSmall.Content = contenidoSmall

        '-----------------------

        Dim contenidoMedium As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoMedium.Children.Add(imagen)

        Dim tileMedium As TileBinding = New TileBinding
        tileMedium.Content = contenidoMedium

        '-----------------------

        Dim contenidoLarge As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoLarge.Children.Add(imagen)

        Dim tileLarge As TileBinding = New TileBinding
        tileLarge.Content = contenidoLarge

        '-----------------------

        If ApplicationData.Current.LocalSettings.Values("titulotile") = "on" Then
            If ApplicationData.Current.LocalSettings.Values("logotile") = "on" Then
                tileWide.Branding = TileBranding.NameAndLogo
                tileSmall.Branding = TileBranding.NameAndLogo
                tileMedium.Branding = TileBranding.NameAndLogo
                tileLarge.Branding = TileBranding.NameAndLogo
            Else
                tileWide.Branding = TileBranding.Name
                tileSmall.Branding = TileBranding.Name
                tileMedium.Branding = TileBranding.Name
                tileLarge.Branding = TileBranding.Name
            End If
        Else
            If ApplicationData.Current.LocalSettings.Values("logotile") = "on" Then
                tileWide.Branding = TileBranding.Logo
                tileSmall.Branding = TileBranding.Logo
                tileMedium.Branding = TileBranding.Logo
                tileLarge.Branding = TileBranding.Logo
            End If
        End If

        Dim visual As TileVisual = New TileVisual
        visual.TileWide = tileWide
        visual.TileSmall = tileSmall
        visual.TileMedium = tileMedium
        visual.TileLarge = tileLarge

        Dim contenido As TileContent = New TileContent
        contenido.Visual = visual

        Dim notificacion As TileNotification = New TileNotification(contenido.GetXml)

        TileUpdateManager.CreateTileUpdaterForSecondaryTile(tile.ID).Update(notificacion)

    End Sub

    '-----------------------------------------------------------------------------

    Private Sub GridConfigVisibilidad(grid As Grid, button As Button)

        buttonConfigApp.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonConfigApp.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonConfigTiles.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonConfigTiles.BorderBrush = New SolidColorBrush(Colors.Transparent)

        button.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#bfbfbf"))
        button.BorderBrush = New SolidColorBrush(Colors.Black)

        gridConfigApp.Visibility = Visibility.Collapsed
        gridConfigTiles.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub


    Private Sub buttonConfigApp_Click(sender As Object, e As RoutedEventArgs) Handles buttonConfigApp.Click

        GridConfigVisibilidad(gridConfigApp, buttonConfigApp)

    End Sub

    Private Sub buttonConfigTiles_Click(sender As Object, e As RoutedEventArgs) Handles buttonConfigTiles.Click

        GridConfigVisibilidad(gridConfigTiles, buttonConfigTiles)

    End Sub

    Private Async Sub buttonBorrarCarpetas_Click(sender As Object, e As RoutedEventArgs) Handles buttonBorrarCarpetas.Click

        Await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Sub()
                                                                     Listado.Borrar()
                                                                 End Sub)

    End Sub

    'TWITTER-----------------------------------------------------------------------------

    Private Async Sub buttonTwitter_Click(sender As Object, e As RoutedEventArgs) Handles buttonTwitter.Click

        Dim boton As Button = e.OriginalSource
        Dim enlace As Uri = boton.Tag

        Await Launcher.LaunchUriAsync(enlace)

    End Sub

    Private Sub buttonTwitterCancelar_Click(sender As Object, e As RoutedEventArgs) Handles buttonTwitterCancelar.Click

        gridTwitter.Visibility = Visibility.Collapsed
        ApplicationData.Current.LocalSettings.Values("twitter") = "off"
        cbTwitter.IsChecked = False

    End Sub

    Private Sub cbTwitter_Checked(sender As Object, e As RoutedEventArgs) Handles cbTwitter.Checked

        gridTwitter.Visibility = Visibility.Visible
        ApplicationData.Current.LocalSettings.Values("twitter") = "on"

    End Sub

    Private Sub cbTwitter_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbTwitter.Unchecked

        gridTwitter.Visibility = Visibility.Collapsed
        ApplicationData.Current.LocalSettings.Values("twitter") = "off"

    End Sub


End Class
