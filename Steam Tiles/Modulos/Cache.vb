Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage

Module Cache

    Public Async Function DescargarImagen(enlace As String, id As String, tipo As String) As Task(Of String)

        Dim imagenFinal As String = String.Empty

        If Not enlace = String.Empty Then
            Dim carpetaImagenes As StorageFolder = Nothing

            If Directory.Exists(ApplicationData.Current.LocalFolder.Path + "\Cache") = False Then
                carpetaImagenes = Await ApplicationData.Current.LocalFolder.CreateFolderAsync("Cache")
            Else
                carpetaImagenes = Await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path + "\Cache")
            End If

            If Not carpetaImagenes Is Nothing Then
                If Not File.Exists(ApplicationData.Current.LocalFolder.Path + "\Cache\" + id + tipo) Then
                    Dim ficheroImagen As IStorageFile = Nothing

                    Try
                        ficheroImagen = Await carpetaImagenes.CreateFileAsync(id + tipo, CreationCollisionOption.ReplaceExisting)
                    Catch ex As Exception

                    End Try

                    If Not ficheroImagen Is Nothing Then
                        Dim descargador As New BackgroundDownloader
                        Dim descarga As DownloadOperation = descargador.CreateDownload(New Uri(enlace), ficheroImagen)
                        descarga.Priority = BackgroundTransferPriority.Default
                        Await descarga.StartAsync

                        If descarga.Progress.Status = BackgroundTransferStatus.Completed Then
                            Dim ficheroDescargado As IStorageFile = descarga.ResultFile

                            imagenFinal = ficheroDescargado.Path
                        End If
                    End If
                Else
                    imagenFinal = ApplicationData.Current.LocalFolder.Path + "\Cache\" + id + tipo
                End If
            Else
                imagenFinal = enlace
            End If
        End If

        Return imagenFinal

    End Function

    Public Async Sub Limpiar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonConfigLimpiarCache")
        boton.IsEnabled = False

        Dim pr As ProgressRing = pagina.FindName("prConfigLimpiarCache")
        pr.Visibility = Visibility.Visible

        Dim cbTiles As ComboBox = pagina.FindName("cbConfigModosTiles")
        cbTiles.IsEnabled = False

        Dim sp1 As StackPanel = pagina.FindName("spModoTile1")
        sp1.IsHitTestVisible = False

        Dim sp2 As StackPanel = pagina.FindName("spModoTile1")
        sp2.IsHitTestVisible = False

        If File.Exists(ApplicationData.Current.LocalFolder.Path + "\juegos0") Then
            File.Delete(ApplicationData.Current.LocalFolder.Path + "\juegos0")
        End If

        If File.Exists(ApplicationData.Current.LocalFolder.Path + "\juegos1") Then
            File.Delete(ApplicationData.Current.LocalFolder.Path + "\juegos1")
        End If

        If Directory.Exists(ApplicationData.Current.LocalFolder.Path + "\Cache") = True Then
            Dim carpetaImagenes As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path + "\Cache")

            If Not carpetaImagenes Is Nothing Then
                Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpetaImagenes.GetFilesAsync

                For Each fichero As StorageFile In ficheros
                    Await fichero.DeleteAsync()
                Next
            End If
        End If

        Steam.Generar(False)

        boton.IsEnabled = True
        pr.Visibility = Visibility.Collapsed
        cbTiles.IsEnabled = True
        sp1.IsHitTestVisible = True
        sp2.IsHitTestVisible = True

    End Sub

End Module
