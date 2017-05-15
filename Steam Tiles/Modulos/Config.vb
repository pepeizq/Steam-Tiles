Imports Windows.Storage

Module Config

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbTitulo As CheckBox = pagina.FindName("cbTilesTitulo")
        Dim cbIconos As CheckBox = pagina.FindName("cbTilesIconos")
        Dim cbIconosLista As ComboBox = pagina.FindName("cbTilesIconosLista")

        cbIconosLista.SelectedIndex = 0

        If ApplicationData.Current.LocalSettings.Values("titulotile") = Nothing Then
            cbTitulo.IsChecked = False
            ApplicationData.Current.LocalSettings.Values("titulotile") = "off"
        End If

        If ApplicationData.Current.LocalSettings.Values("logotile") = Nothing Then
            cbIconos.IsChecked = False
            ApplicationData.Current.LocalSettings.Values("logotile") = "off"
        End If

        '----------------------------------

        If ApplicationData.Current.LocalSettings.Values("titulotile") = "on" Then
            cbTitulo.IsChecked = True
        Else
            cbTitulo.IsChecked = False
        End If

        If ApplicationData.Current.LocalSettings.Values("logotile") = "on" Then
            cbIconos.IsChecked = True
            cbIconosLista.Visibility = Visibility.Visible
        Else
            cbIconos.IsChecked = False
            cbIconosLista.Visibility = Visibility.Collapsed
        End If

    End Sub

End Module
