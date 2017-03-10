Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers
Imports Windows.UI

Module Battlenet

    Public Async Sub CargarJuegos(boolBuscarCarpeta As Boolean)

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonAñadirCarpetaTexto As TextBlock = pagina.FindName("buttonAñadirCarpetaBattlenetTexto")

        Dim botonCarpetaTexto As TextBlock = pagina.FindName("tbBattlenetConfigCarpeta")

        Dim carpeta As StorageFolder = Nothing

        Try
            If boolBuscarCarpeta = True Then
                Dim carpetapicker As FolderPicker = New FolderPicker()

                carpetapicker.FileTypeFilter.Add("*")
                carpetapicker.ViewMode = PickerViewMode.List

                carpeta = Await carpetapicker.PickSingleFolderAsync()
            Else
                carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("BattlenetCarpeta")
            End If
        Catch ex As Exception

        End Try

        Dim listaJuegos As New List(Of Tile)

        If Not carpeta Is Nothing Then
            Dim carpetasJuegos As IReadOnlyList(Of StorageFolder) = Await carpeta.GetFoldersAsync()
            Dim detectadoBool As Boolean = False

            For Each carpetaJuego As StorageFolder In carpetasJuegos
                Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpetaJuego.GetFilesAsync()

                For Each fichero As StorageFile In ficheros
                    If fichero.DisplayName = "Diablo III" And fichero.FileType = ".exe" Then
                        detectadoBool = True
                    End If

                    If fichero.DisplayName = "Hearthstone" And fichero.FileType = ".exe" Then
                        detectadoBool = True
                    End If

                    If fichero.DisplayName = "Heroes of the Storm" And fichero.FileType = ".exe" Then
                        detectadoBool = True
                    End If

                    If fichero.DisplayName = "Overwatch" And fichero.FileType = ".exe" Then
                        detectadoBool = True
                    End If

                    If fichero.DisplayName = "SC2" And fichero.FileType = ".exe" Then
                        detectadoBool = True
                    End If

                    If fichero.DisplayName = "WoW" And fichero.FileType = ".exe" Then
                        detectadoBool = True
                    End If
                Next
            Next

            If detectadoBool = True Then
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("BattlenetCarpeta", carpeta)
                botonCarpetaTexto.Text = carpeta.Path
                botonAñadirCarpetaTexto.Text = recursos.GetString("Boton Cambiar")

                For Each carpetaJuego As StorageFolder In carpetasJuegos
                    Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpetaJuego.GetFilesAsync()

                    For Each fichero As StorageFile In ficheros
                        Dim ejecutable As String = Nothing

                        If fichero.DisplayName = "Diablo III" And fichero.FileType = ".exe" Then
                            ejecutable = "battlenet://D3"
                        End If

                        If fichero.DisplayName = "Hearthstone" And fichero.FileType = ".exe" Then
                            ejecutable = "battlenet://WTCG"
                        End If

                        If fichero.DisplayName = "Heroes of the Storm" And fichero.FileType = ".exe" Then
                            ejecutable = "battlenet://Hero"
                        End If

                        If fichero.DisplayName = "Overwatch" And fichero.FileType = ".exe" Then
                            ejecutable = "battlenet://Pro"
                        End If

                        If fichero.DisplayName = "SC2" And fichero.FileType = ".exe" Then
                            ejecutable = "battlenet://S2"
                        End If

                        If fichero.DisplayName = "WoW" And fichero.FileType = ".exe" Then
                            ejecutable = "battlenet://WoW"
                        End If

                        If Not ejecutable = Nothing Then
                            Dim titulo As String = carpetaJuego.Name

                            Dim tituloBool As Boolean = False
                            Dim i As Integer = 0
                            While i < listaJuegos.Count
                                If listaJuegos(i).Titulo = titulo Then
                                    tituloBool = True
                                End If
                                i += 1
                            End While

                            If tituloBool = False Then
                                Dim juego As New Tile(titulo, Nothing, New Uri(ejecutable), Nothing, "Battle.net", Nothing)
                                listaJuegos.Add(juego)

                                Dim texto As New TextBlock With {
                                     .Text = juego.Titulo,
                                     .Tag = juego,
                                     .Foreground = New SolidColorBrush(Colors.White)
                                    }

                                Dim lvJuegos As ListView = pagina.FindName("lvBattlenetJuegos")
                                lvJuegos.Items.Add(texto)
                            End If
                        End If
                    Next
                Next
            End If
        End If

        Dim tbSiJuegos As TextBlock = pagina.FindName("tbSiJuegosBattlenet")
        Dim tbNoJuegos As TextBlock = pagina.FindName("tbNoJuegosBattlenet")

        If listaJuegos.Count > 0 Then
            tbSiJuegos.Visibility = Visibility.Visible
            tbNoJuegos.Visibility = Visibility.Collapsed
        Else
            tbSiJuegos.Visibility = Visibility.Collapsed
            tbNoJuegos.Visibility = Visibility.Visible
        End If

    End Sub

End Module
