Imports Windows.Storage

Module Configuracion

    Public Sub Iniciar()

        If ApplicationData.Current.LocalSettings.Values("modo_tiles") Is Nothing Then
            ModoTiles(0, True)
        Else
            ModoTiles(ApplicationData.Current.LocalSettings.Values("modo_tiles"), True)
        End If

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbTitulo As TextBlock = pagina.FindName("tbTitulo")
        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ")"

    End Sub

    Public Sub ModoTiles(modo As Integer, arranque As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("modo_tiles") = modo

        If arranque = True Then
            Dim cbTiles As ComboBox = pagina.FindName("cbConfigModosTiles")
            cbTiles.SelectedIndex = modo
        Else
            If modo = 0 Then
                Steam.Generar(False)
            ElseIf modo = 1 Then
                If Not ApplicationData.Current.LocalSettings.Values("cuenta_steam") Is Nothing Then
                    Dim cuenta As String = ApplicationData.Current.LocalSettings.Values("cuenta_steam")
                    Dim tbCuenta As TextBox = pagina.FindName("tbConfigCuenta")
                    tbCuenta.Text = cuenta
                    Steam.Cuenta(cuenta)
                End If
            End If
        End If

        Dim sp1 As StackPanel = pagina.FindName("spModoTile1")

        If modo = 0 Then
            sp1.Visibility = Visibility.Visible
        Else
            sp1.Visibility = Visibility.Collapsed
        End If

        Dim sp2 As StackPanel = pagina.FindName("spModoTile2")

        If modo = 1 Then
            sp2.Visibility = Visibility.Visible
        Else
            sp2.Visibility = Visibility.Collapsed
        End If

    End Sub

End Module
