Imports Windows.Storage

Module Configuracion

    Public Sub Iniciar()

        If ApplicationData.Current.LocalSettings.Values("titulo_tile") Is Nothing Then
            MostrarTitulo(False)
        Else
            MostrarTitulo(ApplicationData.Current.LocalSettings.Values("titulo_tile"))
        End If

        If ApplicationData.Current.LocalSettings.Values("drm_tile") Is Nothing Then
            MostrarDRM(False)
        Else
            MostrarDRM(ApplicationData.Current.LocalSettings.Values("drm_tile"))
        End If

    End Sub

    Public Sub MostrarTitulo(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("titulo_tile") = estado

        Dim cb As CheckBox = pagina.FindName("cbTilesTitulo")
        cb.IsChecked = estado

    End Sub

    Public Sub MostrarDRM(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("drm_tile") = estado

        Dim cb As CheckBox = pagina.FindName("cbTilesIconos")
        cb.IsChecked = estado

        Dim cbLista As ComboBox = pagina.FindName("cbTilesIconosLista")
        cbLista.SelectedIndex = 0

        If estado = True Then
            cbLista.Visibility = Visibility.Visible
        Else
            cbLista.Visibility = Visibility.Collapsed
        End If

    End Sub

End Module
