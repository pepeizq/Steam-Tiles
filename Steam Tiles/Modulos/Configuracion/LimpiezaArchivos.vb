Imports Windows.ApplicationModel.Core
Imports Windows.Storage

Namespace Configuracion
    Module LimpiezaArchivos

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonConfigLimpiarArchivos As Button = pagina.FindName("botonConfigLimpiarArchivos")

            AddHandler botonConfigLimpiarArchivos.Click, AddressOf Limpiar
            AddHandler botonConfigLimpiarArchivos.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_1_05
            AddHandler botonConfigLimpiarArchivos.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_1_05

        End Sub

        Private Async Sub Limpiar(sender As Object, e As RoutedEventArgs)

            Estado(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim pr As ProgressRing = pagina.FindName("prConfigLimpiarArchivos")
            pr.Visibility = Visibility.Visible

            Dim carpeta As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path)
            Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync

            For Each fichero In ficheros
                If fichero.FileType.Contains("png") Then
                    Await fichero.DeleteAsync()
                End If

                If fichero.Name.ToLower.Contains("juegos") Then
                    Await fichero.DeleteAsync()
                End If
            Next

            pr.Visibility = Visibility.Collapsed
            Await CoreApplication.RequestRestartAsync(AppRestartFailureReason.Other)

            Estado(True)

        End Sub

        Public Sub Estado(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonConfigLimpiarArchivos As Button = pagina.FindName("botonConfigLimpiarArchivos")
            botonConfigLimpiarArchivos.IsEnabled = estado

        End Sub

    End Module
End Namespace