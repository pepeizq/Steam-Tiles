Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers

Module Tiles

    Public Async Sub CargaSteam(gridview As GridView, textbox As TextBox, button As Button, sv As ScrollViewer, boolBuscarCarpeta As Boolean, gridAviso As Grid, textBlockAviso As TextBlock)

        button.IsEnabled = False

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        Dim listaTemp As New List(Of String)
        Dim listaFinal As List(Of Tiles) = New List(Of Tiles)
        Dim carpeta As StorageFolder = Nothing

        If boolBuscarCarpeta = True Then
            Try
                Dim picker As FolderPicker = New FolderPicker()

                picker.FileTypeFilter.Add("*")
                picker.ViewMode = PickerViewMode.List

                carpeta = Await picker.PickSingleFolderAsync()
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("carpetaPrincipal", carpeta)
            Catch ex As Exception

            End Try
        Else
            Try
                carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("carpetaPrincipal")
            Catch ex As Exception

            End Try
        End If

        If Not carpeta Is Nothing Then
            Dim i As Integer = 0
            If gridview.Items.Count > 0 Then
                While i < gridview.Items.Count
                    Dim tile As Tiles = gridview.Items(i)

                    listaTemp.Add(tile.titulo + "/*/" + tile.id)
                    i += 1
                End While
            End If

            Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync()
            Dim j As Integer = 0

            For Each fichero As StorageFile In ficheros
                If fichero.FileType.Contains(".acf") Then
                    listaTemp.Add(Await LeerFichero(fichero))
                    j += 1
                End If
            Next

            If j = 0 Then
                MessageBox.EnseñarMensaje(recursos.GetString("Fallo1"))
            End If

            For Each fichero As StorageFile In ficheros
                If fichero.FileType.Contains(".vdf") Then
                    LeerCarpetas(fichero, gridAviso, textBlockAviso)
                End If
            Next

            i = 0
            While i < listaTemp.Count
                Dim k, l As Integer
                k = 0
                While k < listaTemp.Count
                    If String.Equals(listaTemp(k), listaTemp(i)) = True Then
                        l += 1
                        If l > 1 Then
                            listaTemp.RemoveAt(i)
                        End If
                    End If
                    k += 1
                End While
                l = 0
                i += 1
            End While

            listaTemp.Sort()

            i = 0
            While i < listaTemp.Count

                Dim int As Integer

                int = listaTemp(i).IndexOf("/*/")

                Dim titulo As String = listaTemp(i).Remove(int, listaTemp(i).Length - int)
                Dim id As String = listaTemp(i).Remove(0, int + 3)

                Dim imagen As Uri = New Uri("http://cdn.akamai.steamstatic.com/steam/apps/" + id + "/header.jpg", UriKind.RelativeOrAbsolute)

                Dim bitmap As New BitmapImage
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache
                bitmap.UriSource = imagen

                listaFinal.Add(New Tiles(titulo, id, New Uri("steam://rungameid/" + id), bitmap, imagen))

                i += 1
            End While

            gridview.ItemsSource = listaFinal
            textbox.Text = carpeta.Path
        End If

        If listaFinal.Count > 0 Then
            sv.Visibility = Visibility.Visible
        End If

        button.IsEnabled = True

    End Sub

    Private Async Function LeerFichero(fichero As Object) As Task(Of String)

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

            Dim stringFinal As String = titulo + "/*/" + id

            Return stringFinal
        Catch ex As Exception
            MessageBox.EnseñarMensaje("Error en el fichero: " + fichero.ToString)
            Return Nothing
        End Try
    End Function

    Private Async Sub LeerCarpetas(fichero As Object, gridAviso As Grid, textBlockAviso As TextBlock)

        gridAviso.Visibility = Visibility.Collapsed

        Try
            Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
            Dim mensajeFinal As String = Nothing

            Dim text As String = Await FileIO.ReadTextAsync(fichero)
            Dim j As Integer = 0

            Dim temp, temp2 As String
            Dim int, int2 As Integer

            int = text.IndexOf("ContentStatsID")
            temp = text.Remove(0, int + 14)

            int2 = temp.IndexOf(vbLf)
            temp2 = temp.Remove(0, int2)

            Dim i As Integer = 0
            While i < 10
                Dim temp3 As String
                Dim int3 As Integer

                If temp2.Contains(vbLf) Then
                    int3 = temp2.IndexOf(vbLf)
                    temp3 = temp2.Remove(int3, temp2.Length - int3)

                    temp2 = temp2.Remove(0, int3 + 1)

                    If temp3.Length > 1 Then
                        temp3 = temp3.Trim
                        temp3 = temp3.Remove(0, 3)
                        temp3 = temp3.Trim
                        temp3 = temp3.Remove(0, 1)
                        temp3 = temp3.Remove(temp3.Length - 1, 1)
                        temp3 = temp3.Replace(":\\", ":\")

                        If mensajeFinal = Nothing Then
                            mensajeFinal = mensajeFinal + Environment.NewLine + Environment.NewLine + temp3 + "\steamapps"
                        Else
                            mensajeFinal = mensajeFinal + Environment.NewLine + temp3 + "\steamapps"
                        End If

                        j += 1
                    End If
                End If
                i += 1
            End While

            If j > 0 Then
                If j = 1 Then
                    gridAviso.Visibility = Visibility.Visible
                    textBlockAviso.Text = recursos.GetString("Fallo2") + mensajeFinal
                Else
                    gridAviso.Visibility = Visibility.Visible
                    textBlockAviso.Text = recursos.GetString("Fallo3") + mensajeFinal
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Class Tiles

        Public _titulo As String

        Public Property titulo As String
            Get
                Return _titulo
            End Get
            Set(ByVal value As String)
                _titulo = value
            End Set
        End Property

        Public _id As String

        Public Property id As String
            Get
                Return _id
            End Get
            Set(ByVal value As String)
                _id = value
            End Set
        End Property

        Public _enlace As Uri

        Public Property enlace As Uri
            Get
                Return _enlace
            End Get
            Set(ByVal value As Uri)
                _enlace = value
            End Set
        End Property

        Public _imagen As BitmapImage

        Public Property imagen As BitmapImage
            Get
                Return _imagen
            End Get
            Set(ByVal value As BitmapImage)
                _imagen = value
            End Set
        End Property

        Public _imagenUri As Uri

        Public Property imagenUri As Uri
            Get
                Return _imagenUri
            End Get
            Set(ByVal value As Uri)
                _imagenUri = value
            End Set
        End Property

        Public Sub New(ByVal tit As String, ByVal id As String, ByVal enl As Uri, ByVal img As BitmapImage, ByVal uri As Uri)
            _titulo = tit
            _id = id
            _enlace = enl
            _imagen = img
            _imagenUri = uri
        End Sub
    End Class

End Module
