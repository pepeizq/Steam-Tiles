Imports Windows.Storage

Module Config

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbTitulo As CheckBox = pagina.FindName("cbTilesTitulo")
        Dim cbLogo As CheckBox = pagina.FindName("cbTilesBranding")
        Dim slider As Slider = pagina.FindName("sliderTilesOverlay")
        Dim cbCirculo As CheckBox = pagina.FindName("cbTilesCirculo")

        If ApplicationData.Current.LocalSettings.Values("titulotile") = Nothing Then
            cbTitulo.IsChecked = True
            ApplicationData.Current.LocalSettings.Values("titulotile") = "on"
        End If

        If ApplicationData.Current.LocalSettings.Values("logotile") = Nothing Then
            cbLogo.IsChecked = True
            ApplicationData.Current.LocalSettings.Values("logotile") = "on"
        End If

        If ApplicationData.Current.LocalSettings.Values("overlaytile") = Nothing Then
            slider.Value = 0
            ApplicationData.Current.LocalSettings.Values("overlaytile") = slider.Value
        End If

        If ApplicationData.Current.LocalSettings.Values("circulotile") = Nothing Then
            ApplicationData.Current.LocalSettings.Values("circulotile") = "off"
        End If

        '----------------------------------

        If ApplicationData.Current.LocalSettings.Values("titulotile") = "on" Then
            cbTitulo.IsChecked = True
        Else
            cbTitulo.IsChecked = False
        End If

        If ApplicationData.Current.LocalSettings.Values("logotile") = "on" Then
            cbLogo.IsChecked = True
        Else
            cbLogo.IsChecked = False
        End If

        If Not ApplicationData.Current.LocalSettings.Values("overlaytile") = 0 Then
            slider.Value = ApplicationData.Current.LocalSettings.Values("overlaytile")
        End If

        If ApplicationData.Current.LocalSettings.Values("circulotile") = "on" Then
            cbCirculo.IsChecked = True
        Else
            cbCirculo.IsChecked = False
        End If

    End Sub

End Module
