Imports Microsoft.Toolkit.Uwp.Notifications
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Graphics.Imaging
Imports Windows.Storage
Imports Windows.UI.Notifications
Imports Windows.UI.StartScreen

Module Tiles

    Public Async Sub Generar(tile As Tile)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonAñadirTile")
        boton.IsEnabled = False

        Dim imagenPequeña As Grid = pagina.FindName("gridTilePequeñaGenerar")
        Dim boolImagenPequeña As Boolean = Await GenerarImagen(imagenPequeña, tile.ID + "pequena.png")

        Dim imagenMediana As Grid = pagina.FindName("gridTileMedianaGenerar")
        Dim boolImagenMediana As Boolean = Await GenerarImagen(imagenMediana, tile.ID + "mediana.png")

        Dim imagenAncha As Grid = pagina.FindName("gridTileAnchaGenerar")
        Dim boolImagenAncha As Boolean = Await GenerarImagen(imagenAncha, tile.ID + "ancha.png")

        Dim imagenGrande As Grid = pagina.FindName("gridTileGrandeGenerar")
        Dim boolImagenGrande As Boolean = Await GenerarImagen(imagenGrande, tile.ID + "grande.png")

        '-----------------------

        Dim nuevaTile As New SecondaryTile(tile.ID, tile.Titulo, tile.Enlace.ToString, New Uri("ms-appdata:///local/" + tile.ID + "ancha.png", UriKind.RelativeOrAbsolute), TileSize.Wide310x150)

        nuevaTile.VisualElements.Square71x71Logo = New Uri("ms-appdata:///local/" + tile.ID + "pequena.png", UriKind.RelativeOrAbsolute)
        nuevaTile.VisualElements.Square150x150Logo = New Uri("ms-appdata:///local/" + tile.ID + "mediana.png", UriKind.RelativeOrAbsolute)
        nuevaTile.VisualElements.Wide310x150Logo = New Uri("ms-appdata:///local/" + tile.ID + "ancha.png", UriKind.RelativeOrAbsolute)
        nuevaTile.VisualElements.Square310x310Logo = New Uri("ms-appdata:///local/" + tile.ID + "grande.png", UriKind.RelativeOrAbsolute)

        nuevaTile.VisualElements.ShowNameOnSquare310x310Logo = True
        nuevaTile.VisualElements.ForegroundText = ForegroundText.Dark

        Await nuevaTile.RequestCreateAsync()

        '-----------------------

        Dim imagenDRM As AdaptiveImage = Nothing

        If ApplicationData.Current.LocalSettings.Values("drm_tile") = True Then
            imagenDRM = New AdaptiveImage With {
                .HintRemoveMargin = True,
                .HintAlign = AdaptiveImageAlign.Right
            }

            Dim cbIconosLista As ComboBox = pagina.FindName("cbTilesIconosLista")
            Dim imagenIcono As ImageEx = cbIconosLista.SelectedItem
            imagenDRM.Source = imagenIcono.Source
        End If

        '-----------------------

        Dim contenidoPequeño As New TileBindingContentAdaptive

        If boolImagenPequeña = True Then
            Dim fondoImagenPequeña As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "pequena.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoPequeño = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenPequeña
            }
        End If

        'If Not imagenDRM Is Nothing Then
        '    contenidoSmall.Children.Add(imagenDRM)
        'End If

        Dim tilePequeño As New TileBinding With {
            .Content = contenidoPequeño
        }

        '-----------------------

        Dim contenidoMediano As New TileBindingContentAdaptive

        If boolImagenMediana = True Then
            Dim fondoImagenMediano As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "mediana.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoMediano = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenMediano
            }
        End If

        If Not imagenDRM Is Nothing Then
            contenidoMediano.Children.Add(imagenDRM)
        End If

        Dim tileMediano As New TileBinding With {
            .Content = contenidoMediano
        }

        '-----------------------

        Dim contenidoAncho As New TileBindingContentAdaptive

        If boolImagenAncha = True Then
            Dim fondoImagenAncha As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "ancha.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoAncho = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenAncha
            }
        End If

        If Not imagenDRM Is Nothing Then
            contenidoAncho.Children.Add(imagenDRM)
        End If

        Dim tileAncha As New TileBinding With {
            .Content = contenidoAncho
        }

        '-----------------------

        Dim contenidoGrande As New TileBindingContentAdaptive

        If boolImagenGrande = True Then
            Dim fondoImagenGrande As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "grande.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoGrande = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenGrande
            }
        End If

        If Not imagenDRM Is Nothing Then
            contenidoGrande.Children.Add(imagenDRM)
        End If

        Dim tileGrande As New TileBinding With {
            .Content = contenidoGrande
        }

        '-----------------------

        If ApplicationData.Current.LocalSettings.Values("titulo_tile") = True Then
            tileAncha.Branding = TileBranding.Name
            tilePequeño.Branding = TileBranding.Name
            tileMediano.Branding = TileBranding.Name
            tileGrande.Branding = TileBranding.Name
        End If

        Dim visual As New TileVisual With {
            .TileWide = tileAncha,
            .TileSmall = tilePequeño,
            .TileMedium = tileMediano,
            .TileLarge = tileGrande
        }

        Dim contenido As New TileContent With {
            .Visual = visual
        }

        Dim notificacion As New TileNotification(contenido.GetXml)

        Try
            'TileUpdateManager.CreateTileUpdaterForSecondaryTile(tile.ID).Update(notificacion)
        Catch ex As Exception

        End Try

        boton.IsEnabled = True

    End Sub

    Public Async Function GenerarImagen(gridImagen As Grid, clave As String) As Task(Of Boolean)

        Dim descargaFinalizada As Boolean = False

        Dim carpetaInstalacion As StorageFolder = ApplicationData.Current.LocalFolder
        Dim ficheroImagen As StorageFile = Await carpetaInstalacion.CreateFileAsync(clave, CreationCollisionOption.ReplaceExisting)

        Dim resultado As New RenderTargetBitmap()
        Await resultado.RenderAsync(gridImagen)

        Dim buffer As Streams.IBuffer = Await resultado.GetPixelsAsync
        Dim pixeles As Byte() = buffer.ToArray
        Dim rawdpi As DisplayInformation = DisplayInformation.GetForCurrentView()

        Using stream As Streams.IRandomAccessStream = Await ficheroImagen.OpenAsync(FileAccessMode.ReadWrite)
            Dim encoder As BitmapEncoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream)
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, resultado.PixelWidth, resultado.PixelHeight, rawdpi.RawDpiX, rawdpi.RawDpiY, pixeles)

            Await encoder.FlushAsync
        End Using

        Return descargaFinalizada
    End Function

End Module
