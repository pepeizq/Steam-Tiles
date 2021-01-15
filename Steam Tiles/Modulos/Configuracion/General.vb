Imports Windows.Storage

Namespace Configuracion
    Module General

        Public Sub Cargar()

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonAbrirConfig As Button = pagina.FindName("botonAbrirConfig")

            AddHandler botonAbrirConfig.Click, AddressOf AbrirConfigClick
            AddHandler botonAbrirConfig.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto
            AddHandler botonAbrirConfig.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto

            Dim cbSteamConfigModosTiles As ComboBox = pagina.FindName("cbSteamConfigModosTiles")
            cbSteamConfigModosTiles.Items.Add(recursos.GetString("Steam_ConfigImport1"))
            cbSteamConfigModosTiles.Items.Add(recursos.GetString("Steam_ConfigImport2"))

            If ApplicationData.Current.LocalSettings.Values("modo_tiles") Is Nothing Then
                ApplicationData.Current.LocalSettings.Values("modo_tiles") = 0
                cbSteamConfigModosTiles.SelectedIndex = 0

                Dim spSteamConfigModo1 As StackPanel = pagina.FindName("spSteamConfigModo1")
                spSteamConfigModo1.Visibility = Visibility.Visible
            Else
                cbSteamConfigModosTiles.SelectedIndex = ApplicationData.Current.LocalSettings.Values("modo_tiles")

                Dim spSteamConfigModo1 As StackPanel = pagina.FindName("spSteamConfigModo1")
                Dim spSteamConfigModo2 As StackPanel = pagina.FindName("spSteamConfigModo2")

                If cbSteamConfigModosTiles.SelectedIndex = 0 Then
                    spSteamConfigModo1.Visibility = Visibility.Visible
                    spSteamConfigModo2.Visibility = Visibility.Collapsed
                Else
                    spSteamConfigModo1.Visibility = Visibility.Collapsed
                    spSteamConfigModo2.Visibility = Visibility.Visible
                End If
            End If

            AddHandler cbSteamConfigModosTiles.SelectionChanged, AddressOf CambiarModoSelect
            AddHandler cbSteamConfigModosTiles.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico
            AddHandler cbSteamConfigModosTiles.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico

            Dim botonBuscarCarpetaSteam As Button = pagina.FindName("botonBuscarCarpetaSteam")

            AddHandler botonBuscarCarpetaSteam.Click, AddressOf BuscarCarpetaClick
            AddHandler botonBuscarCarpetaSteam.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto
            AddHandler botonBuscarCarpetaSteam.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto

            Dim botonBorrarCarpetasSteam As Button = pagina.FindName("botonBorrarCarpetasSteam")

            AddHandler botonBorrarCarpetasSteam.Click, AddressOf BorrarCarpetasClick
            AddHandler botonBorrarCarpetasSteam.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto
            AddHandler botonBorrarCarpetasSteam.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto

            Dim botonConfigSteamCuentaImagen As Button = pagina.FindName("botonConfigSteamCuentaImagen")

            AddHandler botonConfigSteamCuentaImagen.Click, AddressOf AbrirImagenCuentaClick
            AddHandler botonConfigSteamCuentaImagen.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto
            AddHandler botonConfigSteamCuentaImagen.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto

            Dim tbConfigSteamCuenta As TextBox = pagina.FindName("tbConfigSteamCuenta")
            tbConfigSteamCuenta.PlaceholderText = recursos.GetString("Steam_AccountExample")

            AddHandler tbConfigSteamCuenta.TextChanged, AddressOf CuentaCambiaTexto
            AddHandler tbConfigSteamCuenta.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico
            AddHandler tbConfigSteamCuenta.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico

        End Sub

        Private Sub AbrirConfigClick(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridConfig As Grid = pagina.FindName("gridConfig")

            Dim recursos As New Resources.ResourceLoader()
            Interfaz.Pestañas.Visibilidad(gridConfig, recursos.GetString("Config"), sender)

        End Sub

        Private Sub CambiarModoSelect(sender As Object, e As SelectionChangedEventArgs)

            Steam.Borrar()

            Dim cb As ComboBox = sender
            ApplicationData.Current.LocalSettings.Values("modo_tiles") = cb.SelectedIndex

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spSteamConfigModo1 As StackPanel = pagina.FindName("spSteamConfigModo1")
            Dim spSteamConfigModo2 As StackPanel = pagina.FindName("spSteamConfigModo2")

            If cb.SelectedIndex = 0 Then
                spSteamConfigModo1.Visibility = Visibility.Visible
                spSteamConfigModo2.Visibility = Visibility.Collapsed
            Else
                spSteamConfigModo1.Visibility = Visibility.Collapsed
                spSteamConfigModo2.Visibility = Visibility.Visible
            End If

        End Sub

        Private Sub BuscarCarpetaClick(sender As Object, e As RoutedEventArgs)

            Steam.Generar(True)

        End Sub

        Private Sub BorrarCarpetasClick(sender As Object, e As RoutedEventArgs)

            Steam.Borrar()

        End Sub

        Private Sub AbrirImagenCuentaClick(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim grid As Grid = pagina.FindName("gridConfigSteamCuentaImagen")
            Dim icono As FontAwesome5.FontAwesome = pagina.FindName("iconoConfigSteamCuentaImagen")

            If grid.Visibility = Visibility.Collapsed Then
                grid.Visibility = Visibility.Visible
                icono.Icon = FontAwesome5.EFontAwesomeIcon.Solid_AngleDoubleUp
            Else
                grid.Visibility = Visibility.Collapsed
                icono.Icon = FontAwesome5.EFontAwesomeIcon.Solid_AngleDoubleDown
            End If

        End Sub

        Private Sub CuentaCambiaTexto(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender

            If tb.Text.Trim.Length > 0 Then
                If tb.Text.Trim.Contains("steamcommunity.com/id/") Then
                    Steam.Cuenta(tb.Text.Trim)
                End If
            End If

        End Sub

        Public Sub Estado(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonAbrirConfig As Button = pagina.FindName("botonAbrirConfig")
            botonAbrirConfig.IsEnabled = estado

            Dim cbSteamConfigModosTiles As ComboBox = pagina.FindName("cbSteamConfigModosTiles")
            cbSteamConfigModosTiles.IsEnabled = estado

            Dim botonBuscarCarpetaSteam As Button = pagina.FindName("botonBuscarCarpetaSteam")
            botonBuscarCarpetaSteam.IsEnabled = estado

            Dim botonBorrarCarpetasSteam As Button = pagina.FindName("botonBorrarCarpetasSteam")
            botonBorrarCarpetasSteam.IsEnabled = estado

            Dim botonConfigSteamCuentaImagen As Button = pagina.FindName("botonConfigSteamCuentaImagen")
            botonConfigSteamCuentaImagen.IsEnabled = estado

            Dim tbConfigSteamCuenta As TextBox = pagina.FindName("tbConfigSteamCuenta")
            tbConfigSteamCuenta.IsEnabled = estado

        End Sub

    End Module
End Namespace