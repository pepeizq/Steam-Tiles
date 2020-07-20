Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.UI
Imports Windows.UI.Core

Namespace Tiles
    Module Personalizacion

        ReadOnly listaIconos As New List(Of String) From {
            "1", "2", "3", "4", "5", "6", "7"
        }

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

            '-------------------------------------------

            Dim tvPersonalizacion As TabView = pagina.FindName("tvPersonalizacion")
            tvPersonalizacion.SelectedIndex = 0

            Dim botonImagenOrdenador As Button = pagina.FindName("botonPersonalizacionCambiarImagenOrdenador")

            If Not botonImagenOrdenador Is Nothing Then
                RemoveHandler botonImagenOrdenador.Click, AddressOf CambioImagenOrdenador
                AddHandler botonImagenOrdenador.Click, AddressOf CambioImagenOrdenador
            End If

            Dim tbImagenInternet As TextBox = pagina.FindName("tbPersonalizacionCambiarImagenInternet")

            If Not tbImagenInternet Is Nothing Then
                RemoveHandler tbImagenInternet.TextChanged, AddressOf CambioImagenInternet
                AddHandler tbImagenInternet.TextChanged, AddressOf CambioImagenInternet
            End If

            Dim cbImagenUbicacion As ComboBox = pagina.FindName("cbPersonalizacionImagenUbicacion")

            If Not cbImagenUbicacion Is Nothing Then
                RemoveHandler cbImagenUbicacion.SelectionChanged, AddressOf CambiarImagenUbicacion
                AddHandler cbImagenUbicacion.SelectionChanged, AddressOf CambiarImagenUbicacion
            End If

            Dim cbImagenEstiramiento As ComboBox = pagina.FindName("cbPersonalizacionImagenEstiramiento")

            If Not cbImagenEstiramiento Is Nothing Then
                RemoveHandler cbImagenEstiramiento.SelectionChanged, AddressOf CambiarImagenEstiramiento
                AddHandler cbImagenEstiramiento.SelectionChanged, AddressOf CambiarImagenEstiramiento

                Dim imagen2 As ImageEx = grid.Children(0)

                If Not imagen2 Is Nothing Then
                    If imagen2.Stretch = Stretch.UniformToFill Then
                        cbImagenEstiramiento.SelectedIndex = 0
                    ElseIf imagen2.Stretch = Stretch.Uniform Then
                        cbImagenEstiramiento.SelectedIndex = 1
                    ElseIf imagen2.Stretch = Stretch.Fill Then
                        cbImagenEstiramiento.SelectedIndex = 2
                    ElseIf imagen2.Stretch = Stretch.None Then
                        cbImagenEstiramiento.SelectedIndex = 3
                    End If
                Else
                    cbImagenEstiramiento.SelectedIndex = 0
                End If

                ApplicationData.Current.LocalSettings.Values("modo_estiramiento") = cbImagenEstiramiento.SelectedIndex
            End If

            Dim sliderImagenMargen As Slider = pagina.FindName("sliderPersonalizacionImagenMargen")

            If Not sliderImagenMargen Is Nothing Then
                RemoveHandler sliderImagenMargen.ValueChanged, AddressOf CambiarImagenMargen
                AddHandler sliderImagenMargen.ValueChanged, AddressOf CambiarImagenMargen
            End If

            Dim cbImagenTitulo As CheckBox = pagina.FindName("cbPersonalizacionImagenTitulo")

            If Not cbImagenTitulo Is Nothing Then
                RemoveHandler cbImagenTitulo.Checked, AddressOf CambiarImagenTitulo_Checked
                AddHandler cbImagenTitulo.Checked, AddressOf CambiarImagenTitulo_Checked

                RemoveHandler cbImagenTitulo.Unchecked, AddressOf CambiarImagenTitulo_Unchecked
                AddHandler cbImagenTitulo.Unchecked, AddressOf CambiarImagenTitulo_Unchecked
            End If

            '-------------------------------------------

            Dim colorFondo As ColorPicker = pagina.FindName("colorPickerPersonalizacionFondo")

            If Not colorFondo Is Nothing Then
                RemoveHandler colorFondo.ColorChanged, AddressOf CambiarFondoColor
                AddHandler colorFondo.ColorChanged, AddressOf CambiarFondoColor
            End If

            '-------------------------------------------

            Dim tviPlataforma As TabViewItem = pagina.FindName("tviPersonalizacionPlataforma")

            If Not tviPlataforma Is Nothing Then
                If tipo = 0 Or tipo = 1 Then
                    tviPlataforma.Visibility = Visibility.Collapsed
                ElseIf tipo = 2 Or tipo = 3 Then
                    tviPlataforma.Visibility = Visibility.Visible

                    Dim cbPlataforma As CheckBox = pagina.FindName("cbPersonalizacionPlataforma")

                    If Not cbPlataforma Is Nothing Then
                        RemoveHandler cbPlataforma.Checked, AddressOf EnseñarPlataforma_Checked
                        AddHandler cbPlataforma.Checked, AddressOf EnseñarPlataforma_Checked

                        RemoveHandler cbPlataforma.Unchecked, AddressOf EnseñarPlataforma_Unchecked
                        AddHandler cbPlataforma.Unchecked, AddressOf EnseñarPlataforma_Unchecked

                        Dim spPlataforma As StackPanel = pagina.FindName("spPersonalizacionPlataforma")

                        If Not spPlataforma Is Nothing Then
                            If cbPlataforma.IsChecked = True Then
                                spPlataforma.Visibility = Visibility.Visible
                            Else
                                spPlataforma.Visibility = Visibility.Collapsed
                            End If
                        End If
                    End If

                    Dim cbPlataformaPosicion As ComboBox = pagina.FindName("cbPersonalizacionPlataformaPosicion")

                    If Not cbPlataformaPosicion Is Nothing Then
                        RemoveHandler cbPlataformaPosicion.SelectionChanged, AddressOf CambiarPlataformaPosicion
                        AddHandler cbPlataformaPosicion.SelectionChanged, AddressOf CambiarPlataformaPosicion

                        cbPlataformaPosicion.SelectedIndex = 1
                    End If

                End If
            End If

            '-------------------------------------------

            Dim botonResetear As Button = pagina.FindName("botonResetearPersonalizacion")

            If Not botonResetear Is Nothing Then
                botonResetear.Tag = fuente

                RemoveHandler botonResetear.Click, AddressOf Resetear
                AddHandler botonResetear.Click, AddressOf Resetear
            End If

            '-------------------------------------------

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
            gridInterior.Background = New SolidColorBrush(Colors.Transparent)

            Dim imagen As New ImageEx With {
                .Source = fuente,
                .IsCacheEnabled = True
            }

            If ApplicationData.Current.LocalSettings.Values("modo_estiramiento") = 0 Then
                imagen.Stretch = Stretch.UniformToFill
            ElseIf ApplicationData.Current.LocalSettings.Values("modo_estiramiento") = 1 Then
                imagen.Stretch = Stretch.Uniform
            ElseIf ApplicationData.Current.LocalSettings.Values("modo_estiramiento") = 2 Then
                imagen.Stretch = Stretch.Fill
            ElseIf ApplicationData.Current.LocalSettings.Values("modo_estiramiento") = 3 Then
                imagen.Stretch = Stretch.None
            End If

            gridInterior.Children.Clear()
            gridInterior.Children.Add(imagen)

            BloquearControles(True)

        End Sub

        Private Async Sub Volver(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tipo As Integer = 0

            Dim botonVolver As Button = sender
            tipo = botonVolver.Tag

            Dim id As Integer = 0

            Dim carpeta As StorageFolder = ApplicationData.Current.LocalFolder
            Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync()

            For Each fichero In ficheros
                id = id + 1
            Next

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")
            gridInterior.BorderThickness = New Thickness(0, 0, 0, 0)
            gridInterior.Background = gridExterior.Background

            If gridInterior.Children.Count > 0 Then
                Try
                    Await Tiles.Imagen.Generar(gridInterior, "personalizacion" + id.ToString + ".png", gridInterior.ActualWidth, gridInterior.ActualHeight)
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

        Public Sub CambiarImagenEstiramiento(sender As Object, e As SelectionChangedEventArgs)

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

        Private Sub CambiarImagenMargen(sender As Object, e As RangeBaseValueChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim slider As Slider = sender

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

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

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

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

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim sliderX As Slider = pagina.FindName("sliderPersonalizacionImagenMoverX")
            Dim sliderY As Slider = pagina.FindName("sliderPersonalizacionImagenMoverY")

            Dim mover As New TranslateTransform With {
                .X = sliderX.Value,
                .Y = sliderY.Value
            }

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

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

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

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

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim colorFondo As ColorPicker = sender

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")
            gridInterior.Background = New SolidColorBrush(colorFondo.Color)

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")
            gridExterior.Background = New SolidColorBrush(colorFondo.Color)

        End Sub

        Private Sub EnseñarPlataforma_Checked(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim sp As StackPanel = pagina.FindName("spPersonalizacionPlataforma")
            sp.Visibility = Visibility.Visible

            Dim gvPlataformaIconos As AdaptiveGridView = pagina.FindName("gvPersonalizacionPlataformaIconos")
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

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim sp As StackPanel = pagina.FindName("spPersonalizacionPlataforma")
            sp.Visibility = Visibility.Collapsed

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

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

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

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

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonIcono As Button = sender
            Dim imagenIcono As ImageEx = botonIcono.Content

            Dim cbPlataformaPosicion As ComboBox = pagina.FindName("cbPersonalizacionPlataformaPosicion")

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

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

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

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

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbPosicion As ComboBox = sender

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

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

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

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

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim boton As Button = sender

            Dim original As String = boton.Tag

            Dim imagen As New ImageEx With {
                .IsCacheEnabled = True,
                .Source = original
            }

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

            gridInterior.Children.Add(imagen)

            Dim cbImagenUbicacion As ComboBox = pagina.FindName("cbPersonalizacionImagenUbicacion")

            If Not cbImagenUbicacion Is Nothing Then
                cbImagenUbicacion.SelectedIndex = 0
            End If

            Dim cbImagenEstiramiento As ComboBox = pagina.FindName("cbPersonalizacionImagenEstiramiento")

            If Not cbImagenEstiramiento Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("modo_estiramiento") = Nothing Then
                    cbImagenEstiramiento.SelectedIndex = ApplicationData.Current.LocalSettings.Values("modo_estiramiento")
                Else
                    cbImagenEstiramiento.SelectedIndex = 0
                End If
            End If

            Dim sliderImagenMargen As Slider = pagina.FindName("sliderPersonalizacionImagenMargen")

            If Not sliderImagenMargen Is Nothing Then
                sliderImagenMargen.Value = 0
            End If

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

        Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

    End Module
End Namespace

