''' <summary>
''' 代表学生。与学生相关的功能需要这个类。
''' </summary>
Public Class Student

    ''' <param name="mcid">使用学生的索引查询信息。</param>
    Public Sub New(mcid As String)
        Me.Mcid = mcid
    End Sub

    ''' <param name="mcid">使用学生的索引查询信息。</param>
    ''' <param name="realName">真实姓名。</param>
    Public Sub New(mcid As String, realName As String)
        Me.New(mcid)
        Me.RealName = realName
    End Sub

    ''' <summary>
    ''' 学生的索引。
    ''' </summary>
    Public ReadOnly Property Mcid As String

    ''' <summary>
    ''' 学生的真实姓名。
    ''' </summary>
    Public ReadOnly Property RealName As String

    ''' <summary>
    ''' 使用非加密的方式向服务器发生学生的凭据，创建学生的新实例。
    ''' </summary>
    ''' <param name="studentId">学号。长度是 10，纯数字。</param>
    ''' <returns>学生的新实例</returns>
    Public Shared Async Function CreateAsync(studentId As String) As Task(Of Student)
        If studentId Is Nothing OrElse
            studentId.Length <> 10 OrElse
            Not studentId.All(Function(ch) Char.IsDigit(ch)) Then
            Throw New ArgumentException("请注意格式: 学号长度是 10，纯数字。", NameOf(studentId))
        End If
        Dim infService = Services.MURPNewsService
        Dim resp = Await infService.SearchUserAsync(studentId, Nothing, Nothing)
        Dim loginData = resp.Body.SearchUserResult.userinfo
        If loginData Is Nothing Then
            Throw New LoginException("学生 Id 可能不正确。")
        End If
        Dim userInfo = loginData.Single
        Return New Student(userInfo.mcid, userInfo.customname)
    End Function

    ''' <summary>
    ''' 获取当前学生的完整成绩信息。特别慢。
    ''' </summary>
    Public Async Function GetGradeInformation() As Task(Of GradeInformation())
        Dim infService = Services.MURPNewsService
        Dim resp = Await infService.GetMyGradesAsync(Mcid)
        Dim grades = resp.Body.GetMyGradesResult
        Return Aggregate g In grades Select New GradeInformation(g.Extend2, g.Extend3) Into ToArray
    End Function

    ''' <summary>
    ''' 获取当前学生的当前学期成绩信息。
    ''' </summary>
    Public Async Function GetMarks() As Task(Of GradeInformation())
        Dim infService = Services.MURPNewsService
        Dim resp = Await infService.MarkAsync(Mcid)
        Dim grades = resp.Body.MarkResult
        Return Aggregate g In grades Select New GradeInformation(g.Extend1, g.Extend2) Into ToArray
    End Function
End Class
