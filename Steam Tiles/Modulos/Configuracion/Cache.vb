Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.Storage.FileProperties

Namespace Configuracion
    Module Cache

        Public Sub Cargar()

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim ts As ToggleSwitch = pagina.FindName("tsConfigCache")
            ts.OnContent = recursos.GetString("Activated")
            ts.OffContent = recursos.GetString("Disabled")

            AddHandler ts.Toggled, AddressOf ActivarCache
            AddHandler ts.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico
            AddHandler ts.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico

            Dim sp As StackPanel = pagina.FindName("spConfigCache")

            Dim botonLimpiar As Button = pagina.FindName("botonConfigLimpiarCache")

            AddHandler botonLimpiar.Click, AddressOf Limpiar
            AddHandler botonLimpiar.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_1_05
            AddHandler botonLimpiar.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_1_05

            If Not ApplicationData.Current.LocalSettings.Values("cache") = Nothing Then
                If ApplicationData.Current.LocalSettings.Values("cache") = 0 Then
                    ts.IsOn = False
                    sp.Visibility = Visibility.Collapsed
                Else
                    ts.IsOn = True
                    sp.Visibility = Visibility.Visible
                End If
            Else
                ApplicationData.Current.LocalSettings.Values("cache") = 0
                sp.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub ActivarCache(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim ts As ToggleSwitch = sender
            Dim sp As StackPanel = pagina.FindName("spConfigCache")

            If ts.IsOn = False Then
                ApplicationData.Current.LocalSettings.Values("cache") = 0
                sp.Visibility = Visibility.Collapsed
            Else
                ApplicationData.Current.LocalSettings.Values("cache") = 1
                sp.Visibility = Visibility.Visible
            End If

        End Sub

        Public Async Function DescargarImagen(enlace As String, id As String, tipo As String) As Task(Of String)

            If ApplicationData.Current.LocalSettings.Values("cache") = 1 Then
                If Not enlace = String.Empty Then
                    If enlace.Contains("http://") Or enlace.Contains("https://") Then
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
                                    descarga.Priority = BackgroundTransferPriority.High

                                    Try
                                        Await descarga.StartAsync
                                    Catch ex As Exception

                                    End Try

                                    If descarga.Progress.Status = BackgroundTransferStatus.Completed Then
                                        Dim ficheroDescargado As IStorageFile = descarga.ResultFile
                                        Dim tamaño As BasicProperties = Await ficheroDescargado.GetBasicPropertiesAsync

                                        If tamaño.Size > 0 Then
                                            Return ficheroDescargado.Path
                                        Else
                                            Await ficheroDescargado.DeleteAsync()
                                            Return enlace
                                        End If
                                    ElseIf descarga.Progress.Status = BackgroundTransferStatus.Error Then
                                        Return Nothing
                                    End If
                                End If
                            Else
                                Dim ficheroImagen As IStorageFile = Await StorageFile.GetFileFromPathAsync(ApplicationData.Current.LocalFolder.Path + "\Cache\" + id + tipo)
                                Dim tamaño As BasicProperties = Await ficheroImagen.GetBasicPropertiesAsync

                                If tamaño.Size > 0 Then
                                    Return ApplicationData.Current.LocalFolder.Path + "\Cache\" + id + tipo
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            If enlace = Nothing Then
                Dim fichero As StorageFile = Nothing

                Try
                    fichero = Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///Assets/Juegos/" + id + "_" + tipo + ".png"))
                Catch ex As Exception

                End Try

                If Not fichero Is Nothing Then
                    Return "Assets/Juegos/" + id + "_" + tipo + ".png"
                End If

                Try
                    fichero = Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///Assets/Juegos/" + id + "_" + tipo + ".jpg"))
                Catch ex As Exception

                End Try

                If Not fichero Is Nothing Then
                    Return "Assets/Juegos/" + id + "_" + tipo + ".jpg"
                End If
            End If

            Return enlace

        End Function

        Private Async Sub Limpiar(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Estado(False)

            Dim pr As ProgressRing = pagina.FindName("prConfigLimpiarCache")
            pr.Visibility = Visibility.Visible

            If File.Exists(ApplicationData.Current.LocalFolder.Path + "\juegos0") Then
                File.Delete(ApplicationData.Current.LocalFolder.Path + "\juegos0")
            End If

            If File.Exists(ApplicationData.Current.LocalFolder.Path + "\juegos1") Then
                File.Delete(ApplicationData.Current.LocalFolder.Path + "\juegos1")
            End If

            If Directory.Exists(ApplicationData.Current.LocalFolder.Path + "\Cache") = True Then
                Dim carpetaImagenes As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path + "\Cache")

                If Not carpetaImagenes Is Nothing Then
                    Await carpetaImagenes.DeleteAsync
                End If
            End If

            Dim listaJuegos As New List(Of Tile)
            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Tile))("juegos0", listaJuegos)
            Await helper.SaveFileAsync(Of List(Of Tile))("juegos1", listaJuegos)

            Steam.Generar(False)

            pr.Visibility = Visibility.Collapsed

        End Sub

        Public Sub Estado(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim ts As ToggleSwitch = pagina.FindName("tsConfigCache")
            ts.IsEnabled = estado

            Dim botonLimpiar As Button = pagina.FindName("botonConfigLimpiarCache")
            botonLimpiar.IsEnabled = estado

        End Sub

    End Module
End Namespace