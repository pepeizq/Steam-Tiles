Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_Loaded(sender As Object, e As RoutedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("Games"), FontAwesome5.EFontAwesomeIcon.Solid_Home))
        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("App"), FontAwesome5.EFontAwesomeIcon.Brands_Steam))
        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("Config"), FontAwesome5.EFontAwesomeIcon.Solid_Cog))
        nvPrincipal.MenuItems.Add(New NavigationViewItemSeparator)

    End Sub

    Private Sub Nv_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        Dim item As TextBlock = args.InvokedItem

        If gridProgreso.Visibility = Visibility.Collapsed Then
            If Not item Is Nothing Then
                If item.Text = recursos.GetString("Games") Then
                    If gvTiles.Items.Count = 0 Then
                        Interfaz.Pestañas.Visibilidad(gridAvisoNoJuegos, Nothing, Nothing)
                    Else
                        Interfaz.Pestañas.Visibilidad(gridJuegos, item.Text, item)
                    End If
                ElseIf item.Text = recursos.GetString("App") Then
                    Interfaz.Pestañas.Visibilidad(gridApp, item.Text, item)
                ElseIf item.Text = recursos.GetString("Config") Then
                    Interfaz.Pestañas.Visibilidad(gridConfig, item.Text, item)
                End If
            End If
        End If

    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        Cache.Cargar()
        Interfaz.Juegos.Cargar()
        Interfaz.App2.Cargar()
        Interfaz.AñadirTile.Cargar()
        Interfaz.Busqueda.Cargar()
        Configuracion.Cargar()
        Steam.Generar(False)
        MasTiles.Cargar()
        MasCosas.Cargar()

    End Sub

End Class
