REM  *****  BASIC  *****

' CSV出力設定を保持する構造体
Type ExportInfo
	Path As String
	Sheet As String
	Filter As Integer
	Header As Integer
End Type

Function CurrentPath()
	CurrentPath = ConvertFromUrl(StarDesktop.CurrentComponent.getURL()) + "./../"
End Function

Function GetSheetByName( SheetName As String )
	Dim Doc As Object
	Doc = StarDesktop.CurrentComponent
	GetSheetByName = Doc.Sheets.getByName(SheetName)
End Function

' シートを指定してカーソルを取得する
Function GetCursor( Sheet As Object)
	Dim Range As Object
	Dim Cursor As Object

	Range = Sheet.getCellRangeByName("A1")
	Cursor = Sheet.createCursorByRange(Range)
	Cursor.gotoEndOfUsedArea(True)

	GetCursor = Cursor
End Function

' csv_out に記載された出力情報一覧を作成する
Function GetExportInfo
	Dim Sheet As Object
	Dim Cursor As Object
	Dim nRow As Integer
	
	Sheet = GetSheetByName("csv_out")
	Cursor = GetCursor(Sheet)
	
	Dim Infos$(5 To Cursor.Rows.Count - 1) As ExportInfo

	For nRow = 5 To Cursor.Rows.Count - 1
		Dim Info As ExportInfo
		Info.Path = CurrentPath() + Sheet.getCellByPosition(0, nRow).String
		Info.Sheet = Sheet.getCellByPosition(1, nRow).String
		Info.Filter = Sheet.getCellByPosition(2, nRow).Value - 1
		Info.Header = Sheet.getCellByPosition(3, nRow).Value - 1
		Infos(nRow) = Info
	Next nRow
	
	GetExportInfo = Infos
End Function

' 出力情報によりCSVを書き出す
Sub ExportByInfo( Info As ExportInfo)

	Dim FileName As String
	Dim Cursor As Object
	Dim nRow As Integer
	Dim nColumn As Integer
	Dim Filter As String
	Dim Sheet As Object
	Dim Stream As Object
	
	' シート取得
	Sheet = GetSheetByName(Info.Sheet)
	Cursor = GetCursor(Sheet)
	
	' ファイルオープン
	Set Stream = CreateObject("ADODB.Stream")
	Stream.Charset = "UTF-8"	' ストリームの文字コードをUTF8に設定する
	Stream.Type = 2	' ファイルのタイプ(1:バイナリ 2:テキスト)
 	Stream.Open	' ストリームを開く

	FileName = Info.Path + Info.Sheet + ".csv"

	' レコード出力
	For nRow = Info.Header To Cursor.Rows.Count - 1
		For nColumn = 0 To Cursor.Columns.Count - 1
		
			Filter = Sheet.getCellByPosition(nColumn, Info.Filter).String
			IF Filter = "○" Then
				Stream.WriteText Sheet.getCellByPosition(nColumn, nRow).String + ","
			End IF
		Next nColumn
		Stream.WriteText "", 1
	Next nRow
	
	
  	'	ストリームに名前を付けて保存する(1は新規作成 2は上書き保存)
  	Stream.SaveToFile FileName , 2

  	' ストリームを閉じる
  	Stream.Close
  	Set Stream = Nothing
End Sub

' 外部から実行する関数
Sub CsvExport
	Dim Info As ExportInfo

	For Each Info In  GetExportInfo
		ExportByInfo(Info)
	Next Info

	MsgBox("出力しました")
End Sub


