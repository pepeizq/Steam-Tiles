Imports Windows.Web.Syndication

Module RSS

    Dim listaFeeds As List(Of FeedRSS)
    Dim listaFeedsView As List(Of FeedRSS)
    Dim WithEvents bw As New BackgroundWorker

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim listaView As ListView = pagina.FindName("lvRSS")

        Try
            listaView.Items.Clear()
        Catch ex As Exception

        End Try

        If listaView.Items.Count > 0 Then
            listaFeedsView = New List(Of FeedRSS)
            listaFeedsView = listaView.ItemsSource
        End If

        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Async Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        listaFeeds = New List(Of FeedRSS)

        Dim cliente As SyndicationClient = New SyndicationClient
        Dim enlace As Uri = New Uri("https://pepeizqapps.com/feed/")

        cliente.SetRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)")
        cliente.BypassCacheOnRetrieve = True

        Dim feeds As SyndicationFeed = New SyndicationFeed

        Try
            feeds = Await cliente.RetrieveFeedAsync(enlace)
        Catch ex As Exception

        End Try

        Dim i As Integer = 0

        If feeds.Items.Count > 0 Then
            For Each feed In feeds.Items
                Dim feedUri As String = feed.CommentsUri.ToString
                feedUri = feedUri.Replace("#respond", Nothing)

                i += 1

                If i < 6 Then
                    Dim tituloBool As Boolean = False
                    Dim k As Integer = 0
                    While k < listaFeeds.Count
                        If listaFeeds(k).Enlace.AbsolutePath = feedUri Then
                            tituloBool = True
                        End If
                        k += 1
                    End While

                    If Not listaFeedsView Is Nothing Then
                        k = 0
                        While k < listaFeedsView.Count
                            If listaFeedsView(k).Enlace.AbsolutePath = feedUri Then
                                tituloBool = True
                            End If
                            k += 1
                        End While
                    End If

                    If tituloBool = False Then
                        If Not feed.Title.Text.Trim = Nothing Then
                            Dim rss As New FeedRSS(feed.Title.Text.Trim, New Uri(feedUri))
                            listaFeeds.Add(rss)
                        End If
                    End If
                End If
            Next
        End If

    End Sub

    Private Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        If listaFeeds.Count > 0 Then
            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim listaView As ListView = pagina.FindName("lvRSS")
            listaView.ItemsSource = listaFeeds
        Else
            bw.RunWorkerAsync()
        End If

    End Sub

End Module
