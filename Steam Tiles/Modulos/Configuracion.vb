Imports Windows.Storage

Module Configuracion

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

    End Sub

    Private Sub AbrirConfigClick(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridConfig As Grid = pagina.FindName("gridConfig")

        Dim recursos As New Resources.ResourceLoader()
        Interfaz.Pestañas.Visibilidad_Pestañas(gridConfig, recursos.GetString("Config"))

    End Sub

    Private Sub CambiarModoSelect(sender As Object, e As SelectionChangedEventArgs)

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

    Public Sub Estado(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonAbrirConfig As Button = pagina.FindName("botonAbrirConfig")
        botonAbrirConfig.IsEnabled = estado

        Dim cbSteamConfigModosTiles As ComboBox = pagina.FindName("cbSteamConfigModosTiles")
        cbSteamConfigModosTiles.IsEnabled = estado

    End Sub

End Module
