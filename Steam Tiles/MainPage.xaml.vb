Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_Loaded(sender As Object, e As RoutedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("Games"), FontAwesome5.EFontAwesomeIcon.Solid_Home))
        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("Config"), FontAwesome5.EFontAwesomeIcon.Solid_Cog))
        nvPrincipal.MenuItems.Add(New NavigationViewItemSeparator)
        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("MoreTiles"), FontAwesome5.EFontAwesomeIcon.Solid_ThLarge))
        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("MoreThings"), FontAwesome5.EFontAwesomeIcon.Solid_Cube))

    End Sub

    Private Sub Nv_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        Dim item As TextBlock = args.InvokedItem

        If gridProgreso.Visibility = Visibility.Collapsed Then
            If Not item Is Nothing Then
                If item.Text = recursos.GetString("Games") Then
                    If gvTiles.Items.Count = 0 Then
                        Interfaz.Pestañas.Visibilidad_Pestañas(gridAvisoNoJuegos, Nothing)
                    Else
                        Interfaz.Pestañas.Visibilidad_Pestañas(gridJuegos, item.Text)
                    End If
                ElseIf item.Text = recursos.GetString("Config") Then
                    Interfaz.Pestañas.Visibilidad_Pestañas(gridConfig, item.Text)
                ElseIf item.Text = recursos.GetString("MoreTiles") Then
                    Interfaz.Pestañas.Visibilidad_Pestañas(gridMasTiles, item.Text)
                ElseIf item.Text = recursos.GetString("MoreThings") Then
                    Interfaz.Pestañas.Visibilidad_Pestañas(gridMasCosas, item.Text)
                End If
            End If
        End If

    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        Cache.Cargar()
        Interfaz.Juegos.Cargar()
        Interfaz.AñadirTile.Cargar()
        Interfaz.Busqueda.Cargar()
        Interfaz.MasTiles.Cargar()
        Configuracion.Cargar()
        Steam.Generar(False)
        MasCosas.Cargar()

    End Sub

End Class
