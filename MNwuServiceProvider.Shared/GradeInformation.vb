''' <summary>
''' 成绩信息
''' </summary>
Public Class GradeInformation
    Public Sub New(course As String, grade As Integer)
        Me.Course = course
        Me.Grade = grade
    End Sub
    ''' <summary>
    ''' 课程名称
    ''' </summary>
    Public Property Course As String
    ''' <summary>
    ''' 分数
    ''' </summary>
    Public Property Grade As Integer
End Class
