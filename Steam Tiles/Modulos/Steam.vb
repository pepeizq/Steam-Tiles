Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers
Imports Windows.UI
Imports Windows.UI.Core
Imports Windows.UI.Xaml.Media.Animation

Module Steam

    Dim clave As String = "carpeta35"

    Public Async Sub Generar(boolBuscarCarpeta As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonAñadir As Button = pagina.FindName("botonAñadirCarpetaSteam")
        botonAñadir.IsEnabled = False

        Dim botonBorrar As Button = pagina.FindName("botonBorrarCarpetasSteam")
        botonBorrar.IsEnabled = False

        Dim pr As ProgressRing = pagina.FindName("prTilesSteam")
        pr.Visibility = Visibility.Visible

        Dim gv As GridView = pagina.FindName("gridViewTilesSteam")

        Dim spCarpetas As StackPanel = pagina.FindName("spCarpetasDetectadas")
        spCarpetas.Children.Clear()

        Dim recursos As New Resources.ResourceLoader()
        Dim numCarpetas As ApplicationDataContainer = ApplicationData.Current.LocalSettings

        If boolBuscarCarpeta = True Then
            Try
                Dim picker As New FolderPicker()

                picker.FileTypeFilter.Add("*")
                picker.ViewMode = PickerViewMode.List

                Dim carpeta As StorageFolder = Await picker.PickSingleFolderAsync()
                Dim carpetaTemp As StorageFolder = Nothing

                Dim i As Integer = 0
                While i < (numCarpetas.Values("numCarpetas") + 1)
                    Try
                        carpetaTemp = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(clave + i.ToString)

                        Dim tb As New TextBlock With {
                            .Text = carpetaTemp.Path
                        }

                        spCarpetas.Children.Add(tb)
                    Catch ex As Exception
                        StorageApplicationPermissions.FutureAccessList.AddOrReplace(clave + i.ToString, carpeta)
                        numCarpetas.Values("numCarpetas") = i + 1

                        Dim tb As New TextBlock With {
                            .Text = carpeta.Path
                        }

                        spCarpetas.Children.Add(tb)
                        Exit While
                    End Try
                    i += 1
                End While

            Catch ex As Exception

            End Try
        Else
            Dim i As Integer = 0
            While i < (numCarpetas.Values("numCarpetas") + 1)
                Try
                    Dim carpetaTemp As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(clave + i.ToString)

                    Dim tb As New TextBlock With {
                        .Text = carpetaTemp.Path
                    }

                    spCarpetas.Children.Add(tb)
                Catch ex As Exception

                End Try
                i += 1
            End While
        End If

        If spCarpetas.Children.Count = 0 Then
            Dim tb As New TextBlock With {
                .Text = recursos.GetString("NoFoldersDetected")
            }

            spCarpetas.Children.Add(tb)
        End If

        '-------------------------------------------------------------

        Dim listaTemp As New List(Of String)
        Dim listaFinal As New List(Of Tile)

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
                        If gv.Items.Count > 0 Then
                            While i < gv.Items.Count
                                Dim boton As Button = gv.Items(i)
                                Dim tile As Tile = boton.Tag

                                listaTemp.Add(tile.Titulo + "/*/" + tile.ID)
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
                                        If listaFinal(g).Titulo = titulo Then
                                            tituloBool = True
                                        End If
                                        g += 1
                                    End While

                                    If tituloBool = False Then
                                        Dim imagenAncha As New Uri("http://cdn.edgecast.steamstatic.com/steam/apps/" + id + "/header.jpg", UriKind.RelativeOrAbsolute)
                                        Dim imagenGrande As New Uri("http://cdn.akamai.steamstatic.com/steam/apps/" + id + "/capsule_616x353.jpg", UriKind.RelativeOrAbsolute)

                                        Dim juego As New Tile(titulo, id, "steam://rungameid/" + id, Nothing, imagenAncha, imagenAncha, imagenGrande)

                                        listaFinal.Add(juego)
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

        Dim panelAvisoNoJuegos As Grid = pagina.FindName("panelAvisoNoJuegos")
        Dim gridSeleccionar As Grid = pagina.FindName("gridSeleccionarJuego")

        If listaFinal.Count > 0 Then
            panelAvisoNoJuegos.Visibility = Visibility.Collapsed
            gridSeleccionar.Visibility = Visibility.Visible

            listaFinal.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

            gv.Items.Clear()

            For Each juego In listaFinal
                Dim boton As New Button

                Dim imagen As New ImageEx

                Try
                    imagen.Source = New BitmapImage(juego.ImagenAncha)
                Catch ex As Exception

                End Try

                imagen.IsCacheEnabled = True
                imagen.Stretch = Stretch.UniformToFill
                imagen.Padding = New Thickness(0, 0, 0, 0)

                boton.Tag = juego
                boton.Content = imagen
                boton.Padding = New Thickness(0, 0, 0, 0)
                boton.BorderThickness = New Thickness(1, 1, 1, 1)
                boton.BorderBrush = New SolidColorBrush(Colors.Black)
                boton.Background = New SolidColorBrush(Colors.Transparent)

                Dim tbToolTip As TextBlock = New TextBlock With {
                    .Text = juego.Titulo,
                    .FontSize = 16
                }

                ToolTipService.SetToolTip(boton, tbToolTip)
                ToolTipService.SetPlacement(boton, PlacementMode.Mouse)

                AddHandler boton.Click, AddressOf BotonTile_Click
                AddHandler boton.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler boton.PointerExited, AddressOf UsuarioSaleBoton

                gv.Items.Add(boton)
            Next

            If boolBuscarCarpeta = True Then
                Toast(listaFinal.Count.ToString + " " + recursos.GetString("GamesDetected"), Nothing)
            End If
        Else
            panelAvisoNoJuegos.Visibility = Visibility.Visible
            gridSeleccionar.Visibility = Visibility.Collapsed

            If boolBuscarCarpeta = True Then
                Toast(recursos.GetString("ErrorSteam1"), Nothing)
            End If
        End If

        botonAñadir.IsEnabled = True
        botonBorrar.IsEnabled = True
        pr.Visibility = Visibility.Collapsed

    End Sub

    Private Async Sub BotonTile_Click(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonJuego As Button = e.OriginalSource
        Dim juego As Tile = botonJuego.Tag

        Dim botonAñadirTile As Button = pagina.FindName("botonAñadirTile")
        botonAñadirTile.Tag = juego

        Dim imagenJuegoSeleccionado As ImageEx = pagina.FindName("imagenJuegoSeleccionado")
        Dim imagenCapsula As String = juego.ImagenAncha.ToString
        imagenCapsula = imagenCapsula.Replace("header.jpg", "capsule_184x69.jpg")
        imagenJuegoSeleccionado.Source = New BitmapImage(New Uri(imagenCapsula))

        Dim tbJuegoSeleccionado As TextBlock = pagina.FindName("tbJuegoSeleccionado")
        tbJuegoSeleccionado.Text = juego.Titulo

        Dim gridAñadir As Grid = pagina.FindName("gridAñadirTile")
        gridAñadir.Visibility = Visibility.Visible

        ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("tile", botonJuego)

        Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("tile")

        If Not animacion Is Nothing Then
            animacion.TryStart(gridAñadir)
        End If

        Dim tbTitulo As TextBlock = pagina.FindName("tbTitulo")
        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - " + juego.Titulo

        '---------------------------------------------

        Dim titulo1 As TextBlock = pagina.FindName("tituloTileAnchaEnseñar")
        Dim titulo2 As TextBlock = pagina.FindName("tituloTileAnchaPersonalizar")

        Dim titulo3 As TextBlock = pagina.FindName("tituloTileGrandeEnseñar")
        Dim titulo4 As TextBlock = pagina.FindName("tituloTileGrandePersonalizar")

        titulo1.Text = juego.Titulo
        titulo2.Text = juego.Titulo

        titulo3.Text = juego.Titulo
        titulo4.Text = juego.Titulo

        Try
            juego.ImagenPequeña = Await SacarIcono(juego.ID)
        Catch ex As Exception

        End Try

        If Not juego.ImagenPequeña = Nothing Then
            Dim imagenPequeña1 As ImageEx = pagina.FindName("imagenTilePequeñaEnseñar")
            Dim imagenPequeña2 As ImageEx = pagina.FindName("imagenTilePequeñaGenerar")
            Dim imagenPequeña3 As ImageEx = pagina.FindName("imagenTilePequeñaPersonalizar")

            imagenPequeña1.Source = juego.ImagenPequeña
            imagenPequeña2.Source = juego.ImagenPequeña
            imagenPequeña3.Source = juego.ImagenPequeña

            imagenPequeña1.Tag = juego.ImagenPequeña
            imagenPequeña2.Tag = juego.ImagenPequeña
            imagenPequeña3.Tag = juego.ImagenPequeña
        End If

        If Not juego.ImagenMediana = Nothing Then
            Dim imagenMediana1 As ImageEx = pagina.FindName("imagenTileMedianaEnseñar")
            Dim imagenMediana2 As ImageEx = pagina.FindName("imagenTileMedianaGenerar")
            Dim imagenMediana3 As ImageEx = pagina.FindName("imagenTileMedianaPersonalizar")

            imagenMediana1.Source = juego.ImagenMediana
            imagenMediana2.Source = juego.ImagenMediana
            imagenMediana3.Source = juego.ImagenMediana

            imagenMediana1.Tag = juego.ImagenMediana
            imagenMediana2.Tag = juego.ImagenMediana
            imagenMediana3.Tag = juego.ImagenMediana
        End If

        If Not juego.ImagenAncha = Nothing Then
            Dim imagenAncha1 As ImageEx = pagina.FindName("imagenTileAnchaEnseñar")
            Dim imagenAncha2 As ImageEx = pagina.FindName("imagenTileAnchaGenerar")
            Dim imagenAncha3 As ImageEx = pagina.FindName("imagenTileAnchaPersonalizar")

            imagenAncha1.Source = juego.ImagenAncha
            imagenAncha2.Source = juego.ImagenAncha
            imagenAncha3.Source = juego.ImagenAncha

            imagenAncha1.Tag = juego.ImagenAncha
            imagenAncha2.Tag = juego.ImagenAncha
            imagenAncha3.Tag = juego.ImagenAncha
        End If

        If Not juego.ImagenGrande = Nothing Then
            Dim imagenGrande1 As ImageEx = pagina.FindName("imagenTileGrandeEnseñar")
            Dim imagenGrande2 As ImageEx = pagina.FindName("imagenTileGrandeGenerar")
            Dim imagenGrande3 As ImageEx = pagina.FindName("imagenTileGrandePersonalizar")

            imagenGrande1.Source = juego.ImagenGrande
            imagenGrande2.Source = juego.ImagenGrande
            imagenGrande3.Source = juego.ImagenGrande

            imagenGrande1.Tag = juego.ImagenGrande
            imagenGrande2.Tag = juego.ImagenGrande
            imagenGrande3.Tag = juego.ImagenGrande
        End If

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Dim boton As Button = sender
        Dim imagen As ImageEx = boton.Content

        imagen.Saturation(0).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Dim boton As Button = sender
        Dim imagen As ImageEx = boton.Content

        imagen.Saturation(1).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    Public Sub Borrar()

        StorageApplicationPermissions.FutureAccessList.Clear()

        Dim recursos As New Resources.ResourceLoader()
        Dim numCarpetas As ApplicationDataContainer = ApplicationData.Current.LocalSettings
        numCarpetas.Values("numCarpetas") = 0

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim spCarpetas As StackPanel = pagina.FindName("spCarpetasDetectadas")
        spCarpetas.Children.Clear()

        Dim tb As New TextBlock With {
            .Text = recursos.GetString("NoFoldersDetected")
        }

        spCarpetas.Children.Add(tb)

        Dim gv As GridView = pagina.FindName("gridViewTilesSteam")
        gv.Items.Clear()

        Generar(False)

    End Sub

    Public Async Function SacarIcono(id As String) As Task(Of Uri)

        Dim html As String = Await Decompiladores.HttpClient(New Uri("https://store.steampowered.com/app/" + id + "/"))
        Dim uriIcono As Uri = Nothing

        If Not html = Nothing Then
            If html.Contains("<div class=" + ChrW(34) + "apphub_AppIcon") Then
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = html.IndexOf("<div class=" + ChrW(34) + "apphub_AppIcon")
                temp = html.Remove(0, int)

                int = temp.IndexOf("<img src=")
                temp = temp.Remove(0, int + 10)

                int2 = temp.IndexOf(ChrW(34))
                temp2 = temp.Remove(int2, temp.Length - int2)

                temp2 = temp2.Replace("%CDN_HOST_MEDIA_SSL%", "steamcdn-a.akamaihd.net")

                uriIcono = New Uri(temp2.Trim)
            End If
        End If

        If uriIcono = Nothing Then
            html = Await Decompiladores.HttpClient(New Uri("https://steamdb.info/app/" + id + "/"))

            If Not html = Nothing Then
                If html.Contains("<img class=" + ChrW(34) + "app-icon avatar") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<img class=" + ChrW(34) + "app-icon avatar")
                    temp = html.Remove(0, int)

                    int = temp.IndexOf("src=")
                    temp = temp.Remove(0, int + 5)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    uriIcono = New Uri(temp2.Trim)
                End If
            End If
        End If

        Return uriIcono
    End Function

End Module
