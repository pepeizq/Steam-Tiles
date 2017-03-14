Imports Windows.Storage

Module Config

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbTitulo As CheckBox = pagina.FindName("cbTilesTitulo")
        Dim cbLogos As CheckBox = pagina.FindName("cbTilesLogos")
        Dim gridLogos As Grid = pagina.FindName("gridTilesLogos")
        Dim rbLogos1 As RadioButton = pagina.FindName("rbTipoLogos1")
        Dim rbLogos2 As RadioButton = pagina.FindName("rbTipoLogos2")

        Dim slider As Slider = pagina.FindName("sliderTilesOverlay")
        Dim cbCirculo As CheckBox = pagina.FindName("cbTilesCirculo")

        If ApplicationData.Current.LocalSettings.Values("titulotile") = Nothing Then
            cbTitulo.IsChecked = False
            ApplicationData.Current.LocalSettings.Values("titulotile") = "off"
        End If

        If ApplicationData.Current.LocalSettings.Values("logotile") = Nothing Then
            cbLogos.IsChecked = False
            ApplicationData.Current.LocalSettings.Values("logotile") = "off"
            gridLogos.Visibility = Visibility.Collapsed
            rbLogos1.IsChecked = True
            rbLogos2.IsChecked = False
            ApplicationData.Current.LocalSettings.Values("logotile1") = "on"
            ApplicationData.Current.LocalSettings.Values("logotile2") = "off"
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
            cbLogos.IsChecked = True
            gridLogos.Visibility = Visibility.Visible

            If ApplicationData.Current.LocalSettings.Values("logotile1") = "on" Then
                rbLogos1.IsChecked = True
            Else
                rbLogos1.IsChecked = False
            End If

            If ApplicationData.Current.LocalSettings.Values("logotile2") = "on" Then
                rbLogos2.IsChecked = True
            Else
                rbLogos2.IsChecked = False
            End If
        Else
            cbLogos.IsChecked = False
            gridLogos.Visibility = Visibility.Collapsed
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
