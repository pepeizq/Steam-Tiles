Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Page_Loaded(sender As FrameworkElement, args As Object)

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar

        barra.BackgroundColor = Colors.DimGray
        barra.ForegroundColor = Colors.White
        barra.InactiveForegroundColor = Colors.White
        barra.ButtonBackgroundColor = Colors.DimGray
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonInactiveForegroundColor = Colors.White

        '--------------------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        botonInicioTexto.Text = recursos.GetString("Boton Inicio")
        botonConfigTexto.Text = recursos.GetString("Boton Config")

        commadBarTop.DefaultLabelPosition = CommandBarDefaultLabelPosition.Right

        botonInicioVotarTexto.Text = recursos.GetString("Boton Votar")
        botonInicioCompartirTexto.Text = recursos.GetString("Boton Compartir")
        botonInicioContactoTexto.Text = recursos.GetString("Boton Contactar")
        botonInicioMasAppsTexto.Text = recursos.GetString("Boton Web")

        tbRSS.Text = recursos.GetString("RSS")

        tbNoJuegosSteam.Text = recursos.GetString("No Config")
        tbNoJuegosOrigin.Text = recursos.GetString("No Config")
        tbSiJuegosOrigin.Text = recursos.GetString("Elegir Juego")

        cbTilesTitulo.Content = recursos.GetString("Tile Titulo")
        cbTilesBranding.Content = recursos.GetString("Tile Logo")
        sliderTilesOverlay.Header = recursos.GetString("Tile Overlay")
        cbTilesCirculo.Content = recursos.GetString("Tile Circulo")

        tbDirectoriosSteam.Text = recursos.GetString("Steam Carpetas Añadir")
        buttonAñadirCarpetaSteamTexto.Text = recursos.GetString("Boton Añadir")
        tbCarpetasAñadidasSteam.Text = recursos.GetString("Carpetas Añadidas")
        tbCarpetasAvisoSteam.Text = recursos.GetString("Steam Carpetas Aviso")
        buttonBorrarCarpetasTextoSteam.Text = recursos.GetString("Boton Borrar")

        tbOriginConfigInstrucciones.Text = recursos.GetString("Origin Carpeta Añadir")
        buttonAñadirCarpetaOriginTexto.Text = recursos.GetString("Boton Añadir")
        tbOriginConfigCarpeta.Text = recursos.GetString("Origin Carpeta No Config")

        '--------------------------------------------------------

        tbConsejoConfig.Text = recursos.GetString("Consejo Config")
        tbInicioGrid.Text = recursos.GetString("Grid Arranque")

        cbItemArranqueInicio.Content = recursos.GetString("Boton Inicio")
        cbItemArranqueConfig.Content = recursos.GetString("Boton Config")

        If ApplicationData.Current.LocalSettings.Values("cbarranque") = Nothing Then
            cbArranque.SelectedIndex = 0
            ApplicationData.Current.LocalSettings.Values("cbarranque") = "0"
        Else
            cbArranque.SelectedIndex = ApplicationData.Current.LocalSettings.Values("cbarranque")

            If cbArranque.SelectedIndex = 0 Then
                GridVisibilidad(gridInicio, botonInicio)
            ElseIf cbArranque.SelectedIndex = 1 Then
                GridVisibilidad(gridTilesSteam, botonTilesSteam)
            ElseIf cbArranque.SelectedIndex = 2 Then
                GridVisibilidad(gridTilesOrigin, botonTilesOrigin)
            ElseIf cbArranque.SelectedIndex = 3 Then
                GridVisibilidad(gridConfig, botonConfig)
            Else
                GridVisibilidad(gridInicio, botonInicio)
            End If
        End If

        tbVersionApp.Text = "App " + SystemInformation.ApplicationVersion.Major.ToString + "." + SystemInformation.ApplicationVersion.Minor.ToString + "." + SystemInformation.ApplicationVersion.Build.ToString + "." + SystemInformation.ApplicationVersion.Revision.ToString
        tbVersionWindows.Text = "Windows " + SystemInformation.OperatingSystemVersion.Major.ToString + "." + SystemInformation.OperatingSystemVersion.Minor.ToString + "." + SystemInformation.OperatingSystemVersion.Build.ToString + "." + SystemInformation.OperatingSystemVersion.Revision.ToString

        '--------------------------------------------------------

        RSS.Generar()

        Steam.Generar(False)
        Origin.CargarJuegos(False)

        Config.Generar()

    End Sub

    Private Sub GridVisibilidad(grid As Grid, boton As AppBarButton)

        gridInicio.Visibility = Visibility.Collapsed
        gridTilesSteam.Visibility = Visibility.Collapsed
        gridTilesOrigin.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed
        gridWeb.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

        botonInicio.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonInicio.BorderThickness = New Thickness(0, 0, 0, 0)
        botonTilesSteam.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonTilesSteam.BorderThickness = New Thickness(0, 0, 0, 0)
        botonTilesOrigin.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonTilesOrigin.BorderThickness = New Thickness(0, 0, 0, 0)
        botonConfig.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonConfig.BorderThickness = New Thickness(0, 0, 0, 0)

        If Not boton Is Nothing Then
            boton.BorderBrush = New SolidColorBrush(Colors.White)
            boton.BorderThickness = New Thickness(0, 2, 0, 0)
        End If

    End Sub

    Private Sub BotonInicio_Click(sender As Object, e As RoutedEventArgs) Handles botonInicio.Click

        GridVisibilidad(gridInicio, botonInicio)

    End Sub

    Private Sub BotonTilesSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonTilesSteam.Click

        GridVisibilidad(gridTilesSteam, botonTilesSteam)

    End Sub

    Private Sub BotonTilesOrigin_Click(sender As Object, e As RoutedEventArgs) Handles botonTilesOrigin.Click

        GridVisibilidad(gridTilesOrigin, botonTilesOrigin)

    End Sub

    Private Sub BotonConfig_Click(sender As Object, e As RoutedEventArgs) Handles botonConfig.Click

        GridVisibilidad(gridConfig, botonConfig)
        GridConfigVisibilidad(spConfigTiles, buttonConfigTiles)

    End Sub

    Private Async Sub BotonInicioVotar_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioVotar.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    Private Sub BotonInicioCompartir_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioCompartir.Click

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

    Private Sub BotonInicioContacto_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioContacto.Click

        GridVisibilidad(gridWeb, Nothing)

    End Sub

    Private Sub BotonInicioMasApps_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioMasApps.Click

        If spMasApps.Visibility = Visibility.Visible Then
            spMasApps.Visibility = Visibility.Collapsed
        Else
            spMasApps.Visibility = Visibility.Visible
        End If

    End Sub

    Private Async Sub BotonAppSteamDeals_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamDeals.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9p7836m1tw15"))

    End Sub

    Private Async Sub BotonAppSteamCategories_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamCategories.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9p54scg1n6bm"))

    End Sub

    Private Async Sub BotonAppSteamBridge_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamBridge.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9nblggh441c9"))

    End Sub

    Private Async Sub BotonAppSteamSkins_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamSkins.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9nblggh55b7f"))

    End Sub

    Private Async Sub LvRSS_ItemClick(sender As Object, e As ItemClickEventArgs) Handles lvRSS.ItemClick

        Dim feed As FeedRSS = e.ClickedItem
        Await Launcher.LaunchUriAsync(feed.Enlace)

    End Sub

    Private Sub CbArranque_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbArranque.SelectionChanged

        ApplicationData.Current.LocalSettings.Values("cbarranque") = cbArranque.SelectedIndex

    End Sub

    'TILES-----------------------------------------------------------------------------

    Private Sub GridViewTilesSteam_ItemClick(sender As Object, e As ItemClickEventArgs) Handles gridViewTilesSteam.ItemClick

        Dim tile As Tile = e.ClickedItem
        Tiles.Generar(tile)

    End Sub

    Private Sub LvOriginJuegos_ItemClick(sender As Object, e As ItemClickEventArgs) Handles lvOriginJuegos.ItemClick

        Dim juegoTexto As TextBlock = e.ClickedItem
        Origin.CargarTiles(juegoTexto)

    End Sub

    Private Sub GridViewTilesOrigin_ItemClick(sender As Object, e As ItemClickEventArgs) Handles gridViewTilesOrigin.ItemClick

        Dim tile As Tile = e.ClickedItem
        Tiles.Generar(tile)

    End Sub

    'CONFIG-----------------------------------------------------------------------------

    Private Sub GridConfigVisibilidad(panel As StackPanel, boton As Button)

        buttonConfigTiles.Background = New SolidColorBrush(Colors.SlateGray)
        buttonConfigTiles.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonConfigSteam.Background = New SolidColorBrush(Colors.SlateGray)
        buttonConfigSteam.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonConfigOrigin.Background = New SolidColorBrush(Colors.SlateGray)
        buttonConfigOrigin.BorderBrush = New SolidColorBrush(Colors.Transparent)

        boton.Background = New SolidColorBrush(Colors.DimGray)
        boton.BorderBrush = New SolidColorBrush(Colors.White)

        spConfigTiles.Visibility = Visibility.Collapsed
        spConfigSteam.Visibility = Visibility.Collapsed
        spConfigOrigin.Visibility = Visibility.Collapsed

        panel.Visibility = Visibility.Visible

    End Sub

    Private Sub ButtonConfigTiles_Click(sender As Object, e As RoutedEventArgs) Handles buttonConfigTiles.Click

        GridConfigVisibilidad(spConfigTiles, buttonConfigTiles)

    End Sub

    Private Sub ButtonConfigSteam_Click(sender As Object, e As RoutedEventArgs) Handles buttonConfigSteam.Click

        GridConfigVisibilidad(spConfigSteam, buttonConfigSteam)

    End Sub

    Private Sub ButtonConfigOrigin_Click(sender As Object, e As RoutedEventArgs) Handles buttonConfigOrigin.Click

        GridConfigVisibilidad(spConfigOrigin, buttonConfigOrigin)

    End Sub

    'CONFIGTILES-----------------------------------------------------------------------------

    Private Sub CbTilesTitulo_Checked(sender As Object, e As RoutedEventArgs) Handles cbTilesTitulo.Checked

        ApplicationData.Current.LocalSettings.Values("titulotile") = "on"
        Config.Generar()

    End Sub

    Private Sub CbTilesTitulo_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbTilesTitulo.Unchecked

        ApplicationData.Current.LocalSettings.Values("titulotile") = "off"
        Config.Generar()

    End Sub

    Private Sub CbTilesBranding_Checked(sender As Object, e As RoutedEventArgs) Handles cbTilesBranding.Checked

        ApplicationData.Current.LocalSettings.Values("logotile") = "on"
        Config.Generar()

    End Sub

    Private Sub CbTilesBranding_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbTilesBranding.Unchecked

        ApplicationData.Current.LocalSettings.Values("logotile") = "off"
        Config.Generar()

    End Sub

    Private Sub SliderTilesOverlay_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles sliderTilesOverlay.ValueChanged

        ApplicationData.Current.LocalSettings.Values("overlaytile") = sliderTilesOverlay.Value

    End Sub

    Private Sub CbTilesCirculo_Checked(sender As Object, e As RoutedEventArgs) Handles cbTilesCirculo.Checked

        ApplicationData.Current.LocalSettings.Values("circulotile") = "on"
        Config.Generar()

    End Sub

    Private Sub CbTilesCirculo_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbTilesCirculo.Unchecked

        ApplicationData.Current.LocalSettings.Values("circulotile") = "off"
        Config.Generar()

    End Sub

    'CONFIGSTEAM-----------------------------------------------------------------------------

    Private Sub ButtonAñadirCarpetaSteam_Click(sender As Object, e As RoutedEventArgs) Handles buttonAñadirCarpetaSteam.Click

        Steam.Generar(True)

    End Sub

    Private Sub ButtonBorrarCarpetasSteam_Click(sender As Object, e As RoutedEventArgs) Handles buttonBorrarCarpetasSteam.Click

        Steam.Borrar()

    End Sub

    'CONFIGORIGIN-----------------------------------------------------------------------------

    Private Sub buttonAñadirCarpetaOrigin_Click(sender As Object, e As RoutedEventArgs) Handles buttonAñadirCarpetaOrigin.Click

        Origin.CargarJuegos(True)

    End Sub


End Class
