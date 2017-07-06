Class MainWindow
    Private Sub Hyperlink_Click(sender As Object, e As RoutedEventArgs) Handles LnkPrivacy.Click, LnkGitHub.Click, LnkMail.Click
        Process.Start(sender.NavigateUri.AbsoluteUri)
    End Sub

    Private Async Sub BtnConsult_ClickAsync(sender As Object, e As RoutedEventArgs) Handles BtnConsult.Click
        Await GetGrades(Function(stu) stu.GetMarks)
    End Sub

    Private Async Sub BtnFullConsult_ClickAsync(sender As Object, e As RoutedEventArgs) Handles BtnFullConsult.Click
        Await GetGrades(Function(stu) stu.GetGradeInformation)
    End Sub

    Private Async Function GetGrades(markCallback As Func(Of Student, Task(Of GradeInformation()))) As Task
        Try
            TblError.Visibility = Visibility.Collapsed
            BtnConsult.IsEnabled = False
            TblRealName.Visibility = Visibility.Collapsed
            Dim stu = Await Student.CreateAsync(TxtStudentId.Text)
            If Not String.IsNullOrEmpty(stu.RealName) Then
                RunRealName.Text = stu.RealName
                TblRealName.Visibility = Visibility.Visible
            End If
            Dim grade = Await markCallback(stu)
            DatData.ItemsSource = grade
        Catch ex As Exception
            TblError.Text = ex.Message
            TblError.Visibility = Visibility.Visible
        Finally
            BtnConsult.IsEnabled = True
        End Try
    End Function
End Class
