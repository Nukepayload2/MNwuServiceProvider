Imports MNwuServiceProvider.MURPMainService
Imports MNwuServiceProvider.MURPNewsService

''' <summary>
''' 缓存 Murp 服务的实例。
''' </summary>
Public Class Services

    Private Shared s_MURPMainService As MURPMainServiceSoapClient
    ''' <summary>
    ''' Murp 主要服务。包括登录等。这些服务通常不需要凭据。
    ''' </summary>
    Public Shared ReadOnly Property MURPMainService As MURPMainServiceSoapClient
        Get
            If s_MURPMainService Is Nothing Then
                s_MURPMainService = New MURPMainServiceSoapClient
            End If
            Return s_MURPMainService
        End Get
    End Property

    Private Shared s_MURPNewsService As MURPNewsServiceSoapClient
    ''' <summary>
    ''' Murp 信息服务。通常需要凭据。
    ''' </summary>
    Public Shared ReadOnly Property MURPNewsService As MURPNewsServiceSoapClient
        Get
            If s_MURPNewsService Is Nothing Then
                s_MURPNewsService = New MURPNewsServiceSoapClient
            End If
            Return s_MURPNewsService
        End Get
    End Property
End Class
