﻿'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.510
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by wsdl, Version=1.1.4322.510.
'

'<remarks/>
<System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Web.Services.WebServiceBindingAttribute(Name:="Service1Soap", [Namespace]:="http://tempuri.org/WebService1/Service1")>  _
Public Class Service1
    Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
    
    '<remarks/>
    Public Sub New()
        MyBase.New
        Me.Url = "http://localhost/northwind/service1.asmx"
    End Sub
    
    '<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WebService1/Service1/GetAllEmployees", RequestNamespace:="http://tempuri.org/WebService1/Service1", ResponseNamespace:="http://tempuri.org/WebService1/Service1", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function GetAllEmployees() As System.Data.DataSet
        Dim results() As Object = Me.Invoke("GetAllEmployees", New Object(-1) {})
        Return CType(results(0),System.Data.DataSet)
    End Function
    
    '<remarks/>
    Public Function BeginGetAllEmployees(ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("GetAllEmployees", New Object(-1) {}, callback, asyncState)
    End Function
    
    '<remarks/>
    Public Function EndGetAllEmployees(ByVal asyncResult As System.IAsyncResult) As System.Data.DataSet
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),System.Data.DataSet)
    End Function
    
    '<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/WebService1/Service1/GetEmployeeDetails", RequestNamespace:="http://tempuri.org/WebService1/Service1", ResponseNamespace:="http://tempuri.org/WebService1/Service1", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function GetEmployeeDetails(ByVal EmployeeID As Integer) As structEmployee
        Dim results() As Object = Me.Invoke("GetEmployeeDetails", New Object() {EmployeeID})
        Return CType(results(0),structEmployee)
    End Function
    
    '<remarks/>
    Public Function BeginGetEmployeeDetails(ByVal EmployeeID As Integer, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("GetEmployeeDetails", New Object() {EmployeeID}, callback, asyncState)
    End Function
    
    '<remarks/>
    Public Function EndGetEmployeeDetails(ByVal asyncResult As System.IAsyncResult) As structEmployee
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),structEmployee)
    End Function
End Class

'<remarks/>
<System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tempuri.org/WebService1/Service1")>  _
Public Class structEmployee
    
    '<remarks/>
    Public EmployeeID As Integer
    
    '<remarks/>
    Public LastName As String
    
    '<remarks/>
    Public FirstName As String
    
    '<remarks/>
    Public Title As String
    
    '<remarks/>
    Public TitleOfCourtesy As String
    
    '<remarks/>
    Public BirthDate As Date
    
    '<remarks/>
    Public HireDate As Date
    
    '<remarks/>
    Public Address As String
    
    '<remarks/>
    Public City As String
    
    '<remarks/>
    Public [Region] As String
    
    '<remarks/>
    Public PostalCode As String
    
    '<remarks/>
    Public Country As String
    
    '<remarks/>
    Public HomePhone As String
    
    '<remarks/>
    Public Extension As String
    
    '<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")>  _
    Public Photo() As Byte
    
    '<remarks/>
    Public Notes As String
    
    '<remarks/>
    Public ReportsTo As Integer
    
    '<remarks/>
    Public ReportsToFirstName As String
    
    '<remarks/>
    Public ReportsToLastName As String
    
    '<remarks/>
    Public PhotoPath As String
    
    '<remarks/>
    Public Territories() As String
End Class
