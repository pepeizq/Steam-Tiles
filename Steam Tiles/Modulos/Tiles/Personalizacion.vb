Imports Microsoft.Toolkit.Uwp
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.UI

Namespace Tiles
    Module Personalizacion

        Public Async Sub CambioImagenOrdenador(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            boton.IsEnabled = False

            Dim imagen As ImageEx = boton.Tag

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
                imagen.Source = bitmap
            Catch ex As Exception

            End Try

            boton.IsEnabled = True

        End Sub

        Public Sub CambioImagenInternet(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender
            tb.IsEnabled = False

            Dim imagen As ImageEx = tb.Tag

            If tb.Text.Trim.Length > 0 Then
                If tb.Text.Trim.Contains("http://") Or tb.Text.Trim.Contains("https://") Then
                    Try
                        imagen.Source = tb.Text.Trim
                    Catch ex As Exception

                    End Try
                End If
            End If

            tb.IsEnabled = True

        End Sub

        Public Sub ImagenEstiramiento(sender As Object, e As SelectionChangedEventArgs)

            Dim cb As ComboBox = sender
            cb.IsEnabled = False

            Dim imagen As ImageEx = cb.Tag

            If cb.SelectedIndex = 0 Then
                imagen.Stretch = Stretch.None
            ElseIf cb.SelectedIndex = 1 Then
                imagen.Stretch = Stretch.Fill
            ElseIf cb.SelectedIndex = 2 Then
                imagen.Stretch = Stretch.Uniform
            ElseIf cb.SelectedIndex = 3 Then
                imagen.Stretch = Stretch.UniformToFill
            End If

            cb.IsEnabled = True

        End Sub

        Public Sub ImagenMargen(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim slider As Slider = sender
            slider.IsEnabled = False

            Dim imagen As ImageEx = slider.Tag
            imagen.Margin = New Thickness(slider.Value)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If imagen.Name = "imagenTilePequeña" Then
                Dim tb As TextBlock = pagina.FindName("tbCambiarImagenMargenTilePequeña")
                tb.Text = slider.Value.ToString + "px"
            ElseIf imagen.Name = "imagenTileMediana" Then
                Dim tb As TextBlock = pagina.FindName("tbCambiarImagenMargenTileMediana")
                tb.Text = slider.Value.ToString + "px"
            ElseIf imagen.Name = "imagenTileAncha" Then
                Dim tb As TextBlock = pagina.FindName("tbCambiarImagenMargenTileAncha")
                tb.Text = slider.Value.ToString + "px"
            ElseIf imagen.Name = "imagenTileGrande" Then
                Dim tb As TextBlock = pagina.FindName("tbCambiarImagenMargenTileGrande")
                tb.Text = slider.Value.ToString + "px"
            End If

            slider.IsEnabled = True

        End Sub

        Public Sub ImagenEsquinas(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim slider As Slider = sender
            slider.IsEnabled = False

            Dim imagen As ImageEx = slider.Tag
            imagen.CornerRadius = New CornerRadius(slider.Value)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If imagen.Name = "imagenTilePequeña" Then
                Dim tb As TextBlock = pagina.FindName("tbCambiarImagenEsquinasTilePequeña")
                tb.Text = slider.Value.ToString + "px"
            ElseIf imagen.Name = "imagenTileMediana" Then
                Dim tb As TextBlock = pagina.FindName("tbCambiarImagenEsquinasTileMediana")
                tb.Text = slider.Value.ToString + "px"
            ElseIf imagen.Name = "imagenTileAncha" Then
                Dim tb As TextBlock = pagina.FindName("tbCambiarImagenEsquinasTileAncha")
                tb.Text = slider.Value.ToString + "px"
            ElseIf imagen.Name = "imagenTileGrande" Then
                Dim tb As TextBlock = pagina.FindName("tbCambiarImagenEsquinasTileGrande")
                tb.Text = slider.Value.ToString + "px"
            End If

            slider.IsEnabled = True

        End Sub

        Public Sub FondoTransparente(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim ts As ToggleSwitch = sender
            ts.IsEnabled = False

            Dim grid As Grid = ts.Tag

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If ts.IsOn Then
                grid.Background = New SolidColorBrush(Colors.Transparent)

                If grid.Name = "gridTilePequeña" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenFondoTilePequeña")
                    sp.Visibility = Visibility.Collapsed
                ElseIf grid.Name = "gridTileMediana" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenFondoTileMediana")
                    sp.Visibility = Visibility.Collapsed
                ElseIf grid.Name = "gridTileAncha" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenFondoTileAncha")
                    sp.Visibility = Visibility.Collapsed
                ElseIf grid.Name = "gridTileGrande" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenFondoTileGrande")
                    sp.Visibility = Visibility.Collapsed
                End If
            Else
                grid.Background = New SolidColorBrush(App.Current.Resources("ColorTerciario"))

                If grid.Name = "gridTilePequeña" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenFondoTilePequeña")
                    sp.Visibility = Visibility.Visible

                    Dim cp As ColorPicker = pagina.FindName("cpImagenFondoTilePequeña")
                    cp.Color = Helpers.ColorHelper.ToColor(Helpers.ColorHelper.ToHex(App.Current.Resources("ColorTerciario")))
                ElseIf grid.Name = "gridTileMediana" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenFondoTileMediana")
                    sp.Visibility = Visibility.Visible

                    Dim cp As ColorPicker = pagina.FindName("cpImagenFondoTileMediana")
                    cp.Color = Helpers.ColorHelper.ToColor(Helpers.ColorHelper.ToHex(App.Current.Resources("ColorTerciario")))
                ElseIf grid.Name = "gridTileAncha" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenFondoTileAncha")
                    sp.Visibility = Visibility.Visible

                    Dim cp As ColorPicker = pagina.FindName("cpImagenFondoTileAncha")
                    cp.Color = Helpers.ColorHelper.ToColor(Helpers.ColorHelper.ToHex(App.Current.Resources("ColorTerciario")))
                ElseIf grid.Name = "gridTileGrande" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenFondoTileGrande")
                    sp.Visibility = Visibility.Visible

                    Dim cp As ColorPicker = pagina.FindName("cpImagenFondoTileGrande")
                    cp.Color = Helpers.ColorHelper.ToColor(Helpers.ColorHelper.ToHex(App.Current.Resources("ColorTerciario")))
                End If
            End If

            ts.IsEnabled = True

        End Sub

        Public Sub FondoColor(ByVal sender As Object, ByVal e As ColorChangedEventArgs)

            Dim cp As ColorPicker = sender
            cp.IsEnabled = False

            Dim grid As Grid = cp.Tag
            grid.Background = New SolidColorBrush(cp.Color)

            cp.IsEnabled = True

        End Sub

        Public Sub Titulo(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim ts As ToggleSwitch = sender

            Dim grid As Grid = ts.Tag

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If ts.IsOn = True Then
                ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = True

                If grid.Name = "gridTileAncha" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenTituloTileAncha")
                    sp.Visibility = Visibility.Visible
                ElseIf grid.Name = "gridTileGrande" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenTituloTileGrande")
                    sp.Visibility = Visibility.Visible
                End If
            Else
                ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = False

                If grid.Name = "gridTileAncha" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenTituloTileAncha")
                    sp.Visibility = Visibility.Collapsed
                ElseIf grid.Name = "gridTileGrande" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenTituloTileGrande")
                    sp.Visibility = Visibility.Collapsed
                End If
            End If

        End Sub

        Public Sub TituloColor(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)

            Dim cb As ComboBox = sender
            ApplicationData.Current.LocalSettings.Values("tiles_color_titulo") = cb.SelectedIndex

        End Sub

        Public Sub Icono(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim ts As ToggleSwitch = sender

            Dim grid As Grid = ts.Tag

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If ts.IsOn = True Then
                CargarIconos(grid)

                If grid.Name = "gridTileAncha" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenIconoTileAncha")
                    sp.Visibility = Visibility.Visible
                ElseIf grid.Name = "gridTileGrande" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenIconoTileGrande")
                    sp.Visibility = Visibility.Visible
                End If
            Else
                If grid.Name = "gridTileAncha" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenIconoTileAncha")
                    sp.Visibility = Visibility.Collapsed
                ElseIf grid.Name = "gridTileGrande" Then
                    Dim sp As StackPanel = pagina.FindName("spImagenIconoTileGrande")
                    sp.Visibility = Visibility.Collapsed
                End If

                If grid.Children.Count > 1 Then
                    grid.Children.RemoveAt(grid.Children.Count - 1)
                End If
            End If

        End Sub

        Private Async Sub CargarIconos(grid As Grid)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = Nothing

            If grid.Name = "gridTileAncha" Then
                gv = pagina.FindName("gvImagenIconoTileAncha")
            ElseIf grid.Name = "gridTileGrande" Then
                gv = pagina.FindName("gvImagenIconoTileGrande")
            End If

            If Not gv Is Nothing Then
                gv.Items.Clear()

                Dim carpeta As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(Package.Current.InstalledLocation.Path + "\Assets\Iconos")
                Dim ficheros As IReadOnlyList(Of IStorageItem) = Await carpeta.GetFilesAsync

                For Each fichero In ficheros
                    Dim imagen As New ImageEx With {
                        .Source = fichero.Path,
                        .Width = 32,
                        .Height = 32,
                        .IsCacheEnabled = True
                    }

                    Dim boton As New Button With {
                        .Content = imagen,
                        .Background = New SolidColorBrush(Colors.Transparent),
                        .BorderBrush = New SolidColorBrush(Colors.Transparent),
                        .BorderThickness = New Thickness(0, 0, 0, 0),
                        .Padding = New Thickness(5, 5, 5, 5),
                        .Tag = gv,
                        .Style = App.Current.Resources("ButtonRevealStyle")
                    }

                    AddHandler boton.Click, AddressOf AñadirIcono
                    AddHandler boton.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_Imagen
                    AddHandler boton.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_Imagen

                    gv.Items.Add(boton)
                Next
            End If

        End Sub

        Private Sub AñadirIcono(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim imagen As ImageEx = boton.Content
            Dim gv As GridView = boton.Tag

            Dim imagen2 As New ImageEx With {
                .Source = imagen.Source,
                .Width = 32,
                .Height = 32,
                .IsCacheEnabled = True,
                .Margin = New Thickness(10, 10, 10, 10)
            }

            Dim cbPosicion As ComboBox = Nothing
            Dim grid As Grid = Nothing

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If gv.Name = "gvImagenIconoTileAncha" Then
                cbPosicion = pagina.FindName("cbImagenIconoPosicionTileAncha")
                grid = pagina.FindName("gridTileAncha")
            ElseIf gv.Name = "gvImagenIconoTileGrande" Then
                cbPosicion = pagina.FindName("cbImagenIconoPosicionTileGrande")
                grid = pagina.FindName("gridTileGrande")
            End If

            If Not cbPosicion Is Nothing Then
                If cbPosicion.SelectedIndex = 0 Then
                    imagen2.VerticalAlignment = VerticalAlignment.Top
                    imagen2.HorizontalAlignment = HorizontalAlignment.Left
                ElseIf cbPosicion.SelectedIndex = 1 Then
                    imagen2.VerticalAlignment = VerticalAlignment.Top
                    imagen2.HorizontalAlignment = HorizontalAlignment.Right
                ElseIf cbPosicion.SelectedIndex = 2 Then
                    imagen2.VerticalAlignment = VerticalAlignment.Bottom
                    imagen2.HorizontalAlignment = HorizontalAlignment.Left
                ElseIf cbPosicion.SelectedIndex = 3 Then
                    imagen2.VerticalAlignment = VerticalAlignment.Bottom
                    imagen2.HorizontalAlignment = HorizontalAlignment.Right
                End If
            End If

            If Not grid Is Nothing Then
                If grid.Children.Count = 1 Then
                    grid.Children.Add(imagen2)
                ElseIf grid.Children.Count > 1 Then
                    grid.Children.RemoveAt(grid.Children.Count - 1)
                    grid.Children.Add(imagen2)
                End If
            End If

        End Sub

        Public Sub IconoPosicion(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)

            Dim cb As ComboBox = sender
            Dim grid As Grid = cb.Tag

            If grid.Children.Count > 1 Then
                Dim imagen As ImageEx = grid.Children(1)

                If cb.SelectedIndex = 0 Then
                    imagen.VerticalAlignment = VerticalAlignment.Top
                    imagen.HorizontalAlignment = HorizontalAlignment.Left
                ElseIf cb.SelectedIndex = 1 Then
                    imagen.VerticalAlignment = VerticalAlignment.Top
                    imagen.HorizontalAlignment = HorizontalAlignment.Right
                ElseIf cb.SelectedIndex = 2 Then
                    imagen.VerticalAlignment = VerticalAlignment.Bottom
                    imagen.HorizontalAlignment = HorizontalAlignment.Left
                ElseIf cb.SelectedIndex = 3 Then
                    imagen.VerticalAlignment = VerticalAlignment.Bottom
                    imagen.HorizontalAlignment = HorizontalAlignment.Right
                End If
            End If

        End Sub

    End Module
End Namespace