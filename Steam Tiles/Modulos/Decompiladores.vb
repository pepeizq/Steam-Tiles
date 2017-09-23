Imports Windows.Web.Http

Module Decompiladores

    Public Async Function HttpClient(url As Uri) As Task(Of String)

        Dim cliente As New HttpClient()
        Dim httpFinal As String = Nothing

        cliente.DefaultRequestHeaders.Add("user-agent", "Chrome/45.0.2454.93")

        Try
            Dim respuesta As New HttpResponseMessage
            respuesta = Await cliente.GetAsync(url)
            respuesta.EnsureSuccessStatusCode()

            httpFinal = TryCast(Await respuesta.Content.ReadAsStringAsync(), String)
        Catch ex As Exception

        End Try

        cliente.Dispose()
        Return httpFinal
    End Function

End Module
