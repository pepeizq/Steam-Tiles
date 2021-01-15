Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.UI.Core
Imports Windows.UI.Xaml.Shapes

Namespace Interfaz
    Module EfectosHover

        Public Sub Entra_Basico(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Basico(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_IconoTexto(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim sp As StackPanel = boton.Content

            Dim icono As FontAwesome5.FontAwesome = sp.Children(0)
            icono.Saturation(1).Scale(1.1, 1.1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Dim texto As TextBlock = sp.Children(1)
            texto.Saturation(1).Scale(1.1, 1.1, texto.ActualWidth / 2, texto.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_IconoTexto(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim sp As StackPanel = boton.Content

            Dim icono As FontAwesome5.FontAwesome = sp.Children(0)
            icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Dim texto As TextBlock = sp.Children(1)
            texto.Saturation(1).Scale(1, 1, texto.ActualWidth / 2, texto.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_Icono(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim icono As FontAwesome5.FontAwesome = boton.Content
            icono.Saturation(1).Scale(1.1, 1.1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_Icono(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim icono As FontAwesome5.FontAwesome = boton.Content
            icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_NVItem_Icono(sender As Object, e As PointerRoutedEventArgs)

            Dim item As NavigationViewItem = sender
            Dim icono As FontAwesome5.FontAwesome = item.Icon
            icono.Saturation(1).Scale(1.2, 1.2, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_NVItem_Icono(sender As Object, e As PointerRoutedEventArgs)

            Dim item As NavigationViewItem = sender
            Dim icono As FontAwesome5.FontAwesome = item.Icon
            icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_NVItem_Texto(sender As Object, e As PointerRoutedEventArgs)

            Dim item As NavigationViewItem = sender
            Dim tb As TextBlock = item.Content
            tb.Saturation(1).Scale(1.1, 1.1, tb.ActualWidth / 2, tb.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_NVItem_Texto(sender As Object, e As PointerRoutedEventArgs)

            Dim item As NavigationViewItem = sender
            Dim tb As TextBlock = item.Content
            tb.Saturation(1).Scale(1, 1, tb.ActualWidth / 2, tb.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_NVItem_Ellipse(sender As Object, e As PointerRoutedEventArgs)

            Dim item As NavigationViewItem = sender
            Dim sp As StackPanel = item.Content

            Dim icono As Ellipse = sp.Children(0)
            icono.Saturation(1).Scale(1.1, 1.1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_NVItem_Ellipse(sender As Object, e As PointerRoutedEventArgs)

            Dim item As NavigationViewItem = sender
            Dim sp As StackPanel = item.Content

            Dim icono As Ellipse = sp.Children(0)
            icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_Ellipse(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim icono As Ellipse = boton.Content
            icono.Saturation(1).Scale(1.1, 1.1, boton.ActualWidth / 2, boton.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_Ellipse(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim icono As Ellipse = boton.Content
            icono.Saturation(1).Scale(1, 1, boton.ActualWidth / 2, boton.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_Imagen(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim imagen As ImageEx = boton.Content
            imagen.Saturation(1).Scale(1.02, 1.02, imagen.ActualWidth / 2, imagen.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_Imagen(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim imagen As ImageEx = boton.Content
            imagen.Saturation(1).Scale(1, 1, imagen.ActualWidth / 2, imagen.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_MFItem_Icono(sender As Object, e As PointerRoutedEventArgs)

            Dim item As MenuFlyoutItem = sender
            Dim icono As FontAwesome5.FontAwesome = item.Icon
            icono.Saturation(1).Scale(1.2, 1.2, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_MFItem_Icono(sender As Object, e As PointerRoutedEventArgs)

            Dim item As MenuFlyoutItem = sender
            Dim icono As FontAwesome5.FontAwesome = item.Icon
            icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_Texto(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim tb As TextBlock = boton.Content
            tb.Saturation(1).Scale(1.1, 1.1, boton.ActualWidth / 2, boton.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_Texto(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim tb As TextBlock = boton.Content
            tb.Saturation(1).Scale(1, 1, boton.ActualWidth / 2, boton.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_ImagenTexto(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim sp As StackPanel = boton.Content

            Dim imagen As ImageEx = sp.Children(0)
            imagen.Saturation(1).Scale(1.02, 1.02, imagen.ActualWidth / 2, imagen.ActualHeight / 2).Start()

            Dim texto As TextBlock = sp.Children(1)
            texto.Saturation(1).Scale(1.1, 1.1, texto.ActualWidth / 2, texto.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_ImagenTexto(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim sp As StackPanel = boton.Content

            Dim imagen As ImageEx = sp.Children(0)
            imagen.Saturation(1).Scale(1, 1, imagen.ActualWidth / 2, imagen.ActualHeight / 2).Start()

            Dim texto As TextBlock = sp.Children(1)
            texto.Saturation(1).Scale(1, 1, texto.ActualWidth / 2, texto.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_IconoTexto_Grid(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim grid As Grid = boton.Content

            Dim texto As TextBlock = grid.Children(0)
            texto.Saturation(1).Scale(1.05, 1.05, texto.ActualWidth / 2, texto.ActualHeight / 2).Start()

            Dim icono As FontAwesome5.FontAwesome = grid.Children(1)
            icono.Saturation(1).Scale(1.2, 1.2, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_IconoTexto_Grid(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim grid As Grid = boton.Content

            Dim texto As TextBlock = grid.Children(0)
            texto.Saturation(1).Scale(1, 1, texto.ActualWidth / 2, texto.ActualHeight / 2).Start()

            Dim icono As FontAwesome5.FontAwesome = grid.Children(1)
            icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_GridIcono(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim grid As Grid = boton.Content

            Dim sp As StackPanel = grid.Children(0)
            Dim subgrid As Grid = sp.Children(0)
            subgrid.Saturation(1).Scale(1.02, 1.02, subgrid.ActualWidth / 2, subgrid.ActualHeight / 2).Start()

            Dim icono As FontAwesome5.FontAwesome = grid.Children(1)
            icono.Saturation(1).Scale(1.2, 1.2, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_GridIcono(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim grid As Grid = boton.Content

            Dim sp As StackPanel = grid.Children(0)
            Dim subgrid As Grid = sp.Children(0)
            subgrid.Saturation(1).Scale(1, 1, subgrid.ActualWidth / 2, subgrid.ActualHeight / 2).Start()

            Dim icono As FontAwesome5.FontAwesome = grid.Children(1)
            icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_1_05(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            boton.Saturation(1).Scale(1.05, 1.05, boton.ActualWidth / 2, boton.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_1_05(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            boton.Saturation(1).Scale(1, 1, boton.ActualWidth / 2, boton.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Public Sub Entra_Boton_1_02(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            boton.Saturation(1).Scale(1.02, 1.02, boton.ActualWidth / 2, boton.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Public Sub Sale_Boton_1_02(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            boton.Saturation(1).Scale(1, 1, boton.ActualWidth / 2, boton.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

    End Module
End Namespace