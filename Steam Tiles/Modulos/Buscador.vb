Imports Windows.Storage

Module Buscador

    Dim tienda As String

    Public Async Sub CargarTiles(juegoTexto As TextBlock, tienda_ As String)

        tienda = tienda_

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lvJuegos As ListView = pagina.FindName("lv" + tienda + "Juegos")
        lvJuegos.IsEnabled = False

        Dim prTiles As ProgressRing = pagina.FindName("prTiles" + tienda)
        prTiles.Visibility = Visibility.Visible

        Dim mensaje As TextBlock = pagina.FindName("tbSiJuegos" + tienda)
        mensaje.Visibility = Visibility.Collapsed

        Dim textoTitulo As String = juegoTexto.Text

        textoTitulo = textoTitulo.Replace(" ", "%20")

        Await WebView.ClearTemporaryWebDataAsync()

        Dim wb As New WebView With {
            .Tag = juegoTexto.Tag
            }

        AddHandler wb.NavigationCompleted, AddressOf Wb_NavigationCompleted

        wb.Navigate(New Uri("https://www.google.com/search?q=" + textoTitulo + "+grid&biw=1280&bih=886&source=lnms&tbm=isch&sa=X&ved=0ahUKEwjw8KHftrrRAhUN8GMKHdzFBQMQ_AUICCgB&gws_rd=cr,ssl&ei=H1J2WLa_FIPcjwSRvLSQCg"))

    End Sub

    Private Async Sub Wb_NavigationCompleted(sender As WebView, e As WebViewNavigationCompletedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gvTiles As GridView = pagina.FindName("gridViewTiles" + tienda)

        Try
            gvTiles.Items.Clear()
        Catch ex As Exception

        End Try

        Dim lista As New List(Of String) From {
            "document.documentElement.outerHTML;"
        }

        Dim argumentos As IEnumerable(Of String) = lista
        Dim html As String = Nothing

        Try
            html = Await sender.InvokeScriptAsync("eval", argumentos)
        Catch ex As Exception

        End Try

        Dim boolExito As Boolean = False
        Dim listaFinal As New List(Of Tile)

        Dim tope As Integer = 12

        If Not html = Nothing Then
            Dim i As Integer = 0
            While i < tope
                If html.Contains("<div class=" + ChrW(34) + "rg_meta" + ChrW(34) + ">") Then
                    Dim temp, temp2, temp3, temp4 As String
                    Dim int, int2, int3, int4 As Integer

                    int = html.IndexOf("<div class=" + ChrW(34) + "rg_meta" + ChrW(34) + ">")
                    temp = html.Remove(0, int + 5)

                    html = temp

                    int2 = temp.IndexOf("</div>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    If temp2.Contains(ChrW(34) + "ou" + ChrW(34)) Then
                        int3 = temp2.IndexOf(ChrW(34) + "ou" + ChrW(34))
                        temp3 = temp2.Remove(0, int3 + 6)

                        int4 = temp3.IndexOf(ChrW(34))
                        temp4 = temp3.Remove(int4, temp3.Length - int4)

                        Dim imagen As String = temp4.Trim

                        Dim codigo As ApplicationDataContainer = ApplicationData.Current.LocalSettings

                        If codigo.Values("codigo" + tienda) Is Nothing Then
                            codigo.Values("codigo" + tienda) = "0"
                        End If

                        Dim numCodigo As String = Integer.Parse(codigo.Values("codigo" + tienda)) + 1
                        codigo.Values("codigo" + tienda) = numCodigo

                        Dim juego As Tile = sender.Tag
                        Dim juego_ As New Tile(juego.Titulo, tienda + codigo.Values("codigo" + tienda), juego.Enlace, New Uri(imagen), tienda, juego)
                        listaFinal.Add(juego_)

                        boolExito = True
                    End If
                End If
                i += 1
            End While
        End If

        If boolExito = False Then
            Await WebView.ClearTemporaryWebDataAsync()

            Dim wb As New WebView With {
                .Tag = sender.Tag
            }

            AddHandler wb.NavigationCompleted, AddressOf Wb_NavigationCompleted

            Try
                wb.Navigate(New Uri("javascript:void((function(){var a,b,c,e,f;f=0;a=document.cookie.split('; ');for(e=0;e<    a.length&&a[e];e++){f++;for(b='.'+location.host;b;b=b.replace(/^(?:%5C.|[^%5C.]+)/,'')){for(    c=location.pathname;c;c=c.replace(/.$/,'')){document.cookie=(a[e]+'; domain='+b+'; path='+c+'; expires='+new Date((    new Date()).getTime()-1e11).toGMTString());}}}})())"))
            Catch ex As Exception

            End Try

            wb.Navigate(sender.Source)
        Else
            gvTiles.ItemsSource = listaFinal
            gvTiles.IsEnabled = True

            Dim lvJuegos As ListView = pagina.FindName("lv" + tienda + "Juegos")
            lvJuegos.IsEnabled = True

            Dim prTiles As ProgressRing = pagina.FindName("prTiles" + tienda)
            prTiles.Visibility = Visibility.Collapsed
        End If

    End Sub

End Module
