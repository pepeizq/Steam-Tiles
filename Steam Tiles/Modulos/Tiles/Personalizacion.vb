Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.UI

Namespace Tiles
    Module Personalizacion

        Public Async Sub Cargar(grid As Grid, tipo As Integer)

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

            Dim id As Integer = 0

            Dim carpeta As StorageFolder = ApplicationData.Current.LocalFolder
            Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync()

            For Each fichero In ficheros
                id = id + 1
            Next

            Await Tiles.Imagen.Generar(grid, "personalizacion" + id.ToString + ".png", anchoInterior, altoInterior)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonVolver As Button = pagina.FindName("botonVolverPersonalizacion")
            botonVolver.Tag = tipo

            RemoveHandler botonVolver.Click, AddressOf Volver
            AddHandler botonVolver.Click, AddressOf Volver

            Dim gridPersonalizar As Grid = pagina.FindName("gridPersonalizarTiles")
            gridPersonalizar.Visibility = Visibility.Visible

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")
            gridExterior.BorderThickness = New Thickness(1, 1, 1, 1)
            gridExterior.Width = anchoExterior
            gridExterior.Height = altoExterior

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")
            gridInterior.BorderThickness = New Thickness(1, 1, 1, 1)
            gridInterior.SetValue(Canvas.ZIndexProperty, 99990)
            gridInterior.Width = anchoInterior
            gridInterior.Height = altoInterior

            Dim imagen As New ImageEx With {
                .Source = "ms-appdata:///local/personalizacion" + id.ToString + ".png",
                .IsCacheEnabled = True
            }

            gridInterior.Children.Add(imagen)

            Dim cbOpciones As ComboBox = pagina.FindName("cbOpciones")

            RemoveHandler cbOpciones.SelectionChanged, AddressOf CambiarOpcion
            AddHandler cbOpciones.SelectionChanged, AddressOf CambiarOpcion

            cbOpciones.SelectedIndex = 0

        End Sub

        Public Async Sub Volver(sender As Object, e As RoutedEventArgs)

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

        Public Sub CambiarOpcion(sender As Object, e As SelectionChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbOpciones As ComboBox = pagina.FindName("cbOpciones")

            If cbOpciones.SelectedIndex = 0 Then
                Dim gridFuente As Grid = pagina.FindName("gridPersonalizacionImagenFuente")
                MostrarGridOpcion(gridFuente)

                Dim botonImagenOrdenador As Button = pagina.FindName("botonPersonalizacionCambiarImagenOrdenador")

                RemoveHandler botonImagenOrdenador.Click, AddressOf CambioImagen
                AddHandler botonImagenOrdenador.Click, AddressOf CambioImagen

                Dim cbImagenUbicacion As ComboBox = pagina.FindName("cbPersonalizacionImagenUbicacion")

                RemoveHandler cbImagenUbicacion.SelectionChanged, AddressOf CambiarImagenUbicacion
                AddHandler cbImagenUbicacion.SelectionChanged, AddressOf CambiarImagenUbicacion

                If ApplicationData.Current.LocalSettings.Values("opcion_imagen_ubicacion") Is Nothing Then
                    cbImagenUbicacion.SelectedIndex = 0
                Else
                    cbImagenUbicacion.SelectedIndex = ApplicationData.Current.LocalSettings.Values("opcion_imagen_ubicacion")
                End If


            End If

        End Sub

        Private Async Sub CambioImagen(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonImagenOrdenador As Button = pagina.FindName("botonPersonalizacionCambiarImagenOrdenador")
            botonImagenOrdenador.IsEnabled = False

            Dim ficheroPicker As New FileOpenPicker
            ficheroPicker.FileTypeFilter.Add(".png")
            ficheroPicker.FileTypeFilter.Add(".jpg")
            ficheroPicker.ViewMode = PickerViewMode.List

            Dim ficheroImagen As StorageFile = Await ficheroPicker.PickSingleFileAsync

            Dim bitmap As New BitmapImage

            Try
                Dim stream As FileRandomAccessStream = Await ficheroImagen.OpenAsync(FileAccessMode.Read)
                bitmap.SetSource(stream)

                Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")

                For Each hijo In gridExterior.Children
                    Dim grid As Grid = hijo

                    If grid Is Nothing Then
                        gridExterior.Children.Remove(hijo)
                    End If
                Next

                Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")

                Dim imagen As New ImageEx With {
                    .IsCacheEnabled = True,
                    .Source = bitmap,
                    .Stretch = Stretch.UniformToFill
                }

                If ApplicationData.Current.LocalSettings.Values("opcion_imagen_ubicacion") Is Nothing Then
                    gridInterior.Children.Add(imagen)
                Else
                    If ApplicationData.Current.LocalSettings.Values("opcion_imagen_ubicacion") = 0 Then
                        gridInterior.Children.Add(imagen)
                    Else
                        gridInterior.Children.Clear()
                        gridExterior.Children.Add(imagen)
                    End If
                End If

            Catch ex As Exception

            End Try

            botonImagenOrdenador.IsEnabled = True

        End Sub

        Public Sub CambiarImagenUbicacion(sender As Object, e As SelectionChangedEventArgs)

            Dim cb As ComboBox = sender
            ApplicationData.Current.LocalSettings.Values("opcion_imagen_ubicacion") = cb.SelectedIndex

        End Sub

        Private Sub MostrarGridOpcion(grid As Grid)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridFuente As Grid = pagina.FindName("gridPersonalizacionImagenFuente")
            gridFuente.Visibility = Visibility.Collapsed

            grid.Visibility = Visibility.Visible

        End Sub

    End Module
End Namespace

