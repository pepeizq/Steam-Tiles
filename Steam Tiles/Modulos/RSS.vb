Imports Windows.Web.Syndication

Module RSS

    Dim boolUpdates As Boolean = False

    Dim listaFeeds As List(Of FeedRSS)
    Dim WithEvents bw As New BackgroundWorker

    Public Sub Generar()

        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Async Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        If boolUpdates = False Then
            listaFeeds = New List(Of FeedRSS)

            Dim cliente As SyndicationClient = New SyndicationClient
            Dim enlace As Uri = New Uri("http://steamcommunity.com/groups/pepeizqapps/rss/")
            Dim limite As Integer = 1

            cliente.SetRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)")

            Dim feeds As SyndicationFeed = New SyndicationFeed

            Try
                feeds = Await cliente.RetrieveFeedAsync(enlace)
            Catch ex As Exception

            End Try

            If feeds.Items.Count > 0 Then
                For Each feed In feeds.Items
                    Dim tituloBool As Boolean = False

                    If feed Is Nothing Then
                        tituloBool = True
                    End If

                    Dim feedUri As String = Nothing

                    Try
                        feedUri = feed.Links.First.Uri.AbsolutePath
                        feedUri = feedUri.Replace("#respond", Nothing)

                        If Not feedUri.Contains("http://steamcommunity.com") Then
                            feedUri = "http://steamcommunity.com" + feedUri
                        End If
                    Catch ex As Exception
                        tituloBool = True
                    End Try

                    Dim k As Integer = 0
                    While k < listaFeeds.Count
                        If feed.Title.Text.Trim = Nothing Then
                            tituloBool = True
                        Else
                            If Not listaFeeds(k) Is Nothing Then
                                If listaFeeds(k).Titulo.Trim = feed.Title.Text.Trim Then
                                    tituloBool = True
                                End If

                                If listaFeeds(k).Enlace = New Uri(feedUri) Then
                                    tituloBool = True
                                End If
                            End If
                        End If
                        k += 1
                    End While

                    If tituloBool = False Then
                        Dim rss As New FeedRSS(feed.Title.Text.Trim, New Uri(feedUri))
                        listaFeeds.Add(rss)
                    End If
                Next
            End If

            If listaFeeds.Count > limite Then
                listaFeeds.RemoveRange(limite, listaFeeds.Count - limite)
            End If
        End If

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        If listaFeeds.Count > 0 Then
            Dim sp As StackPanel = pagina.FindName("spRSS")
            sp.Visibility = Visibility.Visible

            Dim boton As Button = pagina.FindName("botonRSS")
            boton.Tag = listaFeeds(0).Enlace

            Dim botonTexto As TextBlock = pagina.FindName("botonRSSTexto")
            botonTexto.Text = listaFeeds(0).Titulo

            boolUpdates = True
        End If

        If boolUpdates = False Then
            Await Task.Delay(5000)
            bw.RunWorkerAsync()
        End If

    End Sub

End Module
