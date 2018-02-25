Imports Windows.UI
Imports Windows.UI.Core

Module NavigationViewItems

    Public Function Generar(titulo As String, simbolo As SymbolIcon, tag As String)

        Dim tb As New TextBlock With {
            .Text = titulo,
            .Foreground = New SolidColorBrush(Colors.White)
        }

        Dim item As New NavigationViewItem With {
            .Content = tb,
            .Icon = simbolo,
            .Foreground = New SolidColorBrush(Colors.White),
            .Tag = tag
        }

        Dim tbToolTip As TextBlock = New TextBlock With {
            .Text = titulo
        }

        ToolTipService.SetToolTip(item, tbToolTip)
        ToolTipService.SetPlacement(item, PlacementMode.Mouse)

        AddHandler item.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler item.PointerExited, AddressOf UsuarioSaleBoton

        Return item

    End Function

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module
