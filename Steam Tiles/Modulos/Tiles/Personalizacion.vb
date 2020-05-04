Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.UI
Imports Windows.UI.Core

Namespace Tiles
    Module Personalizacion
        Private frame As Frame = Window.Current.Content
        Private pagina As Page = frame.Content
        Private gridPersonalizar As Grid = pagina.FindName("gridPersonalizarTiles")
        Private gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")
        Private gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")
        Private botonVolver As Button = pagina.FindName("botonVolverPersonalizacion")
        Private tvPersonalizacion As TabView = pagina.FindName("tvPersonalizacion")
        Private botonImagenOrdenador As Button = pagina.FindName("botonPersonalizacionCambiarImagenOrdenador")
        Private tbImagenInternet As TextBox = pagina.FindName("tbPersonalizacionCambiarImagenInternet")
        Private cbImagenUbicacion As ComboBox = pagina.FindName("cbPersonalizacionImagenUbicacion")
        Private cbImagenEstiramiento As ComboBox = pagina.FindName("cbPersonalizacionImagenEstiramiento")
        Private sliderImagenMargen As Slider = pagina.FindName("sliderPersonalizacionImagenMargen")
        Private sliderImagenMoverX As Slider = pagina.FindName("sliderPersonalizacionImagenMoverX")
        Private cbImagenTitulo As CheckBox = pagina.FindName("cbPersonalizacionImagenTitulo")
        Private sliderImagenMoverY As Slider = pagina.FindName("sliderPersonalizacionImagenMoverY")
        Private colorFondo As ColorPicker = pagina.FindName("colorPickerPersonalizacionFondo")
        Private tviPlataforma As TabViewItem = pagina.FindName("tviPersonalizacionPlataforma")
        Private cbPlataforma As CheckBox = pagina.FindName("cbPersonalizacionPlataforma")
        Private spPlataforma As StackPanel = pagina.FindName("spPersonalizacionPlataforma")
        Private botonResetear As Button = pagina.FindName("botonResetearPersonalizacion")
        Private cbPlataformaPosicion As ComboBox = pagina.FindName("cbPersonalizacionPlataformaPosicion")
        Private gridAñadirTile As Grid = pagina.FindName("gridAñadirTile")
        Private sp As StackPanel = pagina.FindName("spPersonalizacionPlataforma")
        Private gvPlataformaIconos As AdaptiveGridView = pagina.FindName("gvPersonalizacionPlataformaIconos")
        Private gridCarga As Grid = pagina.FindName("gridPersonalizacionCarga")
        Private botonAñadirTile As Button = pagina.FindName("botonAñadirTile")
        Private botonTilePequeña As Button = pagina.FindName("botonTilePequeña")
        Private botonTileMediana As Button = pagina.FindName("botonTileMediana")
        Private botonTileAncha As Button = pagina.FindName("botonTileAncha")
        Private botonTileGrande As Button = pagina.FindName("botonTileGrande")
        Private botonCerrarTiles As Button = pagina.FindName("botonCerrarTiles")
        Private init As Boolean = True

        Private Sub Bind()
            AddHandler botonVolver.Click, AddressOf Volver
            AddHandler botonImagenOrdenador.Click, AddressOf CambioImagenOrdenador
            AddHandler tbImagenInternet.LostFocus, AddressOf CambioImagenInternet
            AddHandler cbImagenUbicacion.SelectionChanged, AddressOf CambiarImagenUbicacion
            AddHandler cbImagenEstiramiento.SelectionChanged, AddressOf CambiarImagenEstiramiento
            AddHandler sliderImagenMargen.ValueChanged, AddressOf CambiarImagenMargen
            AddHandler sliderImagenMoverX.ValueChanged, AddressOf CambiarImagenMover
            AddHandler sliderImagenMoverY.ValueChanged, AddressOf CambiarImagenMover
            AddHandler cbImagenTitulo.Checked, AddressOf CambiarImagenTitulo_Checked
            AddHandler cbImagenTitulo.Unchecked, AddressOf CambiarImagenTitulo_Unchecked
            AddHandler colorFondo.ColorChanged, AddressOf CambiarFondoColor
            AddHandler cbPlataforma.Checked, AddressOf EnseñarPlataforma_Checked
            AddHandler cbPlataforma.Unchecked, AddressOf EnseñarPlataforma_Unchecked
            AddHandler cbPlataformaPosicion.SelectionChanged, AddressOf CambiarPlataformaPosicion
            AddHandler botonResetear.Click, AddressOf Resetear
        End Sub

        ReadOnly listaIconos As New List(Of String) From {
            "1", "2", "3", "4", "5", "6", "7"
        }

        Public Async Sub Cargar(grid As Grid, tipo As Integer, fuente As String)

            BloquearControles(False)
            If init Then
                Bind()
                init = False
            End If


            Dim anchoExterior As Integer = 0
            Dim altoExterior As Integer = 0

            Dim anchoInterior As Integer = 0
            Dim altoInterior As Integer = 0

            If tipo = 0 Then
                anchoInterior = 71
                altoInterior = 71
            ElseIf tipo = 1 Then
                anchoInterior = 150
                altoInterior = 150
            ElseIf tipo = 2 Then
                anchoInterior = 310
                altoInterior = 150
            ElseIf tipo = 3 Then
                anchoInterior = 310
                altoInterior = 310
            End If

            anchoExterior = 2 * anchoInterior
            altoExterior = 2 * altoInterior

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
            botonVolver.Tag = tipo

            gridPersonalizar.Visibility = Visibility.Visible

            gridExterior.BorderThickness = New Thickness(1, 1, 1, 1)
            gridExterior.Width = anchoExterior
            gridExterior.Height = altoExterior

            For Each hijo In gridExterior.Children
                Dim imagen3 As ImageEx = Nothing
                Try
                    imagen3 = hijo
                Catch ex As Exception

                End Try
                If Not imagen3 Is Nothing Then
                    gridExterior.Children.Remove(hijo)
                End If
            Next

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

            tvPersonalizacion.SelectedIndex = 0

            tbImagenInternet.Text = String.Empty

            cbImagenUbicacion.SelectedIndex = 0

            Dim imagen2 As ImageEx = grid.Children(0)

            If imagen2.Stretch = Stretch.UniformToFill Then
                cbImagenEstiramiento.SelectedIndex = 0
            ElseIf imagen2.Stretch = Stretch.Uniform Then
                cbImagenEstiramiento.SelectedIndex = 1
            ElseIf imagen2.Stretch = Stretch.Fill Then
                cbImagenEstiramiento.SelectedIndex = 2
            ElseIf imagen2.Stretch = Stretch.None Then
                cbImagenEstiramiento.SelectedIndex = 3
            End If

            ApplicationData.Current.LocalSettings.Values("modo_estiramiento") = cbImagenEstiramiento.SelectedIndex

            sliderImagenMargen.Value = 0

            sliderImagenMoverX.Value = 0

            sliderImagenMoverY.Value = 0

            If tipo = 0 Or tipo = 1 Then
                cbImagenTitulo.Visibility = Visibility.Collapsed
            Else
                cbImagenTitulo.Visibility = Visibility.Visible
                cbImagenTitulo.IsChecked = False
                cbImagenTitulo.Tag = tipo

                If tipo = 2 Then
                    ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = False
                ElseIf tipo = 3 Then
                    ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") = False
                End If
            End If

            '-------------------------------------------

            colorFondo.Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor(Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToHex(App.Current.Resources("ColorTerciario")))

            gridExterior.Background = New SolidColorBrush(colorFondo.Color)

            '-------------------------------------------

            If tipo = 0 Or tipo = 1 Then
                tviPlataforma.Visibility = Visibility.Collapsed
            ElseIf tipo = 2 Or tipo = 3 Then
                tviPlataforma.Visibility = Visibility.Visible

                If cbPlataforma.IsChecked = True Then
                    spPlataforma.Visibility = Visibility.Visible
                Else
                    spPlataforma.Visibility = Visibility.Collapsed
                End If

                cbPlataformaPosicion.SelectedIndex = 1

            End If

            '-------------------------------------------

            botonResetear.Tag = fuente

            BloquearControles(True)

        End Sub

        Private Async Sub Volver(sender As Object, e As RoutedEventArgs)

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

            gridInterior.BorderThickness = New Thickness(0, 0, 0, 0)

            If gridInterior.Children.Count > 0 Then
                Try
                    Await Tiles.Imagen.Generar(gridInterior, "personalizacion" + id.ToString + ".png", ancho, alto)
                Catch ex As Exception

                End Try
            End If

            gridExterior.BorderThickness = New Thickness(0, 0, 0, 0)

            If gridExterior.Children.Count > 0 Then
                Try
                    Await Tiles.Imagen.Generar(gridExterior, "personalizacion" + id.ToString + ".png", ancho, alto)
                Catch ex As Exception

                End Try
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


            gridAñadirTile.Visibility = Visibility.Visible

            gridPersonalizar.Visibility = Visibility.Collapsed

        End Sub

        Private Async Sub CambioImagenOrdenador(sender As Object, e As RoutedEventArgs)

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

        Private Sub CambioImagenInternet(sender As Object, e As RoutedEventArgs)

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
            Try

                gridExterior.Children.RemoveAt(1)
            Catch ex As Exception

            End Try
            gridInterior.Children.Clear()

            Dim imagen As New ImageEx With {
                .IsCacheEnabled = True,
                .Source = bitmap,
                .Stretch = Stretch.UniformToFill
            }

            If cbImagenUbicacion.SelectedIndex = 0 Then
                gridInterior.Children.Add(imagen)
            ElseIf cbImagenUbicacion.SelectedIndex = 1 Then
                gridExterior.Children.Add(imagen)
            End If

            If imagen.Stretch = Stretch.UniformToFill Then
                cbImagenEstiramiento.SelectedIndex = 0
            ElseIf imagen.Stretch = Stretch.Uniform Then
                cbImagenEstiramiento.SelectedIndex = 1
            ElseIf imagen.Stretch = Stretch.Fill Then
                cbImagenEstiramiento.SelectedIndex = 2
            ElseIf imagen.Stretch = Stretch.None Then
                cbImagenEstiramiento.SelectedIndex = 3
            End If

        End Sub

        Private Sub CambiarImagenUbicacion(sender As Object, e As SelectionChangedEventArgs)
            Dim cb As ComboBox = sender

            Dim imagen As New ImageEx

            Dim _from As Grid = gridExterior
            Dim _to As Grid = gridInterior
            Dim index As Integer = 1

            If cb.SelectedIndex = 1 Then
                _from = gridInterior
                _to = gridExterior
                index = 0
                _from.Background = Nothing
            End If

            Try
                imagen = _from.Children(index)
                _from.Children.Remove(imagen)
                _to.Children.Add(imagen)

            Catch ex As Exception

            End Try

        End Sub

        Private Sub CambiarImagenEstiramiento(sender As Object, e As SelectionChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = sender

            Dim imagen As New ImageEx

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

        Private Sub CambiarImagenMargen(sender As Object, e As RangeBaseValueChangedEventArgs)

            Dim slider As Slider = sender

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    imagen2.Margin = New Thickness(slider.Value, slider.Value, slider.Value, slider.Value)
                End If
            Next

            For Each hijo In gridInterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    imagen2.Margin = New Thickness(slider.Value, slider.Value, slider.Value, slider.Value)
                End If
            Next

        End Sub

        Private Sub CambiarImagenMover(sender As Object, e As RangeBaseValueChangedEventArgs)

            Dim mover As New TranslateTransform With {
                .X = sliderImagenMoverX.Value,
                .Y = sliderImagenMoverY.Value
            }

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    imagen2.RenderTransform = mover
                End If
            Next

            For Each hijo In gridInterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    imagen2.RenderTransform = mover
                End If
            Next

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

            Dim colorFondo As ColorPicker = sender

            gridInterior.Background = New SolidColorBrush(colorFondo.Color)

            gridExterior.Background = New SolidColorBrush(colorFondo.Color)

        End Sub

        Private Sub EnseñarPlataforma_Checked(sender As Object, e As RoutedEventArgs)

            sp.Visibility = Visibility.Visible

            gvPlataformaIconos.Items.Clear()

            For Each icono In listaIconos
                Dim botonIcono As New Button With {
                    .Background = New SolidColorBrush(Colors.Transparent),
                    .Padding = New Thickness(0, 0, 0, 0)
                }

                Dim imagenIcono As New ImageEx With {
                    .Source = "ms-appx:///Assets/Iconos/" + icono + ".png",
                    .IsCacheEnabled = True,
                    .Stretch = Stretch.Uniform,
                    .MaxHeight = 32,
                    .MaxWidth = 32,
                    .Margin = New Thickness(5, 5, 5, 5)
                }

                botonIcono.Content = imagenIcono

                AddHandler botonIcono.Click, AddressOf UsuarioClickeaIcono
                AddHandler botonIcono.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonIcono.PointerExited, AddressOf UsuarioSaleBoton

                gvPlataformaIconos.Items.Add(botonIcono)
            Next

        End Sub

        Private Sub EnseñarPlataforma_Unchecked(sender As Object, e As RoutedEventArgs)

            sp.Visibility = Visibility.Collapsed

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    If imagen2.Name = "iconoPlataforma" Then
                        gridExterior.Children.Remove(hijo)
                    End If
                End If
            Next

            For Each hijo In gridInterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    If imagen2.Name = "iconoPlataforma" Then
                        gridInterior.Children.Remove(hijo)
                    End If
                End If
            Next

        End Sub

        Private Sub UsuarioClickeaIcono(sender As Object, e As RoutedEventArgs)

            Dim botonIcono As Button = sender
            Dim imagenIcono As ImageEx = botonIcono.Content

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    If imagen2.Name = "iconoPlataforma" Then
                        imagen2.Opacity = 0
                        gridExterior.Children.Remove(hijo)
                    End If

                    Dim imagen3 As New ImageEx With {
                        .Source = imagenIcono.Source,
                        .MaxHeight = 24,
                        .MaxWidth = 72,
                        .Name = "iconoPlataforma"
                    }

                    imagen3.SetValue(Canvas.ZIndexProperty, 99991)

                    If cbPlataformaPosicion.SelectedIndex = 0 Then
                        imagen3.HorizontalAlignment = HorizontalAlignment.Left
                        imagen3.VerticalAlignment = VerticalAlignment.Top
                    ElseIf cbPlataformaPosicion.SelectedIndex = 1 Then
                        imagen3.HorizontalAlignment = HorizontalAlignment.Right
                        imagen3.VerticalAlignment = VerticalAlignment.Top
                    ElseIf cbPlataformaPosicion.SelectedIndex = 2 Then
                        imagen3.HorizontalAlignment = HorizontalAlignment.Left
                        imagen3.VerticalAlignment = VerticalAlignment.Bottom
                    ElseIf cbPlataformaPosicion.SelectedIndex = 3 Then
                        imagen3.HorizontalAlignment = HorizontalAlignment.Right
                        imagen3.VerticalAlignment = VerticalAlignment.Bottom
                    End If

                    gridExterior.Children.Add(imagen3)
                End If
            Next

            For Each hijo In gridInterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    If imagen2.Name = "iconoPlataforma" Then
                        imagen2.Opacity = 0
                        gridInterior.Children.Remove(hijo)
                    End If

                    Dim imagen3 As New ImageEx With {
                        .Source = imagenIcono.Source,
                        .MaxHeight = 24,
                        .MaxWidth = 72,
                        .Name = "iconoPlataforma",
                        .Margin = New Thickness(5, 5, 5, 5)
                    }

                    imagen3.SetValue(Canvas.ZIndexProperty, 99991)

                    If cbPlataformaPosicion.SelectedIndex = 0 Then
                        imagen3.HorizontalAlignment = HorizontalAlignment.Left
                        imagen3.VerticalAlignment = VerticalAlignment.Top
                    ElseIf cbPlataformaPosicion.SelectedIndex = 1 Then
                        imagen3.HorizontalAlignment = HorizontalAlignment.Right
                        imagen3.VerticalAlignment = VerticalAlignment.Top
                    ElseIf cbPlataformaPosicion.SelectedIndex = 2 Then
                        imagen3.HorizontalAlignment = HorizontalAlignment.Left
                        imagen3.VerticalAlignment = VerticalAlignment.Bottom
                    ElseIf cbPlataformaPosicion.SelectedIndex = 3 Then
                        imagen3.HorizontalAlignment = HorizontalAlignment.Right
                        imagen3.VerticalAlignment = VerticalAlignment.Bottom
                    End If

                    gridInterior.Children.Add(imagen3)
                End If
            Next

        End Sub

        Private Sub CambiarPlataformaPosicion(sender As Object, e As SelectionChangedEventArgs)

            Dim cbPosicion As ComboBox = sender

            For Each hijo In gridExterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    If imagen2.Name = "iconoPlataforma" Then
                        If cbPosicion.SelectedIndex = 0 Then
                            imagen2.HorizontalAlignment = HorizontalAlignment.Left
                            imagen2.VerticalAlignment = VerticalAlignment.Top
                        ElseIf cbPosicion.SelectedIndex = 1 Then
                            imagen2.HorizontalAlignment = HorizontalAlignment.Right
                            imagen2.VerticalAlignment = VerticalAlignment.Top
                        ElseIf cbPosicion.SelectedIndex = 2 Then
                            imagen2.HorizontalAlignment = HorizontalAlignment.Left
                            imagen2.VerticalAlignment = VerticalAlignment.Bottom
                        ElseIf cbPosicion.SelectedIndex = 3 Then
                            imagen2.HorizontalAlignment = HorizontalAlignment.Right
                            imagen2.VerticalAlignment = VerticalAlignment.Bottom
                        End If
                    End If
                End If
            Next

            For Each hijo In gridInterior.Children
                Dim imagen2 As ImageEx = Nothing

                Try
                    imagen2 = hijo
                Catch ex As Exception

                End Try

                If Not imagen2 Is Nothing Then
                    If imagen2.Name = "iconoPlataforma" Then
                        If cbPosicion.SelectedIndex = 0 Then
                            imagen2.HorizontalAlignment = HorizontalAlignment.Left
                            imagen2.VerticalAlignment = VerticalAlignment.Top
                        ElseIf cbPosicion.SelectedIndex = 1 Then
                            imagen2.HorizontalAlignment = HorizontalAlignment.Right
                            imagen2.VerticalAlignment = VerticalAlignment.Top
                        ElseIf cbPosicion.SelectedIndex = 2 Then
                            imagen2.HorizontalAlignment = HorizontalAlignment.Left
                            imagen2.VerticalAlignment = VerticalAlignment.Bottom
                        ElseIf cbPosicion.SelectedIndex = 3 Then
                            imagen2.HorizontalAlignment = HorizontalAlignment.Right
                            imagen2.VerticalAlignment = VerticalAlignment.Bottom
                        End If
                    End If
                End If
            Next

        End Sub

        Private Sub Resetear(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender

            Dim original As String = boton.Tag

            Dim imagen As New ImageEx With {
                .IsCacheEnabled = True,
                .Source = original
            }

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

            gridInterior.Children.Add(imagen)

            If Not cbImagenUbicacion Is Nothing Then
                cbImagenUbicacion.SelectedIndex = 0
            End If

            If Not ApplicationData.Current.LocalSettings.Values("modo_estiramiento") = Nothing Then
                cbImagenEstiramiento.SelectedIndex = ApplicationData.Current.LocalSettings.Values("modo_estiramiento")
            Else
                cbImagenEstiramiento.SelectedIndex = 0
            End If

            If Not sliderImagenMargen Is Nothing Then
                sliderImagenMargen.Minimum = 0
            End If

            If Not sliderImagenMoverX Is Nothing Then
                sliderImagenMoverX.Value = 0
            End If

            If Not sliderImagenMoverY Is Nothing Then
                sliderImagenMoverY.Value = 0
            End If

        End Sub

        Private Sub BloquearControles(estado As Boolean)


            If estado = False Then
                gridCarga.Visibility = Visibility.Visible
            Else
                gridCarga.Visibility = Visibility.Collapsed
            End If

            botonAñadirTile.IsEnabled = estado
            botonCerrarTiles.IsEnabled = estado
            botonTilePequeña.IsEnabled = estado
            botonTileMediana.IsEnabled = estado
            botonTileAncha.IsEnabled = estado
            botonTileGrande.IsEnabled = estado

        End Sub

        Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

    End Module
End Namespace

