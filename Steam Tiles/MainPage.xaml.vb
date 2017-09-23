Imports Microsoft.Services.Store.Engagement
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_Loaded(sender As Object, e As RoutedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("Tiles"), New SymbolIcon(Symbol.Home), 0))
        nvPrincipal.MenuItems.Add(New NavigationViewItemSeparator)
        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("Config"), New SymbolIcon(Symbol.Setting), 1))
        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("MoreThings"), New SymbolIcon(Symbol.More), 2))

    End Sub

    Private Sub Nv_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Dim item As TextBlock = args.InvokedItem

        If item.Text = recursos.GetString("Tiles") Then
            GridVisibilidad(gridTiles, item.Text)
        ElseIf item.Text = recursos.GetString("Config") Then
            GridVisibilidad(gridConfig, item.Text)
        ElseIf item.Text = recursos.GetString("MoreThings") Then
            GridVisibilidad(gridMasCosas, item.Text)
        End If

    End Sub

    Private Sub Page_Loaded(sender As FrameworkElement, args As Object)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        Dim coreBarra As CoreApplicationViewTitleBar = CoreApplication.GetCurrentView.TitleBar
        coreBarra.ExtendViewIntoTitleBar = True

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar
        barra.ButtonBackgroundColor = Colors.Transparent
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonInactiveBackgroundColor = Colors.Transparent

        '--------------------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        'botonTilesTexto.Text = recursos.GetString("Tiles")
        'botonConfigTexto.Text = recursos.GetString("Boton Config")
        'botonVotarTexto.Text = recursos.GetString("Boton Votar")
        'botonMasCosasTexto.Text = recursos.GetString("Boton Cosas")

        'botonMasAppsTexto.Text = recursos.GetString("Boton Web")
        'botonContactoTexto.Text = recursos.GetString("Boton Contacto")
        'botonReportarTexto.Text = recursos.GetString("Boton Reportar")
        'botonCodigoFuenteTexto.Text = recursos.GetString("Boton Codigo Fuente")

        'tbNoJuegosSteam.Text = recursos.GetString("No Config")
        'tbAvisoSeleccionar.Text = recursos.GetString("Seleccionar")

        'botonAñadirTileTexto.Text = recursos.GetString("Añadir Tile")

        'cbTilesTitulo.Content = recursos.GetString("Tile Titulo")
        'cbTilesIconos.Content = recursos.GetString("Tile Logo")

        'tbDirectoriosSteam.Text = recursos.GetString("Steam Carpetas Añadir")
        'buttonAñadirCarpetaSteamTexto.Text = recursos.GetString("Boton Añadir")
        'tbCarpetasAñadidasSteam.Text = recursos.GetString("Carpetas Añadidas")
        'tbCarpetasAvisoSteam.Text = recursos.GetString("Steam Carpetas Aviso")
        'buttonBorrarCarpetasTextoSteam.Text = recursos.GetString("Boton Borrar")

        '--------------------------------------------------------

        GridVisibilidad(gridTiles, recursos.GetString("Tiles"))
        Steam.Generar(False)
        Config.Generar()

    End Sub

    Private Sub GridVisibilidad(grid As Grid, tag As String)

        tbTitulo.Text = "Steam Tiles (" + SystemInformation.ApplicationVersion.Major.ToString + "." + SystemInformation.ApplicationVersion.Minor.ToString + "." + SystemInformation.ApplicationVersion.Build.ToString + "." + SystemInformation.ApplicationVersion.Revision.ToString + ") - " + tag

        gridTiles.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed
        gridMasCosas.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    'TILES-----------------------------------------------------------------------------

    Private Sub BotonAñadirTile_Click(sender As Object, e As RoutedEventArgs) Handles botonAñadirTile.Click

        Dim tile As Tile = botonAñadirTile.Tag
        Tiles.Generar(tile)

    End Sub

    'CONFIG-----------------------------------------------------------------------------

    Private Sub GridConfigVisibilidad(grid As Grid, boton As Button)

        buttonConfigSteam.Background = New SolidColorBrush(Colors.SlateGray)

        boton.Background = New SolidColorBrush(Colors.DimGray)

        gridConfigSteam.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Sub ButtonConfigSteam_Click(sender As Object, e As RoutedEventArgs) Handles buttonConfigSteam.Click

        GridConfigVisibilidad(gridConfigSteam, buttonConfigSteam)

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

    Private Sub CbTilesIconos_Checked(sender As Object, e As RoutedEventArgs) Handles cbTilesIconos.Checked

        ApplicationData.Current.LocalSettings.Values("logotile") = "on"
        Config.Generar()

    End Sub

    Private Sub CbTilesIconos_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbTilesIconos.Unchecked

        ApplicationData.Current.LocalSettings.Values("logotile") = "off"
        Config.Generar()

    End Sub

    'CONFIGSTEAM-----------------------------------------------------------------------------

    Private Sub ButtonAñadirCarpetaSteam_Click(sender As Object, e As RoutedEventArgs) Handles buttonAñadirCarpetaSteam.Click

        Steam.Generar(True)

    End Sub

    Private Sub ButtonBorrarCarpetasSteam_Click(sender As Object, e As RoutedEventArgs) Handles buttonBorrarCarpetasSteam.Click

        Steam.Borrar()

    End Sub

    'MASCOSAS-----------------------------------------

    Private Async Sub LvMasCosasItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

        ElseIf sp.Tag.ToString = 1 Then

            wvMasCosas.Navigate(New Uri("https://pepeizqapps.com/"))

        ElseIf sp.Tag.ToString = 2 Then

            wvMasCosas.Navigate(New Uri("https://pepeizqapps.com/contact/"))

        ElseIf sp.Tag.ToString = 3 Then

            If StoreServicesFeedbackLauncher.IsSupported = True Then
                Dim ejecutador As StoreServicesFeedbackLauncher = StoreServicesFeedbackLauncher.GetDefault()
                Await ejecutador.LaunchAsync()
            Else
                wvMasCosas.Navigate(New Uri("https://pepeizqapps.com/contact/"))
            End If

        ElseIf sp.Tag.ToString = 4 Then

            wvMasCosas.Navigate(New Uri("https://poeditor.com/join/project/YaZAR0uIW4"))

        ElseIf sp.Tag.ToString = 5 Then

            wvMasCosas.Navigate(New Uri("https://github.com/pepeizq/Steam-Tiles"))

        ElseIf sp.Tag.ToString = 6 Then

            wvMasCosas.Navigate(New Uri("https://pepeizqapps.com/thanks/"))

        End If

    End Sub

End Class
