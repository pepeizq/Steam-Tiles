Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers
Imports Windows.UI
Imports Windows.Web.Http

Module Listado

    Dim clave As String = "carpeta35"

    Public Async Sub Generar(gridview As GridView, button As Button, pr As ProgressRing, sv As ScrollViewer, boolBuscarCarpeta As Boolean)

        button.IsEnabled = False
        pr.Visibility = Visibility.Visible

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tbCarpetas As TextBlock = pagina.FindName("tbCarpetasDetectadasSteam")

        If Not tbCarpetas.Text = Nothing Then
            tbCarpetas.Text = ""
        End If

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        Dim numCarpetas As ApplicationDataContainer = ApplicationData.Current.LocalSettings

        If boolBuscarCarpeta = True Then
            Try
                Dim picker As FolderPicker = New FolderPicker()

                picker.FileTypeFilter.Add("*")
                picker.ViewMode = PickerViewMode.List

                Dim carpeta As StorageFolder = Await picker.PickSingleFolderAsync()
                Dim carpetaTemp As StorageFolder = Nothing

                Dim i As Integer = 0
                While i < (numCarpetas.Values("numCarpetas") + 1)

                    Try
                        carpetaTemp = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(clave + i.ToString)
                        tbCarpetas.Text = tbCarpetas.Text + carpetaTemp.Path + Environment.NewLine
                    Catch ex As Exception
                        StorageApplicationPermissions.FutureAccessList.AddOrReplace(clave + i.ToString, carpeta)
                        numCarpetas.Values("numCarpetas") = i + 1
                        tbCarpetas.Text = tbCarpetas.Text + carpeta.Path + Environment.NewLine
                        Exit While
                    End Try

                    i += 1
                End While

                'If Not carpeta.Path.Contains("steamapps") Then
                '    If Not gridview.Items.Count = 0 Then
                '        Toast("Steam Tiles", recursos.GetString("Fallo2"))
                '    End If
                'End If

            Catch ex As Exception

            End Try
        Else
            Dim i As Integer = 0
            While i < (numCarpetas.Values("numCarpetas") + 1)
                Try
                    Dim carpetaTemp As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(clave + i.ToString)
                    tbCarpetas.Text = tbCarpetas.Text + carpetaTemp.Path + Environment.NewLine
                Catch ex As Exception

                End Try
                i += 1
            End While
        End If

        If tbCarpetas.Text = Nothing Then
            tbCarpetas.Text = recursos.GetString("Ninguna")

            Dim gridTiles As Grid = pagina.FindName("gridTiles")
            gridTiles.Visibility = Visibility.Collapsed

            Dim gridConfig As Grid = pagina.FindName("gridConfig")
            gridConfig.Visibility = Visibility.Visible

            Dim gridConfigApp As Grid = pagina.FindName("gridConfigApp")
            gridConfigApp.Visibility = Visibility.Visible

            Dim buttonConfigApp As Button = pagina.FindName("buttonConfigApp")
            buttonConfigApp.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#bfbfbf"))
            buttonConfigApp.BorderBrush = New SolidColorBrush(Colors.Black)
        Else
            tbCarpetas.Text = tbCarpetas.Text.Trim
        End If

        '-------------------------------------------------------------

        Dim listaTemp As New List(Of String)
        Dim listaFinal As List(Of Tiles) = New List(Of Tiles)

        Dim h As Integer = 0
        While h < numCarpetas.Values("numCarpetas") + 1
            Dim listaCarpetas As New List(Of StorageFolder)
            Dim carpetaMaestra As StorageFolder = Nothing

            Try
                carpetaMaestra = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(clave + h.ToString)
                listaCarpetas.Add(carpetaMaestra)
            Catch ex As Exception

            End Try

            Dim i As Integer = 0

            'If Not carpetaMaestra Is Nothing Then
            '    Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpetaMaestra.GetFilesAsync()

            '    For Each fichero As StorageFile In ficheros
            '        If fichero.Name.Contains("libraryfolders") Then
            '            Try
            '                Dim lineas As String = Await FileIO.ReadTextAsync(fichero)

            '                If Not lineas = Nothing Then
            '                    i = 0
            '                    While i < 10
            '                        If lineas.Contains(ChrW(34) + i.ToString + ChrW(34)) Then
            '                            Dim temp, temp2, temp3 As String
            '                            Dim int, int2, int3 As Integer

            '                            int = lineas.IndexOf(ChrW(34) + i.ToString + ChrW(34))
            '                            temp = lineas.Remove(0, int + 3)

            '                            int2 = temp.IndexOf(ChrW(34))
            '                            temp2 = temp.Remove(0, int2 + 1)

            '                            int3 = temp2.IndexOf(ChrW(34))
            '                            temp3 = temp2.Remove(int3, temp2.Length - int3)

            '                            temp3 = temp3.Replace("\\", "\")

            '                            StorageApplicationPermissions.FutureAccessList.AddOrReplace("carpetaSecundaria" + i.ToString, Await StorageFolder.GetFolderFromPathAsync(temp3))
            '                            Dim carpetaSecundaria As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(temp3 + "\steamapps")
            '                            MessageBox.EnseñarMensaje(carpetaSecundaria.Path)
            '                        End If
            '                        i += 1
            '                    End While
            '                End If
            '            Catch ex As Exception

            '            End Try
            '        End If
            '    Next
            'End If

            If listaCarpetas.Count > 0 Then
                For Each carpeta As StorageFolder In listaCarpetas
                    If Not carpeta Is Nothing Then
                        Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync()

                        i = 0
                        If gridview.Items.Count > 0 Then
                            While i < gridview.Items.Count
                                Dim tile As Tiles = gridview.Items(i)

                                listaTemp.Add(tile.titulo + "/*/" + tile.id)
                                i += 1
                            End While
                        End If

                        For Each fichero As StorageFile In ficheros
                            If fichero.FileType.Contains(".acf") Then
                                Try
                                    Dim lineas As String = Await FileIO.ReadTextAsync(fichero)

                                    Dim temp, temp2 As String
                                    Dim int, int2 As Integer

                                    int = lineas.IndexOf("name")
                                    temp = lineas.Remove(0, int + 5)

                                    int2 = temp.IndexOf("StateFlags")
                                    temp2 = temp.Remove(int2 - 1, temp.Length - int2 + 1)

                                    temp2 = temp2.Trim
                                    temp2 = temp2.Remove(0, 1)
                                    temp2 = temp2.Remove(temp2.Length - 1, 1)

                                    Dim titulo As String = temp2.Trim

                                    Dim temp3, temp4 As String
                                    Dim int3, int4 As Integer

                                    int3 = lineas.IndexOf("appid")
                                    temp3 = lineas.Remove(0, int3 + 6)

                                    int4 = temp3.IndexOf("Universe")
                                    temp4 = temp3.Remove(int4 - 1, temp3.Length - int4 + 1)

                                    temp4 = temp4.Trim
                                    temp4 = temp4.Remove(0, 1)
                                    temp4 = temp4.Remove(temp4.Length - 1, 1)

                                    Dim id As String = temp4.Trim

                                    listaTemp.Add(titulo + "/*/" + id)
                                Catch ex As Exception

                                End Try
                            End If
                        Next

                        i = 0
                        While i < listaTemp.Count
                            If Not listaTemp(i) = Nothing Then
                                If listaTemp(i).Contains("/*/") Then
                                    Dim int As Integer

                                    int = listaTemp(i).IndexOf("/*/")

                                    Dim titulo As String = listaTemp(i).Remove(int, listaTemp(i).Length - int)
                                    Dim id As String = listaTemp(i).Remove(0, int + 3)

                                    Dim tituloBool As Boolean = False
                                    Dim g As Integer = 0
                                    While g < listaFinal.Count
                                        If listaFinal(g).titulo = titulo Then
                                            tituloBool = True
                                        End If
                                        g += 1
                                    End While

                                    If tituloBool = False Then
                                        Try
                                            Dim imagen As Uri = New Uri("http://cdn.akamai.steamstatic.com/steam/apps/" + id + "/header.jpg", UriKind.RelativeOrAbsolute)
                                            Dim client As New HttpClient
                                            Dim response As Streams.IBuffer = Await client.GetBufferAsync(imagen)
                                            Dim stream As Stream = response.AsStream
                                            Dim mem As MemoryStream = New MemoryStream()
                                            Await stream.CopyToAsync(mem)
                                            mem.Position = 0

                                            Dim bitmap As New BitmapImage
                                            bitmap.SetSource(mem.AsRandomAccessStream)

                                            listaFinal.Add(New Tiles(titulo, id, New Uri("steam://rungameid/" + id), bitmap, imagen))
                                        Catch ex As Exception

                                        End Try
                                    End If
                                End If
                            End If
                            i += 1
                        End While
                    End If
                Next
            End If
            h += 1
        End While

        If listaFinal.Count > 0 Then
            listaFinal.Sort(Function(x, y) x.titulo.CompareTo(y.titulo))
            sv.Visibility = Visibility.Visible
        Else
            If boolBuscarCarpeta = True Then
                Toast("Steam Tiles", recursos.GetString("Fallo1"))
            End If
        End If

        gridview.ItemsSource = listaFinal

        button.IsEnabled = True
        pr.Visibility = Visibility.Collapsed

    End Sub

End Module
