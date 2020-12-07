Imports Windows.UI

Namespace Interfaz
    Module NavigationViewItems

        Public Function Generar(titulo As String, simbolo As FontAwesome5.EFontAwesomeIcon)

            Dim tb As New TextBlock With {
                .Text = titulo,
                .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            }

            Dim item As New NavigationViewItem With {
                .Content = tb,
                .Foreground = New SolidColorBrush(Colors.White)
            }

            If Not simbolo = Nothing Then
                Dim icono As New FontAwesome5.FontAwesome With {
                    .Icon = simbolo,
                    .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                }

                item.Icon = icono
            End If

            Dim tbToolTip As TextBlock = New TextBlock With {
                .Text = titulo
            }

            ToolTipService.SetToolTip(item, tbToolTip)
            ToolTipService.SetPlacement(item, PlacementMode.Mouse)

            If Not simbolo = Nothing Then
                AddHandler item.PointerEntered, AddressOf Entra_NVItem_Icono
                AddHandler item.PointerExited, AddressOf Sale_NVItem_Icono
            Else
                AddHandler item.PointerEntered, AddressOf Entra_NVItem_Texto
                AddHandler item.PointerExited, AddressOf Sale_NVItem_Texto
            End If

            Return item

        End Function

    End Module
End Namespace

