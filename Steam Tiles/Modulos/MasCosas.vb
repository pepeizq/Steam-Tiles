Imports FontAwesome.UWP
Imports Microsoft.Services.Store.Engagement
Imports Windows.System
Imports Windows.UI.Core

Module MasCosas

    Public Function Generar(codigoFuente As String, traduccion As String)

        Dim recursos As New Resources.ResourceLoader()

        Dim iconoMasCosas As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.CaretDown,
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

        AddHandler itemMasCosas.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler itemMasCosas.PointerExited, AddressOf UsuarioSaleBoton

        '---------------------------------------------

        Dim menu As New MenuFlyout With {
            .Placement = FlyoutPlacementMode.Top,
            .ShowMode = FlyoutShowMode.Transient
        }

        Dim iconoMasApps As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.Cube
        }

        Dim menuItemMasApps As New MenuFlyoutItem With {
            .Text = "pepeizqapps.com",
            .Icon = iconoMasApps
        }

        AddHandler menuItemMasApps.Click, AddressOf MenuItemMasAppsClick
        AddHandler menuItemMasApps.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemMasApps.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemMasApps)

        Dim iconoDeals As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.Cube
        }

        Dim menuItemDeals As New MenuFlyoutItem With {
            .Text = "pepeizqdeals.com",
            .Icon = iconoDeals
        }

        AddHandler menuItemDeals.Click, AddressOf MenuItemDealsClick
        AddHandler menuItemDeals.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemDeals.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemDeals)

        menu.Items.Add(New MenuFlyoutSeparator)

        Dim iconoVotar As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.ThumbsOutlineUp
        }

        Dim menuItemVotar As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_VoteApp"),
            .Icon = iconoVotar
        }

        AddHandler menuItemVotar.Click, AddressOf MenuItemVotarClick
        AddHandler menuItemVotar.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemVotar.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemVotar)

        Dim iconoContacto As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.CommentOutline
        }

        Dim menuItemContacto As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_Contact"),
            .Icon = iconoContacto
        }

        AddHandler menuItemContacto.Click, AddressOf MenuItemContactoClick
        AddHandler menuItemContacto.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemContacto.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemContacto)

        Dim iconoReportar As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.Bug
        }

        Dim menuItemReportar As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_ReportBug"),
            .Icon = iconoReportar
        }

        AddHandler menuItemReportar.Click, AddressOf MenuItemReportarClick
        AddHandler menuItemReportar.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemReportar.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemReportar)

        If Not traduccion = Nothing Then
            Dim iconoTraducir As New FontAwesome.UWP.FontAwesome With {
                .Icon = FontAwesomeIcon.Globe
            }

            Dim menuItemTraducir As New MenuFlyoutItem With {
                .Text = recursos.GetString("MoreThings_HelpTranslate"),
                .Icon = iconoTraducir,
                .Tag = traduccion
            }

            AddHandler menuItemTraducir.Click, AddressOf MenuItemTraducirClick
            AddHandler menuItemTraducir.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler menuItemTraducir.PointerExited, AddressOf UsuarioSaleBoton

            menu.Items.Add(New MenuFlyoutSeparator)
            menu.Items.Add(menuItemTraducir)
        End If

        If Not codigoFuente = Nothing Then
            If traduccion = Nothing Then
                menu.Items.Add(New MenuFlyoutSeparator)
            End If

            Dim iconoCodigoFuente As New FontAwesome.UWP.FontAwesome With {
                .Icon = FontAwesomeIcon.Github
            }

            Dim menuItemCodigoFuente As New MenuFlyoutItem With {
                .Text = recursos.GetString("MoreThings_SourceCode"),
                .Icon = iconoCodigoFuente,
                .Tag = codigoFuente
            }

            AddHandler menuItemCodigoFuente.Click, AddressOf MenuItemCodigoFuenteClick
            AddHandler menuItemCodigoFuente.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler menuItemCodigoFuente.PointerExited, AddressOf UsuarioSaleBoton

            menu.Items.Add(menuItemCodigoFuente)
        End If

        FlyoutBase.SetAttachedFlyout(itemMasCosas, menu)

        Return itemMasCosas

    End Function

    Private Async Sub MenuItemVotarClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

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

        If StoreServicesFeedbackLauncher.IsSupported = True Then
            Dim ejecutador As StoreServicesFeedbackLauncher = StoreServicesFeedbackLauncher.GetDefault()
            Await ejecutador.LaunchAsync()
        Else
            Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/contact/"))
        End If

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

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module