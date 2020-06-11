Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers
Imports Windows.UI
Imports Windows.UI.Core
Imports Windows.UI.Xaml.Media.Animation

Module Steam

    Public anchoColumna As Integer = 306
    Dim clave As String = "carpeta36"

    Public Async Sub Generar(boolBuscarCarpeta As Boolean)

        Dim modo As Integer = ApplicationData.Current.LocalSettings.Values("modo_tiles")

        Dim helper As New LocalObjectStorageHelper

        Dim recursos As New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim spProgreso As StackPanel = pagina.FindName("spProgreso")
        spProgreso.Visibility = Visibility.Visible

        Dim pbProgreso As ProgressBar = pagina.FindName("pbProgreso")
        pbProgreso.Value = 0

        Dim tbProgreso As TextBlock = pagina.FindName("tbProgreso")
        tbProgreso.Text = String.Empty

        Dim cbTiles As ComboBox = pagina.FindName("cbConfigModosTiles")
        cbTiles.IsEnabled = False

        Dim sp1 As StackPanel = pagina.FindName("spModoTile1")
        sp1.IsHitTestVisible = False

        Dim sp2 As StackPanel = pagina.FindName("spModoTile2")
        sp2.IsHitTestVisible = False

        Dim botonCache As Button = pagina.FindName("botonConfigLimpiarCache")
        botonCache.IsEnabled = False

        Dim gridSeleccionarJuego As Grid = pagina.FindName("gridSeleccionarJuego")
        gridSeleccionarJuego.Visibility = Visibility.Collapsed

        Dim gv As GridView = pagina.FindName("gvTiles")
        gv.Items.Clear()

        Dim listaJuegos As New List(Of Tile)

        If Await helper.FileExistsAsync("juegos" + modo.ToString) = True Then
            listaJuegos = Await helper.ReadFileAsync(Of List(Of Tile))("juegos" + modo.ToString)
        End If

        If modo = 0 Then
            Dim spCarpetas As StackPanel = pagina.FindName("spCarpetasDetectadas")
            spCarpetas.Children.Clear()

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

            Dim listaFicheros As New List(Of SteamFichero)

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
                            If Not carpeta.Path.Contains("steamapps") Then
                                carpeta = Await StorageFolder.GetFolderFromPathAsync(carpeta.Path + "\steamapps")
                            End If

                            Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync()

                            i = 0
                            If gv.Items.Count > 0 Then
                                While i < gv.Items.Count
                                    Dim boton As Button = gv.Items(i)
                                    Dim tile As Tile = boton.Tag

                                    listaFicheros.Add(New SteamFichero(tile.Titulo, tile.ID))
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

                                        listaFicheros.Add(New SteamFichero(titulo, id))
                                    Catch ex As Exception

                                    End Try
                                End If
                            Next

                            Dim k As Integer = 0
                            If listaFicheros.Count > 0 Then
                                For Each fichero In listaFicheros
                                    Dim titulo As String = fichero.Titulo
                                    Dim id As String = fichero.ID

                                    Dim añadir As Boolean = True
                                    Dim g As Integer = 0
                                    While g < listaJuegos.Count
                                        If listaJuegos(g).ID = id Then
                                            añadir = False
                                        End If
                                        g += 1
                                    End While

                                    If id = "228980" Then
                                        añadir = False
                                    End If

                                    If añadir = True Then
                                        Dim imagenLogo As String = String.Empty

                                        Try
                                            imagenLogo = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/logo.png", id, "logo")
                                        Catch ex As Exception

                                        End Try

                                        Dim imagenAnchaReducida As String = String.Empty

                                        Try
                                            imagenAnchaReducida = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/capsule_184x69.jpg", id, "ancha2")
                                        Catch ex As Exception

                                        End Try

                                        Dim imagenAncha As String = String.Empty

                                        Try
                                            imagenAncha = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/header.jpg", id, "ancha")
                                        Catch ex As Exception

                                        End Try

                                        Dim imagenGrande As String = String.Empty

                                        Try
                                            imagenGrande = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/library_600x900.jpg", id, "grande")
                                        Catch ex As Exception

                                        End Try

                                        If imagenGrande = String.Empty Then
                                            Try
                                                imagenGrande = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/capsule_616x353.jpg", id, "grande")
                                            Catch ex As Exception

                                            End Try
                                        End If

                                        Dim juego As New Tile(titulo, id, "steam://rungameid/" + id, Nothing, imagenLogo, imagenAnchaReducida, imagenAncha, imagenGrande)

                                        listaJuegos.Add(juego)
                                    End If

                                    pbProgreso.Value = CInt((100 / listaFicheros.Count) * k)
                                    tbProgreso.Text = k.ToString + "/" + listaFicheros.Count.ToString
                                    k += 1
                                Next
                            End If
                        End If
                    Next
                End If
                h += 1
            End While
        ElseIf modo = 1 Then

            Dim listaIDs As New List(Of String)

            If Await helper.FileExistsAsync("juegosCuenta") = True Then
                listaIDs = Await helper.ReadFileAsync(Of List(Of String))("juegosCuenta")
            End If

            Dim k As Integer = 0
            If listaIDs.Count > 0 Then
                For Each id In listaIDs
                    Dim añadir As Boolean = True
                    Dim g As Integer = 0
                    While g < listaJuegos.Count
                        If listaJuegos(g).ID = id Then
                            añadir = False
                        End If
                        g += 1
                    End While

                    If añadir = True Then
                        Dim htmlAPI As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + id))

                        If Not htmlAPI = Nothing Then
                            Dim temp As String
                            Dim int As Integer

                            int = htmlAPI.IndexOf(":")
                            temp = htmlAPI.Remove(0, int + 1)
                            temp = temp.Remove(temp.Length - 1, 1)

                            Dim api As SteamAPI = JsonConvert.DeserializeObject(Of SteamAPI)(temp)

                            If Not api Is Nothing Then
                                If Not api.Datos Is Nothing Then
                                    Dim imagenLogo As String = String.Empty

                                    Try
                                        imagenLogo = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/logo.png", id, "logo")
                                    Catch ex As Exception

                                    End Try

                                    Dim imagenAnchaReducida As String = String.Empty

                                    Try
                                        imagenAnchaReducida = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/capsule_184x69.jpg", id, "ancha2")
                                    Catch ex As Exception

                                    End Try

                                    Dim imagenAncha As String = String.Empty

                                    Try
                                        imagenAncha = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/header.jpg", id, "ancha")
                                    Catch ex As Exception

                                    End Try

                                    Dim imagenGrande As String = String.Empty

                                    Try
                                        imagenGrande = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/library_600x900.jpg", id, "grande")
                                    Catch ex As Exception

                                    End Try

                                    If imagenGrande = String.Empty Then
                                        Try
                                            imagenGrande = Await Cache.DescargarImagen("https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/capsule_616x353.jpg", id, "grande")
                                        Catch ex As Exception

                                        End Try
                                    End If

                                    Dim juego As New Tile(api.Datos.Titulo, id, "steam://rungameid/" + id, Nothing, imagenLogo, imagenAnchaReducida, imagenAncha, imagenGrande)

                                    listaJuegos.Add(juego)
                                End If
                            End If
                        End If
                    End If

                    pbProgreso.Value = CInt((100 / listaIDs.Count) * k)
                    tbProgreso.Text = k.ToString + "/" + listaIDs.Count.ToString
                    k += 1
                Next
            End If
        End If

        Await helper.SaveFileAsync(Of List(Of Tile))("juegos" + modo.ToString, listaJuegos)

        spProgreso.Visibility = Visibility.Collapsed

        Dim gridTiles As Grid = pagina.FindName("gridTiles")
        Dim gridAvisoNoJuegos As Grid = pagina.FindName("gridAvisoNoJuegos")

        If Not listaJuegos Is Nothing Then
            If listaJuegos.Count > 0 Then
                gridTiles.Visibility = Visibility.Visible
                gridAvisoNoJuegos.Visibility = Visibility.Collapsed
                gridSeleccionarJuego.Visibility = Visibility.Visible

                listaJuegos.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                gv.Items.Clear()

                For Each juego In listaJuegos
                    BotonEstilo(juego, gv)
                Next

                If boolBuscarCarpeta = True Then
                    Toast(listaJuegos.Count.ToString + " " + recursos.GetString("GamesDetected"), Nothing)
                End If
            Else
                gridTiles.Visibility = Visibility.Collapsed
                gridAvisoNoJuegos.Visibility = Visibility.Visible
                gridSeleccionarJuego.Visibility = Visibility.Collapsed

                If boolBuscarCarpeta = True Then
                    Toast(recursos.GetString("ErrorSteam1"), Nothing)
                End If
            End If
        Else
            gridTiles.Visibility = Visibility.Collapsed
            gridAvisoNoJuegos.Visibility = Visibility.Visible
            gridSeleccionarJuego.Visibility = Visibility.Collapsed
        End If

        cbTiles.IsEnabled = True
        sp1.IsHitTestVisible = True
        sp2.IsHitTestVisible = True
        botonCache.IsEnabled = True

    End Sub

    Public Sub BotonEstilo(juego As Tile, gv As GridView)

        Dim panel As New DropShadowPanel With {
            .Margin = New Thickness(5, 5, 5, 5),
            .ShadowOpacity = 0.9,
            .BlurRadius = 5,
            .MaxWidth = anchoColumna + 10
        }

        Dim boton As New Button

        Dim imagen As New ImageEx With {
            .Source = juego.ImagenAncha,
            .IsCacheEnabled = True,
            .Stretch = Stretch.UniformToFill,
            .Padding = New Thickness(0, 0, 0, 0)
        }

        boton.Tag = juego
        boton.Content = imagen
        boton.Padding = New Thickness(0, 0, 0, 0)
        boton.Background = New SolidColorBrush(Colors.Transparent)

        panel.Content = boton

        Dim tbToolTip As TextBlock = New TextBlock With {
            .Text = juego.Titulo,
            .FontSize = 16,
            .TextWrapping = TextWrapping.Wrap
        }

        ToolTipService.SetToolTip(boton, tbToolTip)
        ToolTipService.SetPlacement(boton, PlacementMode.Mouse)

        AddHandler boton.Click, AddressOf BotonTile_Click
        AddHandler boton.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler boton.PointerExited, AddressOf UsuarioSaleBoton

        gv.Items.Add(panel)

    End Sub

    Private Async Sub BotonTile_Click(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonJuego As Button = e.OriginalSource
        Dim juego As Tile = botonJuego.Tag

        Dim botonAñadirTile As Button = pagina.FindName("botonAñadirTile")
        botonAñadirTile.Tag = juego

        Dim imagenJuegoSeleccionado As ImageEx = pagina.FindName("imagenJuegoSeleccionado")
        imagenJuegoSeleccionado.Source = juego.ImagenAnchaReducida

        Dim tbJuegoSeleccionado As TextBlock = pagina.FindName("tbJuegoSeleccionado")
        tbJuegoSeleccionado.Text = juego.Titulo

        Dim gridSeleccionarJuego As Grid = pagina.FindName("gridSeleccionarJuego")
        gridSeleccionarJuego.Visibility = Visibility.Collapsed

        Dim gvTiles As GridView = pagina.FindName("gvTiles")

        If gvTiles.ActualWidth > anchoColumna Then
            ApplicationData.Current.LocalSettings.Values("ancho_grid_tiles") = gvTiles.ActualWidth
        End If

        gvTiles.Width = anchoColumna
        gvTiles.Padding = New Thickness(0, 0, 15, 0)

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

        Dim imagenPequeña As ImageEx = pagina.FindName("imagenTilePequeña")
        imagenPequeña.Source = Nothing

        Try
            juego.ImagenIcono = Await Cache.DescargarImagen(Await SacarIcono(juego.ID), juego.ID, "icono")
        Catch ex As Exception

        End Try

        If Not juego.ImagenIcono = Nothing Then
            imagenPequeña.Source = juego.ImagenIcono
            imagenPequeña.Tag = juego.ImagenIcono
        End If

        Dim imagenMediana As ImageEx = pagina.FindName("imagenTileMediana")
        imagenMediana.Source = Nothing

        Dim imagenAncha As ImageEx = pagina.FindName("imagenTileAncha")
        imagenAncha.Source = Nothing

        If Not juego.ImagenAncha = Nothing Then
            If Not juego.ImagenLogo = Nothing Then
                imagenMediana.Source = juego.ImagenLogo
                imagenMediana.Tag = juego.ImagenLogo
            Else
                imagenMediana.Source = juego.ImagenAncha
                imagenMediana.Tag = juego.ImagenAncha
            End If

            imagenAncha.Source = juego.ImagenAncha
            imagenAncha.Tag = juego.ImagenAncha
        End If

        Dim imagenGrande As ImageEx = pagina.FindName("imagenTileGrande")
        imagenGrande.Source = Nothing

        If Not juego.ImagenGrande = Nothing Then
            imagenGrande.Source = juego.ImagenGrande
            imagenGrande.Tag = juego.ImagenGrande
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

        Dim gv As GridView = pagina.FindName("gvTiles")
        gv.Items.Clear()

        Generar(False)

    End Sub

    Public Async Function SacarIcono(id As String) As Task(Of String)

        Dim modo As Integer = ApplicationData.Current.LocalSettings.Values("modo_tiles")

        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("juegos" + modo.ToString) = True Then
            Dim listaJuegos As List(Of Tile) = Await helper.ReadFileAsync(Of List(Of Tile))("juegos" + modo.ToString)

            For Each juego In listaJuegos
                If id = juego.ID Then
                    If Not juego.ImagenIcono = Nothing Then
                        Return juego.ImagenIcono
                    End If
                End If
            Next
        End If

        Dim html As String = Await Decompiladores.HttpClient(New Uri("https://store.steampowered.com/app/" + id + "/"))
        Dim urlIcono As String = String.Empty

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

                urlIcono = temp2.Trim
            End If
        End If

        If urlIcono = Nothing Then
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

                    urlIcono = temp2.Trim
                End If
            End If
        End If

        If Not urlIcono = String.Empty Then
            If Await helper.FileExistsAsync("juegos" + modo.ToString) = True Then
                Dim listaJuegos As List(Of Tile) = Await helper.ReadFileAsync(Of List(Of Tile))("juegos" + modo.ToString)

                For Each juego In listaJuegos
                    If id = juego.ID Then
                        juego.ImagenIcono = Await Cache.DescargarImagen(urlIcono, id, "icono")
                    End If
                Next

                Await helper.SaveFileAsync(Of List(Of Tile))("juegos" + modo.ToString, listaJuegos)
            End If
        End If

        Return urlIcono
    End Function

    Public Async Sub Cuenta(cuenta As String)

        Dim helper As New LocalObjectStorageHelper

        Dim usuario As SteamCuenta = Nothing

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbTiles As ComboBox = pagina.FindName("cbConfigModosTiles")
        cbTiles.IsEnabled = False

        Dim sp1 As StackPanel = pagina.FindName("spModoTile1")
        sp1.IsHitTestVisible = False

        Dim sp2 As StackPanel = pagina.FindName("spModoTile2")
        sp2.IsHitTestVisible = False

        Dim botonCache As Button = pagina.FindName("botonConfigLimpiarCache")
        botonCache.IsEnabled = False

        Dim htmlID As String = Await Decompiladores.HttpClient(New Uri("https://api.steampowered.com/ISteamUser/ResolveVanityURL/v1/?key=41F2D73A0B5024E9101F8D4E8D8AC21E&vanityurl=" + cuenta))

        If Not htmlID = Nothing Then
            Dim id64 As String = Nothing

            If htmlID.Contains("steamid") Then
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = htmlID.IndexOf("steamid" + ChrW(34))
                temp = htmlID.Remove(0, int)

                int2 = temp.IndexOf(":")
                temp2 = temp.Remove(0, int2 + 1)

                int2 = temp2.IndexOf(ChrW(34))
                temp2 = temp2.Remove(0, int2 + 1)

                int2 = temp2.IndexOf(ChrW(34))
                temp2 = temp2.Remove(int2, temp2.Length - int2)

                id64 = temp2.Trim
            End If

            If id64 = Nothing Then
                id64 = cuenta
            End If

            If Not id64 = Nothing Then
                Dim htmlDatos As String = Await Decompiladores.HttpClient(New Uri("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=41F2D73A0B5024E9101F8D4E8D8AC21E&steamids=" + id64))

                Dim temp3, temp4 As String
                Dim int3, int4 As Integer

                If htmlDatos.Contains(ChrW(34) + "personaname" + ChrW(34)) Then
                    int3 = htmlDatos.IndexOf(ChrW(34) + "personaname" + ChrW(34))
                    temp3 = htmlDatos.Remove(0, int3)

                    int3 = temp3.IndexOf(":")
                    temp3 = temp3.Remove(0, int3 + 1)

                    int3 = temp3.IndexOf(ChrW(34))
                    temp3 = temp3.Remove(0, int3 + 1)

                    int4 = temp3.IndexOf(ChrW(34))
                    temp4 = temp3.Remove(int4, temp3.Length - int4)

                    Dim nombre As String = temp4.Trim

                    Dim temp5, temp6 As String
                    Dim int5, int6 As Integer

                    int5 = htmlDatos.IndexOf(ChrW(34) + "avatarfull" + ChrW(34))
                    temp5 = htmlDatos.Remove(0, int5)

                    int5 = temp5.IndexOf(":")
                    temp5 = temp5.Remove(0, int5 + 1)

                    int5 = temp5.IndexOf(ChrW(34))
                    temp5 = temp5.Remove(0, int5 + 1)

                    int6 = temp5.IndexOf(ChrW(34))
                    temp6 = temp5.Remove(int6, temp5.Length - int6)

                    Dim avatar As String = temp6.Trim

                    usuario = New SteamCuenta(id64, cuenta, nombre, avatar)
                End If
            End If
        End If

        Dim gridCuenta As Grid = pagina.FindName("gridConfigCuentaInfo")

        If Not usuario Is Nothing Then
            gridCuenta.Visibility = Visibility.Visible

            Dim imagenCuenta As ImageEx = pagina.FindName("imagenConfigCuentaInfo")
            imagenCuenta.Source = usuario.Avatar

            Dim tbCuenta As TextBlock = pagina.FindName("tbConfigCuentaInfo")
            tbCuenta.Text = usuario.Nombre

            ApplicationData.Current.LocalSettings.Values("cuenta_steam") = usuario.NombreUrl

            Dim htmlJuegos As String = Await Decompiladores.HttpClient(New Uri("https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key=41F2D73A0B5024E9101F8D4E8D8AC21E&steamid=" + usuario.ID64 + "&include_appinfo=1&include_played_free_games=1"))

            If Not htmlJuegos = Nothing Then
                If htmlJuegos.Contains("game_count") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = htmlJuegos.IndexOf("game_count")
                    temp = htmlJuegos.Remove(0, int)

                    int2 = temp.IndexOf(",")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Replace("game_count", Nothing)
                    temp2 = temp2.Replace(ChrW(34), Nothing)
                    temp2 = temp2.Replace(":", Nothing)
                    temp2 = temp2.Replace(vbNullChar, Nothing)
                    temp2 = temp2.Trim

                    If Not temp2 = Nothing Then
                        Dim listaIDs As New List(Of String)

                        Dim i As Integer = 0
                        While i < temp2
                            If htmlJuegos.Contains(ChrW(34) + "appid" + ChrW(34)) Then
                                Dim temp3, temp4 As String
                                Dim int3, int4 As Integer

                                int3 = htmlJuegos.IndexOf(ChrW(34) + "appid" + ChrW(34))
                                temp3 = htmlJuegos.Remove(0, int3 + 7)

                                htmlJuegos = temp3

                                int4 = temp3.IndexOf(",")
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                temp4 = temp4.Replace(":", Nothing)
                                temp4 = temp4.Trim

                                Dim id As String = temp4

                                Dim añadir As Boolean = True
                                Dim k As Integer = 0
                                While k < listaIDs.Count
                                    If listaIDs(k) = id Then
                                        añadir = False
                                    End If
                                    k += 1
                                End While

                                If añadir = True Then
                                    listaIDs.Add(id)
                                End If
                            End If
                            i += 1
                        End While

                        Await helper.SaveFileAsync(Of List(Of String))("juegosCuenta", listaIDs)

                        Steam.Generar(False)
                    End If
                End If
            End If
        Else
            gridCuenta.Visibility = Visibility.Collapsed
        End If

        cbTiles.IsEnabled = True
        sp1.IsHitTestVisible = True
        sp2.IsHitTestVisible = True
        botonCache.IsEnabled = True

    End Sub

End Module
