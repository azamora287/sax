﻿Imports System.Drawing
Imports System.Web.Script.Serialization
Imports System.Web.UI.WebControls
Imports Wma.Exceptions

Public Class UIControl
    Inherits CompositeControl
    Implements IUIControl

#Region "Atributos"

    Private worksWithAttribute_ As [Enum]

#End Region

#Region "Propiedades"

    Public Property Language As IUIControl.Languages Implements IUIControl.Language

    Public Property Label As String Implements IUIControl.Label

    Public Property IdPermiso As Integer Implements IUIControl.IdPermiso

    Public Property WorksWith As [Enum] Implements IUIControl.WorksWith

    Public Property ToolTipExpireTime As Integer Implements IUIControl.ToolTipExpireTime

        Get

            Return ViewState("ToolTipExpireTime")

        End Get

        Set(value As Integer)

            ViewState("ToolTipExpireTime") = value

        End Set

    End Property

    Public Property ToolTipStatus As IUIControl.ToolTipTypeStatus Implements IUIControl.ToolTipStatus

        Get

            Return ViewState("ToolTipStatus")

        End Get

        Set(value As IUIControl.ToolTipTypeStatus)

            ViewState("ToolTipStatus") = value

        End Set

    End Property
    Public Property ToolTipModality As IUIControl.ToolTipModalities Implements IUIControl.ToolTipModality

        Get

            Return ViewState("ToolTipModality")

        End Get

        Set(value As IUIControl.ToolTipModalities)

            ViewState("ToolTipModality") = value

        End Set

    End Property

    Private Property ToolTipIsVisible As Boolean Implements IUIControl.ToolTipIsVisible

        Get

            Return ViewState("ToolTipIsVisible")

        End Get

        Set(value As Boolean)

            ViewState("ToolTipIsVisible") = value

        End Set

    End Property

    Public Overrides Property ToolTip As String

        Get

            Return MyBase.ToolTip

        End Get

        Set(value As String)

            Dim value_ As String = value

            If value_ IsNot Nothing Then

                If Not String.IsNullOrEmpty(value_) And value_.Length > 250 Then

                    value_ = value_.Substring(0, 249)

                End If

            End If

            MyBase.ToolTip = value_

        End Set

    End Property


#End Region

#Region "Constructores"

    Sub New()

        ToolTipModality = IUIControl.ToolTipModalities.Classic

        ToolTipStatus = IUIControl.ToolTipTypeStatus.OkInfo

        ToolTipExpireTime = 0

        ToolTipIsVisible = False

    End Sub

#End Region

#Region "Metodos"

    Public Sub ShowToolTip()

        ToolTipIsVisible = True

    End Sub

    Public Function GetToolTipSetting() As String

        If Not String.IsNullOrWhiteSpace(ToolTip) Then

            Dim setting As New Dictionary(Of String, String)

            With setting

                .Add("message", ToolTip)

                .Add("expiretime", ToolTipExpireTime)

                .Add("status", ToolTipStatus)

                .Add("mode", ToolTipModality)

                .Add("visible", ToolTipIsVisible)

            End With

            If ToolTipIsVisible = True Then

                ToolTipIsVisible = False

            End If

            'If Not ToolTipModality = IUIControl.ToolTipModalities.Classic Then

            '    ToolTip = Nothing

            'End If

            Return New JavaScriptSerializer().Serialize(setting)

        End If

        Return Nothing

    End Function

#End Region

    'conciderar la posibilidad de un evento de clic

End Class



Public Module ColorExtensions

    <System.Runtime.CompilerServices.Extension()>
    Public Function HtmlPropertyColor(c As Color) As String

        If Not c = Color.Empty Then

            Dim hexColor = c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2")

            Return "style='--tintColor: #" & hexColor & "'"

        End If

        Return ""

    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function ToHex(c As Color) As String

        If Not c = Color.Empty Then

            Return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2")

        End If

        Return ""

    End Function

End Module