Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers
Imports Windows.Web.Http

Module Listado

    Dim clave As String = "carpeta33"

    Public Async Sub Generar(gridview As GridView, button As Button, gridCargando As Grid, sv As ScrollViewer, boolBuscarCarpeta As Boolean)

        button.IsEnabled = False
        gridCargando.Visibility = Visibility.Visible

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
                    Catch ex As Exception
                        StorageApplicationPermissions.FutureAccessList.AddOrReplace(clave + i.ToString, carpeta)
                        numCarpetas.Values("numCarpetas") = i + 1
                        Exit While
                    End Try

                    i += 1
                End While

                If Not carpeta.Path.Contains("steamapps") Then
                    If Not gridview.Items.Count = 0 Then
                        MessageBox.EnseñarMensaje(recursos.GetString("Fallo2"))
                    End If
                End If

            Catch ex As Exception

            End Try
        End If

        '-------------------------------------------------------------

        Dim listaTemp As New List(Of String)
        Dim listaFinal As List(Of Tiles) = New List(Of Tiles)

        Dim h As Integer = 0
        While h < numCarpetas.Values("numCarpetas") + 1
            Dim carpeta As StorageFolder = Nothing

            Try
                carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(clave + h.ToString)
            Catch ex As Exception

            End Try

            If Not carpeta Is Nothing Then
                Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync()

                Dim i As Integer = 0
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
                            Dim text As String = Await FileIO.ReadTextAsync(fichero)

                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = text.IndexOf("name")
                            temp = text.Remove(0, int + 5)

                            int2 = temp.IndexOf("StateFlags")
                            temp2 = temp.Remove(int2 - 1, temp.Length - int2 + 1)

                            temp2 = temp2.Trim
                            temp2 = temp2.Remove(0, 1)
                            temp2 = temp2.Remove(temp2.Length - 1, 1)

                            Dim titulo As String = temp2.Trim

                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = text.IndexOf("appid")
                            temp3 = text.Remove(0, int3 + 6)

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
            h += 1
        End While

        If listaFinal.Count > 0 Then
            listaFinal.Sort(Function(x, y) x.titulo.CompareTo(y.titulo))
            sv.Visibility = Visibility.Visible
        Else
            If boolBuscarCarpeta = True Then
                MessageBox.EnseñarMensaje(recursos.GetString("Fallo1"))
            End If
        End If

        gridview.ItemsSource = listaFinal

        button.IsEnabled = True
        gridCargando.Visibility = Visibility.Collapsed

    End Sub

End Module
