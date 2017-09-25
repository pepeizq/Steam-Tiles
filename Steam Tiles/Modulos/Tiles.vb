Imports Microsoft.Toolkit.Uwp.Notifications
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.UI.Notifications
Imports Windows.UI.StartScreen

Module Tiles

    Public Async Sub Generar(tile As Tile)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonAñadirTile")
        boton.IsEnabled = False

        DescargaImagen(Await Steam.SacarIcono(tile.ID), tile.ID + "icon")
        DescargaImagen(tile.ImagenWide, tile.ID + "wide")
        DescargaImagen(tile.ImagenLarge, tile.ID + "large")

        Dim nuevaTile As SecondaryTile = New SecondaryTile(tile.ID, tile.Titulo, tile.Enlace.ToString, New Uri("ms-appdata:///local/" + tile.ID + "wide.png", UriKind.RelativeOrAbsolute), TileSize.Wide310x150)

        nuevaTile.VisualElements.Wide310x150Logo = New Uri("ms-appdata:///local/" + tile.ID + "wide.png", UriKind.RelativeOrAbsolute)
        nuevaTile.VisualElements.Square310x310Logo = New Uri("ms-appdata:///local/" + tile.ID + "large.png", UriKind.RelativeOrAbsolute)

        Await nuevaTile.RequestCreateAsync()

        Dim imagenDRM As AdaptiveImage = Nothing

        If ApplicationData.Current.LocalSettings.Values("logotile") = "on" Then
            imagenDRM = New AdaptiveImage With {
                .HintRemoveMargin = True,
                .HintAlign = AdaptiveImageAlign.Right
            }

            Dim cbIconosLista As ComboBox = pagina.FindName("cbTilesIconosLista")
            Dim imagenIcono As ImageEx = cbIconosLista.SelectedItem
            imagenDRM.Source = imagenIcono.Source

        End If

        Dim imagen As AdaptiveImage = New AdaptiveImage With {
            .Source = "ms-appdata:///local/" + tile.ID + "wide.png",
            .HintRemoveMargin = True,
            .HintAlign = AdaptiveImageAlign.Stretch,
            .HintCrop = AdaptiveImageCrop.Default
        }

        Dim fondoImagenIcon As TileBackgroundImage = New TileBackgroundImage With {
            .Source = "ms-appdata:///local/" + tile.ID + "icon.png",
            .HintCrop = AdaptiveImageCrop.Default
        }

        Dim fondoImagenWide As TileBackgroundImage = New TileBackgroundImage With {
            .Source = "ms-appdata:///local/" + tile.ID + "wide.png",
            .HintCrop = AdaptiveImageCrop.Default
        }

        Dim fondoImagenLarge As TileBackgroundImage = New TileBackgroundImage With {
            .Source = "ms-appdata:///local/" + tile.ID + "large.png",
            .HintCrop = AdaptiveImageCrop.Default
        }

        '-----------------------

        Dim contenidoSmall As TileBindingContentAdaptive = New TileBindingContentAdaptive With {
            .BackgroundImage = fondoImagenIcon
        }

        'If Not imagenDRM Is Nothing Then
        '    contenidoSmall.Children.Add(imagenDRM)
        'End If

        Dim tileSmall As TileBinding = New TileBinding With {
            .Content = contenidoSmall
        }

        '-----------------------

        Dim contenidoMedium As TileBindingContentAdaptive = New TileBindingContentAdaptive With {
            .BackgroundImage = fondoImagenIcon
        }

        If Not imagenDRM Is Nothing Then
            contenidoMedium.Children.Add(imagenDRM)
        End If

        Dim tileMedium As TileBinding = New TileBinding With {
            .Content = contenidoMedium
        }

        '-----------------------

        Dim contenidoWide As TileBindingContentAdaptive = New TileBindingContentAdaptive With {
            .BackgroundImage = fondoImagenWide
        }

        If Not imagenDRM Is Nothing Then
            contenidoWide.Children.Add(imagenDRM)
        End If

        Dim tileWide As TileBinding = New TileBinding With {
            .Content = contenidoWide
        }

        '-----------------------

        Dim contenidoLarge As TileBindingContentAdaptive = New TileBindingContentAdaptive With {
            .BackgroundImage = fondoImagenLarge
        }

        If Not imagenDRM Is Nothing Then
            contenidoLarge.Children.Add(imagenDRM)
        End If

        Dim tileLarge As TileBinding = New TileBinding With {
            .Content = contenidoLarge
        }

        '-----------------------

        If ApplicationData.Current.LocalSettings.Values("titulotile") = "on" Then
            tileWide.Branding = TileBranding.Name
            tileSmall.Branding = TileBranding.Name
            tileMedium.Branding = TileBranding.Name
            tileLarge.Branding = TileBranding.Name
        End If

        Dim visual As TileVisual = New TileVisual With {
            .TileWide = tileWide,
            .TileSmall = tileSmall,
            .TileMedium = tileMedium,
            .TileLarge = tileLarge
        }

        Dim contenido As TileContent = New TileContent With {
            .Visual = visual
        }

        Dim notificacion As TileNotification = New TileNotification(contenido.GetXml)

        Try
            TileUpdateManager.CreateTileUpdaterForSecondaryTile(tile.ID).Update(notificacion)
        Catch ex As Exception

        End Try

        boton.IsEnabled = True

    End Sub

    Private Async Sub DescargaImagen(uri As Uri, clave As String)

        Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync(clave + ".png", CreationCollisionOption.GenerateUniqueName)
        Dim descargador As BackgroundDownloader = New BackgroundDownloader()

        If Not uri = Nothing Then
            Dim descarga As DownloadOperation = descargador.CreateDownload(uri, ficheroImagen)
            Await descarga.StartAsync
        End If

    End Sub

End Module
