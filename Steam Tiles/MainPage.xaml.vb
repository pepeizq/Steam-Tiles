Imports Microsoft.Services.Store.Engagement
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
            NavegarMasCosas(lvMasCosasMasApps, "https://pepeizqapps.com/")
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

        GridVisibilidad(gridTiles, recursos.GetString("Tiles"))
        nvPrincipal.IsPaneOpen = False

        Steam.Generar(False)
        Config.Generar()

        '--------------------------------------------------------

        Dim transpariencia As New UISettings
        AddHandler transpariencia.AdvancedEffectsEnabledChanged, AddressOf TransparienciaEfectosCambia

    End Sub

    Private Sub TransparienciaEfectosCambia(sender As UISettings, e As Object)

        If sender.AdvancedEffectsEnabled = True Then
            gridConfig.Background = New SolidColorBrush(App.Current.Resources("GridAcrilico"))
            gridConfigTiles.Background = New SolidColorBrush(App.Current.Resources("GridTituloBackground"))
            gridMasCosas.Background = New SolidColorBrush(App.Current.Resources("GridAcrilico"))
        Else
            gridConfig.Background = New SolidColorBrush(Colors.LightGray)
            gridConfigTiles.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridMasCosas.Background = New SolidColorBrush(Colors.LightGray)
        End If

    End Sub

    Private Sub GridVisibilidad(grid As Grid, tag As String)

        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - " + tag

        gridConfig.Visibility = Visibility.Collapsed
        gridMasCosas.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    'TILES-----------------------------------------------------------------------------

    Private Sub BotonAñadirTile_Click(sender As Object, e As RoutedEventArgs) Handles botonAñadirTile.Click

        Dim tile As Tile = botonAñadirTile.Tag
        Tiles.Generar(tile)

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

    'CONFIG-----------------------------------------------------------------------------

    Private Sub BotonAñadirCarpetaSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonAñadirCarpetaSteam.Click

        Steam.Generar(True)

    End Sub

    Private Sub BotonBorrarCarpetasSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonBorrarCarpetasSteam.Click

        Steam.Borrar()

    End Sub

    'MASCOSAS-----------------------------------------

    Private Async Sub LvMasCosasItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

        ElseIf sp.Tag.ToString = 1 Then

            NavegarMasCosas(lvMasCosasMasApps, "https://pepeizqapps.com/")

        ElseIf sp.Tag.ToString = 3 Then

            NavegarMasCosas(lvMasCosasContacto, "https://pepeizqapps.com/contact/")

        ElseIf sp.Tag.ToString = 4 Then

            If StoreServicesFeedbackLauncher.IsSupported = True Then
                Dim ejecutador As StoreServicesFeedbackLauncher = StoreServicesFeedbackLauncher.GetDefault()
                Await ejecutador.LaunchAsync()
            Else
                NavegarMasCosas(lvMasCosasReportarFallo, "https://pepeizqapps.com/contact/")
            End If

        ElseIf sp.Tag.ToString = 5 Then

            NavegarMasCosas(lvMasCosasTraduccion, "https://poeditor.com/join/project/aKmScyB4QT")

        ElseIf sp.Tag.ToString = 6 Then

            NavegarMasCosas(lvMasCosasCodigoFuente, "https://github.com/pepeizq/Steam-Tiles")

        End If

    End Sub

    Private Sub NavegarMasCosas(lvItem As ListViewItem, url As String)

        lvMasCosasMasApps.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        lvMasCosasContacto.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        lvMasCosasReportarFallo.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        lvMasCosasTraduccion.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        lvMasCosasCodigoFuente.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))

        lvItem.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))

        pbMasCosas.Visibility = Visibility.Visible

        wvMasCosas.Navigate(New Uri(url))

    End Sub

    Private Sub WvMasCosas_NavigationCompleted(sender As WebView, args As WebViewNavigationCompletedEventArgs) Handles wvMasCosas.NavigationCompleted

        pbMasCosas.Visibility = Visibility.Collapsed

    End Sub

End Class
