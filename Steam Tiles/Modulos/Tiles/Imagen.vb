Imports Windows.Graphics.Imaging
Imports Windows.Storage

Namespace Tiles
    Module Imagen

        Public Async Function Generar(gridImagen As Grid, clave As String, ancho As Integer, alto As Integer) As Task(Of Boolean)

            Dim descargaFinalizada As Boolean = False

            Dim carpetaInstalacion As StorageFolder = ApplicationData.Current.LocalFolder
            Dim ficheroImagen As StorageFile = Await carpetaInstalacion.CreateFileAsync(clave, CreationCollisionOption.ReplaceExisting)

            Dim resultado As New RenderTargetBitmap()
            Await resultado.RenderAsync(gridImagen, ancho, alto)

            Dim buffer As Streams.IBuffer = Await resultado.GetPixelsAsync
            Dim pixeles As Byte() = buffer.ToArray
            Dim rawdpi As DisplayInformation = DisplayInformation.GetForCurrentView()

            Using stream As Streams.IRandomAccessStream = Await ficheroImagen.OpenAsync(FileAccessMode.ReadWrite)
                Dim encoder As BitmapEncoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream)
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, resultado.PixelWidth, resultado.PixelHeight, rawdpi.RawDpiX, rawdpi.RawDpiY, pixeles)

                Await encoder.FlushAsync
            End Using

            Return descargaFinalizada
        End Function

    End Module
End Namespace

