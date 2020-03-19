Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams

Namespace Tiles
    Module Personalizacion

        Public Async Sub Cargar(grid As Grid, tipo As Integer, fuente As String)

            BloquearControles(False)

            Dim anchoExterior As Integer = 0
            Dim altoExterior As Integer = 0

            Dim anchoInterior As Integer = 0
            Dim altoInterior As Integer = 0

            If tipo = 0 Then
                anchoExterior = 142
                altoExterior = 142

                anchoInterior = 71
                altoInterior = 71
            ElseIf tipo = 1 Then
                anchoExterior = 300
                altoExterior = 300

                anchoInterior = 150
                altoInterior = 150
            ElseIf tipo = 2 Then
                anchoExterior = 620
                altoExterior = 300

                anchoInterior = 310
                altoInterior = 150
            ElseIf tipo = 3 Then
                anchoExterior = 620
                altoExterior = 620

                anchoInterior = 310
                altoInterior = 310
            End If

            Dim carpeta As StorageFolder = ApplicationData.Current.LocalFolder
            Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync()

            For Each fichero In ficheros
                If fichero.Name.Contains("personalizacion") Then
                    Try
                        Await fichero.DeleteAsync()
                    Catch ex As Exception

                    End Try
                End If
            Next

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonVolver As Button = pagina.FindName("botonVolverPersonalizacion")

            If Not botonVolver Is Nothing Then
                botonVolver.Tag = tipo

                RemoveHandler botonVolver.Click, AddressOf Volver
                AddHandler botonVolver.Click, AddressOf Volver
            End If

            Dim gridPersonalizar As Grid = pagina.FindName("gridPersonalizarTiles")
            gridPersonalizar.Visibility = Visibility.Visible

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")
            gridExterior.BorderThickness = New Thickness(1, 1, 1, 1)
            gridExterior.Width = anchoExterior
            gridExterior.Height = altoExterior

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    gridExterior.Children.Remove(hijo)
                End If
            Next

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")
            gridInterior.BorderThickness = New Thickness(1, 1, 1, 1)
            gridInterior.SetValue(Canvas.ZIndexProperty, 99990)
            gridInterior.Width = anchoInterior
            gridInterior.Height = altoInterior

            Dim imagen As New ImageEx With {
                .Source = fuente,
                .IsCacheEnabled = True
            }

            gridInterior.Children.Clear()
            gridInterior.Children.Add(imagen)

            '-------------------------------------------

            Dim botonImagenOrdenador As Button = pagina.FindName("botonPersonalizacionCambiarImagenOrdenador")

            If Not botonImagenOrdenador Is Nothing Then
                RemoveHandler botonImagenOrdenador.Click, AddressOf CambioImagenOrdenador
                AddHandler botonImagenOrdenador.Click, AddressOf CambioImagenOrdenador
            End If

            Dim tbImagenInternet As TextBox = pagina.FindName("tbPersonalizacionCambiarImagenInternet")

            If Not tbImagenInternet Is Nothing Then
                tbImagenInternet.Text = String.Empty

                RemoveHandler tbImagenInternet.TextChanged, AddressOf CambioImagenInternet
                AddHandler tbImagenInternet.TextChanged, AddressOf CambioImagenInternet
            End If

            Dim cbImagenUbicacion As ComboBox = pagina.FindName("cbPersonalizacionImagenUbicacion")

            If Not cbImagenUbicacion Is Nothing Then
                RemoveHandler cbImagenUbicacion.SelectionChanged, AddressOf CambiarImagenUbicacion
                AddHandler cbImagenUbicacion.SelectionChanged, AddressOf CambiarImagenUbicacion

                cbImagenUbicacion.SelectedIndex = 0
            End If

            Dim cbImagenEstiramiento As ComboBox = pagina.FindName("cbPersonalizacionImagenEstiramiento")

            If Not cbImagenEstiramiento Is Nothing Then
                RemoveHandler cbImagenEstiramiento.SelectionChanged, AddressOf CambiarImagenEstiramiento
                AddHandler cbImagenEstiramiento.SelectionChanged, AddressOf CambiarImagenEstiramiento

                cbImagenEstiramiento.SelectedIndex = 0
            End If

            Dim tbImagenMargen As TextBox = pagina.FindName("tbPersonalizacionImagenMargen")

            If Not tbImagenMargen Is Nothing Then
                tbImagenMargen.Text = 0

                RemoveHandler tbImagenMargen.TextChanged, AddressOf CambiarImagenMargen
                AddHandler tbImagenMargen.TextChanged, AddressOf CambiarImagenMargen

                RemoveHandler tbImagenMargen.BeforeTextChanging, AddressOf CambiarImagenMargenControl
                AddHandler tbImagenMargen.BeforeTextChanging, AddressOf CambiarImagenMargenControl
            End If

            Dim cbImagenTitulo As CheckBox = pagina.FindName("cbPersonalizacionImagenTitulo")

            If Not cbImagenTitulo Is Nothing Then
                If tipo = 0 Or tipo = 1 Then
                    cbImagenTitulo.Visibility = Visibility.Collapsed
                Else
                    cbImagenTitulo.Visibility = Visibility.Visible
                    cbImagenTitulo.IsChecked = False
                    cbImagenTitulo.Tag = tipo

                    RemoveHandler cbImagenTitulo.Checked, AddressOf CambiarImagenTitulo_Checked
                    AddHandler cbImagenTitulo.Checked, AddressOf CambiarImagenTitulo_Checked

                    RemoveHandler cbImagenTitulo.Unchecked, AddressOf CambiarImagenTitulo_Unchecked
                    AddHandler cbImagenTitulo.Unchecked, AddressOf CambiarImagenTitulo_Unchecked

                    If tipo = 2 Then
                        ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = False
                    ElseIf tipo = 3 Then
                        ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") = False
                    End If
                End If
            End If

            Dim colorFondo As ColorPicker = pagina.FindName("colorPickerPersonalizacionFondo")

            If Not colorFondo Is Nothing Then
                colorFondo.Color = ColorHelper.ToColor(ColorHelper.ToHex(App.Current.Resources("ColorTerciario")))

                RemoveHandler colorFondo.ColorChanged, AddressOf CambiarFondoColor
                AddHandler colorFondo.ColorChanged, AddressOf CambiarFondoColor

                gridExterior.Background = New SolidColorBrush(colorFondo.Color)
            End If

            '-------------------------------------------

            Dim botonResetear As Button = pagina.FindName("botonResetearPersonalizacion")

            If Not botonResetear Is Nothing Then
                botonResetear.Tag = fuente

                RemoveHandler botonResetear.Click, AddressOf Resetear
                AddHandler botonResetear.Click, AddressOf Resetear
            End If

            BloquearControles(True)

        End Sub

        Private Async Sub Volver(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tipo As Integer = 0

            Dim botonVolver As Button = sender
            tipo = botonVolver.Tag

            Dim ancho As Integer = 0
            Dim alto As Integer = 0

            If tipo = 0 Then
                ancho = 71
                alto = 71
            ElseIf tipo = 1 Then
                ancho = 150
                alto = 150
            ElseIf tipo = 2 Then
                ancho = 310
                alto = 150
            ElseIf tipo = 3 Then
                ancho = 310
                alto = 310
            End If

            Dim id As Integer = 0

            Dim carpeta As StorageFolder = ApplicationData.Current.LocalFolder
            Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync()

            For Each fichero In ficheros
                id = id + 1
            Next

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")
            gridInterior.BorderThickness = New Thickness(0, 0, 0, 0)

            If gridInterior.Children.Count > 0 Then
                Await Tiles.Imagen.Generar(gridInterior, "personalizacion" + id.ToString + ".png", ancho, alto)
            End If

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")
            gridExterior.BorderThickness = New Thickness(0, 0, 0, 0)

            If gridExterior.Children.Count > 0 Then
                Await Tiles.Imagen.Generar(gridExterior, "personalizacion" + id.ToString + ".png", ancho, alto)
            End If

            Dim imagen As New ImageEx With {
                .Source = "ms-appdata:///local/personalizacion" + id.ToString + ".png",
                .IsCacheEnabled = True
            }

            If tipo = 0 Then
                Dim imagenPequeña As ImageEx = pagina.FindName("imagenTilePequeña")
                imagenPequeña.Source = "ms-appdata:///local/personalizacion" + id.ToString + ".png"
            ElseIf tipo = 1 Then
                Dim imagenMediana As ImageEx = pagina.FindName("imagenTileMediana")
                imagenMediana.Source = "ms-appdata:///local/personalizacion" + id.ToString + ".png"
            ElseIf tipo = 2 Then
                Dim imagenAncha As ImageEx = pagina.FindName("imagenTileAncha")
                imagenAncha.Source = "ms-appdata:///local/personalizacion" + id.ToString + ".png"
            ElseIf tipo = 3 Then
                Dim imagenGrande As ImageEx = pagina.FindName("imagenTileGrande")
                imagenGrande.Source = "ms-appdata:///local/personalizacion" + id.ToString + ".png"
            End If

            Dim gridAñadirTile As Grid = pagina.FindName("gridAñadirTile")
            gridAñadirTile.Visibility = Visibility.Visible





            Dim gridPersonalizarTiles As Grid = pagina.FindName("gridPersonalizarTiles")
            gridPersonalizarTiles.Visibility = Visibility.Collapsed

        End Sub

        Private Async Sub CambioImagenOrdenador(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonImagenOrdenador As Button = pagina.FindName("botonPersonalizacionCambiarImagenOrdenador")
            botonImagenOrdenador.IsEnabled = False

            Dim ficheroPicker As New FileOpenPicker
            ficheroPicker.FileTypeFilter.Add(".png")
            ficheroPicker.FileTypeFilter.Add(".jpg")
            ficheroPicker.FileTypeFilter.Add(".jpeg")
            ficheroPicker.ViewMode = PickerViewMode.List

            Dim ficheroImagen As StorageFile = Await ficheroPicker.PickSingleFileAsync

            Dim bitmap As New BitmapImage

            Try
                Dim stream As FileRandomAccessStream = Await ficheroImagen.OpenAsync(FileAccessMode.Read)
                bitmap.SetSource(stream)
            Catch ex As Exception

            End Try

            CambioImagenIncrustar(bitmap)

            botonImagenOrdenador.IsEnabled = True

        End Sub

        Private Sub CambioImagenInternet(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbImagenInternet As TextBox = pagina.FindName("tbPersonalizacionCambiarImagenInternet")
            tbImagenInternet.IsEnabled = False

            If tbImagenInternet.Text.Trim.Length > 0 Then
                If tbImagenInternet.Text.Trim.Contains("http://") Or tbImagenInternet.Text.Trim.Contains("https://") Then
                    Try
                        CambioImagenIncrustar(tbImagenInternet.Text.Trim)
                    Catch ex As Exception

                    End Try
                End If
            End If

            tbImagenInternet.IsEnabled = True

        End Sub

        Private Sub CambioImagenIncrustar(bitmap As Object)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    gridExterior.Children.Remove(hijo)
                End If
            Next

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

            Dim imagen As New ImageEx With {
                .IsCacheEnabled = True,
                .Source = bitmap,
                .Stretch = Stretch.UniformToFill
            }

            Dim cbImagenUbicacion As ComboBox = pagina.FindName("cbPersonalizacionImagenUbicacion")

            If cbImagenUbicacion.SelectedIndex = 0 Then
                gridInterior.Children.Add(imagen)
            ElseIf cbImagenUbicacion.SelectedIndex = 1 Then
                gridInterior.Children.Clear()
                gridExterior.Children.Add(imagen)
            End If

        End Sub

        Private Sub CambiarImagenUbicacion(sender As Object, e As SelectionChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = sender

            Dim imagen As New ImageEx

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    imagen.Source = imagen2.Source
                    imagen.IsCacheEnabled = imagen2.IsCacheEnabled
                    imagen.Stretch = imagen2.Stretch

                    gridExterior.Children.Remove(hijo)
                End If
            Next

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

            For Each hijo In gridInterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    imagen.Source = imagen2.Source
                    imagen.IsCacheEnabled = imagen2.IsCacheEnabled
                    imagen.Stretch = imagen2.Stretch

                    gridInterior.Children.Remove(hijo)
                End If
            Next

            If cb.SelectedIndex = 0 Then
                gridInterior.Children.Add(imagen)
            ElseIf cb.SelectedIndex = 1 Then
                gridExterior.Children.Add(imagen)
            End If

        End Sub

        Private Sub CambiarImagenEstiramiento(sender As Object, e As SelectionChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = sender

            Dim imagen As New ImageEx

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    imagen.Source = imagen2.Source
                    imagen.IsCacheEnabled = imagen2.IsCacheEnabled

                    If cb.SelectedIndex = 0 Then
                        imagen.Stretch = Stretch.UniformToFill
                    ElseIf cb.SelectedIndex = 1 Then
                        imagen.Stretch = Stretch.Uniform
                    ElseIf cb.SelectedIndex = 2 Then
                        imagen.Stretch = Stretch.Fill
                    ElseIf cb.SelectedIndex = 3 Then
                        imagen.Stretch = Stretch.None
                    End If

                    gridExterior.Children.Remove(hijo)
                    gridExterior.Children.Add(imagen)
                End If
            Next

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

            For Each hijo In gridInterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    imagen.Source = imagen2.Source
                    imagen.IsCacheEnabled = imagen2.IsCacheEnabled

                    If cb.SelectedIndex = 0 Then
                        imagen.Stretch = Stretch.UniformToFill
                    ElseIf cb.SelectedIndex = 1 Then
                        imagen.Stretch = Stretch.Uniform
                    ElseIf cb.SelectedIndex = 2 Then
                        imagen.Stretch = Stretch.Fill
                    ElseIf cb.SelectedIndex = 3 Then
                        imagen.Stretch = Stretch.None
                    End If

                    gridInterior.Children.Remove(hijo)
                    gridInterior.Children.Add(imagen)
                End If
            Next

        End Sub

        Private Sub CambiarImagenMargen(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBox = sender

            If tb.Text.Trim.Length > 0 Then
                Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

                For Each hijo In gridExterior.Children
                    Dim imagen2 As ImageEx = Nothing

                    Try
                        imagen2 = hijo
                    Catch ex As Exception

                    End Try

                    If Not imagen2 Is Nothing Then
                        imagen2.Margin = New Thickness(tb.Text.Trim, tb.Text.Trim, tb.Text.Trim, tb.Text.Trim)
                    End If
                Next

                Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

                For Each hijo In gridInterior.Children
                    Dim imagen2 As ImageEx = Nothing

                    Try
                        imagen2 = hijo
                    Catch ex As Exception

                    End Try

                    If Not imagen2 Is Nothing Then
                        imagen2.Margin = New Thickness(tb.Text.Trim, tb.Text.Trim, tb.Text.Trim, tb.Text.Trim)
                    End If
                Next
            End If

        End Sub

        Private Sub CambiarImagenMargenControl(sender As Object, e As TextBoxBeforeTextChangingEventArgs)

            If Char.IsDigit(e.NewText) = False Then
                e.Cancel = True
            Else
                Dim i As Integer = e.NewText

                If i < 0 Or i > 99 Then
                    e.Cancel = True
                End If
            End If

        End Sub

        Private Sub CambiarImagenTitulo_Checked(sender As Object, e As RoutedEventArgs)

            Dim cb As CheckBox = sender
            Dim tipo As Integer = cb.Tag

            If tipo = 2 Then
                ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = True
            ElseIf tipo = 3 Then
                ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") = True
            End If

        End Sub

        Private Sub CambiarImagenTitulo_Unchecked(sender As Object, e As RoutedEventArgs)

            Dim cb As CheckBox = sender
            Dim tipo As Integer = cb.Tag

            If tipo = 2 Then
                ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = False
            ElseIf tipo = 3 Then
                ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") = False
            End If

        End Sub

        Private Sub CambiarFondoColor(sender As Object, args As ColorChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim colorFondo As ColorPicker = sender

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")
            gridInterior.Background = New SolidColorBrush(colorFondo.Color)

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")
            gridExterior.Background = New SolidColorBrush(colorFondo.Color)

        End Sub

        Private Sub Resetear(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim boton As Button = sender

            Dim gridOriginal As Grid = boton.Tag

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    gridExterior.Children.Remove(hijo)
                End If
            Next

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

            For Each hijo In gridInterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    gridInterior.Children.Remove(hijo)
                End If
            Next

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridCarga As Grid = pagina.FindName("gridPersonalizacionCarga")

            If estado = False Then
                gridCarga.Visibility = Visibility.Visible
            Else
                gridCarga.Visibility = Visibility.Collapsed
            End If

            Dim botonAñadirTile As Button = pagina.FindName("botonAñadirTile")
            botonAñadirTile.IsEnabled = estado

            Dim botonCerrarTiles As Button = pagina.FindName("botonCerrarTiles")
            botonCerrarTiles.IsEnabled = estado

            Dim botonTilePequeña As Button = pagina.FindName("botonTilePequeña")
            botonTilePequeña.IsEnabled = estado

            Dim botonTileMediana As Button = pagina.FindName("botonTileMediana")
            botonTileMediana.IsEnabled = estado

            Dim botonTileAncha As Button = pagina.FindName("botonTileAncha")
            botonTileAncha.IsEnabled = estado

            Dim botonTileGrande As Button = pagina.FindName("botonTileGrande")
            botonTileGrande.IsEnabled = estado

        End Sub

    End Module
End Namespace

