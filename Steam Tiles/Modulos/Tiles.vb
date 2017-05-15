Imports Microsoft.Toolkit.Uwp.Notifications
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.UI.Notifications
Imports Windows.UI.StartScreen

Module Tiles

    Public Async Sub Generar(tile As Tile)

        Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("header.png", CreationCollisionOption.GenerateUniqueName)
        Dim downloader As BackgroundDownloader = New BackgroundDownloader()
        Dim descarga As DownloadOperation = downloader.CreateDownload(tile.Imagen, ficheroImagen)
        Await descarga.StartAsync

        Dim nuevaTile As SecondaryTile = New SecondaryTile(tile.ID, tile.Titulo, tile.Enlace.ToString, New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute), TileSize.Wide310x150)

        nuevaTile.VisualElements.Wide310x150Logo = New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute)
        nuevaTile.VisualElements.Square310x310Logo = New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute)

        Await nuevaTile.RequestCreateAsync()

        Dim imagenDRM As AdaptiveImage = Nothing

        If ApplicationData.Current.LocalSettings.Values("logotile") = "on" Then
            imagenDRM = New AdaptiveImage With {
                .HintRemoveMargin = True,
                .HintAlign = AdaptiveImageAlign.Right
            }

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content
            Dim cbIconosLista As ComboBox = pagina.FindName("cbTilesIconosLista")
            Dim imagenIcono As ImageEx = cbIconosLista.SelectedItem
            imagenDRM.Source = imagenIcono.Source

        End If

        Dim imagen As AdaptiveImage = New AdaptiveImage With {
            .Source = "ms-appdata:///local/" + ficheroImagen.Name,
            .HintRemoveMargin = True,
            .HintAlign = AdaptiveImageAlign.Stretch,
            .HintCrop = AdaptiveImageCrop.Default
        }

        Dim fondoImagen As TileBackgroundImage = New TileBackgroundImage With {
            .Source = "ms-appdata:///local/" + ficheroImagen.Name,
            .HintCrop = AdaptiveImageCrop.Default
        }

        '-----------------------

        Dim contenidoWide As TileBindingContentAdaptive = New TileBindingContentAdaptive With {
            .BackgroundImage = fondoImagen
        }

        If Not imagenDRM Is Nothing Then
            contenidoWide.Children.Add(imagenDRM)
        End If

        Dim tileWide As TileBinding = New TileBinding With {
            .Content = contenidoWide
        }

        '-----------------------

        Dim contenidoSmall As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoSmall.Children.Add(imagen)

        If Not imagenDRM Is Nothing Then
            contenidoSmall.Children.Add(imagenDRM)
        End If

        Dim tileSmall As TileBinding = New TileBinding With {
            .Content = contenidoSmall
        }

        '-----------------------

        Dim contenidoMedium As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoMedium.Children.Add(imagen)

        If Not imagenDRM Is Nothing Then
            contenidoMedium.Children.Add(imagenDRM)
        End If

        Dim tileMedium As TileBinding = New TileBinding With {
            .Content = contenidoMedium
        }

        '-----------------------

        Dim contenidoLarge As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoLarge.Children.Add(imagen)

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

    End Sub

End Module
