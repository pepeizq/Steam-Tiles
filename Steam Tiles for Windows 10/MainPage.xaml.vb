Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core
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

        checkboxTilesSteamTitulo.Content = recursos.GetString("Titulo Tile")

        tbTwitterConfig.Text = recursos.GetString("Twitter")

        '--------------------------------------------------------

        Await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Sub()
                                                                     Listado.Generar(False)
                                                                 End Sub)

        If ApplicationData.Current.LocalSettings.Values("titulotilesteam") = "on" Then
            checkboxTilesSteamTitulo.IsChecked = True
            imageTileTitulo.Source = New BitmapImage(New Uri(Me.BaseUri, "/Assets/Otros/titulo1.png"))
        Else
            imageTileTitulo.Source = New BitmapImage(New Uri(Me.BaseUri, "/Assets/Otros/titulo0.png"))
        End If

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

    'CBTITULOS-----------------------------------------------------------------------------

    Private Sub checkboxTilesSteamTitulo_Checked(sender As Object, e As RoutedEventArgs) Handles checkboxTilesSteamTitulo.Checked

        ApplicationData.Current.LocalSettings.Values("titulotilesteam") = "on"
        imageTileTitulo.Source = New BitmapImage(New Uri(Me.BaseUri, "/Assets/Otros/titulo1.png"))

    End Sub

    Private Sub checkboxTilesSteamTitulo_Unchecked(sender As Object, e As RoutedEventArgs) Handles checkboxTilesSteamTitulo.Unchecked

        ApplicationData.Current.LocalSettings.Values("titulotilesteam") = "off"
        imageTileTitulo.Source = New BitmapImage(New Uri(Me.BaseUri, "/Assets/Otros/titulo0.png"))

    End Sub

    '-----------------------------------------------------------------------------

    Private Async Sub gridviewTiles_ItemClick(sender As Object, e As ItemClickEventArgs) Handles gridViewTiles.ItemClick

        Dim tile As Tile = e.ClickedItem

        Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("headersteam.png", CreationCollisionOption.GenerateUniqueName)
        Dim downloader As BackgroundDownloader = New BackgroundDownloader()
        Dim descarga As DownloadOperation = downloader.CreateDownload(tile.ImagenUri, ficheroImagen)
        Await descarga.StartAsync

        Dim nuevaTile As SecondaryTile = New SecondaryTile(tile.ID, tile.Titulo, "steam://rungameid/" + tile.ID, New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute), TileSize.Wide310x150)

        Dim frame As FrameworkElement = TryCast(sender, FrameworkElement)
        Dim button As GeneralTransform = frame.TransformToVisual(Nothing)
        Dim point As Point = button.TransformPoint(New Point())
        Dim rect As Rect = New Rect(point, New Size(frame.ActualWidth, frame.ActualHeight))

        nuevaTile.RoamingEnabled = False
        nuevaTile.VisualElements.Wide310x150Logo = New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute)

        If ApplicationData.Current.LocalSettings.Values("titulotilesteam") = "on" Then
            nuevaTile.VisualElements.ShowNameOnWide310x150Logo = True
        End If

        Try
            Await nuevaTile.RequestCreateForSelectionAsync(rect)
        Catch ex As Exception

        End Try

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

    '-----------------------------------------------------------------------------

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
