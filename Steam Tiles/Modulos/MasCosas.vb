Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Windows.Services.Store
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI.Core

Module MasCosas

    Public Function Generar(codigoFuente As String, traduccion As String, video As String)

        Dim recursos As New Resources.ResourceLoader()

        Dim iconoMasCosas As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Solid_CaretDown,
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
        }

        Dim tbMasCosas As New TextBlock With {
            .Text = recursos.GetString("MoreThings"),
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
        }

        Dim itemMasCosas As New NavigationViewItem With {
            .Icon = iconoMasCosas,
            .Content = tbMasCosas
        }

        Dim tbToolTip As TextBlock = New TextBlock With {
            .Text = recursos.GetString("MoreThings")
        }

        ToolTipService.SetToolTip(itemMasCosas, tbToolTip)
        ToolTipService.SetPlacement(itemMasCosas, PlacementMode.Mouse)

        AddHandler itemMasCosas.PointerEntered, AddressOf UsuarioEntraBotonNVItem
        AddHandler itemMasCosas.PointerExited, AddressOf UsuarioSaleBotonNVItem

        '---------------------------------------------

        Dim menu As New MenuFlyout With {
            .Placement = FlyoutPlacementMode.Top,
            .ShowMode = FlyoutShowMode.Transient
        }

        Dim iconoMasApps As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Solid_Cube
        }

        Dim menuItemMasApps As New MenuFlyoutItem With {
            .Text = "pepeizqapps.com",
            .Icon = iconoMasApps
        }

        AddHandler menuItemMasApps.Click, AddressOf MenuItemMasAppsClick
        AddHandler menuItemMasApps.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemMasApps.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemMasApps)

        Dim iconoDeals As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Solid_Cube
        }

        Dim menuItemDeals As New MenuFlyoutItem With {
            .Text = "pepeizqdeals.com",
            .Icon = iconoDeals
        }

        AddHandler menuItemDeals.Click, AddressOf MenuItemDealsClick
        AddHandler menuItemDeals.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemDeals.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemDeals)

        menu.Items.Add(New MenuFlyoutSeparator)

        Dim iconoCalificar As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Regular_ThumbsUp
        }

        Dim menuItemCalificar As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_RateApp"),
            .Icon = iconoCalificar
        }

        AddHandler menuItemCalificar.Click, AddressOf MenuItemCalificarClick
        AddHandler menuItemCalificar.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemCalificar.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemCalificar)

        Dim iconoContacto As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Regular_Comment
        }

        Dim menuItemContacto As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_Contact"),
            .Icon = iconoContacto
        }

        AddHandler menuItemContacto.Click, AddressOf MenuItemContactoClick
        AddHandler menuItemContacto.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemContacto.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemContacto)

        Dim iconoReportar As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Solid_Bug
        }

        Dim menuItemReportar As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_ReportBug"),
            .Icon = iconoReportar
        }

        AddHandler menuItemReportar.Click, AddressOf MenuItemReportarClick
        AddHandler menuItemReportar.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemReportar.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemReportar)

        If Not traduccion = Nothing Then
            Dim iconoTraducir As New FontAwesome5.FontAwesome With {
                .Icon = FontAwesome5.EFontAwesomeIcon.Solid_Globe
            }

            Dim menuItemTraducir As New MenuFlyoutItem With {
                .Text = recursos.GetString("MoreThings_HelpTranslate"),
                .Icon = iconoTraducir,
                .Tag = traduccion
            }

            AddHandler menuItemTraducir.Click, AddressOf MenuItemTraducirClick
            AddHandler menuItemTraducir.PointerEntered, AddressOf UsuarioEntraBotonMFItem
            AddHandler menuItemTraducir.PointerExited, AddressOf UsuarioSaleBotonMFItem

            menu.Items.Add(New MenuFlyoutSeparator)
            menu.Items.Add(menuItemTraducir)
        End If

        If Not codigoFuente = Nothing Then
            If traduccion = Nothing Then
                menu.Items.Add(New MenuFlyoutSeparator)
            End If

            Dim iconoCodigoFuente As New FontAwesome5.FontAwesome With {
                .Icon = FontAwesome5.EFontAwesomeIcon.Brands_Github
            }

            Dim menuItemCodigoFuente As New MenuFlyoutItem With {
                .Text = recursos.GetString("MoreThings_SourceCode"),
                .Icon = iconoCodigoFuente,
                .Tag = codigoFuente
            }

            AddHandler menuItemCodigoFuente.Click, AddressOf MenuItemCodigoFuenteClick
            AddHandler menuItemCodigoFuente.PointerEntered, AddressOf UsuarioEntraBotonMFItem
            AddHandler menuItemCodigoFuente.PointerExited, AddressOf UsuarioSaleBotonMFItem

            menu.Items.Add(menuItemCodigoFuente)
        End If

        If Not video = Nothing Then
            If traduccion = Nothing Then
                If codigoFuente = Nothing Then
                    menu.Items.Add(New MenuFlyoutSeparator)
                End If
            End If

            Dim iconoVideo As New FontAwesome5.FontAwesome With {
                .Icon = FontAwesome5.EFontAwesomeIcon.Brands_Youtube
            }

            Dim menuItemVideo As New MenuFlyoutItem With {
                .Text = recursos.GetString("MoreThings_Video"),
                .Icon = iconoVideo,
                .Tag = video
            }

            AddHandler menuItemVideo.Click, AddressOf MenuItemVideoClick
            AddHandler menuItemVideo.PointerEntered, AddressOf UsuarioEntraBotonMFItem
            AddHandler menuItemVideo.PointerExited, AddressOf UsuarioSaleBotonMFItem

            menu.Items.Add(menuItemVideo)
        End If

        FlyoutBase.SetAttachedFlyout(itemMasCosas, menu)

        Return itemMasCosas

    End Function

    Private Sub MenuItemCalificarClick(sender As Object, e As RoutedEventArgs)

        CalificarApp(False)

    End Sub

    Public Async Sub CalificarApp(primeraVez As Boolean)

        Dim recursos As New Resources.ResourceLoader()

        Dim usuarios As IReadOnlyList(Of User) = Await User.FindAllAsync

        If Not usuarios Is Nothing Then
            If usuarios.Count > 0 Then
                Dim usuario As User = usuarios(0)

                Dim contexto As StoreContext = StoreContext.GetForUser(usuario)
                Dim config As ApplicationDataContainer = ApplicationData.Current.LocalSettings

                Dim sacarVentana As Boolean = True

                If primeraVez = True Then
                    If config.Values("Calificar_App") = 1 Then
                        sacarVentana = False
                    End If
                End If

                If sacarVentana = True Then
                    Dim review As StoreRateAndReviewResult = Await contexto.RequestRateAndReviewAppAsync

                    If review.Status = StoreRateAndReviewStatus.Succeeded Then
                        Notificaciones.Toast(recursos.GetString("MoreThings_RateAppThanks"), Nothing)
                        config.Values("Calificar_App") = 1
                    ElseIf review.Status = StoreRateAndReviewStatus.Error Then
                        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))
                        config.Values("Calificar_App") = 1
                    Else
                        config.Values("Calificar_App") = 0
                    End If
                End If
            End If
        End If

    End Sub

    Private Async Sub MenuItemMasAppsClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/"))

    End Sub

    Private Async Sub MenuItemDealsClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/"))

    End Sub

    Private Async Sub MenuItemContactoClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/contact/"))

    End Sub

    Private Async Sub MenuItemReportarClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/contact/"))

    End Sub

    Private Async Sub MenuItemTraducirClick(sender As Object, e As RoutedEventArgs)

        Dim item As MenuFlyoutItem = sender
        Dim enlace As String = item.Tag

        If Not enlace = String.Empty Then
            Await Launcher.LaunchUriAsync(New Uri(enlace))
        End If

    End Sub

    Private Async Sub MenuItemCodigoFuenteClick(sender As Object, e As RoutedEventArgs)

        Dim item As MenuFlyoutItem = sender
        Dim enlace As String = item.Tag

        If Not enlace = String.Empty Then
            Await Launcher.LaunchUriAsync(New Uri(enlace))
        End If

    End Sub

    Private Async Sub MenuItemVideoClick(sender As Object, e As RoutedEventArgs)

        Dim item As MenuFlyoutItem = sender
        Dim enlace As String = item.Tag

        If Not enlace = String.Empty Then
            Await Launcher.LaunchUriAsync(New Uri(enlace))
        End If

    End Sub

    Private Sub UsuarioEntraBotonNVItem(sender As Object, e As PointerRoutedEventArgs)

        Dim item As NavigationViewItem = sender
        Dim icono As FontAwesome5.FontAwesome = item.Icon
        icono.Saturation(1).Scale(1.2, 1.2, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBotonNVItem(sender As Object, e As PointerRoutedEventArgs)

        Dim item As NavigationViewItem = sender
        Dim icono As FontAwesome5.FontAwesome = item.Icon
        icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    Private Sub UsuarioEntraBotonMFItem(sender As Object, e As PointerRoutedEventArgs)

        Dim item As MenuFlyoutItem = sender
        Dim icono As FontAwesome5.FontAwesome = item.Icon
        icono.Saturation(1).Scale(1.1, 1.1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBotonMFItem(sender As Object, e As PointerRoutedEventArgs)

        Dim item As MenuFlyoutItem = sender
        Dim icono As FontAwesome5.FontAwesome = item.Icon
        icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module