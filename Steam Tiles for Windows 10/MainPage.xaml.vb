Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.StartScreen

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Page_Loading(sender As FrameworkElement, args As Object)

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        tbDirectorioSteam.Text = recursos.GetString("Directorio")
        buttonBuscarRutaTilesSteam.Content = recursos.GetString("Boton Busqueda")
        checkboxTilesSteamTitulo.Content = recursos.GetString("Titulo Tile")
        buttonVotaciones.Content = recursos.GetString("Boton Votar")
        buttonContactar.Content = recursos.GetString("Boton Contactar")
        buttonWeb.Content = recursos.GetString("Boton Web")

        Listado.Generar(gridViewTilesSteam, buttonBuscarRutaTilesSteam, progressRingCarga, scrollViewerGridSteam, False)

    End Sub

    '-----------------------------------------------------------------------------

    Private Sub buttonBuscarRutaTilesSteam_Click(sender As Object, e As RoutedEventArgs) Handles buttonBuscarRutaTilesSteam.Click

        Listado.Generar(gridViewTilesSteam, buttonBuscarRutaTilesSteam, progressRingCarga, scrollViewerGridSteam, True)

    End Sub

    Private Sub buttonBuscarRutaTilesSteam_PointerEntered(sender As Object, e As PointerRoutedEventArgs) Handles buttonBuscarRutaTilesSteam.PointerEntered

        buttonBuscarRutaTilesSteam.BorderBrush = New SolidColorBrush(Colors.Black)
        buttonBuscarRutaTilesSteam.Background = New SolidColorBrush(Colors.DarkGray)

    End Sub

    Private Sub buttonBuscarRutaTilesSteam_PointerExited(sender As Object, e As PointerRoutedEventArgs) Handles buttonBuscarRutaTilesSteam.PointerExited

        buttonBuscarRutaTilesSteam.BorderBrush = New SolidColorBrush(Colors.DarkGray)
        buttonBuscarRutaTilesSteam.Background = New SolidColorBrush(Colors.Transparent)

    End Sub

    '-----------------------------------------------------------------------------

    Private Sub buttonVotaciones_PointerEntered(sender As Object, e As PointerRoutedEventArgs) Handles buttonVotaciones.PointerEntered

        buttonVotaciones.BorderBrush = New SolidColorBrush(Colors.Black)
        buttonVotaciones.Background = New SolidColorBrush(Colors.DarkGray)

    End Sub

    Private Sub buttonVotaciones_PointerExited(sender As Object, e As PointerRoutedEventArgs) Handles buttonVotaciones.PointerExited

        buttonVotaciones.BorderBrush = New SolidColorBrush(Colors.DarkGray)
        buttonVotaciones.Background = New SolidColorBrush(Colors.Transparent)

    End Sub

    Private Async Sub buttonVotaciones_Click(sender As Object, e As RoutedEventArgs) Handles buttonVotaciones.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    '-----------------------------------------------------------------------------

    Private Sub buttonContactar_PointerEntered(sender As Object, e As PointerRoutedEventArgs) Handles buttonContactar.PointerEntered

        buttonContactar.BorderBrush = New SolidColorBrush(Colors.Black)
        buttonContactar.Background = New SolidColorBrush(Colors.DarkGray)

    End Sub

    Private Sub buttonContactar_PointerExited(sender As Object, e As PointerRoutedEventArgs) Handles buttonContactar.PointerExited

        buttonContactar.BorderBrush = New SolidColorBrush(Colors.DarkGray)
        buttonContactar.Background = New SolidColorBrush(Colors.Transparent)

    End Sub

    Private Async Sub buttonContactar_Click(sender As Object, e As RoutedEventArgs) Handles buttonContactar.Click

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/contact/"))

    End Sub

    '-----------------------------------------------------------------------------

    Private Sub buttonWeb_PointerEntered(sender As Object, e As PointerRoutedEventArgs) Handles buttonWeb.PointerEntered

        buttonWeb.BorderBrush = New SolidColorBrush(Colors.Black)
        buttonWeb.Background = New SolidColorBrush(Colors.DarkGray)

    End Sub

    Private Sub buttonWeb_PointerExited(sender As Object, e As PointerRoutedEventArgs) Handles buttonWeb.PointerExited

        buttonWeb.BorderBrush = New SolidColorBrush(Colors.DarkGray)
        buttonWeb.Background = New SolidColorBrush(Colors.Transparent)

    End Sub

    Private Async Sub buttonWeb_Click(sender As Object, e As RoutedEventArgs) Handles buttonWeb.Click

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/"))

    End Sub

    '-----------------------------------------------------------------------------

    Private Sub checkboxTilesSteamTitulo_Checked(sender As Object, e As RoutedEventArgs) Handles checkboxTilesSteamTitulo.Checked

        ApplicationData.Current.LocalSettings.Values("titulotilesteam") = "on"

    End Sub

    Private Sub checkboxTilesSteamTitulo_Unchecked(sender As Object, e As RoutedEventArgs) Handles checkboxTilesSteamTitulo.Unchecked

        ApplicationData.Current.LocalSettings.Values("titulotilesteam") = "off"

    End Sub

    '-----------------------------------------------------------------------------

    Private Async Sub gridviewTiles_ItemClick(sender As Object, e As ItemClickEventArgs) Handles gridViewTilesSteam.ItemClick

        Dim tile As Tiles = e.ClickedItem

        Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("headersteam.png", CreationCollisionOption.GenerateUniqueName)
        Dim downloader As BackgroundDownloader = New BackgroundDownloader()
        Dim descarga As DownloadOperation = downloader.CreateDownload(tile.imagenUri, ficheroImagen)
        Await descarga.StartAsync

        Dim nuevaTile As SecondaryTile = New SecondaryTile(tile.id, tile.titulo, "steam://rungameid/" + tile.id, New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute), TileSize.Wide310x150)

        Dim frame As FrameworkElement = TryCast(sender, FrameworkElement)
        Dim button As GeneralTransform = frame.TransformToVisual(Nothing)
        Dim point As Point = button.TransformPoint(New Point())
        Dim rect As Rect = New Rect(point, New Size(frame.ActualWidth, frame.ActualHeight))

        nuevaTile.RoamingEnabled = False
        nuevaTile.VisualElements.Wide310x150Logo = New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute)

        If ApplicationData.Current.LocalSettings.Values("titulotilesteam") = "on" Then
            nuevaTile.VisualElements.ShowNameOnWide310x150Logo = True
        End If

        Await nuevaTile.RequestCreateForSelectionAsync(rect)

    End Sub

End Class
