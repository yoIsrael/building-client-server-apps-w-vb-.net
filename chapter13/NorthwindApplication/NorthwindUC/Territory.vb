Option Strict On
Option Explicit On 

Imports NorthwindTraders.NorthwindShared.Structures
Imports NorthwindTraders.NorthwindShared.Interfaces
Imports BusinessRules.Errors
Imports NorthwindTraders.NorthwindShared
Imports NorthwindTraders.NorthwindShared.Errors

Public Class Territory
    Inherits BusinessBase

    Private Const LISTENER As String = "TerritoryDC.rem"
    Private msTerritory As structTerritory

    Private mblnNew As Boolean

#Region " Private Attributes"
    Private mstrTerritoryID As String = ""
    Private mstrTerritoryDescription As String = ""
    Private mobjRegion As New Region
#End Region

#Region " Public Attributes"

    Public Property TerritoryID() As String
        Get
            Return mstrTerritoryID
        End Get
        Set(ByVal Value As String)
            Try
                If Value Is Nothing Then
                    Throw New ArgumentNullException("Territory ID")
                End If

                If Value.Length = 0 Then
                    Throw New ZeroLengthException
                Else
                    If Value.Length > 20 Then
                        Throw New MaximumLengthException(20)
                    End If
                End If

                If mstrTerritoryID <> Value Then
                    mstrTerritoryID = Value
                    If Not Loading Then
                        mobjRules.BrokenRule("Territory ID", False)
                        mblnDirty = True
                    End If
                End If
            Catch exc As Exception
                mobjRules.BrokenRule("Territory ID", True)
                mstrTerritoryID = Value
                mblnDirty = True
                Throw exc
            End Try
        End Set
    End Property

    Public Property TerritoryDescription() As String
        Get
            Return mstrTerritoryDescription
        End Get
        Set(ByVal Value As String)
            Try
                If Value Is Nothing Then
                    Throw New ArgumentNullException("Territory Description")
                End If

                If Value.Length = 0 Then
                    Throw New ZeroLengthException
                Else
                    If Value.Length > 50 Then
                        Throw New MaximumLengthException(50)
                    End If
                End If

                If mstrTerritoryDescription <> Value Then
                    mstrTerritoryDescription = Value
                    If Not Loading Then
                        mobjRules.BrokenRule("Territory Description", False)
                        mblnDirty = True
                    End If
                End If
            Catch exc As Exception
                mobjRules.BrokenRule("Territory Description", True)
                mstrTerritoryDescription = Value
                mblnDirty = True
                Throw exc
            End Try
        End Set
    End Property

    Public Property Region() As Region
        Get
            Return mobjRegion
        End Get
        Set(ByVal Value As Region)
            Try
                If Value Is Nothing Then
                    Throw New ArgumentNullException("Region")
                End If

                If Not Value Is mobjRegion Then
                    mobjRegion = Value
                    If Not Loading Then
                        mobjRules.BrokenRule("Region", False)
                        mblnDirty = True
                    End If
                End If
            Catch exc As Exception
                mobjRules.BrokenRule("Region", True)
                mobjRegion = Value
                mblnDirty = True
                Throw exc
            End Try
        End Set
    End Property
#End Region

    Public Sub New()
        MyBase.New(LISTENER)
        mblnNew = True
        mobjRules.BrokenRule("Territory ID", True)
        mobjRules.BrokenRule("Territory Description", True)
        mobjRules.BrokenRule("Region", True)
    End Sub

    Public Sub New(ByVal strID As String)
        MyBase.New(LISTENER)
        mblnNew = False
        mstrTerritoryID = strID
    End Sub

    Public Overrides Function ToString() As String
        Return mstrTerritoryDescription
    End Function

    Public Sub LoadRecord()
        Dim objITerritory As ITerritory
        Dim sTerritory As structTerritory
        Dim objRegionMgr As RegionMgr

        objITerritory = CType(Activator.GetObject(GetType(ITerritory), _
        AppConstants.REMOTEOBJECTS & LISTENER), ITerritory)
        sTerritory = objITerritory.LoadRecord(mstrTerritoryID)
        objITerritory = Nothing

        With sTerritory
            Me.mstrTerritoryID = .TerritoryID
            Me.mstrTerritoryDescription = .TerritoryDescription.Trim
        End With

        objRegionMgr = objRegionMgr.GetInstance
        mobjRegion = objRegionMgr.Item(sTerritory.RegionID)

        sTerritory = Nothing
    End Sub

    Public Sub Delete()
        Dim objITerritory As ITerritory

        objITerritory = CType(Activator.GetObject(GetType(ITerritory), _
        AppConstants.REMOTEOBJECTS & LISTENER), ITerritory)
        objITerritory.Delete(mstrTerritoryID)
        objITerritory = Nothing
    End Sub

    Public Sub Save()
        If mobjRules.Count = 0 Then
            If mblnDirty = True Then
                Dim objITerritory As ITerritory
                Dim sTerritory As structTerritory

                With sTerritory
                    .TerritoryID = mstrTerritoryID
                    .TerritoryDescription = mstrTerritoryDescription
                    .RegionID = mobjRegion.RegionID
                    .IsNew = mblnNew
                End With

                objITerritory = _
                CType(Activator.GetObject(GetType(ITerritory), _
                AppConstants.REMOTEOBJECTS & LISTENER), ITerritory)

                objITerritory.Save(sTerritory)
                objITerritory = Nothing

                If mblnNew Then
                    mblnNew = False
                    CallChangedEvent(Me, New _
                    ChangedEventArgs(ChangedEventArgs.eChange.Added))
                Else
                    CallChangedEvent(Me, New _
                    ChangedEventArgs(ChangedEventArgs.eChange.Updated))
                End If
            End If
        End If
    End Sub
End Class

Public Class TerritoryMgr
    Inherits System.Collections.DictionaryBase

    Public Sub New(Optional ByVal blnLoad As Boolean = True)
        If blnLoad Then
            Load()
        End If
    End Sub

    Public Sub Add(ByVal obj As Territory)
        dictionary.Add(obj.TerritoryID, obj)
    End Sub

    Public Function Item(ByVal Key As Object) As Territory
        Return CType(dictionary.Item(Key), Territory)
    End Function

    Public Sub Remove(ByVal Key As Object)
        dictionary.Remove(Key)
    End Sub

    Private Sub Load()
        Dim objITerritory As ITerritory
        Dim dRow As DataRow
        Dim ds As DataSet
        Dim objRegionMgr As RegionMgr

        objITerritory = CType(Activator.GetObject(GetType(ITerritory), _
        AppConstants.REMOTEOBJECTS & "TerritoryDC.rem"), ITerritory)
        ds = objITerritory.LoadProxy()
        objITerritory = Nothing

        objRegionMgr = objRegionMgr.GetInstance

        For Each dRow In ds.Tables(0).Rows
            Dim objTerritory As New _
            Territory(Convert.ToString(dRow.Item("TerritoryID")))
            With objTerritory
                .Loading = True
                .TerritoryDescription = _
                Convert.ToString(dRow.Item("TerritoryDescription")).Trim
                .Region = _
                objRegionMgr.Item(Convert.ToInt32(dRow.Item("RegionID")))
                .Loading = False
            End With
            Me.Add(objTerritory)
        Next

        ds = Nothing
    End Sub
End Class
