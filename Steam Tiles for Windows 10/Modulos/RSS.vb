Imports Windows.Web.Syndication

Module RSS

    Dim listaFeeds As List(Of FeedRSS)
    Dim WithEvents bw As BackgroundWorker

    Public Sub Generar()

        listaFeeds = New List(Of FeedRSS)

        bw = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Async Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        Dim cliente As SyndicationClient = New SyndicationClient
        Dim enlace As Uri = New Uri("https://pepeizqapps.com/feed/")

        cliente.SetRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)")

        Dim feeds As SyndicationFeed = New SyndicationFeed
        feeds = Await cliente.RetrieveFeedAsync(enlace)

        Dim i As Integer = 0

        For Each feed In feeds.Items
            Dim feedUri As String = feed.CommentsUri.ToString
            feedUri = feedUri.Replace("#respond", Nothing)

            i += 1

            If i < 6 Then
                Dim rss As New FeedRSS(feed.Title.Text, New Uri(feedUri))
                listaFeeds.Add(rss)
            End If
        Next

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
